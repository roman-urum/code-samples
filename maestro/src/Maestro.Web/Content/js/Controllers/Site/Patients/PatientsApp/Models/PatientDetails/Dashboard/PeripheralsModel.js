'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, BackboneNested, Helpers, app) {
    return Backbone.NestedModel.extend({

        url: function () {
            return '/' + app.siteId + '/Patients/Peripherals' +
                '?patientId=' + this.patientId;
        },

        initialize: function (attrs, options) {
            if (!options) throw 'Options required';

            this.patientId = options.patientId;
            _.extend(this, new Backbone.Memento(this));
        },

        parse: function (response) {
            return {
                devices: response
            };
        }

    });
});