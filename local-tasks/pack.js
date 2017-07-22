const
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  spawn = requireModule("spawn");

gulp.task("pack", () => {
  return doPack();
});

function doPack() {
  return spawn(
    "tools/nuget.exe",
    ["pack", "Package.nuspec", "-OutputDirectory", packageDir]
  );
}
