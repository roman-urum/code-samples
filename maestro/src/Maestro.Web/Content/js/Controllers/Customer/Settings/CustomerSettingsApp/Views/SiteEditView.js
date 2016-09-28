'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'BackboneModelBinder',
    './BaseItemView'
], function (
    $,
    _,
    Marionette,
    App,
    BackboneModelBinder,
    BaseItemView
) {
    return BaseItemView.extend({
        template: '#siteEditView',

        className: 'site-edit-view',

        //This like a modal-content view.
        //Events for site savind are processed in parent SiteItemView
        events: {
            'click .js-site-remove': 'onSiteRemove',
            'switchChange.bootstrapSwitch .js-site-toggle': 'onSiteToggle'
        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);
        },

        templateHelpers: function () {
            var self = this;
            return {
                orgs: this.getOrgsData(),
                rootOrgName: App.models.generalModel.get('customerName'),
                getOrgsPath: function () {
                    return self.getOrgsPath();
                }
            }
        },

        onRender: function () {
            this.$('.basic-checkbox').bootstrapSwitch({
                size: 'mini',
                onText: 'ENABLED',
                offText: 'DISABLED'
            });

            this.modelBinder.bind(this.model, this.el);
        },

        getOrgsData: function () {
            var orgsTree = this.getOrgsTree();
            var orgsList = [];

            getList(orgsTree);

            function getList (collection) {
                collection.each(function (model) {
                    orgsList.push(model.toJSON());
                    var childCollection = model.get('children');
                    if (childCollection) getList(childCollection);
                });
            }

            return _.map(orgsList, function(orgObj) {
                return _.extend(orgObj, {indentLevel: getOrgIndentLevel(orgObj)});
            });

            function getOrgIndentLevel(org) {
                var parentId = org.parentOrganizationId;
                if (parentId) {
                    var parentOrg = App.models.orgsCollection.findWhere({'id': parentId});
                }
                return parentOrg ? 1 + getOrgIndentLevel(parentOrg.toJSON()) : 1;
            }
        },

        getOrgsTree: function () {
            return getChildrenCollection('');

            function getChildrenCollection(parentOrganizationId) {
                var childOrgs = App.models.orgsCollection.where({ 'parentOrganizationId': parentOrganizationId });

                var collection = new Backbone.Collection(childOrgs);

                collection.each(function (model) {
                    if (model.get('id')) {
                        model.set({'children': getChildrenCollection(model.get('id'))}, {silent: true});
                    }
                });

                return collection;
            }
        },

        getOrgsPath: function () {
            var orgs = App.models.orgsCollection;
            var currentOrgId = this.model.get('parentOrganizationId');
            var divider = '\u00A0> ';   //use non-breakable space

            function concatOrgNames(orgId) {
                var org, orgName, str = '';
                if (orgId) {
                    org = orgs.findWhere({id: orgId});
                    orgName = org.get('name').split(' ').join('\u00A0');    //replace all spaces
                    str = concatOrgNames(org.get('parentOrganizationId')) + divider + orgName;
                }
                return str;
            }

            return App.models.generalModel.get('customerName') + concatOrgNames(currentOrgId);
        },

        onSiteRemove: function (e) {
            e.preventDefault();
            this.trigger('site:remove');
        },

        onSiteToggle: function () {
            this.model.set('isActive', !this.model.get('isActive'));
        }

    });
});