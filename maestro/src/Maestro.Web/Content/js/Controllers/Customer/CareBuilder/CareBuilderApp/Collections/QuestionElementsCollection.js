'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel'
], function ($, _, Backbone, QuestionElementModel) {
    return Backbone.Collection.extend({

        model: QuestionElementModel,

        url: function () {
            return '/CareBuilder/Search';
        },

        parse: function (response) {

            return $.map(response, function (model, index) {

                var result = new QuestionElementModel({
                    id: model.Id,
                    'questionElementString': { 'value': model.Name },
                    'tags': model.tags
                });

                return result;
            });
        }
    });
});