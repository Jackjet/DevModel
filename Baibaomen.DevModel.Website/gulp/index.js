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
var replace = require('gulp-replace');
var minifyCSS = require('gulp-minify-css');
var minifyHtml = require('gulp-minify-html');
var stripDebug = require('gulp-strip-debug');
var uglify = require('gulp-uglify');
var changed = require('gulp-changed');

//required. Orelse the autoprefix will throw error.
var Promise = require('es6-promise').Promise;

gulp.task('clean-dest-dir', function () {
    return del([config.path.dest]).then(function (paths) {
        console.log('Folders that would be deleted:\n', paths.join('\n'));
    });
});

gulp.task('inject-appjs-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.app + '/**/*.js']).pipe(sort(function (i, j) { return i.path == j.path ? 0 : i.path > j.path ? 1 : -1; }));

    return target.pipe(inject(sources, { relative: true, starttag: '<!-- inject:app:{{ext}} -->' }))
        .pipe(gulp.dest(config.path.src));
});

gulp.task('inject-js-dev', function () {
    var target = gulp.src(config.path.src + '/' + config.path.indexPage);
    var sources = gulp.src([config.path.js + '/**/*.js']).pipe(sort(function (i, j) { return i.path == j.path ? 0 : i.path > j.path ? 1 : -1; }));

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

gulp.task('minify-html', function () {
    gulp.src([config.path.dest + "/**/*.html"])
      .pipe(minifyHtml())
      .pipe(gulp.dest(config.path.dest));
});

gulp.task('minify-scripts-app', function () {
    gulp.src([config.path.app + '/**/*.js'])
      .pipe(concat(config.path.appBundle))
      .pipe(stripDebug())
      .pipe(uglify())
      .pipe(gulp.dest(config.path.dest));
});

gulp.task('minify-scripts-js', function () {
    gulp.src([config.path.js + '/**/*.js'])
      .pipe(concat(config.path.jsBundle))
      .pipe(stripDebug())
      .pipe(uglify())
      .pipe(gulp.dest(config.path.dest));
});

gulp.task('_prepare-dev', function () {
    return runSequence('inject-css-dev', 'inject-js-dev', 'inject-appjs-dev');
});

var replaceStagingTask = function () {
    return gulp.src([config.path.dest + '/' + config.path.appBundle, config.path.dest + '/' + config.path.jsBundle, config.path.dest + '/' + config.path.cssBundle])
    .pipe(replace('http://localhost', 'http://staging'))
    .pipe(replace('http://someother:1992/', 'http://staging:1992/'))
    .pipe(gulp.dest(config.path.dest));
};

gulp.task('replace-staging', replaceStagingTask);


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

gulp.task('_release-staging', function () {
    return runSequence('clean-dest-dir','copy-html', 'minify-html', 'minify-scripts-app', 'minify-scripts-js', 'minify-css', 'replace-staging');
});