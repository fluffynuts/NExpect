const
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  runSequence = require("run-sequence"),
  spawn = requireModule("spawn");

gulp.task("prepare-pack", (done) => {
  runSequence(
    "build-for-release",
    "increment-package-version",
    done);
});
gulp.task("pack", [ "prepare-pack" ], () => {
  return doPack();
});

function doPack() {
  return spawn(
    "tools/nuget.exe",
    ["pack", "src/NExpect/Package.nuspec", "-OutputDirectory", packageDir]
  );
}
