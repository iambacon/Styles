import path from "node:path";
import { mkdir, rm } from "node:fs/promises";

for (const target of process.argv.slice(2)) {
  await rm(target, { recursive: true, force: true });

  if (!path.extname(target)) {
    await mkdir(target, { recursive: true });
  }
}
