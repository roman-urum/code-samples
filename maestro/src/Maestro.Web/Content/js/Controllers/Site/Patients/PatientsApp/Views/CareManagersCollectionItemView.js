'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({
        className: 'list-group-item clearfix',

        template: _.template($('#careManagersListItemTemplate').html()),

        events: {
            'click .js-assign-care-manager': 'assignCareManager'
        },

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        assignCareManager: function () {

            app.collections.careManagerAssignedsCollection.add(this.model);
            app.collections.careManagersCollection.remove(this.model);

        }
    });
});