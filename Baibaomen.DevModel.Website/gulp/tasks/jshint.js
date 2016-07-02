var gulp = require('gulp');

var config = require('../config');

// include plug-ins
var jshint = require('gulp-jshint');

// JS hint task
gulp.task('jshint', function () {
    gulp.src([config.path.dest + '/*.js'])
      .pipe(jshint())
      .pipe(jshint.reporter('default'));
});