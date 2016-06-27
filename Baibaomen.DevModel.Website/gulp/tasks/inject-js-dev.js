'use strict'

var gulp = require('gulp');

var config = require('../config');
var inject = require('gulp-inject');

gulp.task('injectjs-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.app + '/**/*.js']);

    return target.pipe(inject(sources, { relative: true }))
        .pipe(gulp.dest(config.path.src));

});
