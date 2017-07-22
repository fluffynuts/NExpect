const 
  gulp = requireModule("gulp-with-help"),
  packageDir = require("./config").packageDir,
  path = require("path"),
  fs = require("fs"),
  runSequence = require("run-sequence"),
  spawn = requireModule("spawn");

gulp.task("release", [ "test-dotnet" ], (done) => {
  runSequence(
    "increment-package-version",
    "pack",
    "push",
    done
  );
});

gulp.task("push", () => {
  const package = fs.readdirSync(packageDir)
                    .filter(p => p.endsWith("nupkg"))
                    .map(p => path.join(packageDir, p))
                    .sort()
                    .reverse()[0];
  if (!package) {
    throw new Error(`No package found in ${packageDir}`);
  }
  console.log(`pushing package ${package}`);
  return spawn("tools/nuget.exe", [ "push", package, "-Source", "nuget.org" ]);
});