/// <binding AfterBuild='scriptsCareBuilderApp, scriptsDashboardApp, scriptsPatientsApp, scriptsCustomerSettingsApp' />
var gulp = require('gulp');
var amdOptimize = require('amd-optimize');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');

var uglifyOptions = {
    output: {
        beautify: false,
        comments: false
    },
    compress: {
        sequences: true,
        booleans: true,
        conditionals: true,
        hoist_funs: false,
        hoist_vars: false,
        warnings: false
    },
    mangle: true,
    outSourceMap: false,
    basePath: 'www',
    sourceRoot: '/'
};

var amdOptimizeOptions = {
    configFile: 'Content/js/config.js',
    baseUrl: 'Content/js',
    findNestedDependencies: true,
    preserveFiles: false
};

gulp.task('scriptsCareBuilderApp', function () {
    return gulp
        .src(['Content/js/Controllers/Customer/CareBuilder/**/*.js'])
        .pipe(amdOptimize("appCareBuilder", amdOptimizeOptions))
        .pipe(concat('appCareBuilder.min.js', { newLine: ';' }))
        .pipe(uglify(uglifyOptions))
        .pipe(gulp.dest('Content/js'));
});

gulp.task('scriptsCustomerSettingsApp', function () {
    return gulp
        .src(['Content/js/Controllers/Customer/Settings/**/*.js', 'Content/js/Controllers/Root/Thresholds/**/*.js'])
        .pipe(amdOptimize("appCustomerSettings", amdOptimizeOptions))
        .pipe(concat('appCustomerSettings.min.js', { newLine: ';' }))
        .pipe(uglify(uglifyOptions))
        .pipe(gulp.dest('Content/js'));
});

gulp.task('scriptsDashboardApp', function () {
    return gulp
        .src(['Content/js/Controllers/Site/Dashboard/**/*.js'])
        .pipe(amdOptimize("appDashboard", amdOptimizeOptions))
        .pipe(concat('appDashboard.min.js', { newLine: ';' }))
        .pipe(uglify(uglifyOptions))
        .pipe(gulp.dest('Content/js'));
});

gulp.task('scriptsPatientsApp', function () {
    return gulp
        .src(['Content/js/Controllers/Site/Patients/**/*.js', 'Content/js/Controllers/Root/Thresholds/**/*.js'])
        .pipe(amdOptimize("appPatients", amdOptimizeOptions))
        .pipe(concat('appPatients.min.js', { newLine: ';' }))
        .pipe(uglify(uglifyOptions))
        .pipe(gulp.dest('Content/js'));
});