const
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  runSequence = require("run-sequence"),
  spawn = requireModule("spawn");

gulp.task("prepare-pack", (done) => {
  runSequence(
    "build-for-release",
    "increment-package-version",
    done);
});
gulp.task("pack", [ "prepare-pack" ], () => {
  return doPack();
});

gulp.task("test-pack", ["build-for-release"], () => {
  return doPack();
});

function doPack() {
  return Promise.all([
    packNExpect(),
    packNExpectNSubstitute()
  ]);
}

function packNExpectNSubstitute() {
  return pack("src/NExpect.NSubstitute/Package.nuspec");
}

function packNExpect() {
  return pack("src/NExpect/Package.nuspec");
}

function pack(nuspec) {
  return spawn(
    "tools/nuget.exe",
    [ "pack", nuspec, "-OutputDirectory", packageDir ]
  );
}
