# Changelog

All notable changes to this project will be documented in this file.

## [2.0.0] - 2026-06-27

### Changed

- Updated project dependencies and resolved all reported npm audit vulnerabilities.
- Updated the project runtime contract to Node.js 24 and npm 11.
- Replaced the Grunt build pipeline with direct npm scripts and small Node.js helper scripts.
- Migrated Sass imports to the module system with `@use`.
- Replaced deprecated Sass functions and division syntax.
- Added GitHub Actions verification for pushes and pull requests.
- Added project verification scripts for CSS builds, documentation builds, and npm audit checks.
- Stopped tracking generated CSS outputs and documented generated asset ignores.

### Notes

- The generated production CSS was verified as unchanged during the Sass migration.
- Existing Backstop reference images are stale and should be refreshed in a separate maintenance pass.
