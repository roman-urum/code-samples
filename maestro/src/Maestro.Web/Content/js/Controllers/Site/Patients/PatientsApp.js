'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Router',
    'Controllers/Validation',
    'backbone.memento',
    'bootstrap',
    'bootstrap-switch',
    'bootstrap-select',
    'bootstrap-typeahead',
    'bootstrap-datetimepicker',
    'jquery.inputmask',
    'inputmask.date.extensions',
    'inputmask.numeric.extensions',
    'jquery-chosen',
    'jquery-ui',
    'jquery-ui-touch',
    'session-timeout'
], function ($, _, Backbone, app, Router) {

    var initialize = function () {
        // window.i18n = Locales;
        // window.partial = function( template ) {
        //     return _.template( templates[template] )();
        // };

        Backbone.Collection.prototype.save = function (options) {
            Backbone.sync("create", this, options);
        };
        Backbone.Collection.prototype.update = function (options) {
            Backbone.sync("update", this, options);
        };

        app.vent = _.extend({}, Backbone.Events);

        app.router = new Router();

    };

    return {
        initialize: initialize
    };

});