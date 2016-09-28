'use strict';

define([
    'jquery',
    'underscore',
    './BaseItemView'
], function (
    $,
    _,
    BaseItemView
) {
    return BaseItemView.extend({
        template: '#conditionItemView',

        tagName: 'tr',

        className: 'condition-item-view'
    });
});