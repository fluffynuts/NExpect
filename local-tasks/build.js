const
  gulp = requireModule("gulp-with-help"),
  runSequence = require("run-sequence"),
  msbuild = require("gulp-msbuild");

gulp.task("prebuild", ["nuget-restore", "install-tools"]);

gulp.task("build",
  "Builds all Visual Studio solutions in tree",
  ["prebuild"],
  () => {
    return gulp.src([
      "src/NExpect.sln"
    ]).pipe(msbuild({
      toolsVersion: "auto",
      targets: ["Clean", "Build"],
      configuration: "Debug",
      stdout: true,
      verbosity: "minimal",
      errorOnFail: true,
      architecture: "x64",
      nologo: false
    }));
  });

gulp.task("build-for-release",
  "Builds all Visual Studio solutions in tree",
  ["nuget-restore"],
  () => {
    return gulp.src([
      "src/NExpect.sln"
    ]).pipe(msbuild({
      toolsVersion: "auto",
      targets: ["Clean", "Build"],
      configuration: "BuildForRelease",
      stdout: true,
      verbosity: "minimal",
      errorOnFail: true,
      nologo: false
    }));
  });
