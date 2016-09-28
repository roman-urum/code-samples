'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SearchModel'
], function (
    $,
    _,
    Backbone,
    AppGlobalVariables,
    SearchModel
) {
    return Backbone.View.extend({

        className: 'panel panel-default',

        model: SearchModel,

        template: _.template($('#programListItemTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        events: {
            'click .js-edit-program': 'editProgram'
        },

        editProgram: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                AppGlobalVariables.router.navigate(href, {
                    trigger: true
                });
            }
        }
    });
});