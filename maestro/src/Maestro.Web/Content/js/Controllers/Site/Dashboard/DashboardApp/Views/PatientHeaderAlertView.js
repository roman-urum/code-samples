'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace'
], function($, _, Backbone, app) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'row alert-type',
        templates: {
            MEASUREMENT: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderTHRESHOLD'),
            BEHAVIOR: app.OPTIONS.TEMPLATE('patientCardViewTemplateHeaderBEHAVIOR')
        },

        initialize: function() {
            this.setId();
        },

        setId: function() {
            this.$el.attr('id', this.model.attributes.id);
        },

        render: function() {
            this.$el.html(this.templates[this.model.attributes.alertType.NAME](this.model.toJSON()));

            return this;
        }
    });
});