'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({
        className: 'b-measurements-devices',

        template: _.template($('#deviceSingleItemTemplate').html()),

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});