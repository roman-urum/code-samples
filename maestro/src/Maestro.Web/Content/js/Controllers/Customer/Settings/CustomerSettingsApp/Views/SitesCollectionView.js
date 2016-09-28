'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    './BaseCollectionView',
    './SiteItemView'
], function (
    $,
    _,
    Marionette,
    App,
    BaseCollectionView,
    SiteItemView
) {
    return BaseCollectionView.extend({
        className: 'sites-collection-view banded-list',

        childView: SiteItemView,

        useSpinner: true,

        onRender: function () {
            this.$('.basic-checkbox').bootstrapSwitch();
        }
    });
});