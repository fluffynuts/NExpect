const
    { ls, readTextFile, writeTextFile, FsEntities } = require("yafs"),
    gulp = requireModule("gulp");

gulp.task("strip-xmldoc", async () => {
    // strip out imported member docs - they're internal
    // and of no use to consumers
    const docFiles = await ls(".", { entities: FsEntities.files, recurse: true, fullPaths: true, match: /.*bin[/|\\]Release[/|\\].*\.xml$/ });
    for (const doc of docFiles) {
        const
            contents = await readTextFile(doc),
            lines = contents.split("\n"),
            stripped = [];
        let inImportedMember = false;
        for (const line of lines) {
            if (line.match(/member.*:Imported\./)) {
                inImportedMember = true;
            } else if (line.match(/<\/member/)) {
                inImportedMember = false;
            }
            if (!inImportedMember) {
                stripped.push(line);
            }
        }
        console.log(`stripped ${lines.length - newLines.length} lines from ${doc}`);
        await writeTextFile(doc, stripped.join("\n"));
    }
});