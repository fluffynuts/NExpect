const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  editXml = require("gulp-edit-xml");

gulp.task(
  "increment-package-version",
  [
    "increment-nexpect-package-version",
    "increment-nexpect-nsubstitute-package-version"
  ],
  () => {
    return Promise.resolve();
  }
);

gulp.task("increment-nexpect-package-version", () => {
  return incrementPackageVersionFor("NExpect");
});

gulp.task("increment-nexpect-nsubstitute-package-version", () => {
  return incrementPackageVersionFor("NExpect.NSubstitute");
});

function incrementPackageVersionFor(project) {
  const containingFolder = `src/${project}`;
  return gulp
    .src(`${containingFolder}/Package.nuspec`)
    .pipe(increment(project))
    .pipe(gulp.dest(containingFolder));
}

function increment(project) {
  return editXml(
    xml => {
      const node = xml.package.metadata[0].version,
        current = node[0],
        parts = current.split("."),
        major = parseInt(parts[0]),
        minor = parseInt(parts[1]),
        patch = parseInt(parts[2]);
      testNaN({ major, minor, patch });
      const newVersion = `${major}.${minor}.${patch + 1}`;
      node[0] = newVersion;
      gutil.log(
        gutil.colors.yellow(`${project}: package version incremented to: ${newVersion}`)
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
