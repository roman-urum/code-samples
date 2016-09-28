require.config({
    waitSeconds: 15,
    baseUrl: '/Content/js',
    urlArgs: '', //'bust=' +  (new Date()).getTime(),
    paths: {
        'jquery': 'libs/jquery-2.1.3',
        'jquery-validate': 'libs/jquery.validate',
        'jquery-validate-unobtrusive': 'libs/jquery.validate.unobtrusive',
        'jquery-ui': 'libs/jquery-ui-custom',
        'jquery-ui-touch': 'libs/jquery.ui.touch-punch',
        'jquery-chosen': 'libs/chosen.jquery',
        'bootstrap': 'libs/bootstrap',
        'moment': 'libs/moment/moment',
        'moment-timezone-with-data': 'libs/moment/moment-timezone-with-data',
        'bootstrap-datetimepicker': 'libs/bootstrap-datetimepicker.modified',
        'bootstrap-switch': 'libs/bootstrap-switch.modified',
        'bootstrap-select': 'libs/bootstrap-select',
        'bootstrap-typeahead': 'libs/bootstrap3-typeahead',
        'underscore': 'Controllers/UnderscoreConfig',
        'originalUnderscore': 'libs/underscore',
        'backbone': 'libs/backbone',
        'highcharts': 'libs/highcharts',
        'i18n': 'libs/i18n',
        'text': 'libs/text',
        'async': 'libs/async',
        'bootstrap-tokenfield': 'libs/bootstrap-tokenfield',
        'BackboneBootstrapModal': 'libs/backboneBootstrapModal',
        'BackboneBootstrapAlert': 'libs/backboneBootstrapAlert',
        'BackboneModelBinder': 'libs/backboneModelBinder',
        'BackboneCollectionBinder': 'libs/backboneCollectionBinder',
        'backbone-nested': 'libs/backbone-nested',
        'BootstrapSlider': 'libs/bootstrap-slider',
        'backbone.memento': 'libs/backbone.memento.modified',
        'backbone.marionette': 'libs/backbone.marionette',
        'backbone-model-file-upload': 'libs/backbone-model-file-upload',
        'Backbone.Validation': 'libs/backbone-validation-amd',
        'inputmask.date.extensions': 'libs/inputmask/inputmask.date.extensions.min',
        'inputmask.extensions': 'libs/inputmask/inputmask.extensions.min',
        'inputmask.numeric.extensions': 'libs/inputmask/inputmask.numeric.extensions.min',
        'inputmask.phone.extensions': 'libs/inputmask/inputmask.phone.extensions.min',
        'inputmask.regex.extensions': 'libs/inputmask/inputmask.regex.extensions.min',
        'jquery.inputmask': 'libs/inputmask/jquery.inputmask.min',
        'inputmask.dependencyLib': 'libs/inputmask/inputmask.dependencyLib.jquery.min',
        'inputmask': 'libs/inputmask/inputmask.min',
        'calendar': 'libs/calendar',
        'session-timeout': 'custom/controls/session-timeout',
        'BackboneZoomCalls': 'libs/backboneZoomCalls',
        'tagcloud': 'libs/jquery.tagcloud'
    },
    shim: {
        'originalUnderscore': {
            'exports': '_'
        },
        'session-timeout': {
            'deps': ['jquery']
        },
        'jquery-validate': {
            'deps': ['jquery']
        },
        'jquery-validate-unobtrusive': {
            'deps': ['jquery', 'jquery-validate']
        },
        'bootstrap': {
            'deps': ['jquery']
        },
        'bootstrap-switch': {
            'deps': ['jquery', 'bootstrap']
        },
        'jquery-chosen': {
            'deps': ['jquery']
        },
        'backbone.marionette': {
            'deps': ['backbone']
        },
        'Backbone.Validation': {
            'deps': ['underscore', 'backbone']
        },
        'backbone.memento': {
            'deps': ['underscore', 'backbone']
        }
    }
});