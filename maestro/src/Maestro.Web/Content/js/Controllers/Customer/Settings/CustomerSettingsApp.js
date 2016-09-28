﻿'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'async',
    './CustomerSettingsApp/Models/GeneralSettingsModel',
    './CustomerSettingsApp/Models/SitesCollection',
    './CustomerSettingsApp/Models/OrgsCollection',
    './CustomerSettingsApp/Models/UsersCollection',
    './CustomerSettingsApp/Models/RolesCollection',
    './CustomerSettingsApp/Models/UsersFiltersModel',
    './CustomerSettingsApp/Models/ConditionsCollection',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Router',
    './CustomerSettingsApp/Views/MainView',
    'Controllers/SessionExpiration',
    'backbone.memento',
    'bootstrap',
    'bootstrap-switch',
    'session-timeout',
    'Backbone.Validation',
    'Controllers/Validation'
], function (
    $,
    _,
    Backbone,
    async,
    GeneralSettingsModel,
    SitesCollection,
    OrgsCollection,
    UsersCollection,
    RolesCollection,
    UsersFiltersModel,
    ConditionsCollection,
    App,
    Router,
    MainView,
    SessionExpiration
) {

    //debugger;
    //var validation = Validation;

    function initialize () {
        App.models.generalModel = new GeneralSettingsModel();
        App.models.generalModel.fetch();

        App.models.sitesCollection = new SitesCollection();
        App.models.orgsCollection = new OrgsCollection();
        App.models.usersCollection = new UsersCollection();
        App.models.rolesCollection = new RolesCollection();
        App.models.usersFiltersModel = new UsersFiltersModel();
        App.models.conditionsCollection = new ConditionsCollection();

        //TODO: handle server errors cases
        async.parallel([
            function (cb) {
                App.models.sitesCollection.fetch({
                    success: function () {
                        cb();
                    }
                });
            },
            function (cb) {
                App.models.orgsCollection.fetch({
                    success: function () {
                        cb();
                    }
                });
            },
            function (cb) {
                App.models.rolesCollection.fetch({
                    success: function () {
                        cb();
                    }
                });
            },
            function (cb) {
                App.models.conditionsCollection.fetch({
                    success: function () {
                        cb();
                    }
                });
            }
        ], function (err, results) {
            App.models.usersCollection.fetch();
            App.models.usersFiltersModel.refresh();
        });

        App.router = new Router();
        App.start();
        App.main.show(new MainView());

        Backbone.history.start({
            pushState: true
        });

        SessionExpiration.initialize();
    }

    return {
        initialize: initialize
    };
});