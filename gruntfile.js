'use strict';
const sass = require('node-sass');

module.exports = function (grunt) {
    grunt.initConfig({
        // Shell commands
        shell: {
            eleventyBuild: {
                command: 'npx @11ty/eleventy',
                options: {
                    stderr: false,
                    execOptions: {
                        cwd: 'docs',
                        maxBuffer: 1000 * 1024,
                    }
                }
            },
            eleventyServe: {
                command: 'npx @11ty/eleventy --serve',
                options: {
                    stderr: false,
                    execOptions: {
                        cwd: 'docs',
                        maxBuffer: 1000 * 1024,
                    }
                }
            },
            zipSite: { 
                command: 'powershell -command "Compress-Archive -Path docs/_site/* -DestinationPath dist/site.zip -Force"' 
            }
        },

        concurrent: {
            options: {
                logConcurrentOutput: true
            },
            dev: ['watch', 'shell:eleventyServe'],
            sassCompile: ['sass', 'sass:styleguide']
        },

        // Sass
        sass: {
            options: {
                implementation: sass
            },
            dist: {
                files: [
                    {
                        'docs/assets/css/style.css': 'src/sass/style.scss'
                    }
                ]
            },
            styleguide: {
                files: [
                    {
                        'docs/assets/css/styleguide.css': 'docs/assets/sass/styleguide.scss'
                    }
                ]
            },
            prod: {
                files: [
                    {
                        'dist/css/style.css': 'src/sass/style.scss'
                    }
                ]
            }
        },

        // PostCSS
        postcss: {
            prod: {
                options: {
                    map: false,

                    processors: [
                        require('autoprefixer')(), // add vendor prefixes
                        require('cssnano')() // minify the result
                    ]
                },
                src: 'dist/css/style.css'
            },
            dev: {
                options: {
                    map: {
                        inline: false
                    },

                    processors: [
                        require('autoprefixer')() // add vendor prefixes
                    ]
                },
                src: 'docs/assets/css/*.css'
            },
            docs: {
                options: {
                    map: false,

                    processors: [
                        require('autoprefixer')(),// add vendor prefixes
                        require('cssnano')()// minify the result
                    ]
                },
                src: 'docs/assets/css/*.css'
            }
        },

        // Watch
        watch: {
            scss: {
                files: 'src/sass/**/*.scss',
                tasks: ['sass', 'postcss:dev'],
                options: {
                    spawn: false
                }
            },
            styleguide: {
                files: 'docs/assets/**/*.scss',
                tasks: ['sass:styleguide'],
                options: {
                    spawn: false
                }
            }
        }
    });

    // Load plugins
    grunt.loadNpmTasks('grunt-shell');
    grunt.loadNpmTasks('grunt-concurrent');
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-postcss');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default',
        'Compile all SCSS files and rebuild docs, then watch for file changes and re-compile',
        ['concurrent:dev']);

    grunt.registerTask('build',
        'Compile all SCSS files minified and output to dist folder',
        ['sass:prod', 'postcss:prod']);

    grunt.registerTask('deploy',
        'Build documentation site and Zip ready for deployment',
        ['concurrent:sassCompile', 'postcss:docs', 'shell:eleventyBuild', 'shell:zipSite']);
};
