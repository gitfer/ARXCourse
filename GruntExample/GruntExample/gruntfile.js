module.exports = function (grunt) {
    'use strict';
    // Project configuration.
    grunt.initConfig({
        watch: {
            myUtils: {
                files: ['gruntfile.js', 'Scripts/app/**/*.js'],
                tasks: 'jshint'
            },
            myStyles: {
                files: ['Content/app/**/*.scss'],
                tasks: 'sass'
            }
        },
        jshint: {
            options: {
                jshintrc: '.jshintrc',
                force: true
            },
            all: ['gruntfile.js', 'Scripts/app/**/*.js']
        },
        sass: {
            dist: {
                files: [{
                    expand: true,
                    cwd: 'Content/app',
                    src: ['*.scss'],
                    dest: 'Content/app',
                    ext: '.css'
                }]
            }
        }
    });

    // Load the Grunt plugins.
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-sass');

    // Register the default tasks.
    grunt.registerTask('default', ['jshint', 'sass', 'watch']);
};