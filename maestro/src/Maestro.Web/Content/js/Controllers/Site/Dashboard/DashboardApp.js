'use strict';

define([
    'Controllers/Site/Dashboard/DashboardApp/Views/PatientDashboardView',
    'Controllers/Site/Dashboard/DashboardApp/Views/AlertTypesView',
    'Controllers/Site/Dashboard/DashboardApp/Views/AlertSeveritiesView',
    'Controllers/Site/Dashboard/DashboardApp/Views/SitesView',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Constants',
    'bootstrap',
    'session-timeout',
    'backbone.memento'

], function (PatientDashboard, AlertTypesView, AlertSeveritiesView, SitesView, app, Constants) {

    var initialize = function () {

        app.siteId = window.location.pathname.split('/')[1];
        app.app.views.patientDashboard = new PatientDashboard();
        app.app.views.alertSeverities = new AlertSeveritiesView();
        app.app.views.alertTypes = new AlertTypesView();
        app.app.views.sites = new SitesView();
        app.app.dashboardFilter = {
            types: [],
            severities: []
        }

        $('#severity-filter')
            .html(app.app.views.alertSeverities.render().el);

        $('#alert-type-filter')
            .html(app.app.views.alertTypes.render().el);


        $('#patient-list-container')
            .html(app.app.views.patientDashboard.render(Constants.currentCareManager.userId).el);

        $('#sites-list-body-content')
            .html(app.app.views.sites.render().el);
    };

    return {
        initialize: initialize
    };
});