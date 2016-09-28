'use strict';

define([
    'backbone',
    'moment',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Helpers'
], function(Backbone, Moment, app, Helpers) {
    return Backbone.Model.extend({
        initialize: function() {
            this.detectColor();
        },

        detectColor: function() {
            this.attributes.severityOfLatestAlert = Helpers.convertKeysToCamelCase(this.attributes.severityOfLatestAlert);
            this.attributes.cardColor = this.attributes.severityOfLatestAlert ?
                this.attributes.severityOfLatestAlert.colorCode :
                app.OPTIONS.ALERT.DEFAULT_COLOR;

            return this;
        }
    });
});