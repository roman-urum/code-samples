'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './ConditionModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    ConditionModel
) {
    return BaseCollection.extend({

        url: '/Settings/CustomerConditions',

        model: ConditionModel,

        comparator: 'name'

    });
});