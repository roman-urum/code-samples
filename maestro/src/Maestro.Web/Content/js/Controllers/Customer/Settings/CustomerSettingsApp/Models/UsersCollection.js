'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './UserModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    UserModel
) {
    return BaseCollection.extend({

        url: '/Settings/CustomerUsers',

        model: UserModel

    });
});