'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    SelectionAnswerSetModel
) {
    return Backbone.View.extend({
        model: SelectionAnswerSetModel,

        template: _.template($('#selectionAnswerSetListItemBodyTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});