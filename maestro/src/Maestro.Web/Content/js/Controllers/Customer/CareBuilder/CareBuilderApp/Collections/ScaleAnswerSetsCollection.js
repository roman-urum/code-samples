'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel'
], function ($, _, Backbone, ScaleAnswerSetModel) {
    return Backbone.Collection.extend({
        model: ScaleAnswerSetModel,

        url: function () {
            return '/CareBuilder/ScaleAnswerSets';
        }
    });
});