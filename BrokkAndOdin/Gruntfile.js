module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        less: {    
            dev: {
                files: {
                    'Content/Stylesheets/site.css': 'Content/site.less',
                    'Content/Stylesheets/gallery.css': 'Content/gallery.less',
                    'Content/Stylesheets/bootstrap.css': 'Content/bootstrap-custom.less',
                    'Content/Stylesheets/video.css': 'Content/video.less'
                }
            }
        },
        watch: {
            files: ['**/*.less'],
            tasks: ['less']
        }
    });

    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-newer');

    grunt.registerTask('default', ['less', 'newer:watch']);
    grunt.registerTask('build', ['less']);
}