'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Views/PatientIdentifiersCollectionItemView'
], function ($, _, Backbone, app, Helpers, PatientIdentifiersCollectionItemView) {
    return Backbone.View.extend({

        className: 'row',

        events: {

        },

        initialize: function () {

        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderIdentifier, this);
            return this;
        },

        renderIdentifier: function (identifier) {
            app.views.patientIdentifiersCollectionItemView = new PatientIdentifiersCollectionItemView({ model: identifier });
            this.$el.append(app.views.patientIdentifiersCollectionItemView.render().el);

            return this;
        }
    });
});