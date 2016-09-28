'use strict';

define([
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function(Backbone, app) {
    return Backbone.Model.extend({
        initialize: function() {
            this.detectColor();
        },

        detectColor: function() {
            if (!this.attributes.colorCode) {
                this.attributes.colorCode = app.OPTIONS.ALERT.NOT_EXISTING_COLOR;
            }

            return this;
        }
    });
});