import fs from "node:fs";
import path from "node:path";
import postcss from "postcss";

const args = process.argv.slice(2);
const watch = args.includes("--watch");
const [source, target, scope = ".sg-preview-example"] = args.filter(
  (arg) => arg !== "--watch",
);

if (!source || !target) {
  console.error("Usage: node scripts/scope-preview-css.mjs <source> <target> [scope] [--watch]");
  process.exit(1);
}

const scopedSelector = (selector) => {
  const trimmed = selector.trim();

  if (!trimmed || trimmed.startsWith(scope)) {
    return trimmed;
  }

  if (trimmed === "html" || trimmed === "body" || trimmed === ":root") {
    return scope;
  }

  if (trimmed.startsWith("html ")) {
    return `${scope} ${trimmed.slice(5)}`;
  }

  if (trimmed.startsWith("body ")) {
    return `${scope} ${trimmed.slice(5)}`;
  }

  return `${scope} ${trimmed}`;
};

const writeScopedCss = () => {
  const css = fs.readFileSync(source, "utf8");
  const root = postcss.parse(css, { from: source });

  root.walkRules((rule) => {
    if (rule.parent?.type === "atrule" && /keyframes$/i.test(rule.parent.name)) {
      return;
    }

    rule.selectors = rule.selectors.map(scopedSelector);
  });

  fs.writeFileSync(target, root.toString());
};

writeScopedCss();

if (watch) {
  const sourceDir = path.dirname(source);
  const sourceFile = path.basename(source);
  let timeout;

  fs.watch(sourceDir, (eventType, fileName) => {
    if (fileName !== sourceFile) {
      return;
    }

    clearTimeout(timeout);
    timeout = setTimeout(writeScopedCss, 75);
  });
}
