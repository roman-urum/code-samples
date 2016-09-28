'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './SiteModel',
    './OrgModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    SiteModel,
    OrgModel
) {
    return BaseCollection.extend({

        url: '/Settings/CustomerOrganizations',

        model: OrgModel,

        comparator: function (model1, model2) {
            var name1 = model1.get('name').toLowerCase();
            var name2 = model2.get('name').toLowerCase();
            var result = name1 < name2 ? -1 : 1;

            if (model1 instanceof SiteModel && model2 instanceof OrgModel) {
                result = -1;
            } else if (model1 instanceof OrgModel && model2 instanceof SiteModel) {
                result = 1;
            }

            return result;
        },

        deepCloneAsTree: function (options) {
            options = options || {};

            var self = this;
            var sitesCollection = options.sitesCollection;
            return getChildrenCollection('');

            function getChildrenCollection(parentOrganizationId) {

                //get cloned collection child orgs
                var childOrgs = self.where({ 'parentOrganizationId': parentOrganizationId });
                var childOrgsClones = _.map(childOrgs, function (orgModel) {
                    return new OrgModel(orgModel.toJSON());
                });
                var collection = new Backbone.Collection(childOrgsClones, {comparator: self.comparator});

                if (sitesCollection) {
                    //get cloned collection child sites
                    var childSites = sitesCollection.where({ 'parentOrganizationId': parentOrganizationId });
                    var childSitesClones = _.map(childSites, function (siteModel) {
                        return new SiteModel(siteModel.toJSON());
                    });
                    collection.unshift(childSitesClones, {silent: true});
                }
                
                collection.each(function (model) {
                    if (model instanceof OrgModel && model.get('id')) {
                        model.set({'children': getChildrenCollection(model.get('id'))}, {silent: true});
                    }
                });
                collection.sort();

                return collection;
            }
        }

    });
});