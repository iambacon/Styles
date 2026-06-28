# Changelog

All notable changes to this project will be documented in this file.

## Unreleased

### Bug Fixes

- **test**: Modernise Backstop visual regression configuration
- **test**: Refresh and stabilise Backstop reference screenshots

## [2.0.0] - 2026-06-27

### Features

- **build**: (breaking) Replace Grunt with npm scripts and Node.js helper scripts
- **build**: (breaking) Require Node.js 24 and npm 11
- **build**: Add GitHub Actions checks for pushes and pull requests
- **build**: Add verification scripts for CSS, documentation, and npm audit checks
- **css**: Migrate Sass imports to the module system with `@use`
- **css**: Replace deprecated Sass functions and division syntax
- **docs**: Add changelog for release notes

### Bug Fixes

- **build**: Resolve all reported npm audit vulnerabilities
- **build**: Stop tracking generated CSS assets
- **build**: Preserve existing generated CSS output during Sass migration
- **ci**: Update GitHub Actions versions to avoid Node.js 20 deprecation warnings

### Migration

- This version updates the project build tooling but does not intentionally change the generated production CSS.
- Use Node.js 24 and npm 11 when working on the project.
- Replace any direct Grunt commands with npm scripts:
  - `npm run build` builds the production CSS.
  - `npm run docs:build` builds and zips the documentation site.
  - `npm run check` runs the same verification used in CI.
