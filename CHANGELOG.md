# Changelog

All notable changes to this project will be documented in this file.

## [2.1.0] - 2026-07-03

### Features

- **docs**: Polish the style guide shell layout, navigation, and preview presentation
- **docs**: Replace the menu logo with a brand lockup
- **docs**: Scope library styles to component and page previews
- **docs**: Reframe page snapshots as site examples
- **docs**: Standardise base, utility, component, and page documentation

### Bug Fixes

- **docs**: Improve style guide shell accessibility
- **docs**: Refine preview code boxes
- **docs**: Cool the style guide shell neutrals
- **test**: Modernise Backstop visual regression configuration
- **test**: Refresh and stabilise Backstop reference screenshots

### Migration

- The docs shell now uses its own `sg-` styles more consistently.
- Library CSS is scoped to rendered previews, reducing accidental coupling between the docs interface and the blog styles.

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
