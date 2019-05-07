const gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  path = require("path"),
  fs = require("fs"),
  runSequence = require("run-sequence"),
  spawn = requireModule("spawn");

gulp.task("release", ["build-cover-report"], done => {
  runSequence("pack", "push", "commit-release", "tag-and-push", done);
});

gulp.task("push", () => {
  const nexpectPackage = fs
    .readdirSync(packageDir)
    .filter(
      p =>
        p.startsWith("NExpect") &&
        p.indexOf("NSubstitute") === -1 &&
        p.endsWith("nupkg")
    )
    .map(p => path.join(packageDir, p))
    .sort()
    .reverse()[0];
  const nexpectNSubPackage = fs
    .readdirSync(packageDir)
    .filter(
      p =>
        p.startsWith("NExpect") &&
        p.indexOf("NSubstitute") > -1 &&
        p.endsWith("nupkg")
    )
    .map(p => path.join(packageDir, p))
    .sort()
    .reverse()[0];
  if (!nexpectPackage || !nexpectNSubPackage) {
    throw new Error(`No package found in ${packageDir}`);
  }
  return Promise.all([
    pushPackage(nexpectPackage),
    pushPackage(nexpectNSubPackage)
  ]);
});

function pushPackage(package) {
  console.log(`pushing package ${package}`);
  return spawn("tools/nuget.exe", [ "push", package, "-Source", "nuget.org" ]);
}
