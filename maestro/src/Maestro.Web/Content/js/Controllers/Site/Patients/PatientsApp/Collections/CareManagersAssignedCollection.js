'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Models/CareManagerAssignedModel',
    'Controllers/Helpers'
], function ($, _, Backbone, app, CareManagerAssignedModel, Helpers) {
    return Backbone.Collection.extend({

        model: CareManagerAssignedModel,

        initialize: function () {

            _.extend(this, new Backbone.Memento(this));

        },

        parse: function (response, options) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});