const gulp = requireModule("gulp-with-help"),
  gutil = require("gulp-util"),
  gitPushTags = requireModule("git-push-tags"),
  gitPush = requireModule("git-push"),
  gitTagFromCsProj = requireModule("gulp-git-tag-from-csproj");

gulp.task("tag-and-push", () => {
  return gulp.src("**/NExpect/NExpect.csproj")
    .pipe(gitTagFromCsProj());
});

gulp.task("push-tags", "Pushes tags and commits", () => {
  return gitPushTags()
    .then(() => gitPush())
    .then(() =>
      gutil.log(gutil.colors.green("-> all commits and tags pushed!"))
    );
});
