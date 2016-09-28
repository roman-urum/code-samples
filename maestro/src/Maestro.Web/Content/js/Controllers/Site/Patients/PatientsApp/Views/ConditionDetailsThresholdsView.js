'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/ThresholdsApp',
    'Controllers/Constants'
], function ($, _, Backbone, ThresholdsApp, Constants) {
    return Backbone.View.extend({
        initialize: function () {
        },

        render: function () {
            //this.$el.html(this.template(this.model.attributes));

            this.onRender();

            return this;
        },

        onRender: function () {
            var self = this;

            var template = _.template(
                '<div class="thresholds-board">\
                    <h3 id="thresholds-title">Limit-Based Thresholds</h3>\
                    <div class="thresholds-container">\
                        <img src="/Content/img/spinner.gif" class="spinner" />\
                    </div>\
                    <br />\
                    <a class="btn btn-danger thresholds-save-button hidden">save</a>\
                </div>');

            var $thresholdsTabContainer = this.$el.html(template()),
                $thresholdsSaveButton = $thresholdsTabContainer.find('a.thresholds-save-button'),
                $thresholdsContentContainer = $thresholdsTabContainer.find('div.thresholds-container');

            var config = {

                crud: {
                    get: '/Settings/DefaultThresholds',
                    save: '/Settings/DefaultThreshold'
                },

                mode: Constants.thresholdAppModes.CONDITION,

                allowUnusedButton: false,

                conditionId: self.model.get('id'),

                isReadOnly: true,

                isReInit: true,

                events: {
                    beforeSave: function() {
                    },
                    afterSave: function () {
                    },
                    afterLoad: function() {
                        $thresholdsContentContainer.html(thresholdsApp.el);
                    }
                }
            };

            var thresholdsApp = new ThresholdsApp(config);
        }
    });
});