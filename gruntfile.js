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
                        maxBuffer: 1000*1024,
                    }
                }
            },
            eleventyServe: {
                command: 'npx @11ty/eleventy --serve',
                options: {
                    stderr: false,
                    execOptions: {
                        cwd: 'docs',
                        maxBuffer: 1000*1024,
                    }
                }
            }
        },
        
        // Sass
        sass: {
            options: {
                implementation: sass
            },
            dist: {
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
                src: 'dist/css/style.css'
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
            }
        }
    });

    // Load plugins
    grunt.loadNpmTasks('grunt-shell');
    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-postcss');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default',
        'Compile all SCSS files, then watch for file changes and re-compile',
        ['shell:eleventyServe', 'watch']);

    grunt.registerTask('build',
        'Compile all SCSS files minified',
        ['sass', 'postcss:prod']);
};
