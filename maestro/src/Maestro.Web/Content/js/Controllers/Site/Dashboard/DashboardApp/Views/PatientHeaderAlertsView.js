'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientHeaderAlertView'
], function($, _, Backbone, app, PatientHeaderAlertView) {
    return Backbone.View.extend({
        tagName: 'div',

        render: function() {
            this.collection.each(function(alert) {
                this.$el.append((new PatientHeaderAlertView({model: alert})).render().el);
            }, this);

            return this;
        }
    });
});