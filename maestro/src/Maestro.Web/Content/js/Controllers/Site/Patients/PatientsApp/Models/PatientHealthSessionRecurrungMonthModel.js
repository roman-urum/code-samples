'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, app, Helpers) {
    return Backbone.Model.extend({
        defaults: {
            number: null,
            isSelected: false
        },

        initialize: function () {

        }


    });
});