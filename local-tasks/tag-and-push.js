const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  Git = require("simple-git"),
  git = new Git(),
  gitPushTags = requireModule("git-push-tags"),
  gitPush = requireModule("git-push"),
  gitTagFromPackageNuspec = requireModule("gulp-git-tag-from-package-nuspec");

gulp.task("tag-and-push", () => {
  return gulp.src("**/NExpect/Package.nuspec")
    .pipe(gitTagFromPackageNuspec());
});

gulp.task("push-tags", "Pushes tags and commits", () => {
  return gitPushTags()
    .then(() => gitPush())
    .then(() =>
      gutil.log(gutil.colors.green("-> all commits and tags pushed!"))
    );
});
