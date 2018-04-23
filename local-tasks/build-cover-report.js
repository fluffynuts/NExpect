const
  gulp = requireModule("gulp-with-help"),
  runSequence = require("run-sequence");

gulp.task("build-cover-report", cb => {
  runSequence("build", "cover-dotnet", "report-generator", cb);
});