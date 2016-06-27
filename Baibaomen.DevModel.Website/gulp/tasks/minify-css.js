var gulp = require('gulp');

var config = require('../config');

//required. Orelse the autoprefix will throw error.
var Promise = require('es6-promise').Promise;

// include plug-ins
var concat = require('gulp-concat');
var autoprefix = require('gulp-autoprefixer');
var minifyCSS = require('gulp-minify-css');

// CSS concat, auto-prefix and minify
gulp.task('minify-css', function () {
    gulp.src([config.path.css + '/*.css'])
      .pipe(concat(config.path.cssBundle))
      .pipe(autoprefix('last 2 versions'))
      .pipe(minifyCSS())
      .pipe(gulp.dest(config.path.dest));
});