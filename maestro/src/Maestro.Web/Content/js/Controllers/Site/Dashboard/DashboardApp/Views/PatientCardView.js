'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientHeaderAlertsView',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardDetailsView',
    'Controllers/Site/Dashboard/DashboardApp/Collections/PatientHeaderAlertCollection',
    'Controllers/Site/Dashboard/DashboardApp/Collections/PatientDetailsAlertCollection',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientHeaderView',
    'Controllers/Site/Dashboard/DashboardApp/Collections/AlertHeaderCollection'
], function($, _, Backbone, app,
            PatientHeaderAlertsView,
            PatientCardDetailsView,
            PatientHeaderAlertCollection,
            PatientDetailsAlertCollection,
            PatientHeaderView,
            AlertHeaderCollection) {
    return Backbone.View.extend({
        tagName: 'div',
        visibleClass: 'panel-visible',
        className: 'panel panel-default',
        template: app.OPTIONS.TEMPLATE('patientCardViewTemplate'),
        $patientLink: $(),
        hasDetails: false,
        isLoading: false,
        hasSeverities: false,
        careManagerId: null,

        events: {
            'click.toggle-panel .panel-heading': 'togglePanel',
            'click.patient-page .panel-heading a.show-patient-page': 'redirectToPatientPage'
        },

        initialize: function (options) {
            this.careManagerId = options.careManagerId;
            this.bind();
            this.setId();
            this.listen();

            this.model.attributes.url = app.OPTIONS.URL.LINK;
            this.createCollection();

            this.hasSeverities = !!this.model.attributes.severityOfLatestAlert;
        },

        bind: function() {
            this.removeAlert = this.removeAlert.bind(this);
            this.renderDetails = this.renderDetails.bind(this);
            this.associateDetails = this.associateDetails.bind(this);
        },

        setId: function() {
            this.$el.attr('id', this.model.attributes.patientInfo.id);
        },

        listen: function() {
            this.listenTo(this.model, 'remove-alert-down', this.removeAlert);
        },

        createCollection: function() {
            this.alertHeaderCollection = new AlertHeaderCollection(this.model.attributes.counts);
            this.patientHeaderView = new PatientHeaderView({
                collection: this.alertHeaderCollection
            });
        },

        removeAlert: function(deletedItem, model, cb) {
            this.alertHeaderCollection.trigger('remove-header-alert', deletedItem);
            this.patientDetailsAlertCollection.trigger('remove-detailed-alert', deletedItem);

            var newColor = 'transparent',
                newLatestItem = this.patientDetailsAlertCollection.models[0];

            if (newLatestItem && newLatestItem.attributes.noColor) {
                newColor = newLatestItem.attributes.color;
            }

            if(this.hasSeverities) {
                this.$el.find('.panel-heading > .color-line').css({background: newColor});
            }

            cb(!!this.patientDetailsAlertCollection.models.length);
        },

        togglePanel: function() {
            this.addDetails();
            this.$el.find('.panel-collapse').collapse('toggle');
        },

        redirectToPatientPage: function(e) {
            e.preventDefault();
            e.stopPropagation();

            window.location.href = this.$patientLink.attr('href');
        },

        addDetails: function() {
            if (!this.hasDetails && !this.isLoading) {
                this.isLoading = true;

                app.patientStorage.getPatientDetails(
                    this.model.attributes.patientInfo.id,
                    app.app.views.alertTypes.getUnchecked(),
                    app.app.views.alertSeverities.getUnchecked(),
                    this.careManagerId
                )
                    .then(this.associateDetails)
                    .then(this.renderDetails)
                    .fail(app.errorHandler);
            }
        },

        associateDetails: function(details) {
            _.each(details, function(detail) {
                if (detail.name !== 'BloodPressure') {
                    detail.reading.alert.type = app.OPTIONS.ALERT.GET_NAME_BY_CODE(detail.reading.alert.type);

                } else {
                    detail.reading.alert.type = app.OPTIONS.ALERT.GET_NAME_BY_CODE(detail.reading.alert.type);
                    if (detail.reading.measurement.vitals.diastolicBloodPressure.vitalAlert) {
                        detail.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.type =
                            app.OPTIONS.ALERT.GET_NAME_BY_CODE(detail.reading.measurement.vitals.diastolicBloodPressure.vitalAlert.type);
                    }
                    if (detail.reading.measurement.vitals.systolicBloodPressure.vitalAlert) {
                        detail.reading.measurement.vitals.systolicBloodPressure.vitalAlert.type =
                            app.OPTIONS.ALERT.GET_NAME_BY_CODE(detail.reading.measurement.vitals.systolicBloodPressure.vitalAlert.type);
                    }
                }
                detail.patientInfo = this.model.attributes.patientInfo;
                detail.cardCollection = this.model.collection;
            }, this);

            this.hasDetails = true;

            return details;
        },

        renderDetails: function (details) {

            this.patientDetailsAlertCollection = new PatientDetailsAlertCollection(details);
            this.patientCardDetailsView = new PatientCardDetailsView({
                collection: this.patientDetailsAlertCollection,
                model: this.model
            });

            this.$el.find('.panel-body').html(this.patientCardDetailsView.render().el);
        },

        render: function() {
            this.$el
                .html(this.template(this.model.attributes))
                .find('.card-header')
                .html(this.patientHeaderView.render().el);

            this.$patientLink = this.$el.find('a.show-patient-page');

            setTimeout(function() {
                this.$el.addClass(this.visibleClass);
            }.bind(this), 0);

            return this;
        }
    });
});