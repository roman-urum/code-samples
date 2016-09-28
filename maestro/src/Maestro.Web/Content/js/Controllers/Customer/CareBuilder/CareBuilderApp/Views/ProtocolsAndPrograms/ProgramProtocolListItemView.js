'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel'
], function (
    $,
    _,
    Backbone,
    AppGlobalVariables,
    ProtocolModel
) {
    return Backbone.View.extend({
        model: ProtocolModel,

        className: 'program-search-result-item-ci clearfix',

        template: _.template($('#programProtocolListItemTemplate').html()),

        close: function () {
            this.remove();
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            this.$el.data('name', this.model.get('name').value);
            this.$el.data('id', this.model.get('id'));

            this.initDragBindings();

            return this;
        },

        initDragBindings: function () {
            var self = this;

            this.$el.draggable({
                appendTo: 'body',
                cursor: "move",
                helper: 'clone',
                cursorAt: { top: 15, left: 116 },
                start: function (event, ui) { },
                stop: function (event, ui) { }
            });
        }
    });
});