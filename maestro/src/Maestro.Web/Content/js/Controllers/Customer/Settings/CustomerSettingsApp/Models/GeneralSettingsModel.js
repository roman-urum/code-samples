'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    './BaseModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts'
], function (
    $,
    _,
    Backbone,
    BackboneNested,
    BaseModel,
    App,
    Alerts
) {
    return BaseModel.extend({

        url: '/Settings/CustomerSettings',

        defaults: {
            customerName: '',
            idleSessionTimeout: '',
            logoImage: '',
            logoPath: '',
            passwordExpiration: '',
            subdomain: ''
        },

        validation: {
            customerName: [{
                required: true,
                msg: 'Please enter customer name'
            },{
                pattern: /^[0-9a-zA-Z]+$/,
                msg: 'Please use only letters and numbers'
            }],
            passwordExpiration: {
                range: [1, 9999]
            },
            idleSessionTimeout: {
                range: [5, 20]
            }
        },

        save: function (options) {
            options = options || {};
            var data = this.toJSON();

            //var formData = new FormData();
            //formData.append('logoImage', data.logoImage);
            $.ajax({
                url: this.url,
                method: 'PUT',
                dataType: 'json',
                data: data,
                //contentType: false,
                //processData: false,
                success: function () {
                    Alerts.success('Settings were updated successfully');
                    if (_.isFunction(options.success)) options.success();
                },
                error: function (err) {
                    Alerts.danger(err.responseJSON.ErrorMessage);
                    if (_.isFunction(options.error)) options.error();
                }
            });
        }

    });
});