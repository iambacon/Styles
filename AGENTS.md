# AGENTS.md

Guidance for AI agents and contributors editing this repository.

## Repository Role

Styles is the pattern library and documentation site for iambacon.co.uk. Treat
the Sass under `src/sass` as the source library/blog CSS. Treat the files under
`docs` as the documentation site that demonstrates and tests that library.

## Docs Page Structure

Reusable pattern, object, base, and utility documentation pages should use the
shared docs layout and follow this content order:

1. `Purpose`
2. `Example` or `Reference`
3. `Usage notes`
4. `Accessibility notes`

Use the page title to name the pattern plainly. Keep the purpose short and
specific, then let the example show the real markup. Put implementation caveats,
variant guidance, and accessibility expectations in the notes sections rather
than mixing them into the preview.

## Examples

- Use real public classes from the library styles whenever a page demonstrates a
  reusable pattern.
- Keep docs-only wrappers and helpers prefixed with `sg-`.
- Avoid `href="#"` in examples. Use harmless in-page anchors such as `#example`
  unless the example needs to demonstrate a real destination.
- Prefer accessible example markup by default: meaningful link text, labelled
  navigation landmarks, table captions where useful, scoped table headers, and
  useful image `alt` text.

## Docs-Only Styling

Docs-only styles belong in `docs/assets/sass/styleguide.scss` and should use
`sg-` prefixed classes. Do not change `src/sass` to style the documentation
shell, navigation, previews, or explanatory content.

## Site Examples

Files in `docs/pages` are static site snapshots used as integration examples and
visual regression targets. They are not reusable component API pages.

The `/pages/` overview page should explain why these snapshots exist. Do not
force the copied page snapshots into the standard pattern-page structure unless
the snapshots themselves are being intentionally redesigned.

## Verification

Before finishing docs changes, run:

```sh
npm run docs:build
```

For visual or responsive changes, also check the affected pages in a browser at
mobile and desktop widths. Add or update Backstop scenarios when a new docs page
or important visual state should be covered by regression tests.
