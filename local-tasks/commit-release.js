const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  editXml = require("gulp-edit-xml"),
  Git = require("simple-git"),
  git = new Git(),
  containingFolder = "src/NExpect";

// TODO: move up into gulp-tasks
gulp.task("commit-release", () => {
  return new Promise((resolve, reject) => {
    gulp.src(`${containingFolder}/NExpect.csproj`).pipe(
      editXml(xml => {
        const packageVersionPropGroup = xml.Project.PropertyGroup.filter(
            g => !!g.PackageVersion
          )[0],
          node = packageVersionPropGroup.PackageVersion[0],
          version = node[0].trim();

        gutil.log(gutil.colors.cyan(`Committing release ${version}`));
        if (process.env.DRY_RUN) {
          resolve(" -- dry run: no commit --");
          return xml;
        }
        git.add("./*", err => {
          if (err) {
            reject(`Unable to add all files: ${err}`);
          }
          git.commit(`:bookmark: release version ${version}`, err => {
            return err
              ? reject(`Unable to commit release ${version}: ${err}`)
              : resolve(`Release ${version} committed`);
          });
        });
        return xml;
      })
    );
  });
});
