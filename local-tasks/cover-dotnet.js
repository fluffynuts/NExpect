var gulp = requireModule("gulp-with-help"),
    dotNetCover = requireModule("gulp-dotnetcover");
gulp.task("cover-dotnet", "Runs tests from projects matching *.Tests with DotCover coverage", function() {
    return gulp.src(["**/*.Tests.dll", "!src/PeanutButter/**/*"])
             .pipe(dotNetCover({
                 debug: false,
                 architecture: "x86",
                 exclude: ["FluentMigrator.*",
                            "PeanutButter.*",
                            "AutoMapper",
                            "AutoMapper.*",
                            "WindsorTestHelpers.*",
                            "MvcTestHelpers",
                            "TestUtils",
                            "*.Tests.*"]
             }));
});
