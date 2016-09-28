'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BaseModel,
    Alerts,
    Helpers
) {
    return BaseModel.extend({

        url: function () {
            var url = '/Settings/CustomerOrganizations';
            if (!this.isNew) {
                url += ('/' + this.get('id'));
            }
            return url;
        },

        isNew: false,
        isSaving: false,

        defaults: {
            id: '',
            name: '',
            parentOrganizationId: ''
        },

        initialize: function (data) {
            data = data || {};

            if (!data.id && !data.Id) {
                this.set({ id: Helpers.getGUID() }, { silent: true });
                this.isNew = true;
            }

            BaseModel.prototype.initialize.apply(this, arguments);

            if (!this.get('parentOrganizationId')) {
                this.set('parentOrganizationId', '');
            }
        },

        parse: function (response) {
            response = BaseModel.prototype.parse.apply(this, arguments);

            if (!response.parentOrganizationId || response.parentOrganizationId == null) {
                response.parentOrganizationId = '';
            }
            return response;
        },

        save: function (options) {
            options = options || {};
            var self = this;
            var method = this.isNew ? 'POST' : 'PUT';
            var data = this.toJSON();

            this.isSaving = true;
            delete data.id;
            delete data.children;

            $.ajax({
                url: this.url(),
                method: method,
                dataType: 'json',
                data: data,
                success: function (response) {
                    Alerts.success('Organization was saved successfully');

                    if (self.isNew) {
                        response = Helpers.convertKeysToCamelCase(response);
                        self.set('id', response.id);
                        self.isNew = false;
                        self.isSaving = false;
                    }

                    if (_.isFunction(options.success)) options.success.apply(self, arguments);
                },
                error: function (err) {
                    self.isSaving = false;
                    Alerts.danger(err.responseJSON ? err.responseJSON.ErrorMessage : 'Error occured');
                    if (_.isFunction(options.error)) options.error.apply(self, arguments);
                }
            });
        },

        destroy: function (options) {
            options = options || {};
            var self = this;
            var method = 'DELETE';

            $.ajax({
                url: this.url(),
                method: method,
                success: function (response) {
                    Alerts.success('Organization was removed successfully');
                    if (_.isFunction(options.success)) options.success.apply(self, arguments);
                },
                error: function (err) {
                    Alerts.danger(err.responseJSON ? err.responseJSON.ErrorMessage : 'Error occured');
                    if (_.isFunction(options.error)) options.error.apply(self, arguments);
                }
            });
        },

        validation: {
            name: [{
                required: true,
                msg: 'Sub-org name is required'
            },{
                pattern: /^[-0-9a-zA-Z\s]+$/,
                msg: 'Name cannot contain special characters'
            }]
        }

    });
});