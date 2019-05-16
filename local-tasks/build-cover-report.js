const
  gulp = requireModule("gulp-with-help"),
  runSequence = requireModule("run-sequence");

gulp.task("build-cover-report", cb => {
  runSequence("build", "cover-dotnet", "report-generator", cb);
});