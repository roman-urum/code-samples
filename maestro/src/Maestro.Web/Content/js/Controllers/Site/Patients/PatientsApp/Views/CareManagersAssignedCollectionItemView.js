'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({
        className: 'list-group-item clearfix',

        template: _.template($('#careManagersAssignedListItemTemplate').html()),

        events: {
            'click .js-remove-care-manager': 'removeCareManager'
        },

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        removeCareManager: function () {
            app.collections.careManagerAssignedsCollection.remove(this.model);
            app.collections.careManagersCollection.add(this.model);
        }
    });
});