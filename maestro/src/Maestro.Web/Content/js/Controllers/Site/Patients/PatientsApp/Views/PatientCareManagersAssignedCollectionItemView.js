'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({

        tagName: 'li',

        template: _.template($('#patientCareManagersAssignedListItemTemplate').html()),

        events: {
            'click .js-remove-care-manager': 'removeCareManager'
        },

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        removeCareManager: function (e) {

            console.log('removeCareManager');

            app.collections.careManagerAssignedsCollection.store();
            app.collections.careManagerAssignedsCollection.remove(this.model);
            app.collections.careManagersCollection.add(this.model);

            $(e.currentTarget)
                .addClass('spin')
                .removeClass('js-remove-care-manager')
                .html('<span class="glyphicon glyphicon-refresh" />');

            app.vent.trigger("saveCareManagers", this.model);
        }
    });
});