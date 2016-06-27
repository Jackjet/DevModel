
var gulp = require('gulp');

var config = require('../config');

// include plug-ins
var concat = require('gulp-concat');
var stripDebug = require('gulp-strip-debug');
var uglify = require('gulp-uglify');

// JS concat, strip debugging and minify
gulp.task('minify-scripts-app', function () {
    gulp.src([config.path.app + '/**/*.js'])
      .pipe(concat(config.path.appBundle))
      .pipe(stripDebug())
      .pipe(uglify())
      .pipe(gulp.dest(config.path.dest));
});