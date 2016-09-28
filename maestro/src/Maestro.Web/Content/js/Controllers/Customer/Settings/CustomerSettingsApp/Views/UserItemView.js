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
        template: '#userItemView',

        tagName: 'tr',

        className: 'user-item-view',

        events: {
            'switchChange.bootstrapSwitch .js-toggle-user': 'toggleUser'
        },

        toggleUser: function () {
            this.model.set('isEnabled', !this.model.get('isEnabled'));
            this.model.save();
        }
    });
});