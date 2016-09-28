'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        tagName: 'option',

        template: _.template($('#devicesSelectListItemTemplate').html()),

        events: {

        },

        initialize: function () {

        },

        render: function () {
            var self = this;
            this.$el.html(this.template(this.model.attributes));
            this.$el.attr('value', this.model.attributes.id);
            this.$el.attr('data-icon', 'glyphicon-device-status-' + this.model.attributes.status + '-ci');

            return this;
        }

    });

});