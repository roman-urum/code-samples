'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
], function (
    $,
    _,
    Backbone,
    App
) {
    return Backbone.View.extend({
        tagName: "ul",

        initialize: function () {

        },

        render: function () {

            // this.collection.each(function(person){
            // var personView = new AppGlobalVariables.Views.Person({model: person})

            // this.$el.append(personView.el);

            // }, this)
            // return this;
        }
    });
});