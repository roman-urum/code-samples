'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'BackboneBootstrapModal',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    '../Models/SiteModel',
    './BaseLayoutView',
    './SitesCollectionView',
    './SiteEditView'
], function (
    $,
    _,
    Marionette,
    BackboneBootstrapModal,
    App,
    SiteModel,
    BaseLayoutView,
    SitesCollectionView,
    SiteEditView
) {
    return BaseLayoutView.extend({
        template: '#sitesView',

        className: 'sites-view',

        regions: {
            list: '.sites-list'
        },

        events: {
            'click .js-add-site': 'showAddSiteView'
        },

        initialize: function () {
            BaseLayoutView.prototype.initialize.apply(this, arguments);
        },

        onRender: function() {
            this.showChildView('list', new SitesCollectionView({model: this.options.model}));
        },

        showAddSiteView: function (e) {
            e.preventDefault();

            this.addSiteModal = new BackboneBootstrapModal({
                content: new SiteEditView({
                    create: true,
                    model: new SiteModel()
                }),
                title: 'Add New Site',
                okText: 'Add Site',
                cancelText: 'Cancel',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.addSiteModal.open();
        }
    });
});