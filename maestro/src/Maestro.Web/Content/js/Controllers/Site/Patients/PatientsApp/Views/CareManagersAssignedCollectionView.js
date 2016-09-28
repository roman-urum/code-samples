'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/CareManagersAssignedCollectionItemView'
], function ($, _, Backbone, app, CareManagersAssignedCollectionItemView) {
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
            app.views.careManagersAssignedCollectionItemView = new CareManagersAssignedCollectionItemView({ model: patient })
            this.$el.append(app.views.careManagersAssignedCollectionItemView.render().el);
            return this;
        }
    });
});