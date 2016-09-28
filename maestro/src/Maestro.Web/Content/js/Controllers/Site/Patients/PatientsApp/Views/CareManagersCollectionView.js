'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/CareManagersCollectionItemView'
], function ($, _, Backbone, app, CareManagersCollectionItemView) {
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
            app.vent.trigger("patientEditCareManagersChanged");

            return this;
        },

        renderCareManager: function (patient) {
            app.views.careManagersCollectionItemView = new CareManagersCollectionItemView({ model: patient });
            this.$el.append(app.views.careManagersCollectionItemView.render().el);

            return this;
        }

    });
});

