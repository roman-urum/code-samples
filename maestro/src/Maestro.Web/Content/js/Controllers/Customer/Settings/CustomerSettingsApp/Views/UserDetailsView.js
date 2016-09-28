'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts',
    'BackboneModelBinder',
    './BaseItemView',
    './BaseLayoutView',
    './UserPermissionsView'
], function (
    $,
    _,
    Marionette,
    App,
    Alerts,
    BackboneModelBinder,
    BaseItemView,
    BaseLayoutView,
    UserPermissionsView
) {
    return BaseItemView.extend({
        template: '#userDetailsView',

        className: 'user-details-view',

        useSpinner: true,

        templateHelpers: function () {
            var self = this;
            return {
                sitesCollection: App.models.sitesCollection.toJSON() || [],
                rolesCollection: App.models.rolesCollection.toJSON() || [],
                isCreate: this.isCreate,
                getSitePath: function (siteId) {
                    return self.getSitePath(siteId);
                }
            };
        },

        events: {
            'click .js-submit-user': 'submitUser',
            'click #reset-password': 'requestPasswordReset',
            'click #resend-invite': 'requestInviteResend'
        },

        initialize: function (options) {
            BaseItemView.prototype.initialize.apply(this, arguments);
            this.modelBinder = new BackboneModelBinder();

            var self = this;
            this.isCreate = (options.action === 'create');

            this.listenTo(this.model, 'change:customerUserRoleId', function () {
                self.renderPermissions();
            });
        },

        onRender: function () {
            this.renderPermissions();

            this.modelBinder.bind(this.model, this.el);
        },

        //TODO: refactor this using LayoutView's region
        renderPermissions: function () {
            var view = new UserPermissionsView({model: this.model});
            view.render();
            this.$('.permissions-wrapper').html(view.$el);
        },

        submitUser: function (e) {
            this.model.save({
                success: function () {
                    App.models.usersCollection.fetch();
                }
            });
        },

        requestPasswordReset: function (e) {
            e.preventDefault();
            var url = '/Settings/SendResetPasswordEmail?id=' + this.model.get('id');

            $.ajax({
                url: url,
                method: 'GET',
                success: function () {
                    Alerts.success('Request was sent');
                },
                error: function (err) {
                    Alerts.danger(err.responseJSON ? err.responseJSON.ErrorMessage : 'Error occured');
                }
            });
        },

        requestInviteResend: function (e) {
            e.preventDefault();
            var url = '/Settings/ResendInvite';

            $.ajax({
                url: url,
                method: 'POST',
                data: {email: this.model.get('email')},
                success: function (response) {
                    Alerts.success('Request was sent');
                },
                error: function (err) {
                    Alerts.danger(err.responseJSON ? err.responseJSON.ErrorMessage : 'Error occured');
                }
            });
        },

        getSitePath: function (siteId) {
            var orgs = App.models.orgsCollection;
            var site = App.models.sitesCollection.findWhere({id: siteId});
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

            return App.models.generalModel.get('customerName') + concatOrgNames(site.get('parentOrganizationId'));
        }
    });
});