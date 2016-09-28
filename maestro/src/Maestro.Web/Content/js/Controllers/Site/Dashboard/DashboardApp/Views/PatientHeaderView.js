'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientHeaderItemView',
    'Controllers/Site/Dashboard/DashboardApp/Collections/AlertHeaderItemCollection'
], function($, _, Backbone, app, PatientHeaderItemView, AlertHeaderItemCollection) {
    return Backbone.View.extend({
        tagName: 'div',
        template: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeader'),
        defaultSeverity: {
            Count: 0,
            Id: app.OPTIONS.ALERT.NOT_EXISTING_ID,
            Name: 'Without Severity',
            ColorCode: '#000',
            Severity: 0
        },

        initialize: function() {
            this.collection.each(function(item) {
                item.attributes.alertType = app.OPTIONS.ALERT.GET_NAME_BY_CODE(item.attributes.alertType).NAME;
            }, this);

            this.listen();
        },

        listen: function() {
            this.listenTo(this.collection, 'remove-header-alert', this.removeAlert);
        },

        removeAlert: function(deletedItem) {
            var $alertValue = this.$el
                    .find('.alert-count-item[data-type="' + app.OPTIONS.ALERT.GET_NAME_BY_CODE(deletedItem.alertType).NAME + '"][data-id="' + deletedItem.severityId + '"] .alert-data-value i'),
                text = $alertValue.text(),
                currentText = text -= 1;

            if (!isNaN(text)) {
                $alertValue.text(currentText > 0 ? currentText : '-');
            }
        },

        render: function() {
            this.collection.each(function(item) {
                if (!item.attributes.alertSeverityCounts.length) {
                    var alertSeverityCount = $.extend({}, this.defaultSeverity);
                    alertSeverityCount.count = item.attributes.alertTypeCount;

                    item.attributes.alertSeverityCounts = [alertSeverityCount];
                }

                var alertHeaderItemCollection = new AlertHeaderItemCollection();
                _.each(item.attributes.alertSeverityCounts, function(item) {
                    alertHeaderItemCollection.add(item);
                });

                var patientHeaderItemView = new PatientHeaderItemView({
                    model: {
                        alertType: item.attributes.alertType
                    },
                    collection: alertHeaderItemCollection
                });

                var $severityPoints = $(this.template(item.attributes));
                $severityPoints.find('.severity-points').html(patientHeaderItemView.render().el);

                this.$el.append($severityPoints);
            }, this);

            return this;
        }
    });
});