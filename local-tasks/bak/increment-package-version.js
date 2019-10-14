const
  gulp = requireModule("gulp-with-help"),
  incrementPackageVersion = requireModule("gulp-increment-nuget-package-version"),
  incrementDependencyVersion = requireModule("gulp-increment-nuget-package-dependency-version");

gulp.task("increment-package-version", () => {
  return gulp
    .src(["src/**/Package.nuspec", "!src/PeanutButter/**/*"])
    .pipe(incrementPackageVersion())
    .pipe(incrementDependencyVersion("NExpect"))
    .pipe(gulp.dest("src"));
});