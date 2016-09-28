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
    './SiteSelectItemView'
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
    SiteSelectItemView
) {
    var OrgsSelectTreeView =  BaseCompositeView.extend({
        template: '#orgSelectItemTreeView',

        className: 'org-select-item-tree-view',

        childView: OrgsSelectTreeView,

        childViewContainer: '.org-children',

        ui: {
            head:           '.org-head-container',
            //headOverlay:    '.org-head-overlay-container .node-overlay',
            //nameInput:      '.js-org-name'
        },

        events: {
            'click .js-select-all':         'onSelectAll',
            'click .js-clear-all':          'onClearAll',
            'click .js-toggle-branch':      'onToggleBranch'
        },

        initialize: function (options) {
            options = options || {};
            if (!options.model) throw 'Organization model is required';

            var self = this;
            BaseCompositeView.prototype.initialize.apply(this, arguments);
        },

        //marionette method: select which type of view to use for node: orgs child tree view or site view
        buildChildView: function(model, OrgsSelectTreeView, childViewOptions){
            var options = _.extend({model: model}, childViewOptions);
            var ViewClass;

            if (model instanceof OrgModel) {
                ViewClass = OrgsSelectTreeView;
            } else if (model instanceof SiteModel) {
                ViewClass = SiteSelectItemView;
            }

            return new ViewClass(options);
        },

        initData: function () {
            this.collection = this.model.get('children') || new Backbone.Collection();
        },

        onBeforeRender: function () {
            this.initData();
        },

        onRender: function () {
            this.$el.attr('cid', this.cid);
            //this.renderHead();
        },

        //manually change head's html depending on model state
        renderHead: function () {
            var tplSelector = this.model.isEditing ? this.headEditTemplate : this.headTemplate;
            var tpl = $(tplSelector).html();
            var data = _.extend({children: []}, this.model.toJSON(), this.templateHelpers());
            this.ui.head.html(_.template(tpl)(data));
            this.getNameInputEl().focus();
        },

        onSelectAll: function (e) {
            if (e) {
                e.preventDefault();
                e.stopImmediatePropagation();
            }

            var self = this;
            this.collection.each(function (model) {
                model.set('isSelected', true);
            });
            this.children.invoke('onSelectAll');
            this.triggerSiteToggle();

            this.render();
        },

        onClearAll: function (e) {
            if (e) {
                e.preventDefault();
                e.stopImmediatePropagation();
            }

            var self = this;
            this.collection.each(function (model) {
                model.set('isSelected', false);
            });
            this.children.invoke('onClearAll');
            this.triggerSiteToggle();

            this.render();
        },

        triggerSiteToggle: function () {
            this.trigger('site:toggle');
        },

        //Implement events bubbling
        //Pass event further to the tree root
        onChildviewSiteToggle: function () {
            this.trigger('site:toggle');
        },

        onToggleBranch: function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
        },

        onDestroy: function () {
        }
    });

    return OrgsSelectTreeView;
});