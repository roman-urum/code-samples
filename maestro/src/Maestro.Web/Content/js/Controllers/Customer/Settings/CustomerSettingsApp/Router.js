'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application'
], function (
    $,
    _,
    Backbone,
    Marionette,
    App
) {

    return Marionette.AppRouter.extend({
        initialize: function () {
        },

        routes: {
            '': 'redirectToGeneral',
            'Settings': 'redirectToGeneral',
            'Settings/:tabName': 'showMainMenuTab',
            'Settings/Users/Create': 'showCreateUserPage',
            'Settings/Users/:userId/:action': 'showEditUserPage',
            'Settings/Conditions/Create': 'showCreateConditionPage',
            'Settings/Conditions/:conditionId/:subTab': 'showEditConditionPage'
        },

        redirectToGeneral: function () {
            App.router.navigate('/Settings/General', { trigger: true, replace: true });
        },

        showMainMenuTab: function (tabName) {
            if (!App.main.currentView) throw 'Main View should be rendered';
            App.main.currentView.showTab(tabName);
        },

        showEditUserPage: function (userId, action) {
            if (!App.main.currentView) throw 'Main View should be rendered';
            App.main.currentView.showUserPage(userId, action);
        },

        showCreateUserPage: function () {
            if (!App.main.currentView) throw 'Main View should be rendered';
            App.main.currentView.showCreateUserForm();
        },

        showCreateConditionPage: function () {
            if (!App.main.currentView) throw 'Main View should be rendered';
            App.main.currentView.showCreateConditionPage();
        },

        showEditConditionPage: function (conditionId, subTab) {
            if (!App.main.currentView) throw 'Main View should be rendered';
            App.main.currentView.showEditConditionPage(conditionId, subTab);
        }
    });
});