module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        sass: {    
            options: {
                compass: true  
            },
            dev: {
                files: {
                    'Content/Stylesheets/global.css': 'Content/site.scss'
                }
            }
        },
        watch: {
            files: ['**/*.scss'],
            tasks: ['sass']
        }
    });

    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-newer');

    grunt.registerTask('default', ['sass', 'newer:watch']);
    grunt.registerTask('build', ['sass']);
}