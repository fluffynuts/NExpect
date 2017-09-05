var gulp = requireModule("gulp-with-help"),
  child_process = require("child_process"),
  debug = require("debug")("nuget-restore"),
  nugetRestore = requireModule("./gulp-nuget-restore"),
  resolveNuget = requireModule("./resolve-nuget");

function createNugetRestoreTask() {
  var deps = [];
  try {
    resolveNuget();
  } catch (ignore) {
    debug("should download nuget");
    deps.push("get-local-nuget");
  }
  gulp.task("nuget-restore", "Restores all nuget packages in all solutions", deps, function () {
    return gulp.src(["src/NExpect.sln"])
      .pipe(nugetRestore({
        debug: false
      }));
  });
}

createNugetRestoreTask();
