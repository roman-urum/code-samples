'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({
        tagName: 'table',

        className: 'table table-striped',

        template: _.template($('#patientMeasurementTemplate').html()),

        events: {

        },

        initialize: function () {
            app.vent.bind("saveMeasurementCollection", this.saveMeasurementCollection);
        },

        render: function () {
            var self = this;

            this.model.set('peripherals',{});

            this.$el.html(this.template( this.model.attributes ));
            this.$el.find('.btn-group-peripherals-ci .btn').addClass('disabled');

            return this;
        },

        saveMeasurementCollection: function () {
            app.vent.trigger('measurementSavedSuccess');
        }
    });
});