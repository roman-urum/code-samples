'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './ConditionsCollectionItemView'
], function ($, _, Backbone, ConditionsCollectionItemView) {
    return ConditionsCollectionItemView.extend({
        template: _.template($('#conditionsAssignedListItemTemplate').html())
    });
});