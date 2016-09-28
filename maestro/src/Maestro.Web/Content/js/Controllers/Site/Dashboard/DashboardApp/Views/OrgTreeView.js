'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'Controllers/Site/Dashboard/DashboardApp/AppNamespace',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/OrgModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Models/SiteModel',
    './SiteItemView'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    app,
    OrgModel,
    SiteModel,
    SiteItemView
) {
    var OrgsTreeView =  Backbone.View.extend({
        template: app.OPTIONS.TEMPLATE('orgItemTreeView'),

        className: 'org-item-tree-view',

        childViewContainer: '.org-children',

        events: {
        },

        initialize: function (options) {
            options = options || {};
            if (!options.model) throw 'Organization model is required';

            Backbone.View.prototype.initialize.apply(this, arguments);
        },

        //marionette-like method: select which type of view to use for node: orgs child tree view or site view
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
                isRoot: this.model.get('root')
            }
        },

        onBeforeRender: function () {
            this.initData();
        },

        render: function () {
            this.onBeforeRender();
            var data = _.extend({}, this.model.toJSON(), this.templateHelpers());
            this.$el.html(this.template(data));
            this.renderSitesList();
            return this;
        },

        renderSitesList: function () {
            var self = this;
            var childrenContainer = this.$(this.childViewContainer);
            this.collection.each(function (model) {
                childrenContainer.append(self.buildChildView(model, OrgsTreeView).render().el);
            });
        }

    });

    return OrgsTreeView;
});