'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardView'
], function($, _, Backbone, app, PatientCardView) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'panel-group',
        $window: $(window),
        $document: $(document),
        isLoading: false,
        noCards: app.OPTIONS.TEMPLATE('patientCardViewTemplateNoCards'),
        careManagerId: null,

        initialize: function (options) {

            this.careManagerId = options.careManagerId;

            this.bind();
            this.listen();
        },

        bind: function() {
            this.loadMore = this.loadMore.bind(this);
            this.addPatients = this.addPatients.bind(this);
            this.afterAdding = this.afterAdding.bind(this);
            this.removeAlert = this.removeAlert.bind(this);
            this.renderPatient = this.renderPatient.bind(this);
        },

        listen: function() {
            this.$window.on('scroll.loadMoreCards', this.loadMore);

            this.listenTo(this.collection, 'add', this.renderPatient);
            this.listenTo(this.collection, 'remove-alert-up', this.removeAlert);
        },

        detectIds: function(result, model) {
            if (model.attributes.name === 'BloodPressure') {
                (function() {
                    var alert = model.attributes.reading.measurement.vitals.diastolicBloodPressure.vitalAlert;

                    if (alert) {
                        result.push(alert.id);
                    }
                }());
                (function() {
                    var alert = model.attributes.reading.measurement.vitals.systolicBloodPressure.vitalAlert;

                    if (alert) {
                        result.push(alert.id);
                    }
                }());
            } else {
                result.push(model.attributes.reading.alert.id);
            }
        },

        mapAlertIdsFromModel: function(model) {
            var result = [];

            this.detectIds.bind(this, result)(model);

            return result;
        },

        mapAlertIdsFromCollection: function(collection) {
            var result = [];

            _.each(collection, this.detectIds.bind(this, result));

            return result;
        },

        getMeasurementIdFromModel: function(model) {
            return model.attributes.reading.measurement.id;
        },

        removeAlert: function(type, model, success) {
            switch (type) {
                case app.OPTIONS.ALERT.ACTIONS.ACKNOWLEDGEAll:
                {
                    app.patientStorage
                        .acknowledgeAlerts(model.model.attributes.patientInfo.id, this.mapAlertIdsFromCollection(model.collection))
                        .then(this.afterRemove.bind(this, model.model, success))
                        .fail(app.errorHandler);

                    break;
                }
                case app.OPTIONS.ALERT.ACTIONS.ACKNOWLEDGE:
                {
                    app.patientStorage
                        .acknowledgeAlerts(model.attributes.patientInfo.id, this.mapAlertIdsFromModel(model))
                        .then(this.afterRemove.bind(this, model, success))
                        .fail(app.errorHandler);

                    break;
                }
                case app.OPTIONS.ALERT.ACTIONS.IGNORE:
                {
                    app.patientStorage
                        .ignoreReading(this.getMeasurementIdFromModel(model), model.attributes.patientInfo.id, this.mapAlertIdsFromModel(model))
                        .then(this.afterRemove.bind(this, model, success))
                        .fail(app.errorHandler);

                    break;
                }
            }
        },

        afterRemove: function(model, success, deletedItems) {
            var item = this.collection.find(function(item) {
                return item.attributes.patientInfo.id === model.attributes.patientInfo.id;
            });

            if (this.collection.length && item) {
                _.each(deletedItems, function(deletedItem) {
                    item.trigger('remove-alert-down', deletedItem, model, function(hasMore) {
                        if (!hasMore) {
                            this.collection.remove(item);
                            this.$el.find('.panel.panel-default[id="' + deletedItem.patientId + '"]').off().remove();
                        }
                    }.bind(this));

                    success(deletedItem);
                }, this);
            }

            if (!this.collection.length) {
                $('.patient-list').html(this.noCards());
            }
        },

        shouldLoad: function() {
            return this.$window.height() + this.$window.scrollTop() >= this.$document.innerHeight() / 2;
        },

        renderPatient: function(patient) {
            this.$el.append((new PatientCardView({model: patient, careManagerId: this.careManagerId})).render().el);
        },

        addPatients: function(patients) {
            this.collection.add(patients);
        },

        afterAdding: function() {
            this.isLoading = false;

            this.loadMore();
        },

        loadMore: function() {
            if (!app.patientStorage.hasMore()) {
                this.$el.next('.spinner').remove();
                this.$window.off('scroll.loadMoreCards');
            }

            if (!(this.shouldLoad() && !this.isLoading) || !app.patientStorage.hasMore()) {
                return;
            }

            this.isLoading = true;
            app.patientStorage
                .getPatients(app.OPTIONS.ALERT.CURRENT_COUNT(), app.app.dashboardFilter.types, app.app.dashboardFilter.severities, this.careManagerId)
                .then(this.addPatients)
                .fail(app.errorHandler)
                .always(this.afterAdding);
        },

        render: function() {
            this.collection.each(this.renderPatient);

            return this;
        }
    });
});

