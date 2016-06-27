'use strict'

var gulp = require('gulp');
var del = require('del');
var config = require('../config');

var cleanTask = function () {
    return del([config.path.dest]).then(function (paths) {
        console.log('Folders that would be deleted:\n', paths.join('\n'));
    });
};

gulp.task('clean-dest-dir', cleanTask);