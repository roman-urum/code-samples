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
            return this.model.isEditing ? '#siteItemEditView' : '#siteItemView';
        },

        className: 'site-item-view',

        ui: {
            headOverlay:    '.org-head-overlay-container .node-overlay',
            nameInput:      '.js-site-name'
        },

        events: {
            'click .js-site-edit': 'onSitePropsEdit',
            'click .js-site-edit-save': 'onSiteEditSave',
            'click .js-site-edit-cancel': 'onSiteEditCancel'
        },

        initialize: function () {
            BaseItemView.prototype.initialize.apply(this, arguments);
            Backbone.Validation.bind(this);

            this.editSiteModal = null;
            this.removeSiteModal = null;
        },

        onRender: function () {
            this.bindUIElements();
        },

        onDomRefresh: function () {
            this.ui.nameInput.focus();
        },

        onSitePropsEdit: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();

            this.model.store();

            var self = this;
            var editView = new SiteEditView({model: this.model});
            this.editSiteModal = new BackboneBootstrapModal({
                content: editView,
                title: this.model.get('name'),
                okText: 'Save',
                cancelText: 'Cancel',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.editSiteModal.on('ok', function () {
                if (!self.model.isValid(true)) {
                    self.editSiteModal.preventClose();
                    return;
                }
                self.render();
                self.showProgress();
                self.model.save({
                    success: function () {
                        self.hideProgress();
                        self.trigger('self:save:success');
                        App.trigger('site:props:saved', self.model);
                    },
                    error: function () {
                        self.hideProgress();
                        self.model.restore();
                        self.render();
                        self.trigger('self:save:error');
                    }
                });
            });

            this.editSiteModal.on('cancel', function () {
                self.model.restore();
            });

            editView.on('site:remove', function () {
                self.editSiteModal.close();
                self.onSiteRemove();
            });

            this.editSiteModal.open();
        },

        onSiteEditSave: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            var self = this;

            //Warning
            var name = this.ui.nameInput.val();
            this.model.set('name', name);

            if (!self.model.isValid(true)) {
                //self.model.restore();
                return;
            }

            //TODO: process a server error case
            this.trigger('self:save');
            this.model.save({
                success: function () {
                    self.hideProgress();
                    if (self.model.isCreating) {
                        self.model.isCreating = false;
                        self.trigger('self:save:success');
                        App.models.sitesCollection.unshift(self.model);
                    }
                },
                error: function () {
                    self.hideProgress();
                    self.trigger('self:remove');    //undo creating stub model
                }
            });
            this.model.isEditing = false;
            this.render();
            this.showProgress();    //show progress after render because of changing template
        },

        onSiteRemove: function () {
            var self = this;
            this.removeSiteModal = new BackboneBootstrapModal({
                content: 'Are you sure?',
                title: 'Site Remove Confirmation',
                okText: 'Remove',
                cancelText: 'Cancel',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.removeSiteModal.on('ok', function () {
                self.showProgress();
                self.model.destroy({
                    success: function () {
                        App.models.sitesCollection.remove(self.model);
                        self.trigger('self:remove');
                    },
                    error: function () {
                        self.hideProgress();
                    }
                });
            });

            this.removeSiteModal.open();
        },

        onSiteEditCancel: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            this.trigger('self:remove');
        },

        showProgress: function () {
            if (this.ui.headOverlay) this.ui.headOverlay.show();
        },

        hideProgress: function () {
            if (this.ui.headOverlay) this.ui.headOverlay.hide();
        }
    });
});