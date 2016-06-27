'use strict'

var gulp = require('gulp');

var config = require('../config');
var inject = require('gulp-inject');

gulp.task('injectjs-staging', function () {
    var target = gulp.src(config.path.dest + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.dest + '/' + config.path.jsBundle, config.path.dest + '/' + config.path.appBundle]);

    return target.pipe(inject(sources, { relative: true }))
        .pipe(gulp.dest(config.path.dest));

});
