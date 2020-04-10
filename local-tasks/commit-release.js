const 
  gulp = requireModule("gulp-with-help"),
  chalk = require("chalk"),
  logger = requireModule("log"),
  editXml = require("gulp-edit-xml"),
  Git = require("simple-git"),
  git = new Git(),
  env = requireModule("env"),
  containingFolder = "src/NExpect";

// TODO: move up into gulp-tasks as a generic that can take
// in a gulp.src stream
gulp.task("commit-release", () => {
  return new Promise((resolve, reject) => {
    gulp.src(`${containingFolder}/NExpect.csproj`).pipe(
      editXml(xml => {
        const packageVersionPropGroup = xml.Project.PropertyGroup.filter(
            g => !!g.PackageVersion
          )[0],
          node = packageVersionPropGroup.PackageVersion,
          version = node[0].trim();

        logger.notice(chalk.cyanBright(`Committing release ${version}`));
        if (env.resolveFlag("DRY_RUN")) {
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
