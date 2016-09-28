'use strict';

define([
    'originalUnderscore'
], function (_) {
    _.templateSettings = {
        escape: /<%[=-]([\s\S]+?)%>/g,
        interpolate: /<%cleanHtml([\s\S]+?)cleanHtml%>/g,
        evaluate: /<%([\s\S]+?)%>/g
    };

    return _;
});