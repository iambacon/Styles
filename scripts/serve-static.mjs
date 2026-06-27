import { createReadStream, existsSync, statSync } from "node:fs";
import { createServer } from "node:http";
import { extname, join, normalize, resolve, sep } from "node:path";

const root = resolve(process.argv[2] || "docs/_site");
const port = Number(process.env.PORT || process.argv[3] || 8080);

const contentTypes = new Map([
  [".css", "text/css; charset=utf-8"],
  [".gif", "image/gif"],
  [".html", "text/html; charset=utf-8"],
  [".ico", "image/x-icon"],
  [".jpg", "image/jpeg"],
  [".js", "text/javascript; charset=utf-8"],
  [".json", "application/json; charset=utf-8"],
  [".png", "image/png"],
  [".svg", "image/svg+xml"],
  [".txt", "text/plain; charset=utf-8"],
  [".webmanifest", "application/manifest+json; charset=utf-8"],
  [".xml", "application/xml; charset=utf-8"],
]);

const isInsideRoot = (filePath) =>
  filePath === root || filePath.startsWith(`${root}${sep}`);

const resolveFile = (urlPath) => {
  const decodedPath = decodeURIComponent(urlPath.split("?")[0]);
  const safePath = normalize(decodedPath).replace(/^(\.\.[/\\])+/, "");
  let filePath = resolve(join(root, safePath));

  if (!isInsideRoot(filePath)) {
    return null;
  }

  if (existsSync(filePath) && statSync(filePath).isDirectory()) {
    filePath = join(filePath, "index.html");
  }

  return filePath;
};

createServer((request, response) => {
  const filePath = resolveFile(request.url || "/");

  if (!filePath || !existsSync(filePath) || !statSync(filePath).isFile()) {
    response.writeHead(404, { "Content-Type": "text/plain; charset=utf-8" });
    response.end("Not found");
    return;
  }

  response.writeHead(200, {
    "Content-Type": contentTypes.get(extname(filePath)) || "application/octet-stream",
  });
  createReadStream(filePath).pipe(response);
}).listen(port, "0.0.0.0", () => {
  console.log(`Serving ${root} at http://localhost:${port}/`);
});
