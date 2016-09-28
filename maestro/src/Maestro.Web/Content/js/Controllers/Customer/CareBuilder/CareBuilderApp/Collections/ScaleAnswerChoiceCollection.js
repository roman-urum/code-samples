'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerChoiceModel'
], function ($, _, Backbone, ScaleAnswerChoiceModel) {
    return Backbone.Collection.extend({
        model: ScaleAnswerChoiceModel
    });
});