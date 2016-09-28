'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function ($, _, Backbone, app, helpers) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'patient-search-details',
        template: _.template($('#patientsListItemDetailsTemplate').html()),

        initialize: function () {
            this.model.fetch();

            this.listenTo(this.model, 'sync', this.renderContent);
        },

        renderContent: function () {
            this.$el.html(this.template(this.model.attributes));
        },

        render: function () {
            helpers.renderSpinner(this.$el);

            return this;
        }
    });
});