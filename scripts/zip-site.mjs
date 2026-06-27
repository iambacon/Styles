import { createWriteStream } from "node:fs";
import { mkdir, readdir } from "node:fs/promises";
import path from "node:path";
import process from "node:process";
import yazl from "yazl";

const source = process.argv[2] ?? "docs/_site";
const destination = process.argv[3] ?? "dist/site.zip";

await mkdir(path.dirname(destination), { recursive: true });

const output = createWriteStream(destination);
const zipfile = new yazl.ZipFile();

const complete = new Promise((resolve, reject) => {
  output.on("close", resolve);
  output.on("error", reject);
  zipfile.outputStream.on("error", reject);
});

await addDirectory(source);
zipfile.outputStream.pipe(output);
zipfile.end();
await complete;

async function addDirectory(dir) {
  const entries = await readdir(dir, { withFileTypes: true });

  for (const entry of entries) {
    const entryPath = path.join(dir, entry.name);
    const archivePath = toPosix(path.relative(source, entryPath));

    if (entry.isDirectory()) {
      zipfile.addEmptyDirectory(`${archivePath}/`);
      await addDirectory(entryPath);
    } else if (entry.isFile()) {
      zipfile.addFile(entryPath, archivePath);
    }
  }
}

function toPosix(value) {
  return value.split(path.sep).join("/");
}
