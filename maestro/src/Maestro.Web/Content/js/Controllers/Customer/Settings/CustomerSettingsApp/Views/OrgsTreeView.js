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
    './BaseCompositeView',
    './SiteItemView'
], function (
    $,
    _,
    Backbone,
    Marionette,
    BackboneBootstrapModal,
    App,
    OrgModel,
    SiteModel,
    BaseCompositeView,
    SiteItemView
) {
    var OrgsTreeView =  BaseCompositeView.extend({
        template: '#orgItemTreeView',
        headTemplate: '#orgItemTreeViewHead',
        headEditTemplate: '#orgItemTreeViewHeadEdit',

        className: 'org-item-tree-view',

        childView: OrgsTreeView,

        childViewContainer: '.org-children',

        ui: {
            head:           '.org-head-container',
            headOverlay:    '.org-head-overlay-container .node-overlay',
            nameInput:      '.js-org-name'
        },

        events: {
            'click .js-site-add':           'onSiteAdd',
            'click .js-suborg-add':         'onSubOrgAdd',
            'click .js-suborg-edit':        'onSubOrgEdit',
            'click .js-suborg-edit-save':   'onSaveSubOrg',
            'click .js-suborg-edit-cancel': 'onSubOrgEditCancel',
            'click .js-suborg-remove':      'onSubOrgRemove',
            'click .js-toggle-branch':      'onToggleBranch'
        },

        initialize: function (options) {
            options = options || {};
            if (!options.model) throw 'Organization model is required';

            var self = this;
            this.orgsCollection = App.models.orgsCollection;
            this.sitesCollection = App.models.sitesCollection;

            this.removeSiteModal = null;

            Backbone.Validation.bind(this);
            BaseCompositeView.prototype.initialize.apply(this, arguments);

            App.on('site:props:saved', function (siteModel) {
                //automagically add/remove site when it was moved in another organization
                if (self.hasSite(siteModel) && siteModel.get('parentOrganizationId') !== self.model.get('id')) {
                    self.collection.remove(siteModel);
                } else if (!self.hasSite(siteModel) && siteModel.get('parentOrganizationId') === self.model.get('id')) {
                    self.collection.unshift(siteModel);
                    self.collection.sort();
                }
            });
        },

        //marionette method: select which type of view to use for node: orgs child tree view or site view
        buildChildView: function(model, OrgsTreeView, childViewOptions){
            var options = _.extend({model: model}, childViewOptions);
            var ViewClass;

            if (model instanceof OrgModel) {
                ViewClass = OrgsTreeView;
            } else if (model instanceof SiteModel) {
                ViewClass = SiteItemView;
            }

            return new ViewClass(options);
        },

        initData: function () {
            this.collection = this.model.get('children') || new Backbone.Collection();
        },

        templateHelpers: function () {
            return {
                isRoot: this.model.get('root'),
                isEditing: this.model.isEditing,
                isCreating: this.model.isCreating
            }
        },

        onBeforeRender: function () {
            this.initData();
        },

        onRender: function () {
            this.$el.attr('cid', this.cid);
            this.renderHead();
        },

        onDomRefresh: function () {
            this.getNameInputEl().focus();
        },

        //manually change head's html depending on model state
        renderHead: function () {
            var tplSelector = this.model.isEditing ? this.headEditTemplate : this.headTemplate;
            var tpl = $(tplSelector).html();
            var data = _.extend({children: []}, this.model.toJSON(), this.templateHelpers());
            this.ui.head.html(_.template(tpl)(data));
            this.getNameInputEl().focus();
        },

        onSiteAdd: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            this.removeNewModelStub();
            this.addNewSiteModelStub();
        },

        onSubOrgAdd: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            this.removeNewModelStub();
            this.addNewOrgModelStub();
        },

        onSubOrgEdit: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            this.model.isEditing = true;
            this.renderHead();
        },

        onSaveSubOrg: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            var self = this;
            this.model.store();
            this.model.set('name', this.getNameInputEl().val());

            if (!self.model.isValid(true)) {
                return;
            }

            this.trigger('self:save');
            this.showProgress();
            this.model.save({
                success: function () {
                    self.hideProgress();
                    if (self.model.isCreating) {
                        self.model.isCreating = false;
                        App.models.orgsCollection.unshift(self.model);
                    }
                    self.trigger('self:save:success');
                },
                error: function () {
                    self.hideProgress();
                    if (self.model.isCreating) {
                        self.model.isCreating = false;
                        self.trigger('self:remove');
                    } else {
                        self.model.restore();
                        self.renderHead();
                    }
                    self.trigger('self:save:error');
                }
            });
            this.cancelEditForm();
        },

        onSubOrgEditCancel: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            if (this.model.isCreating) {
                this.removeEditForm();
            } else {
                this.cancelEditForm();
            }
        },

        onSubOrgRemove: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            if ($(e.target).attr('disabled')) return;

            var self = this;
            this.removeSiteModal = new BackboneBootstrapModal({
                content: 'Are you sure?',
                title: 'Organization Remove Confirmation',
                okText: 'Remove',
                cancelText: 'Cancel',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            this.removeSiteModal.on('ok', function () {
                self.cancelEditForm();
                self.showProgress();
                self.model.destroy({
                    success: function () {
                        App.models.orgsCollection.remove(self.model);
                        self.trigger('self:remove');
                    },
                    error: function () {
                        self.hideProgress();
                    }
                });
            });

            this.removeSiteModal.open();
        },

        onToggleBranch: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
        },

        addNewSiteModelStub: function () {
            var site = new SiteModel({
                name: '',
                parentOrganizationId: this.model.get('id') || ''
            });
            site.isEditing = true;
            site.isCreating = true;

            this.collection.unshift(site);
        },

        addNewOrgModelStub: function () {
            var subOrg = new OrgModel({
                name: '',
                parentOrganizationId: this.model.get('id') || ''
            });
            subOrg.isEditing = true;
            subOrg.isCreating = true;

            //add new org model after all sites (which are always in the beginning)
            //but before all other orgs
            this.collection.add(subOrg, {at: this.getSitesArray().length});
        },

        removeNewModelStub: function () {
            var newModel = _.findWhere(this.collection.models, {isNew: true, isSaving: false});
            this.collection.remove(newModel);
        },

        cancelEditForm: function () {
            this.model.isEditing = false;
            this.renderHead();
        },

        removeEditForm: function () {
            this.trigger('self:remove');    //handled by parent onChildviewSelfRemove method
        },

        //handling event bubbling from child view in marionette way
        //http://marionettejs.com/docs/v2.4.5/marionette.collectionview.html#childview-event-bubbling-from-child-views
        onChildviewSelfRemove: function(childView) {
            this.collection.remove(childView.model);
        },

        onChildviewSelfSaveSuccess: function(childView) {
            this.collection.sort();
        },

        showProgress: function () {
            this.ui.headOverlay.show();
        },

        hideProgress: function () {
            this.ui.headOverlay.hide();
        },

        getNameInputEl: function () {
            //TODO: review this
            //Workaround for broken this.ui.nameInput link
            //after re-rendering suborg-head-container
            return this.$(this.ui.nameInput.selector);
        },

        getSitesArray: function () {
            return this.collection.filter(function (model) {
                return model instanceof SiteModel;
            });
        },

        hasSite: function (siteId) {
            if (siteId instanceof Backbone.Model) {
                siteId = siteId.get('id');
            }
            var sitesIds = _.map(this.getSitesArray(), function (model) { return model.get('id'); });
            return _.contains(sitesIds, siteId);
        },

        onDestroy: function () {
        }
    });

    return OrgsTreeView;
});