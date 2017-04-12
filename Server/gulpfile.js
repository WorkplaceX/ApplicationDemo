var gulp = require('gulp');
var shell = require('gulp-shell')
var runSequence = require('run-sequence');
var es = require('event-stream');
var clean = require('gulp-clean');

// npm run gulp (../Universal/)
gulp.task('universal', shell.task([
  'echo X=$(pwd) && cd .. && echo Y=$(pwd) && cd Submodule/Universal && echo Z=$(pwd) && npm run gulp'
]))

gulp.task('clean', function () {
    return gulp.src('./Universal/')
        .pipe(clean())
})

gulp.task('cleanExpress', function () {
    return gulp.src('../Submodule/UniversalExpress/Universal/')
        .pipe(clean({ force: true }))
})

// Copy file
gulp.task('copy', function () {
    return es.concat(
        gulp.src('../Submodule/Universal/publish/**/*.*')
            .pipe(gulp.dest('./Universal/')),
        gulp.src('../Submodule/Client/*.html')
            .pipe(gulp.dest('./Universal/')),
        gulp.src('../Submodule/Client/*.css')
            .pipe(gulp.dest('./Universal/')),
        gulp.src('../Submodule/Client/*.js')
            .pipe(gulp.dest('./Universal/')),
        gulp.src('../Submodule/Client/dist/**/*.js')
            .pipe(gulp.dest('./Universal/dist/')),
        gulp.src('../Submodule/Universal/publish/**/*.*')
            .pipe(gulp.dest('../Submodule/UniversalExpress/Universal/')),
        gulp.src('../Submodule/Client/node_modules/bootstrap/dist/css/bootstrap.min.css')
            .pipe(gulp.dest('./wwwroot/node_modules/bootstrap/dist/css/'))
    );
})

gulp.task('default', function () {
    return runSequence('universal', 'clean', 'cleanExpress', 'copy');
});