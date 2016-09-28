'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone.marionette',
    'BackboneBootstrapModal',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    '../Models/OrgModel',
    '../Models/SiteModel',
    './BaseLayoutView',
    './OrgsTreeView'
], function (
    $,
    _,
    Backbone,
    Marionette,
    BackboneBootstrapModal,
    App,
    OrgModel,
    SiteModel,
    BaseLayoutView,
    OrgsTreeView
) {
    return BaseLayoutView.extend({
        template: '#orgsView',

        className: 'orgs-view use-spinner',

        regions: {
            list: '.orgs-list'
        },

        initialize: function () {
            BaseLayoutView.prototype.initialize.apply(this, arguments);
            var self = this;

            this.isOrgsFetched = App.models.orgsCollection.isFetched;
            this.isSitesFetched = App.models.sitesCollection.isFetched;

            this.listenTo(App.models.orgsCollection, 'fetched', function () {
                self.isOrgsFetched = true;
                self.render();
            });

            this.listenTo(App.models.sitesCollection, 'fetched', function () {
                self.isSitesFetched = true;
                self.render();
            });
        },

        onBeforeRender: function () {
            this.initData();
        },

        onRender: function() {
            var self = this;
            this.showChildView('list', new OrgsTreeView({
                model: new Backbone.Model({
                    id: '', //for congruence purposes, while parentOrganizationId of root sites is '' too
                    name: App.models.generalModel.get('customerName'),
                    root: true,
                    children: self.collection
                })
            }));

            if (!this.isDataFetched()) {
                this.showSpinner();
            }
        },

        isDataFetched: function () {
            return this.isOrgsFetched && this.isSitesFetched;
        },

        //create tree collection of orgs & sites
        initData: function () {
            this.collection = App.models.orgsCollection.deepCloneAsTree({
                sitesCollection: App.models.sitesCollection
            });
        },

        showSpinner: function () {
            var tplFunc = _.template($('#spinnerTemplate').html());
            this.$el.append(tplFunc());
        },

        onDestroy: function () {
        }
    });
});