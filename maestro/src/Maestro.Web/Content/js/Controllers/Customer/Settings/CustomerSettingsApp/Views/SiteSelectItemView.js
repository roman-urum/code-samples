'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'BackboneBootstrapModal',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'BackboneModelBinder',
    './BaseItemView',
    './SiteEditView'
], function (
    $,
    _,
    Marionette,
    BackboneBootstrapModal,
    App,
    BackboneModelBinder,
    BaseItemView,
    SiteEditView
) {
    return BaseItemView.extend({
        getTemplate: function(){
            return '#siteSelectItemView';
        },

        className: 'site-select-item-view',

        ui: {
            //headOverlay:    '.org-head-overlay-container .node-overlay',
            //nameInput:      '.js-site-name'
        },

        events: {
            'change .js-site-toggle': 'onSiteToggle'
        },

        initialize: function () {
            BaseItemView.prototype.initialize.apply(this, arguments);

            this.editSiteModal = null;
            this.removeSiteModal = null;
        },

        onRender: function () {
            this.bindUIElements();
        },

        onDomRefresh: function () {
            //this.ui.nameInput.focus();
        },

        templateHelpers: function () {
            return {
                isSelected: !!this.model.get('isSelected')
            }
        },

        onSiteToggle: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            this.model.set('isSelected', !this.model.get('isSelected'));
            this.triggerSiteToggle();
        },

        triggerSiteToggle: function () {
            this.trigger('site:toggle');
        },

        showProgress: function () {
            if (this.ui.headOverlay) this.ui.headOverlay.show();
        },

        hideProgress: function () {
            if (this.ui.headOverlay) this.ui.headOverlay.hide();
        }
    });
});