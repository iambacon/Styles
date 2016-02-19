/// <vs SolutionOpened='dev' />
module.exports = function (grunt) {
    'use strict';

    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-autoprefixer');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        // Sass
        sass: {
            options: {
                //sourceMap: true, // Create source map
                //outputStyle: 'compressed' // Minify output
            },
            dist: {
                files: [
                  {
                      expand: true, // Recursive
                      cwd: "Content/sass/pages", // The startup directory
                      src: ["**/*.scss"], // Source files
                      dest: "Content/stylesheets/pages", // Destination
                      ext: ".css" // File extension
                  }
                ]
            }
        },

        // Autoprefixer
        autoprefixer: {
            options: {
                map: {
                    inline: false
                }
            },

            // Prefix all files
            multiple_files: {
                src: 'Content/stylesheets/pages/**/*.css'
            }
        },

        // Watch
        watch: {
            css: {
                files: ['Content/sass/**/*.scss'],
                tasks: ['sass', 'autoprefixer'],
                options: {
                    spawn: false
                }
            }
        }
    });

    grunt.registerTask('dev', ['watch']);
    grunt.registerTask('prod', ['sass', 'autoprefixer']);
};