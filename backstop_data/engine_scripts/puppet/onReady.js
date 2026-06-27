module.exports = async (page, scenario, vp) => {
  console.log("SCENARIO > " + scenario.label);
  await require("./clickAndHoverHelper")(page, scenario);

  await page.evaluate(async () => {
    document.documentElement.style.overflowX = "hidden";
    document.body.style.overflowX = "hidden";

    document.querySelectorAll('[loading="lazy"]').forEach((elem) => {
      elem.loading = "eager";
    });

    if (document.fonts) {
      await document.fonts.ready;
    }

    await Promise.all(
      Array.from(document.images).map((image) => {
        if (image.complete) {
          return Promise.resolve();
        }

        return image.decode ? image.decode().catch(() => {}) : Promise.resolve();
      })
    );
  });
};
