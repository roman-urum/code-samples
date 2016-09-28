var gulp = require('gulp');
var gulpLess = require('gulp-less');

gulp.task('lessTranspile', function() {
  return gulp.src('styles/main.less')
    .pipe(gulpLess())
    .pipe(gulp.dest('./css/'));
});

gulp.task('watch', function() {
  gulp.watch('styles/*.less', ['lessTranspile']);
});

gulp.task('default');