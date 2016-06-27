
var gulp = require('gulp');

var config = require('../config');

// include plug-ins
var minifyHTML = require('gulp-minify-html');

// minify new or changed HTML pages
gulp.task('minify-html', function () {
    gulp.src([config.path.dest + "/**/*.html"])
      .pipe(minifyHTML())
      .pipe(gulp.dest(config.path.dest));
});