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
        template: '#noConditionsView',

        tagName: 'tr'
    });
});