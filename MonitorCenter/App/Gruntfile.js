module.exports = function (grunt) {
    grunt.initConfig({
        clean: {
            dist: ["dist/"]
        },
        concat: {
            dist: {
                src: ["app.js","controller/*.js"],
                dest: "dist/js/scripts.js"
            },
            ryan: {
                src: ["controller/*.js"],
                dest: "dist/js/controller.js"
            }
        },
        uglify: {
            dist: {
                src: ["dist/js/scripts.js"],
                dest: "dist/js/scripts.min.js"
            }
        }

    });
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-contrib-concat");
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.registerTask("default", ["clean", "concat", "uglify"]);
    
}