const gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  path = require("path"),
  fs = require("fs"),
  runSequence = requireModule("run-sequence"),
  findLocalNuget = requireModule("find-local-nuget"),
  env = requireModule("env");
  spawn = requireModule("spawn");

env.associate(["DRY_RUN"], ["push"]);

gulp.task("release", ["test-dotnet"], done => {
  runSequence("pack", "push", "commit-release", "tag-and-push", done);
});

gulp.task("push", () => {
  const packages = [
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
      var parts = p
        .split(".")
        .filter(part => part !== "nupkg" && isNaN(parseInt(part)));
      return parts.join(".") === id;
    })
    .map(p => path.join(packageDir, p))
    .sort()
    .reverse()[0];
}

function pushPackage(package) {
  console.log(`pushing package ${package}`);
  return findLocalNuget().then(nuget => {
    const args = ["push", package, "-Source", "nuget.org"];
    if (env.resolveFlag("DRY_RUN")) {
      console.log(`${nuget} ${args.join(" ")}`);
      return Promise.resolve();
    }
    return spawn(nuget, args)
  });
}
