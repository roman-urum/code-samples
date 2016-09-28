'use strict';

define([
    'jquery',
    'underscore',
    'BackboneModelBinder',
    './BaseLayoutView',
    './ConditionDetailsView',
    './ConditionThresholdsView',
    './ConditionContentView',
    '../Models/ConditionModel',
    '../Models/ThresholdsStubModel',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application'
], function (
    $,
    _,
    BackboneModelBinder,
    BaseLayoutView,
    ConditionDetailsView,
    ConditionThresholdsView,
    ConditionContentView,
    ConditionModel,
    ThresholdsStubModel,
    App
) {
    return BaseLayoutView.extend({
        template: '#conditionEditLayoutView',

        className: 'condition-edit-view',

        useSpinner: true,

        regions: {
            details: '#details-container',
            thresholds: '#thresholds-container',
            content: '#content-container'
        },

        events: {
            'click .js-cancel-button': 'doNotSaveAnyChanges',
            'click .condition-details-tabs .js-tab-link': 'navigateConditionTab'
        },

        childEvents: {
            'show:thresholds-tab': function (childView) {
                App.navigate('Settings/Conditions/' + childView.model.get('id') + '/Thresholds');
            },
            'show:content-tab': function (childView) {
                App.navigate('Settings/Conditions/' + childView.model.get('id') + '/Content');
            }
        },

        initialize: function (options) {
            this.modelBinder = new BackboneModelBinder();
            Backbone.Validation.bind(this);

            this.isCreate = (options.action === 'create');
            this.subTab = options.subTab;

            if (!this.isCreate) {
                this.model.store();
            }
        },

        onBeforeRender: function () {

        },

        onRender: function () {
            this.modelBinder.bind(this.model, this.el);

            this.hideCreateConditionButton();

            switch (this.subTab) {
                case 'Details':
                {
                    var conditionDetailsView = new ConditionDetailsView({
                        model: this.isCreate ? new ConditionModel() : this.model.get('condition')
                    });

                    this.getRegion('details').show(conditionDetailsView);

                    break;
                }
                case 'Thresholds':
                {
                    this.showThresholdsTab();

                    break;
                }
                case 'Content':
                {
                    this.showConetntTab();                    
                    break;
                }
            }

            if (this.isCreate) {
                this.disableTabsOnCreate();
            }
        },

        hideCreateConditionButton: function () {
            $('.js-create-condition').hide();
        },

        disableTabsOnCreate: function () {
            this.$('.condition-details-tabs li:not(.active)').addClass('disabled');
        },

        doNotSaveAnyChanges: function (e) {
            e.preventDefault();

            if (!this.isCreate) {
                this.model.restore();
            }

            App.navigate('Settings/Conditions');
        },

        navigateConditionTab: function (e) {
            
            e.preventDefault();

            var target = $(e.target).data('href');

            App.navigate('Settings/Conditions/' + this.model.get('condition').get('id') + '/' + target, {
                trigger: true
            });
        },

        showThresholdsTab: function () {
            this.getRegion('details').empty();            

            this.getRegion('thresholds').show(new ConditionThresholdsView({ model: this.isCreate ? new ConditionModel() : this.model.get('condition') }));
        },

        showConetntTab: function() {
            
            this.getRegion('details').empty();
            this.getRegion('content').show(new ConditionContentView({model: this.model.get('condition')}));

        }

    });
});