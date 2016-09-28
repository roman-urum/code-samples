'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerChoiceModel'
], function ($, _, Backbone, SelectionAnswerChoiceModel) {
    return Backbone.Collection.extend({
        initialize: function () {
            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        model: SelectionAnswerChoiceModel
    });
});