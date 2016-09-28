'use strict';

define([
    'jquery',
    'underscore',
    'BackboneModelBinder',
    './BaseItemView',
    '../Models/ConditionModel',
    'Controllers/Root/Thresholds/ThresholdsApp',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Constants'
], function (
    $,
    _,
    BackboneModelBinder,
    BaseItemView,
    ConditionModel,
    ThresholdsApp,
    App,
    Constants
) {
    return BaseItemView.extend({
        template: '#conditionThresholdsView',

        className: 'condition-thresholds-view',

        model: ConditionModel, // This model is used just to keep conditionId within this View

        onRender: function () {
            var self = this;
            
            this.initPopover();

            var $thresholdsSaveAndExitButton = this.$el.find('a.js-save-condition-thresholds-and-exit-button'),
                $thresholdsSaveAndNextTabButton = this.$el.find('a.js-save-condition-thresholds-and-next-button'),
                $thresholdsContentContainer = this.$el.find('div.thresholds-container'),
                $cancelButton = this.$el.find('.js-cancel-button');

            var config = {

                crud: {
                    get: '/Settings/DefaultThresholds',
                    save: '/Settings/DefaultThreshold'
                },

                mode: Constants.thresholdAppModes.CONDITION,

                allowUnusedButton: false,

                conditionId: self.model.get('id'),

                isReInit: true,

                events: {
                    beforeSave: function() {
                        $thresholdsSaveAndExitButton.data('loading-text', 'Updating...').button('loading');
                        $thresholdsSaveAndNextTabButton.data('loading-text', 'Updating...').button('loading');
                        $cancelButton.addClass('disabled');
                    },
                    afterSave: function () {
                        $thresholdsSaveAndExitButton.button('reset');
                        $thresholdsSaveAndNextTabButton.button('reset');
                        $cancelButton.removeClass('disabled');

                        if (!self.isPreventNavigate) {
                            App.navigate('Settings/Conditions');
                        } else {
                            self.triggerMethod('show:content-tab');
                        }
                    },
                    afterLoad: function() {
                        $thresholdsSaveAndExitButton
                            .on('click', function(e) {
                                e.preventDefault();

                                self.isPreventNavigate = false;

                                thresholdsApp.save();
                            });

                        $thresholdsSaveAndNextTabButton
                            .on('click', function (e) {
                                e.preventDefault();

                                self.isPreventNavigate = true;

                                thresholdsApp.save();
                            });

                        $thresholdsContentContainer.html(thresholdsApp.el);
                    }
                }
            };

            var thresholdsApp = new ThresholdsApp(config);
        },

        initPopover: function () {
            this.$el.find('[data-toggle="popover"]').popover({
                template: '<div class="popover">\
                    <div class="arrow"></div>\
                        <div class="popover-header">\
                            <button type="button" class="close" data-dismiss="popover" aria-hidden="true">&times;</button>\
                                <h3 class="popover-title"></h3>\
                        </div>\
                        <div class="popover-content"></div>\
                    </div>',
                html: true,
                placement: 'bottom'
            }).on('shown.bs.popover', function (e) {
                $('[data-dismiss="popover"]')
                    .off('click')
                    .on('click', function () {
                        $(e.currentTarget).click();
                    });
            });
        }
    });
});