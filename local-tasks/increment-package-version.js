const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  editXml = require("gulp-edit-xml");

gulp.task("increment-package-version", () => {
  return gulp
    .src(["src/**/Package.nuspec", "!src/PeanutButter/**/*"])
    .pipe(incrementPackageVersion())
    .pipe(incrementDependencyVersion("NExpect"))
    .pipe(gulp.dest("src"));
});

function incrementDependencyVersion(packageMatch) {
  if (typeof packageMatch === "string") {
    packageMatch = new RegExp(`^${packageMatch}$`);
  }
  return editXml(
    xml => {
      const meta = xml.package.metadata[0],
        dependencies = meta.dependencies[0].group;
      dependencies.forEach(dep => {
        var dependency = (dep.dependency || [])[0];
        if (!dependency) {
          return;
        }
        if ((dependency.$.id || "").match(packageMatch)) {
          const newVersion = incrementVersion(dependency.$.version);
          gutil.log(
            gutil.colors.yellow(
              `${dependency.$.id}: dependency ${
                dependency.$.id
              } version incremented to: ${newVersion} from ${dependency.$.version}`
            )
          );
          dependency.$.version = newVersion;
        }
      });
      return xml;
    },
    {
      builderOptions: { renderOpts: { pretty: true } }
    }
  );
}

function incrementVersion(versionString) {
  const parts = versionString.split("."),
    major = parseInt(parts[0]),
    minor = parseInt(parts[1]),
    patch = parseInt(parts[2]);
  testNaN({ major, minor, patch });
  return `${major}.${minor}.${patch + 1}`;
}

function incrementPackageVersion() {
  return editXml(
    xml => {
      const meta = xml.package.metadata[0],
        packageName = meta.id[0],
        node = meta.version,
        current = node[0];
      const newVersion = incrementVersion(current);
      node[0] = newVersion;
      gutil.log(
        gutil.colors.yellow(
          `${packageName}: package version incremented to: ${newVersion}`
        )
      );
      return xml;
    },
    { builderOptions: { renderOpts: { pretty: true } } }
  );
}

function testNaN(version) {
  Object.keys(version).forEach(k => {
    if (isNaN(version[k])) {
      throw new Error(`${k} is not an integer`);
    }
  });
}
