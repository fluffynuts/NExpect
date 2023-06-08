const gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  path = require("path"),
  fs = require("fs"),
  runSequence = requireModule("run-sequence"),
  nugetPush = requireModule("nuget-push"),
  env = requireModule("env");
spawn = requireModule("spawn");

env.associate(["DRY_RUN"], ["push"]);

gulp.task("release", done => {
  runSequence("pack", "push", "commit-release", "tag-and-push", done);
});

gulp.task("push", "pushes packages to nuget.org", () => {
  const packages = [
    findNupkg("NExpect"),
    findNupkg("NExpect.Matchers.NSubstitute"),
    findNupkg("NExpect.Matchers.AspNetCore"),
    findNupkg("NExpect.Matchers.Xml"),
    findNupkg("NExpect.Matchers.AspNetMvc")
  ].flat();
  promises = packages.map(pushPackage);
  return Promise.all(promises);
});

gulp.task("test-push", "", () => {
  const packages = {
    NExpect: findNupkg("NExpect"),
    NSubstitute: findNupkg("NExpect.Matchers.NSubstitute"),
    AspNetCore: findNupkg("NExpect.Matchers.AspNetCore"),
    Xml: findNupkg("NExpect.Matchers.Xml"),
    AspNetMvc: findNupkg("NExpect.Matchers.AspNetMvc")
  };
  for (const k in packages) {
    console.log(`${k}: ${packages[k]}`);
  }
  return Promise.resolve();
});

function findNupkg(id) {
  return findPackage(id, "nupkg");
}

function findPackage(id, ext) {
  return fs
    .readdirSync(packageDir)
    .filter(p => p.endsWith(`.${ext}`))
    .filter(p => {
      const info = parseNugetPackageFilename(p);
      return info.id == id;
    })
    .map(p => path.join(packageDir, p))
    .sort()
    .reverse()[0];
}

const pkgRegex = /^(?<id>[a-zA-Z.]+[a-zA-Z])\.(?<version>[\d.]+[\d]+)-?(?<tag>.*)\.nupkg/;
function parseNugetPackageFilename(filename) {
  const matches = filename.match(pkgRegex);
  return {
    id: matches.groups.id,
    version: matches.groups.version,
    tag: matches.groups.tag
  };
}

function pushPackage(package) {
  return nugetPush(package);
}
