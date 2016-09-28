'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Views/ConditionsAssignedCollectionItemView'
], function ($, _, Backbone, ConditionsAssignedCollectionItemView) {
    return Backbone.View.extend({
        className: 'list-group',

        initialize: function () {
            this.listenTo(this.collection, 'add', this.render);
            this.listenTo(this.collection, 'remove', this.render);
            this.listenTo(this.collection, 'reset', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderCareManager, this);

            return this;
        },

        renderCareManager: function (patient) {
            var view = new ConditionsAssignedCollectionItemView({ model: patient });
            this.$el.append(view.render().el);

            return this;
        }
    });
});