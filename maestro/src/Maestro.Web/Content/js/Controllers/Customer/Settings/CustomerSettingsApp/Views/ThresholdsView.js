'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Root/Thresholds/ThresholdsApp',
    './BaseItemView',
    'Controllers/Constants'
], function (
    $,
    _,
    Marionette,
    App,
    ThresholdsApp,
    BaseItemView,
    Constants
) {
    return BaseItemView.extend({
        template: '#thresholdsView',

        className: 'thresholds-view',

        moveToNexTab: false,

        onRender: function () {
            var self = this;

            var template = _.template(
                '<div class="thresholds-board">\
                    <h3 id="thresholds-title">Limit-Based Thresholds</h3>\
                    <div class="thresholds-container">\
                        <img src="/Content/img/spinner.gif" class="spinner" />\
                    </div>\
                    <br />\
                    <div class="well">\
                        <div class="row">\
                            <div class="col-sm-12">\
                                <a class="btn btn-danger thresholds-save-and-next-tab-button hidden">Save and Next Tab</a>\
                                <a class="btn btn-primary thresholds-save-and-exit-button hidden">Save and Exit</a>\
                            </div>\
                        </div>\
                    </div>\
                </div>');

            var $thresholdsTabContainer = self.$('#ci-manage-thresholds').html(template()),
                $thresholdsSaveGoToNextTabButton = $thresholdsTabContainer.find('a.thresholds-save-and-next-tab-button'),
                $thresholdsSaveAndExitButton = $thresholdsTabContainer.find('a.thresholds-save-and-exit-button'),
                $thresholdsContentContainer = $thresholdsTabContainer.find('div.thresholds-container');

            var config = {

                crud: {
                    get: '/Settings/DefaultThresholds',
                    save: '/Settings/DefaultThreshold'
                },

                mode: Constants.thresholdAppModes.DEFAULT,

                allowUnusedButton: false,

                isReInit: true,

                events: {
                    afterValidation: function (isValid) {},
                    beforeSave: function() {
                        $thresholdsSaveGoToNextTabButton.data('loading-text', 'Updating...').button('loading');
                        $thresholdsSaveAndExitButton.data('loading-text', 'Updating...').button('loading');
                    },
                    afterSave: function() {
                        $thresholdsSaveGoToNextTabButton.button('reset');
                        $thresholdsSaveAndExitButton.button('reset');

                        if (self.moveToNexTab) {
                            App.router.navigate('/Settings/Conditions', { trigger: true, replace: true });
                        } else {
                            //thresholdsApp.views.mainView.render();
                            thresholdsApp.views.mainView.renderThresholdsSeverityCollectionsView();
                        }                       

                    },
                    afterLoad: function() {
                        $thresholdsSaveGoToNextTabButton
                            .removeClass('hidden')
                            .on('click', function(e) {
                                e.preventDefault();
                                self.moveToNexTab = true;
                                thresholdsApp.save();                                
                            });

                        $thresholdsSaveAndExitButton
                            .removeClass('hidden')
                            .on('click', function(e) {
                                e.preventDefault();
                                self.moveToNexTab = false;
                                thresholdsApp.save();

                            });
                        $thresholdsContentContainer.html(thresholdsApp.el);
                    }
                }
            };

            var thresholdsApp = new ThresholdsApp(config);
        }
    });
});