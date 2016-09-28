'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    var app = {
        isValid: !0,
        views: {},
        models: {},
        // options: {},
        collections: {},
        // vent: _.extend({}, Backbone.Events)
    };

    var reInit = function () {

        for (var prop in app) {
            if (app.hasOwnProperty(prop)) {
                delete app[prop];
            }
        }

        app.isValid = !0;
        app.views = {};
        app.models = {};
        // app.options = {};
        app.collections = {};
        // app.vent = _.extend({}, Backbone.Events);
        app.reInit = reInit;
    };

    app.reInit = reInit;

    return app;
});