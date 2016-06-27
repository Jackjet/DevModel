'use strict'

var gulp = require('gulp');
var htmlReplace = require('gulp-html-replace');
var config = require('../config');

var replaceStagingTask = function () {
    return gulp.src([config.path.dest + '/' + config.path.appBundle, config.path.dest + '/' + config.path.jsBundle, config.path.dest + '/' + config.path.cssBundle])
    .pipe(replace('http://localhost', 'http://staging'))
    .pipe(replace('http://someother:1992/', 'http://staging:1992/'))
    .pipe(gulp.dest(config.path.dest));
};

gulp.task('replace-staging', replaceStagingTask);
