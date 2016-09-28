'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCareManagersAssignedCollectionItemView'
], function ($, _, Backbone, app, PatientCareManagersAssignedCollectionItemView) {
    return Backbone.View.extend({

        tagName: 'ul',

        className: 'list-unstyled',

        initialize: function () {
            this.listenTo(this.collection, 'change', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderCareManager, this);

            return this;
        },

        renderCareManager: function (patient) {
            app.views.patientCareManagersAssignedCollectionItemView = new PatientCareManagersAssignedCollectionItemView({ model: patient, vent: app.vent });
            this.$el.append(app.views.patientCareManagersAssignedCollectionItemView.render().el);

            return this;
        }

    });
});