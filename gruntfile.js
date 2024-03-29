"use strict";
const sass = require("sass");

module.exports = function (grunt) {
  grunt.initConfig({
    // Shell commands
    shell: {
      eleventyBuild: {
        command: "npx @11ty/eleventy",
        options: {
          stderr: false,
          execOptions: {
            cwd: "docs",
            maxBuffer: 1000 * 1024,
          },
        },
      },
      eleventyServe: {
        command: "npx @11ty/eleventy --serve",
        options: {
          stderr: false,
          execOptions: {
            cwd: "docs",
            maxBuffer: 1000 * 1024,
          },
        },
      },
    },

    // Concurrent
    concurrent: {
      options: {
        logConcurrentOutput: true,
      },
      dev: ["watch", "shell:eleventyServe"],
      sassCompile: ["sass", "sass:styleguide"],
    },

    // Sass
    sass: {
      options: {
        implementation: sass,
      },
      dist: {
        files: [
          {
            "docs/assets/css/style.css": "src/sass/style.scss",
          },
        ],
      },
      styleguide: {
        files: [
          {
            "docs/assets/css/styleguide.css":
              "docs/assets/sass/styleguide.scss",
          },
        ],
      },
      prod: {
        files: [
          {
            "dist/css/style.css": "src/sass/style.scss",
          },
        ],
      },
    },

    // PostCSS
    postcss: {
      prod: {
        options: {
          map: false,

          processors: [
            require("autoprefixer")(), // add vendor prefixes
            require("cssnano")(), // minify the result
          ],
        },
        src: "dist/css/style.css",
      },
      dev: {
        options: {
          map: {
            inline: false,
          },

          processors: [
            require("autoprefixer")(), // add vendor prefixes
          ],
        },
        src: "docs/assets/css/*.css",
      },
      docs: {
        options: {
          map: false,

          processors: [
            require("autoprefixer")(), // add vendor prefixes
            require("cssnano")(), // minify the result
          ],
        },
        src: "docs/assets/css/*.css",
      },
    },

    // Watch
    watch: {
      scss: {
        files: "src/sass/**/*.scss",
        tasks: ["sass", "postcss:dev"],
        options: {
          spawn: false,
        },
      },
      styleguide: {
        files: "docs/assets/**/*.scss",
        tasks: ["sass:styleguide"],
        options: {
          spawn: false,
        },
      },
    },

    // Bump
    bump: {
      options: {
        files: ["package.json"],
        updateConfigs: [],
        commit: true,
        commitMessage: "Release v%VERSION%",
        commitFiles: ["package.json"],
        createTag: true,
        tagName: "v%VERSION%",
        tagMessage: "Version %VERSION%",
        push: false,
        gitDescribeOptions: "--tags --always --abbrev=1 --dirty=-d",
        globalReplace: false,
        prereleaseName: false,
        metadata: "",
        regExp: false,
      },
    },

    // Compress
    compress: {
      main: {
        options: {
          archive: "dist/site.zip",
        },
        files: [
          {
            src: ["**/*"],
            cwd: "docs/_site/",
            expand: true,
          },
        ],
      },
    },

    // CacheBust
    cacheBust: {
      css: {
        options: {
          deleteOriginals: true,
          baseDir: "docs/_site",
          assets: ["assets/css/**"],
        },
        src: ["docs/_site/**/*.html"],
      },
      prod: {
        options: {
          deleteOriginals: true,
          baseDir: "dist/css",
          assets: ["**"],
          separator: "-",
        },
        // fake file we don't want to update any HTML
        src: ["dummy.html"],
      },
    },

    // Clean
    clean: ["docs/_site/assets/css/*", "dist/css/*"],
  });

  // Load plugins
  grunt.loadNpmTasks("grunt-shell");
  grunt.loadNpmTasks("grunt-concurrent");
  grunt.loadNpmTasks("grunt-sass");
  grunt.loadNpmTasks("@lodder/grunt-postcss");
  grunt.loadNpmTasks("grunt-contrib-watch");
  grunt.loadNpmTasks("grunt-bump");
  grunt.loadNpmTasks("grunt-contrib-compress");
  grunt.loadNpmTasks("grunt-cache-bust");
  grunt.loadNpmTasks("grunt-contrib-clean");

  grunt.registerTask(
    "default",
    "Compile all SCSS files and rebuild docs, then watch for file changes and re-compile",
    ["concurrent:dev"]
  );

  grunt.registerTask(
    "build",
    "Compile all SCSS files minified and output to dist folder",
    ["clean", "sass:prod", "postcss:prod", "cacheBust:prod"]
  );

  grunt.registerTask(
    "deploy",
    "Build documentation site and Zip ready for deployment",
    [
      "concurrent:sassCompile",
      "postcss:docs",
      "clean",
      "shell:eleventyBuild",
      "cacheBust",
      "compress:main",
    ]
  );
};
