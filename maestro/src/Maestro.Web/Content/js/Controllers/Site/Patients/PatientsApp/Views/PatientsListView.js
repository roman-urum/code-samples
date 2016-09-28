'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientsCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientsCollectionView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientSearchBoxView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientModel'
], function ($, _, Backbone, app, Helpers, PatientsCollection, PatientsCollectionView, PatientSearchBoxView, PatientModel) {
    return Backbone.View.extend({
        el: '#patients-container',
        template: _.template($('#patientsListTemplate').html()),

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template());

            if (app.views.patientSearchBoxView) {
                app.views.patientSearchBoxAdvancedView.undelegateEvents();
                app.views.patientSearchBoxAdvancedView.remove();
                app.views.patientSearchBoxView.undelegateEvents();
                app.views.patientSearchBoxView.remove();
            }

            app.views.patientSearchBoxView = new PatientSearchBoxView();

            this.$el.find('#patient-search-box').html(app.views.patientSearchBoxView.render().el);

            if (app.collections.patientsCollection && app.views.patientsCollectionView) {
                app.collections.patientsCollection.reset();
                app.views.patientsCollectionView.undelegateEvents();
                app.views.patientsCollectionView.remove();
            }

            app.collections.patientsCollection = new PatientsCollection();
            app.views.patientsCollectionView = new PatientsCollectionView({ collection: app.collections.patientsCollection });
            this.$el.find('#patients-list-container').html(app.views.patientsCollectionView.render().el);

            return this;
        }
    });
});