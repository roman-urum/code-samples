'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './RoleModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    RoleModel
) {
    return BaseCollection.extend({

        url: '/Settings/UserRoles',

        model: RoleModel

    });
});