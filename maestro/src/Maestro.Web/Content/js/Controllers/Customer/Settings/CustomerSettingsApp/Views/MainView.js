'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    '../Models/GeneralSettingsModel',
    '../Models/OrgsCollection',
    '../Models/SitesCollection',
    '../Models/UsersCollection',
    '../Models/ConditionsCollection',
    '../Models/ThresholdsStubModel',
    './BaseLayoutView',
    './GeneralSettingsView',
    './OrgsView',
    './UsersView',
    './ThresholdsView',
    './ConditionsView'
], function (
    $,
    _,
    Marionette,
    App,
    GeneralSettingsModel,
    OrgsCollection,
    SitesCollection,
    UsersCollection,
    ConditionsCollection,
    ThresholdsStubModel,
    BaseLayoutView,
    GeneralSettingsView,
    OrgsView,
    UsersView,
    ThresholdsView,
    ConditionsView
) {
    return BaseLayoutView.extend({
        template: '#mainView',

        className: 'main-view',

        regions: {
            general: '#content-general',
            sites: '#content-sites',
            users: '#content-users',
            thresholds: '#content-thresholds',
            conditions: '#content-conditions'
        },

        events: {
        },

        //switch to needed tab
        showTab: function (tabName) {
            tabName = tabName.toLowerCase();

            //render view
            var region = this.getRegion(tabName);
            if (region.currentView) {
                region.currentView.render();
            } else {
                this.addTabContent(tabName);
            }

            //show bootstrap tab
            this.$('.main-settings-sections #tab-' + tabName).tab('show');
        },

        //show needed view with needed model
        addTabContent: function (tabName) {
            var self = this;
            var ViewClass, ModelClass, modelName;

            switch (tabName) {
                case 'general':
                    ViewClass = GeneralSettingsView;
                    ModelClass = GeneralSettingsModel;
                    modelName = 'generalModel';
                    break;
                case 'sites':
                    ViewClass = OrgsView;
                    ModelClass = OrgsCollection;
                    modelName = 'orgsCollection';
                    break;
                case 'users':
                    ViewClass = UsersView;
                    ModelClass = UsersCollection;
                    modelName = 'usersCollection';
                    break;
                case 'thresholds':
                    ViewClass = ThresholdsView;
                    ModelClass = ThresholdsStubModel;
                    modelName = 'thresholdsStubModel';
                    break;
                case 'conditions':
                    ViewClass = ConditionsView;
                    ModelClass = ConditionsCollection;
                    modelName = 'conditionsCollection';
                    break;
            }

            if (!ViewClass) return;

            this.getModel(ModelClass, modelName, function (model) {
                //show view in marionette way
                self.showChildView(tabName, new ViewClass({ model: model }));
            });
        },

        //return needed model (existing or new one)
        getModel: function (ModelClass, modelName, cb) {
            var model = App.models[modelName];

            if (!model) {
                model = new ModelClass();
                model.fetch();
                App.models[modelName] = model;
            }

            cb(model);
        },

        showUserPage: function (userId, action) {
            this.showTab('Users');
            this.getRegion('users').currentView.showUserPage(userId, action);
        },

        showCreateUserForm: function () {
            this.showTab('Users');
            this.getRegion('users').currentView.showCreateUserForm();
        },

        showEditConditionPage: function (conditionId, subTab) {
            this.showTab('Conditions');
            this.getRegion('conditions').currentView.showEditConditionPage(conditionId, subTab);
        },

        showCreateConditionPage: function () {
            this.showTab('Conditions');
            this.getRegion('conditions').currentView.showCreateConditionPage();
        }
    });
});