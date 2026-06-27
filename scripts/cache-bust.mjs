import { createHash } from "node:crypto";
import { readdir, readFile, rename, writeFile } from "node:fs/promises";
import path from "node:path";

const args = new Map();

for (let i = 2; i < process.argv.length; i += 2) {
  args.set(process.argv[i], process.argv[i + 1]);
}

const baseDir = args.get("--base");
const assetsDir = args.get("--assets");
const separator = args.get("--separator") ?? ".";
const htmlDir = args.get("--html");

if (!baseDir || !assetsDir) {
  throw new Error(
    "Usage: cache-bust --base <dir> --assets <dir> [--html <dir>] [--separator .|-]"
  );
}

const assetRoot = path.join(baseDir, assetsDir);
const cssFiles = await findFiles(assetRoot, ".css");
const replacements = new Map();

for (const file of cssFiles) {
  const contents = await readFile(file);
  const hash = createHash("md5").update(contents).digest("hex").slice(0, 16);
  const parsed = path.parse(file);
  const hashedPath = path.join(
    parsed.dir,
    `${parsed.name}${separator}${hash}${parsed.ext}`
  );

  await rename(file, hashedPath);

  const originalRelative = toPosix(path.relative(baseDir, file));
  const hashedRelative = toPosix(path.relative(baseDir, hashedPath));

  replacements.set(originalRelative, hashedRelative);
}

if (htmlDir) {
  const htmlFiles = await findFiles(htmlDir, ".html");

  for (const file of htmlFiles) {
    let contents = await readFile(file, "utf8");

    for (const [originalRelative, hashedRelative] of replacements) {
      contents = contents.replaceAll(originalRelative, hashedRelative);
    }

    await writeFile(file, contents);
  }
}

async function findFiles(dir, extension) {
  const entries = await readdir(dir, { withFileTypes: true });
  const files = [];

  for (const entry of entries) {
    const entryPath = path.join(dir, entry.name);

    if (entry.isDirectory()) {
      files.push(...(await findFiles(entryPath, extension)));
    } else if (entry.isFile() && entryPath.endsWith(extension)) {
      files.push(entryPath);
    }
  }

  return files;
}

function toPosix(value) {
  return value.split(path.sep).join("/");
}
