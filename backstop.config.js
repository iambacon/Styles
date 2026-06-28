const baseUrl =
  process.env.BACKSTOP_BASE_URL || "http://host.docker.internal:8080";

const scenario = (label, path, options = {}) => ({
  selectors: ["document"],
  label,
  url: `${baseUrl}${path}`,
  requireSameDimensions: false,
  ...options,
});

module.exports = {
  id: "backstop_default",
  viewports: [
    {
      label: "phone",
      width: 375,
      height: 667,
    },
    {
      label: "tablet",
      width: 768,
      height: 1024,
    },
    {
      label: "laptop",
      width: 1024,
      height: 768,
    },
    {
      label: "desktop",
      width: 1440,
      height: 900,
    },
  ],
  onBeforeScript: "puppet/onBefore.js",
  onReadyScript: "puppet/onReady.js",
  scenarios: [
    scenario("Home", ""),
    scenario("Base code", "/base/code"),
    scenario("Base colours", "/base/colours"),
    scenario("Base grid", "/base/grid"),
    scenario("Base typography", "/base/typography"),
    scenario("Article", "/components/article"),
    scenario("Article content", "/components/article-content"),
    scenario("Aside nav", "/components/aside-nav"),
    scenario("Author", "/components/author"),
    scenario("Buttons", "/components/buttons"),
    scenario("Header", "/components/header"),
    scenario("Links list", "/components/links-list"),
    scenario("Meter", "/components/meter"),
    scenario("Page title", "/components/page-title"),
    scenario("Pagination", "/components/pagination"),
    scenario("Tag", "/components/tag"),
    scenario("Lists", "/objects/lists"),
    scenario("Utility colours", "/utilities/colours"),
    scenario("Utility display", "/utilities/display"),
    scenario("Utility floats", "/utilities/floats"),
    scenario("Utility grid", "/utilities/grid"),
    scenario("Utility spacing", "/utilities/spacing"),
    scenario("Utility social", "/utilities/social"),
    scenario("Utility typography", "/utilities/typography"),
    scenario("Site examples", "/pages"),
    scenario("Homepage template", "/pages/homepage", { delay: 1000 }),
    scenario("Blog landing template", "/pages/blog-landing", { delay: 1000 }),
    scenario("Post template", "/pages/post"),
    scenario("Filtered results template", "/pages/filtered-results", {
      delay: 1000,
    }),
    scenario("Hire me template", "/pages/hire-me"),
  ],
  paths: {
    bitmaps_reference: "backstop_data/bitmaps_reference",
    bitmaps_test: "backstop_data/bitmaps_test",
    engine_scripts: "backstop_data/engine_scripts",
    html_report: "backstop_data/html_report",
    ci_report: "backstop_data/ci_report",
  },
  report: ["browser"],
  engine: "puppeteer",
  engineOptions: {
    args: ["--no-sandbox"],
  },
  asyncCaptureLimit: 5,
  asyncCompareLimit: 50,
  debug: false,
  debugWindow: false,
};
