const syntaxHighlight = require("@11ty/eleventy-plugin-syntaxhighlight");

module.exports = config => {
    config.addPassthroughCopy('assets/css');
    config.addPassthroughCopy('assets/fav');
    config.addPassthroughCopy('images');
    config.addPlugin(syntaxHighlight);
};