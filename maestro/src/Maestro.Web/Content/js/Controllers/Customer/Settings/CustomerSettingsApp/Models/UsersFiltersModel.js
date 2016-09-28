'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application'
], function (
    $,
    _,
    Backbone,
    BaseModel,
    App
) {
    return BaseModel.extend({

        defaults: {
            sites: [],
            roles: [],
            searchStr: null,
            sortingCase: null,
            filterByRole: null,
            filterByStatus: null,
            filterBySite: null
        },

        refresh: function () {
            this.set({
                sites: App.models.sitesCollection.toJSON(),
                roles: App.models.rolesCollection.toJSON()
            });
            this.trigger('fetched');
        }

    });
});