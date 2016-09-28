'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function($, _, Backbone, app) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'patient-ignore-reading-modal',
        template: app.OPTIONS.TEMPLATE('patientCardViewIgnoreReadingModal'),

        render: function() {
            this.$el.html(this.template());

            return this;
        }
    });
});

