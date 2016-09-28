'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers
) {
    return Backbone.View.extend({

        className: 'panel panel-default',

        template: _.template($('#openEndedAnswerSetListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            return this;
        }

    });
});