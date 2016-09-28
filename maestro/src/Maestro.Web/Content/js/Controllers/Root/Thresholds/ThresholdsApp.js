'use strict';

define([
    'jquery',
    'underscore',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Root/Thresholds/Views/MainView',
    'Controllers/Constants',
    'bootstrap',
    'backbone.memento',
    'Backbone.Validation',
    'Controllers/Validation'
], function ($, _, app, MainView, Constants) {

    function normalizeOptions(options) {
        if (typeof options.initCollections !== 'object') {
            options.initCollections = {};
        }

        if (typeof options.events !== 'object') {
            options.events = {};
        }

        if (typeof options.crud !== 'object') {
            options.crud = {};
        }

        // if (typeof options.id !== 'object') {
        //     options.id = {};
        // }

        if (!options.mode) {
            options.mode = Constants.thresholdAppModes.PATIENT;
        }
        if (!options.isReadOnly) {
            options.isReadOnly = false;
        }

        return options;
    }

    function initialize(options) {

        if (options.isReInit) {
            app.reInit();
        }

        app = _.extend( app, normalizeOptions(options) );
        app.vent = app.vent ||  _.extend({}, Backbone.Events);

        if( app.views.mainView )
            app.views.mainView.remove();

        app.views.mainView = new MainView();

        this.views = app.views;
        this.el = app.views.mainView.render().el;
        this.save = app.views.mainView.save.bind(app.views.mainView);
        this.getCollection = function (name) {
            return app.collections[name];
        }
    }

    return initialize;
});