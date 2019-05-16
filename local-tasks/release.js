const gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  path = require("path"),
  fs = require("fs"),
  runSequence = requireModule("run-sequence"),
  spawn = requireModule("spawn");

gulp.task("release", ["build-cover-report"], done => {
  runSequence("pack", "push", "commit-release", "tag-and-push", done);
});

gulp.task("push", () => {
  const
    packages = [
      findNupkg("NExpect"),
      findNupkg("NExpect.Matchers.NSubstitute"),
      findNupkg("NExpect.Matchers.AspNetCore")
    ];
    promises = packages.map(pushPackage);
  return Promise.all(promises);
});

function findNupkg(id) {
  return fs
    .readdirSync(packageDir)
    .filter(p => {
      var parts = p.split(".").filter(part =>
        part !== "nupkg" && isNaN(parseInt(part))
      );
      return parts.join(".") === id;
    })
    .map(p => path.join(packageDir, p))
    .sort()
    .reverse()[0];
}

function pushPackage(package) {
  console.log(`pushing package ${package}`);
  return spawn("tools/nuget.exe", ["push", package, "-Source", "nuget.org"]);
}
