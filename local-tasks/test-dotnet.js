var gulp = requireModule("gulp-with-help"),
  fs = require("fs"),
  os = require("os"),
  nunit = require("gulp-nunit-runner"),
  testUtilFinder = requireModule("testutil-finder");

gulp.task("test-dotnet",
  "Runs all tests in your solution via NUnit",
  [ "build" ],
  function () {
  if (!fs.existsSync("buildreports")) {
    fs.mkdir("buildreports");
  }
  var agents = parseInt(process.env.MAX_NUNIT_AGENTS);
  if (isNaN(agents)) {
    agents = os.cpus().length - 1;
  }
  return gulp.src([
    "**/bin/Debug/**/*.Tests.dll",
    "**/bin/*.Tests.dll",
    "!src/PeanutButter/**/*.Tests.dll"
  ], { read: false }).pipe(nunit({
    executable: testUtilFinder.latestNUnit({ architecture: "x86" }),
    options: {
      result: "buildreports/nunit-result.xml",
      agents: agents
    }
  }));
});
