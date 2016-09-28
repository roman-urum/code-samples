'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseCollection',
    './SiteModel'
], function (
    $,
    _,
    Backbone,
    BaseCollection,
    SiteModel
) {
    return BaseCollection.extend({

        url: '/Settings/CustomerSites',

        model: SiteModel

    });
});