const
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  runSequence = requireModule("run-sequence"),
  getToolsFolder = requireModule("get-tools-folder"),
  del = require("del"),
  nugetPack = requireModule("gulp-nuget-pack");

gulp.task("prepare-pack", (done) => {
  runSequence(
    "build-for-release",
    "increment-package-version",
    done);
});
gulp.task("pack", [ "prepare-pack", "clean-packages" ], () => {
  return doPack();
});
gulp.task("clean-packages", () => {
  return del("packages");
});

gulp.task("test-pack", ["build-for-release"], () => {
  return doPack();
});

gulp.task("quick-pack", () => {
  return doPack();
});

function doPack() {
  return gulp.src([
    "**/*.nuspec",
    "!**/PeanutButter/**/*.nuspec",
    "!node_modules/**/*.nuspec",
    `!${getToolsFolder()}/**/*.nuspec`])
    .pipe(nugetPack())
    .pipe(gulp.dest(packageDir));
}
