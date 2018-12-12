const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  editXml = require("gulp-edit-xml"),
  Git = require("simple-git"),
  git = new Git(),
  containingFolder = "src/NExpect";

gulp.task("tag-and-push", () => {
  return new Promise((resolve, reject) => {
    gulp.src(`${containingFolder}/Package.nuspec`).pipe(
      editXml(xml => {
        const node = xml.package.metadata[0].version,
          version = node[0].trim();

        gutil.log(gutil.colors.cyan(`Tagging at: "v${version}"`));
        gitTag(`v${version}`, `chore(release): ${version}`)
          .then(() => gitPushTags())
          .then(() => gitPush())
          .then(() => {
            gutil.log(gutil.colors.green("-> all commits and tags pushed!"));
            resolve();
          })
          .catch(err => reject(err));
        return xml;
      })
    );
  });
});

gulp.task("push-tags", "Pushes tags and commits", () => {
  return gitPushTags()
    .then(() => gitPush())
    .then(() =>
      gutil.log(gutil.colors.green("-> all commits and tags pushed!"))
    );
});

function gitTag(tag, comment) {
  return new Promise((resolve, reject) => {
    git.addAnnotatedTag(tag, comment, err => {
      if (err) {
        return reject(err);
      }
      resolve();
    });
  });
}

function gitPushTags() {
  return new Promise((resolve, reject) => {
    gutil.log(gutil.colors.green("pushing tags..."));
    git.pushTags("origin", err => {
      if (err) {
        return reject(err);
      }
      resolve();
    });
  });
}

function gitPush() {
  return new Promise((resolve, reject) => {
    gutil.log(gutil.colors.green("pushing local commits..."));
    git.push("origin", "master", err => {
      if (err) {
        return reject(err);
      }
      resolve();
    });
  });
}
