'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts',
    'BackboneModelBinder',
    './BaseItemView'
], function (
    $,
    _,
    Backbone,
    Marionette,
    App,
    Alerts,
    BackboneModelBinder,
    BaseItemView
) {
    return BaseItemView.extend({
        template: '#generalSettingsView',

        className: 'general-settings',

        useSpinner: true,

        ui: {
            confirmationCheckbox: '.js-confirmation-checkbox'
        },

        events: {
            'click .js-settings-submit': 'onSettingsSubmit',
            'click .js-cache-refresh': 'onCacheRefresh'
        },

        initialize: function (options) {
            BaseItemView.prototype.initialize.apply(this, arguments);

            this.modelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);

            var self = this;
            this.listenTo(this.model, 'change:logoImage', function () {
                //self.model.attributes.logoImage = $('input[type=file]')[0].files[0];
            });
        },

        onRender: function () {
            this.modelBinder.bind(this.model, this.el);
        },

        onSettingsSubmit: function (e) {
            if (this.ui.confirmationCheckbox.prop('checked')) {

                var file = this.$(':file');
                if (this.model.isValid(true)) this.model.save();

            } else {
                Alerts.warning('Please confirm changes by checking the checkbox below the form')
            }
        },

        onCacheRefresh: function (e) {
            e.preventDefault();

        }
    });
});