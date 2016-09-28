'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts'
], function (
    $,
    _,
    Backbone,
    BaseModel,
    App,
    Alerts
) {
    return BaseModel.extend({

        url: function () {
            var userId = this.get('id'),
                url = '/Settings/CustomerUsers';

            if (userId) {
                url += ('/' + userId);
            }

            return url;
        },

        defaults: {
            id: '',
            firstName: '',
            lastName: '',
            isEmailVerified: null,
            isEnabled: null,
            email: '',
            phone: '',
            state: '',
            city: '',
            zipCode: '',
            address1: '',
            address2: '',
            address3: '',
            customerUserId: '',
            customerUserRoleId: '',
            customerUserRole: {},
            doNotSendInvitation: null,
            nationalProviderIdentificator: '',
            siteIds: [],
            sites: []
        },

        validation: {
            firstName: [{
                required: true,
                msg: 'Please enter first name'
            },{
                pattern: /^[^!@#$%\^&0-9]+$/,
                msg: 'First name cannot contain special characters'
            }],
            lastName: [{
                required: true,
                msg: 'Please enter first name'
            },{
                pattern: /^[^!@#$%\^&0-9]+$/,
                msg: 'Last name cannot contain special characters'
            }],
            email: {
                pattern: 'email'
            },
            customerUserRoleId: [{
                required: true,
                msg: 'Please select role'
            }]
        },

        initialize: function () {
            BaseModel.prototype.initialize.apply(this, arguments);

            var self = this;
            this.listenTo(this, 'change:customerUserRoleId', function () {
                self.set('customerUserRole', App.models.rolesCollection.findWhere({id: self.get('customerUserRoleId')}).toJSON());
            });
        },

        parse: function (response) {
            response = BaseModel.prototype.parse.apply(this, arguments);

            //'populate' relation fields
            //rolesCollection & sitesCollection
            //should be already fetched
            response.customerUserRole = App.models.rolesCollection.findWhere({id: response.customerUserRoleId}).toJSON();

            response.siteIds = response.sites;
            response.sites = response.sites.map(function (siteId) {
                return App.models.sitesCollection.findWhere({id: siteId}).toJSON();
            });

            return response;
        },

        save: function (options) {
            options = options || {};
            var userId = this.get('id');
            var data = this.toJSON();
            data.sites = data.siteIds;
            delete data.customerUserRole;
            delete data.siteIds;

            return $.ajax({
                url: this.url(),
                method: userId ? 'PUT' : 'POST',
                dataType: 'json',
                data: data,
                success: function (responseObj, responseText, xhr) {
                    Alerts.success('User was saved successfully');
                    if (_.isFunction(options.success)) options.success(responseObj, responseText, xhr);
                },
                error: function (err) {
                    Alerts.danger(err.responseJSON.Message + '. ' + err.responseJSON.Details);
                    if (_.isFunction(options.error)) options.error();
                }
            });
        },

        refresh: function () {
            this.set({
                sites: App.models.sitesCollection.toJSON(),
                roles: App.models.rolesCollection.toJSON()
            });
            this.trigger('fetched');
        }

    });
});