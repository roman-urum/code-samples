'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'BackboneModelBinder',
    '../Models/SiteModel',
    './BaseItemView',
    './BaseLayoutView',
    './OrgsSelectTreeView',
    './UserPermissionsView'
], function (
    $,
    _,
    Marionette,
    App,
    BackboneModelBinder,
    SiteModel,
    BaseItemView,
    BaseLayoutView,
    OrgsSelectTreeView,
    UserPermissionsView
) {
    return BaseLayoutView.extend({
        template: '#userEditView',

        className: 'user-edit-view',

        useSpinner: true,

        regions: {
            permissions: '.permissions-wrapper',
            sites: '.sites-wrapper'
        },

        templateHelpers: function () {
            return {
                sitesCollection: App.models.sitesCollection.toJSON() || [],
                rolesCollection: App.models.rolesCollection.toJSON() || [],
                isCreate: this.isCreate
            };
        },

        events: {
            'click .js-submit-user': 'submitUser',
            'click .js-user-edit-cancel': 'cancelUserEdit',
            'click #reset-password': 'requestPasswordReset',
            'click #resend-invite': 'requestInviteResend'
        },

        initialize: function (options) {
            BaseItemView.prototype.initialize.apply(this, arguments);

            this.modelBinder = new BackboneModelBinder();
            this.manualModelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);
            this.model.store();

            var self = this;
            this.isCreate = (options.action === 'create');

            this.listenTo(this.model, 'change:customerUserRoleId', function () {
                self.renderPermissions();
            });
        },

        onBeforeRender: function () {
            var self = this;
            
            if (App.models.rolesCollection.length <= 0) {
                App.models.rolesCollection.bind('sync', function () {
                    self.$el.find('.js-select-role').empty();
                    App.models.rolesCollection.each(function (model) {
                        var $roleOption = $('<option>');
                        $roleOption.val(model.get('id'));
                        $roleOption.text(model.get('name'));
                        self.$el.find('.js-select-role').append($roleOption);
                    });
                    self.$el.find('.js-select-role').val(0);
                });
            }

            //this.initData();
        },

        onRender: function() {
            var self = this;

            self.modelBinder.bind(self.model, self.$el.find(':not(.js-manual-bind)'));

            //We double binding to bind isEnabled property only using property converter. 
            //Converter is used because default binding behavior does not bind boolean property to string el value and string el value to boolean prop as well.
            self.manualModelBinder.bind(self.model, self.$el.find('.js-manual-bind'), {
                isEnabled: {
                    selector: '[name="isEnabled"]',
                    converter: function (direction, value) {
                        if (direction === 'ModelToView') {
                            return value.toString();
                        } else if (direction === 'ViewToModel') {
                            return JSON.parse(value);
                        }
                    }
                }
            });

            self.showChildView('permissions', new UserPermissionsView({ model: self.model }));

            var orgsCollections = App.models.orgsCollection.deepCloneAsTree({
                sitesCollection: App.models.sitesCollection
            });

            setSelected(orgsCollections);

            function setSelected(collection) {
                if (!collection) return;
                collection.each(function (model) {
                    if (model instanceof SiteModel) {
                        model.set('isSelected', _.contains(self.model.get('siteIds'), model.get('id')));
                    } else {
                        setSelected(model.get('children'));
                    }
                });
            }

            var viewModel = new Backbone.Model({
                id: '', //for congruence purposes, while parentOrganizationId of root sites is '' too
                name: App.models.generalModel.get('customerName'),
                root: true,
                children: orgsCollections
            });
            var sitesView = new OrgsSelectTreeView({
                model: viewModel
            });
            sitesView.on('site:toggle', function () {
                var selectedSites = self.getSelectedSites.call(self);
                var selectedSitesData = _.map(selectedSites, function (model) { return model.toJSON(); });
                self.model.set({
                    siteIds: _.pluck(selectedSites, 'id'),
                    sites: selectedSitesData
                }, {silent: true});
            });
            self.showChildView('sites', sitesView);
        },

        getSelectedSites: function () {
            var treeCollection = this.getRegion('sites').currentView.model.get('children');
            if (!treeCollection) return;
            return getSelected(treeCollection);

            function getSelected(collection) {
                var selectedSites = [];
                if (collection) {
                    collection.each(function (model) {
                        if (model instanceof SiteModel) {
                            if (model.get('isSelected')) selectedSites.push(model);
                        } else {
                            Array.prototype.push.apply(selectedSites, getSelected(model.get('children')));
                        }
                    });
                }
                return selectedSites;
            }
        },

        isDataFetched: function () {
            return this.isOrgsFetched && this.isSitesFetched;
        },

        //create tree collection of orgs & sites
        initData: function () {
            var self = this;
            this.collection = App.models.orgsCollection.deepCloneAsTree({
                sitesCollection: App.models.sitesCollection
            });

            setSelected(this.collection);

            function setSelected(collection) {
                if (!collection) return;
                collection.each(function (model) {
                    if (model instanceof SiteModel) {
                        model.set('isSelected', _.contains(self.model.get('siteIds'), model.get('id')));
                    } else {
                        setSelected(model.get('children'));
                    }
                });
            }
        },

        //TODO: refactor this using LayoutView's region
        renderPermissions: function () {
            var view = new UserPermissionsView({model: this.model});
            view.render();
            this.$('.permissions-wrapper').html(view.$el);
        },

        submitUser: function (e) {
            var self = this;
            var $target = $(e.target);

            if (self.model.isValid(true)) {
                $target.data('loading-text', 'Updating...').button('loading');

                App.models.usersCollection.add(self.model);

                self.model.save({
                    success: function (responseObj, responseText, xhr) {
                        
                        $target.button('reset');

                        if (!self.model.get('id')) {
                            self.model.set('id', responseObj.Id);
                        }

                        App.navigate('/Settings/Users/' + self.model.get('id') + '/Details');
                    },
                    error: function() {
                        $target.button('reset');
                    }
                });
            }
        },

        cancelUserEdit: function (e) {
            e.preventDefault();
            this.model.restore();
            App.navigate('/Settings/Users');
        },

        requestPasswordReset: function () {},

        requestInviteResend: function () {}

    });
});