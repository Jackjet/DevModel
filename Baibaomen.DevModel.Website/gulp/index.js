'use strict'

var requireDir = require('require-dir');

//requireDir('./tasks');

var sort = require('sort-stream2');
var del = require('del');
var config = require('./config');
var gulp = require('gulp');
var inject = require('gulp-inject');
var runSequence = require('run-sequence');
var concat = require('gulp-concat');
var autoprefix = require('gulp-autoprefixer');
var minifyCSS = require('gulp-minify-css');
var minifyHtml = require('gulp-minify-html');

//required. Orelse the autoprefix will throw error.
var Promise = require('es6-promise').Promise;

gulp.task('clean-dest-dir', function () {
    return del([config.path.dest]).then(function (paths) {
        console.log('Folders that would be deleted:\n', paths.join('\n'));
    });
});

gulp.task('inject-appjs-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.app + '/**/*.js']);

    return target.pipe(inject(sources, { relative: true, starttag: '<!-- inject:app:{{ext}} -->' }))
        .pipe(gulp.dest(config.path.src));
});

gulp.task('inject-js-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.js + '/**/*.js']);

    return target.pipe(inject(sources, { relative: true, starttag: '<!-- inject:js:{{ext}} -->' }))
        .pipe(gulp.dest(config.path.src));
});

gulp.task('inject-css-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.css + '/**/*.css']).pipe(sort(function (i, j) { return i.path == j.path ? 0 : i.path > j.path ? 1 : -1;}));

    return target.pipe(inject(sources, { relative: true, starttag: '<!-- inject:{{ext}} -->' }))
        .pipe(gulp.dest(config.path.src));
});


// CSS concat, auto-prefix and minify
gulp.task('minify-css', function () {
    //gulp.src([config.path.css + '/*.css'], {base:'.'})
    gulp.src([config.path.css + '/*.css']).pipe(sort(function (i, j) { return i.path == j.path ? 0 : i.path > j.path ? 1 : -1;}))
      .pipe(concat(config.path.cssBundle))
      .pipe(autoprefix('last 2 versions'))
      .pipe(minifyCSS())
      .pipe(gulp.dest(config.path.destCss));
});

//gulp.task('release-staging', function () {
//    return runSequence('clean-dest-dir', 'minify-html', 'minify-scripts-app', 'minify-scripts-js', 'minify-css', 'replace-staging');
//});