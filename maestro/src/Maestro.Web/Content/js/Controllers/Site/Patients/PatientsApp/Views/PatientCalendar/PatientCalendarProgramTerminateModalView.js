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

        template: _.template($('#patientCalendarProgramTerminateModalTemplate').html()),

        modelBinder: new BackboneModelBinder(),

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));

            this.modelBinder.bind(this.model, this.el);

            this.$el.find('#termination-datetimepicker').datetimepicker({
                format: "YYYY-MM-DD",
                enabledDates: this.model.get('enabledDates')
            }).on('dp.change', function (e) {
                var terminationDate = $(this).find('#terminationDp').val();
                $(this).siblings('#terminationUtc').val(terminationDate).trigger('change');
            }).keydown(function (e) {
                e.preventDefault();
            });

            return this;
        }

    });
});