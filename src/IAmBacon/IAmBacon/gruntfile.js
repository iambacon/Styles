/// <vs BeforeBuild='default' SolutionOpened='default' />
module.exports = function (grunt) {
    'use strict';

    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-autoprefixer');

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
                map: true // Update source map (creates one if it can't find an existing map)
            },

            // Prefix all files
            multiple_files: {
                src: 'Content/stylesheets/pages/**/*.css'
            },
        }
    });

    grunt.registerTask('default', ['sass', 'autoprefixer']);
};