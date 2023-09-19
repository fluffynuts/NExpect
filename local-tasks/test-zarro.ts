/// <reference path="../node_modules/zarro/types.d.ts" />
(function() {
    const
      gulp = requireModule<Gulp>("gulp");

    gulp.task("test-zarro", async () => {
        const
          testZarro = requireModule<TestZarro>("test-zarro");
        await testZarro({
          packageVersion: "beta",
          tasks: [ "test", "pack" ]
        });
    });
})();
