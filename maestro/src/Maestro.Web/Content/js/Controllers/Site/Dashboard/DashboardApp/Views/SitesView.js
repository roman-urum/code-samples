'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/GeneralSettingsModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/OrgsCollection',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/SitesCollection',
    './OrgTreeView'
], function($, _, Backbone, app, GeneralSettingsModel, OrgsCollection, SitesCollection, OrgTreeView) {
    return Backbone.View.extend({

        template: app.OPTIONS.TEMPLATE('sitesView'),
        spinnerTemplate: '<div class="spinner-box"><div class="spinner-container"><img src="/Content/img/spinner.gif" class="spinner"></div></div>',

        visibleClass: 'visible',

        sitesListContainer: '.sites-list-container',

        events: {
        },

        initialize: function() {
            var self = this;

            this.generalSettings = new GeneralSettingsModel();
            this.generalSettings.fetch({
                data: {
                    isBrief: false
                },
                success: function () {
                    self.sitesCollection = new SitesCollection(self.generalSettings.get('sites'));
                    self.orgsCollection = new OrgsCollection(self.generalSettings.get('organizations'));
                    self.render();
                },
                error: function() { self.render(); }
            });
        },

        onBeforeRender: function () {
            var collection;

            if (this.isDataFetched()) {
                collection = this.orgsCollection.deepCloneAsTree({
                    sitesCollection: this.sitesCollection
                });
            }

            this.collection = collection || new Backbone.Collection();
        },

        render: function () {
            this.onBeforeRender();
            if (this.isDataFetched()) {
                this.$el.html(this.template());
                this.renderSitesList();
            } else {
                //Workaround with local spinner here
                //because fetching orgs & sites can take quite long
                this.renderSpinner();
            }
            return this;
        },

        renderSitesList: function () {
            var rootName = this.generalSettings.isFetched ? this.generalSettings.get('customerName') : '';
            var sitesListModel = new Backbone.Model({
                name: rootName,
                children: this.collection
            });
            var sitesListView = new OrgTreeView({model: sitesListModel});
            this.$(this.sitesListContainer).html(sitesListView.render().el);
        },

        renderSpinner: function () {
            var template = _.template(this.spinnerTemplate);
            this.$el.html(template());
        },

        isDataFetched: function () {
            return this.generalSettings.isFetched;
        }

    });
});