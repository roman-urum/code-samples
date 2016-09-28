'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SelectionAnswerSetsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ScaleAnswerSetsCollection',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerSetsCollection,
    ScaleAnswerSetsCollection,
    Helpers
) {
    return Backbone.Model.extend({
        defaults: {
            selectionAnswerSets: new SelectionAnswerSetsCollection(),
            scaleAnswerSets: new ScaleAnswerSetsCollection()
        },

        parse: function (response) {
            response = Helpers.convertKeysToCamelCase(response);

            response.selectionAnswerSets = new SelectionAnswerSetsCollection(Helpers.convertKeysToCamelCase(response.selectionAnswerSets), { parse: true });
            response.scaleAnswerSets = new ScaleAnswerSetsCollection(Helpers.convertKeysToCamelCase(response.scaleAnswerSets), { parse: true });

            return response;
        },

        url: function () {
            return '/CareBuilder/AnswerSets';
        }
    });
});