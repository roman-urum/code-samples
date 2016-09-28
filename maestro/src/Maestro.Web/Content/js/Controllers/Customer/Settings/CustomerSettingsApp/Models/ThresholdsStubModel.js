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

    //This is stub model just to render ThresholdsView in standart way
    return BaseModel.extend({

        fetch: function () {}

    });
});