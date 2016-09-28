'use strict';

define([
    'jquery',
    'underscore',
    './BaseCompositeView',
    './ConditionItemView',
    './NoConditionsView'
], function (
    $,
    _,
    BaseCompositeView,
    ConditionItemView,
    NoConditionsView
) {
    return BaseCompositeView.extend({
        className: 'conditions-collection-view',

        template: '#conditionsCollectionView',

        childView: ConditionItemView,

        childViewContainer: 'tbody',

        emptyView: NoConditionsView,

        useSpinner: true
    });
});