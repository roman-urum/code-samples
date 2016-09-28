'use strict';

define([
    'jquery',
    'underscore',
    'BackboneModelBinder',
    './BaseItemView',
    '../Models/ConditionModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Customer/Settings/CustomerSettingsApp/Alerts'
], function (
    $,
    _,
    BackboneModelBinder,
    BaseItemView,
    ConditionModel,
    App,
    Alerts
) {
    return BaseItemView.extend({
        template: '#conditionDetailsView',

        className: 'condition-details-view',

        model: ConditionModel,

        events: {
            'click .js-save-condition-and-exit-button': 'saveCondition',
            'click .js-save-condition-and-next-button': 'saveConditionAndNextTab'
        },

        initialize: function () {
            this.modelBinder = new BackboneModelBinder();

            Backbone.Validation.bind(this);
        },

        onBeforeRender: function () {

        },

        onRender: function () {
            this.modelBinder.bind(this.model, this.el);
        },

        saveCondition: function (e, preventNavigate) {
            e.preventDefault();

            var self = this;
            var isCreate = this.model.isNew();
            
            if (this.model.isValid(true)) {
                var saveButtons = this.$('.js-save-condition-and-exit-button, .js-save-condition-and-next-button');
                var cancelButton = this.$('.js-cancel-button');

                saveButtons.data('loading-text', 'Updating...').button('loading');
                cancelButton.addClass('disabled');

                this.model.save(null, {
                    success: function (response) {
                        Alerts.success('Condition was saved successfully');

                        if (isCreate) {
                            App.models.conditionsCollection.add(self.model);
                        }

                        if (!preventNavigate) {
                            App.navigate('Settings/Conditions');
                        } else {
                            self.triggerMethod('show:thresholds-tab');
                        }
                    },
                    error: function (model, xhr, options) {
                        Alerts.danger(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : 'Error occured');

                        saveButtons.button('reset');
                        cancelButton.removeClass('disabled');
                    }
                });
            }
        },

        saveConditionAndNextTab: function (e) {
            this.saveCondition(e, true);
        }
    });
});