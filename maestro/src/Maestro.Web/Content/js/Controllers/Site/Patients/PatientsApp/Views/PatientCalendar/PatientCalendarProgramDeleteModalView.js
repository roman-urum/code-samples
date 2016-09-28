'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'BackboneModelBinder'
], function ($, _, Backbone, app, Helpers, BackboneModelBinder) {
    return Backbone.View.extend({

        template: _.template($('#patientCalendarProgramDeleteModalTemplate').html()),

        modelBinder: new BackboneModelBinder(),

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template());

            return this;
        }

    });
});