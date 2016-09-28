'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application'
], function (
    $,
    _,
    Backbone,
    BaseModel,
    App
) {
    return BaseModel.extend({

        defaults: {
            id: '',
            name: '',
            customerId: '',
            permissions: []
        }

    });
});