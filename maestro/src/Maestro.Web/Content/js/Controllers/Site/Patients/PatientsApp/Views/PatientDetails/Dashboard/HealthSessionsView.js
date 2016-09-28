'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapAlert',
    'highcharts',
    'async',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Models/PatientDetails/Dashboard/LastHealthSessionModel',
    './../../BaseMaestroView'
], function($,
            _,
            Backbone,
            BackboneBootstrapAlert,
            HighchartsLib,
            async,
            app,
            Helpers,
            moment,
            Constants,
            LastHealthSessionModel,
            BaseMaestroView) {
    return BaseMaestroView.extend({

        template: _.template($('#patientDashboardHealthSessionsViewTemplate').html()),
        isSending: false,

        events: {
            'click .js-check-connection': 'checkConnection'
        },

        initialize: function(options) {
            if (!options) {
                throw 'Options required';
            }

            var self = this;
            this.model = options.model;
        },

        render: function() {
            var data = _.extend({}, this.calculateTemplateData());
            this.$el.html(this.template(data));
            return this;
        },

        calculateTemplateData: function() {
            var nextSessionDate = this.model.get('nextHealthSessionDate');
            var nextSessionText = nextSessionDate ? moment(nextSessionDate).format('M/D/YYYY hh:mm A') : '';
            var currentPrograms = this.model.get('activePrograms') || [];

            var patientModel = app.models.patientModel;
            var currentTimeForPatient = '';
            var preferredSessionTime = '';

            if (patientModel) {
                currentTimeForPatient = moment(moment.utc()).tz(patientModel.get('timeZone')).format('hh:mm A (Z)');
                preferredSessionTime = patientModel.get('preferredSessionTime');
            }

            var lastConnectedDate = this.model.get('lastConnectedDate');
            var lastConnectedDateText = lastConnectedDate ? moment(lastConnectedDate).format('M/D/YYYY hh:mm A') : '';

            return {
                id: app.models.patientModel.get('id'),
                siteId: app.models.patientModel.get('siteId'),
                isModelFetched: this.model.isFetched,
                healthSessionsMissedCount: this.model.get('healthSessionsMissedCount'),
                nextSessionText: nextSessionText,
                currentPrograms: currentPrograms,
                currentTimeForPatient: currentTimeForPatient,
                preferredSessionTime: preferredSessionTime,
                lastConnectedDeviceType: this.model.get('lastConnectedDeviceType'),
                lastConnectedDateText: lastConnectedDateText
            }
        },

        checkConnection: function() {
            var url = '/' + app.siteId + '/Patients/CheckConnection?patientId=' + app.patientId;

            if (!this.isSending) {
                this.isSending = true;
                $.ajax({
                    url: url,
                    method: 'POST',
                    success: function() {
                        (new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Check Connection Request Sent',
                            autoClose: true
                        }))
                            .show();
                    }
                }).always(function() {
                    this.isSending = false;
                }.bind(this));
            }


        }

    });
});