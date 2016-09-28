'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    './BaseItemView'
], function (
    $,
    _,
    Marionette,
    App,
    BaseItemView
) {
    return BaseItemView.extend({
        template: '#userPermissionsView',

        className: 'user-permissions-view',

        events: {
            'click .js-expand-all': 'expandAll',
            'click .js-collapse-all': 'collapseAll'
        },

        expandAll: function (e) {
            e.preventDefault();
            this.$('.collapse').collapse('show');
        },

        collapseAll: function (e) {
            e.preventDefault();
            this.$('.collapse').collapse('hide');
        }

    });
});