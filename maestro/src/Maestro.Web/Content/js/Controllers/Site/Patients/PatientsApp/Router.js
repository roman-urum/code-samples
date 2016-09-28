'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCreateView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientsListView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditView',
    'Controllers/Site/Patients/PatientsApp/Views/PatientEditInfoView'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        PatientCreateView,
        PatientsListView,
        PatientModel,
        PatientView,
        PatientEditView,
        PatientEditInfoView
    ) {
    var Router = Backbone.Router.extend({

        initialize: function () {
            app.siteId = window.location.pathname.split('/')[1];
            app.managePermission = $('input#user-manage-permission').val() === 'True';
            Backbone.history.start({
                root: '/' + app.siteId + '/Patients/',
                pushState: true
            });
        },

        routes: {

            'PatientDetails/:patientId/': 'showPatientDetails',
            'PatientDetails/:patientId/:tab/': 'showPatientDetails',
            'EditPatient/:patientId/': 'showEditPatient',
            'EditPatient/:patientId/:tab/': 'showEditPatient',
            'CreatePatient/': 'showCreatePatient',
            'Index': 'showPatientsList',
            '': 'showPatientsList'
        },

        _getPatientModel: function (patientId, callback) {
            app.patientId = patientId;

            if (app.models.patientModel &&
                app.models.patientModel.get('id') === patientId) {

                callback();
            } else {
                Helpers.renderSpinner($("#patients-container"));

                app.models.patientModel = new PatientModel();
                app.models.patientModel.fetch({
                    success: function () {
                        callback();
                    }
                });
            }
        },

        _renderPatientView: function (patientId, tabName) {
            this._getPatientModel(patientId, function () {
                if (app.views.patientView)
                    app.views.patientView.undelegateEvents();
                app.views.patientView = new PatientView({ model: app.models.patientModel });
                app.views.patientView.render(tabName);
            });
        },

        _addSlashInTheEnd: function () {
            if (window.location.href.substr(window.location.href.length - 1) != '/') {
                window.history.replaceState({}, '', window.location.href + '/');
            }
        },

        _clearView: function (view) {
            if (view) {
                view.unbind();
                view.undelegateEvents();
            }
        },

        showPatientDetails: function (patientId, tabName) {
            tabName = tabName ? tabName : 'Dashboard';
            this._renderPatientView(patientId, tabName);
        },

        showEditPatient: function (patientId, tabName) {
            this._getPatientModel(patientId, function () {
                if (app.views.patientEditView)
                    app.views.patientEditView.undelegateEvents();
                app.views.patientEditView = new PatientEditView({ model: app.models.patientModel });
                app.views.patientEditView.render(tabName);
            });
        },

        showCreatePatient: function () {
            app.models.patientModel = new PatientModel();
            this._clearView(app.views.patientCreateView);
            app.views.patientCreateView = new PatientCreateView({ model: app.models.patientModel });
            app.views.patientCreateView.render();
        },

        showPatientsList: function () {
            this._addSlashInTheEnd();
            this._clearView(app.views.patientsListView);
            app.views.patientsListView = new PatientsListView();
            app.views.patientsListView.render();
        }
    });

    return Router;
});