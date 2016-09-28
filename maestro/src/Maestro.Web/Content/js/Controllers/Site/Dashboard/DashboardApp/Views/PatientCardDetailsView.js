'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardDetailView',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientCardBloodPressureDetailView'
], function($, _, Backbone, app, PatientCardDetailView, PatientCardBloodPressureDetailView) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'patient-alerts-details',
        template: app.OPTIONS.TEMPLATE('patientCardViewTemplateDetailFooter'),
        isAcknowledging: false,

        events: {
            'click .acknowledge-all': 'removeAllAlerts'
        },

        initialize: function() {
            this.bind();
            this.listen();

            this.detectSiblings();
        },

        listen: function() {
            this.listenTo(this.collection, 'remove-detailed-alert', this.removeAlert);
        },

        bind: function() {
            this.removeAlert = this.removeAlert.bind(this);
            this.renderDetail = this.renderDetail.bind(this);
        },

        removeAllAlerts: function(e) {
            e.preventDefault();

            if (this.isAcknowledging) {
                return;
            }

            this.isAcknowledging = true;

            this.model.collection.trigger('remove-alert-up', app.OPTIONS.ALERT.ACTIONS.ACKNOWLEDGEAll, {
                model: this.model,
                collection: this.collection.models
            }, this.removeAlert.bind(this, function() {
                this.isAcknowledging = false;
            }.bind(this)));
        },

        detectRemovableAlertId: function(item) {
            return item.attributes.reading.measurement && !$.isArray(item.attributes.reading.measurement.vitals) ?
                item.attributes.reading.measurement.vitals.systolicBloodPressure.id :
                item.attributes.reading.alert.id;
        },

        removeAlert: function(deletedItem) {
            var item = this.collection.find(function(item) {
                return item.attributes.id === deletedItem.alertId || (item.attributes.reading.measurement && !$.isArray(item.attributes.reading.measurement.vitals) ? item.attributes.reading.measurement.vitals.systolicBloodPressure.id === deletedItem.alertId : false);
            });

            if (item) {
                this.collection.remove(item);
                this.$el.find('.patient-alert-detail[id=' + this.detectRemovableAlertId(item) + ']').remove();

                if (this.collection.length === 1) {
                    this.collection.at(0).attributes.siblings = false;
                    this.collection.at(0).trigger('watch-patient');
                }
                this.detectSiblings();
                this.renderFooter();
            }
        },

        detectSiblings: function() {
            this.collection.map(function(detail) {
                detail.attributes.siblings = !!(this.collection.length - 1);

                return detail;
            }, this);
        },

        renderDetail: function(detail) {
            detail.collection = this.model.collection;

            if (detail.get('name') === 'BloodPressure') {
                this.$el.append((new PatientCardBloodPressureDetailView({model: detail})).render().el);
            }
            else {
                this.$el.append((new PatientCardDetailView({model: detail})).render().el);
            }

            setTimeout(function() {
                this.$el.addClass('alerts-visible');
            }.bind(this), 50);
        },

        renderFooter: function() {
            var $footer = this.$el.find('.patient-alert-footer');

            if (this.collection.length > 1 && !$footer.size()) {
                this.$el.append(this.template({
                    link: app.OPTIONS.URL.LINK,
                    patientId: this.collection.at(0).attributes.patientInfo.id
                }));
            }

            if (this.collection.length <= 1) {
                this.$el.find('.patient-alert-footer').remove();
            }
        },

        render: function() {
            this.$el.empty();

            _.each(this.collection.models, this.renderDetail);

            this.renderFooter();

            return this;
        }
    });
});

