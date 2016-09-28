'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'highcharts',
    'async',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/LastHealthSessionModel',
    './../../BaseMaestroView'
], function (
    $,
    _,
    Backbone,
    HighchartsLib,
    async,
    app,
    Helpers,
    LastHealthSessionModel,
    BaseMaestroView
) {
    return BaseMaestroView.extend({

        template: _.template($('#patientDashboardThresholdsViewTemplate').html()),

        events: {
        },

        initialize: function (options) {
            if (!options) throw 'Options required';

            var self = this;
            this.model = options.model;
        },

        render: function () {


            var data = _.extend({
                id: app.models.patientModel.get('id'),
                siteId: app.models.patientModel.get('siteId'),
                isModelFetched: this.model.isFetched,
                thresholds: []
            }, this.model.toJSON());

            this.$el.html(this.template(data));
            return this;
        },

        remove: function () {

        }

    });
});