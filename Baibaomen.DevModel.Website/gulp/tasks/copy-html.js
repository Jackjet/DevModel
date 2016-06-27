
var gulp = require('gulp');

var config = require('../config');

var changed = require('gulp-changed');
// include plug-ins
var minifyHTML = require('gulp-minify-html');

// minify new or changed HTML pages
gulp.task('copy-html', function () {
    gulp.src([config.path.src + "/*.html"])
      .pipe(changed(config.path.dest))
      //.pipe(minifyHTML())
      .pipe(gulp.dest(config.path.dest));
    
    gulp.src([config.path.app + "/**/*.html"])
      .pipe(changed(config.path.destApp))
      //.pipe(minifyHTML())
      .pipe(gulp.dest(config.path.destApp));
});