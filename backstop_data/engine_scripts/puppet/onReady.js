module.exports = async (page, scenario, vp) => {
  console.log('SCENARIO > ' + scenario.label);
  await require('./clickAndHoverHelper')(page, scenario);

  // Change image attribute loading from lazy to eager
  page.evaluate(async () => {
    document.querySelectorAll('[loading="lazy"]').forEach((elem) => {
      elem.loading = 'eager';
    });
  });
};
