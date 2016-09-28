'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionDetailsView',
    'BackboneBootstrapModal'
], function ($, _, Backbone, app, ConditionDetailsView, BackboneBootstrapModal) {
    return Backbone.View.extend({
        className: 'list-group-item clearfix',

        template: _.template($('#conditionsListItemTemplate').html()),

        events: {
            'click .js-assign-condition': 'assignCondition',
            'click .js-remove-condition': 'removeCondition',
            'click .condition-more': 'showConditionDetails'
        },

        initialize: function () {
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        },

        assignCondition: function () {
            app.vent.trigger('condition:assign', this.model);
        },

        removeCondition: function () {
            app.vent.trigger('condition:remove', this.model);
        },

        showConditionDetails: function (e) {
            e.preventDefault();

            var view = new ConditionDetailsView({model: this.model});

            this.detailsModal = new BackboneBootstrapModal({
                template: _.template($('#modalLgTemplate').html()),
                content: view,
                okText: 'Cancel',
                okCloses: true,
                cancelText: false,
                allowCancel: true,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });
            this.detailsModal.open();

        }
    });
});