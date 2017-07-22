const 
  gulp = requireModule("gulp-with-help"),
  del = require("del"),
  packageDir = require("./config").packageDir;

gulp.task("clean-packages", () => {
  return del(packageDir);
});
