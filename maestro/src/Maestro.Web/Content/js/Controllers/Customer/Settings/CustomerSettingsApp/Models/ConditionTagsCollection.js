'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './ConditionTagModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    ConditionTagModel
) {
    return BaseCollection.extend({

        url: '/Settings/CustomerConditionsTags',

        model: ConditionTagModel,

        comparator: 'name'

    });
});