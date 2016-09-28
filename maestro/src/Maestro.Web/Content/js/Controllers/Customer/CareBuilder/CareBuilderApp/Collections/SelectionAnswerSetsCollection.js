'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerSetModel
) {
    return Backbone.Collection.extend({
        model: SelectionAnswerSetModel,

        url: function () {
            return '/CareBuilder/SelectionAnswerSets';
        }
    });
});