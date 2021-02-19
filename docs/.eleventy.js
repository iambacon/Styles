const syntaxHighlight = require("@11ty/eleventy-plugin-syntaxhighlight");

module.exports = config => {
    config.addPassthroughCopy('assets/css');
    config.addPassthroughCopy('assets/fav');
    config.addPassthroughCopy('assets/images');
    config.addPassthroughCopy('images');
    config.addPassthroughCopy('web.config');
    config.addPassthroughCopy('robots.txt');
    config.addPlugin(syntaxHighlight);
};