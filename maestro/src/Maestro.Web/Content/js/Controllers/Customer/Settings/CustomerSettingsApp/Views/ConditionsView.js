'use strict';

define([
    'jquery',
    'underscore',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    './BaseLayoutView',
    '../Models/ConditionModel',
    '../Models/ConditionEditLayoutModel',
    './ConditionsCollectionView',
    './ConditionEditLayoutView'
], function (
    $,
    _,
    App,
    BaseLayoutView,
    ConditionModel,
    ConditionEditLayoutModel,
    ConditionsCollectionView,
    ConditionEditLayoutView
) {
    return BaseLayoutView.extend({
        template: '#conditionsView',

        className: 'conditions-view',

        regions: {
            list: '.conditions-list',
            edit: '.condition-edit'
        },

        onRender: function () {
            var self = this;
            this.getRegion('list').show(new ConditionsCollectionView({ model: this.options.model }), { replaceElement: true });

            App.vent.on('conditions-view:showThresholdsTab', function () {
                self.showTab('thresholds');
            });
        },

        showTab: function (tabName) {
            tabName = tabName.toLowerCase();

            //show bootstrap tab
            this.$('.condition-details-tabs #tab-' + tabName).tab('show');
        },

        showCreateConditionPage: function () {
            this.getRegion('list').empty();

            this.getRegion('edit').show(new ConditionEditLayoutView({ model: new ConditionEditLayoutModel({ condition: new ConditionModel() }), action: 'create', subTab: 'Details' }));

            this.showTab('Details');

            this.toggleHeader('Create');
        },

        showEditConditionPage: function (conditionId, subTab) {
            this.getRegion('list').empty();

            var conditionModel = App.models.conditionsCollection.findWhere({ id: conditionId });

            if (!conditionModel) {
                return App.navigate('Settings/Conditions');
            }

            var model = new ConditionEditLayoutModel({ condition: conditionModel });

            this.getRegion('edit').show(new ConditionEditLayoutView({ model: model, action: 'edit', subTab: subTab }));

            this.showTab(subTab);

            this.toggleHeader('Edit');
        },

        toggleHeader: function (action) {
            this.$('.js-conditions-header').hide();

            switch (action) {
                case 'Create':
                {
                    this.$('.js-create-condition-header').show();

                    break;
                }

                case 'Edit':
                {
                    this.$('.js-edit-condition-header').show();

                    break;
                }

                default:
                {
                    this.$('.js-conditions-header').show();
                }
            }
        }
    });
});