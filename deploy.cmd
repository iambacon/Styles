@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

:: ----------------------
:: KUDU Deployment Script
:: Version: 1.0.17
:: ----------------------

:: Prerequisites
:: -------------

:: Verify node.js installed
where node 2>nul >nul
IF %ERRORLEVEL% NEQ 0 (
  echo Missing node.js executable, please install node.js, if already installed make sure it can be reached from current environment.
  goto error
)

:: Setup
:: -----

setlocal enabledelayedexpansion

SET ARTIFACTS=%~dp0%..\artifacts

IF NOT DEFINED DEPLOYMENT_SOURCE (
  SET DEPLOYMENT_SOURCE=%~dp0%.
)

IF NOT DEFINED DEPLOYMENT_TARGET (
  SET DEPLOYMENT_TARGET=%ARTIFACTS%\wwwroot
)

IF NOT DEFINED NEXT_MANIFEST_PATH (
  SET NEXT_MANIFEST_PATH=%ARTIFACTS%\manifest

  IF NOT DEFINED PREVIOUS_MANIFEST_PATH (
    SET PREVIOUS_MANIFEST_PATH=%ARTIFACTS%\manifest
  )
)

IF NOT DEFINED KUDU_SYNC_CMD (
  :: Install kudu sync
  echo Installing Kudu Sync
  call npm install kudusync -g --silent
  IF !ERRORLEVEL! NEQ 0 goto error

  :: Locally just running "kuduSync" would also work
  SET KUDU_SYNC_CMD=%appdata%\npm\kuduSync.cmd
)
IF NOT DEFINED DEPLOYMENT_TEMP (
  SET DEPLOYMENT_TEMP=%temp%\___deployTemp%random%
  SET CLEAN_LOCAL_DEPLOYMENT_TEMP=true
)

IF DEFINED CLEAN_LOCAL_DEPLOYMENT_TEMP (
  IF EXIST "%DEPLOYMENT_TEMP%" rd /s /q "%DEPLOYMENT_TEMP%"
  mkdir "%DEPLOYMENT_TEMP%"
)

IF DEFINED MSBUILD_PATH goto MsbuildPathDefined
SET MSBUILD_PATH=%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe
:MsbuildPathDefined

IF NOT DEFINED PROJECT (
  echo Missing PROJECT app setting. Please configure in Azure portal and redeploy.
  goto error
)

IF /I "%PROJECT%" EQU  "src\IAmBacon\IAmBacon.Admin\IAmBacon.Admin.csproj" (
  SET ASPCORE_DEPLOY=true
)

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
:: Deployment
:: ----------

IF DEFINED ASPCORE_DEPLOY goto DotnetCoreDeploy

echo Handling .NET Web Application deployment.

:: 1. Restore NuGet packages
IF /I "src\IAmBacon\IAmBacon.sln" NEQ "" (
  call :ExecuteCmd nuget restore "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.sln"
  IF !ERRORLEVEL! NEQ 0 goto error
)

:: 2. Build to the temporary path
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\%PROJECT%" /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%DEPLOYMENT_TEMP%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT_SOURCE%\src\IAmBacon\\" %SCM_BUILD_ARGS%
) ELSE (
  call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\%PROJECT%" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release;UseSharedCompilation=false /p:SolutionDir="%DEPLOYMENT_SOURCE%\src\IAmBacon\\" %SCM_BUILD_ARGS%
)

IF !ERRORLEVEL! NEQ 0 goto error

goto runTests

:DotnetCoreDeploy
echo Handling ASP.NET Core Web Application deployment.

:: 1. Restore nuget packages
call :ExecuteCmd dotnet restore "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.sln"
IF !ERRORLEVEL! NEQ 0 goto error

:: 2. Build and publish
call :ExecuteCmd dotnet publish "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.Admin\IAmBacon.Admin.csproj" --output "%DEPLOYMENT_TEMP%" --configuration Release
IF !ERRORLEVEL! NEQ 0 goto error

:: 2a. Build test projects to temporary path
:runTests
call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.Web.Tests\IAmBacon.Web.Tests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error
call :ExecuteCmd dotnet msbuild "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Admin.Tests\IAmBacon.Core.Admin.Tests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error
call :ExecuteCmd dotnet msbuild "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Application.Tests\IAmBacon.Core.Application.Tests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error
call :ExecuteCmd dotnet msbuild "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Domain.Tests\IAmBacon.Core.Domain.Tests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error
call :ExecuteCmd dotnet msbuild "%DEPLOYMENT_SOURCE%\test\IntegrationTests\IAmBacon.Core.Admin.IntegrationTests\IAmBacon.Core.Admin.IntegrationTests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error
call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.Domain.Tests\IAmBacon.Domain.Tests.csproj"
IF !ERRORLEVEL! NEQ 0 goto error

:: 2b. Run unit tests
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.Web.Tests\bin\Debug\IAmBacon.Web.Tests.dll"
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Admin.Tests\bin\Debug\netcoreapp2.2\IAmBacon.Core.Admin.Tests.dll"
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Application.Tests\bin\Debug\netcoreapp2.2\IAmBacon.Core.Application.Tests.dll"
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\test\IAmBacon.Core.Domain.Tests\bin\Debug\netcoreapp2.2\IAmBacon.Core.Domain.Tests.dll"
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\test\IntegrationTests\IAmBacon.Core.Admin.IntegrationTests\bin\Debug\netcoreapp2.2\IAmBacon.Core.Admin.IntegrationTests.dll"
call :ExecuteCmd vstest.console.exe "%DEPLOYMENT_SOURCE%\src\IAmBacon\IAmBacon.Domain.Tests\bin\Debug\IAmBacon.Domain.Tests.dll"

IF !ERRORLEVEL! NEQ 0 goto error

IF DEFINED ASPCORE_DEPLOY goto :kuduSync

:: 3. Restore Grunt packages and run Grunt tasks
pushd %DEPLOYMENT_TEMP%
echo Installing Grunt packages
call npm install rimraf -g
call npm install
IF !ERRORLEVEL! NEQ 0 goto error
echo Running Grunt tasks
call :ExecuteCmd grunt prod
IF !ERRORLEVEL! NEQ 0 goto error
echo cleaning up...
call :ExecuteCmd rimraf node_modules Content\sass package.json gruntfile.js package-lock.json
IF !ERRORLEVEL! NEQ 0 goto error

:: 4. KuduSync
:kuduSync
IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
  call :ExecuteCmd "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
  IF !ERRORLEVEL! NEQ 0 goto error
)

::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

:: Post deployment stub
IF DEFINED POST_DEPLOYMENT_ACTION call "%POST_DEPLOYMENT_ACTION%"
IF !ERRORLEVEL! NEQ 0 goto error

goto end

:: Execute command routine that will echo out when error
:ExecuteCmd
setlocal
set _CMD_=%*
call %_CMD_%
if "%ERRORLEVEL%" NEQ "0" echo Failed exitCode=%ERRORLEVEL%, command=%_CMD_%
exit /b %ERRORLEVEL%

:error
endlocal
echo An error has occurred during web site deployment.
call :exitSetErrorLevel
call :exitFromFunction 2>nul

:exitSetErrorLevel
exit /b 1

:exitFromFunction
()

:end
endlocal
echo Finished successfully.
