const
  gulp = requireModule("gulp-with-help"),
  editXml = require("gulp-edit-xml");

gulp.task("increment-package-version", () => {
  gulp.src("Package.nuspec")
    .pipe(editXml(xml => {
      const 
        node = xml.package.metadata[0].version,
        current = node[0],
        parts = current.split("."),
        major = parseInt(parts[0]),
        minor = parseInt(parts[1]),
        patch = parseInt(parts[2]);
      testNaN({ major, minor, patch});
      node[0] = `${major}.${minor}.${patch + 1}`;
      return xml;
    }, { builderOptions: { renderOpts: { pretty: true } } }))
    .pipe(gulp.dest("."));
});

function testNaN(version) {
  Object.keys(version).forEach(k => {
    if (isNaN(version[k])) {
      throw new Error(`${k} is not an integer`);
    }
  });
}