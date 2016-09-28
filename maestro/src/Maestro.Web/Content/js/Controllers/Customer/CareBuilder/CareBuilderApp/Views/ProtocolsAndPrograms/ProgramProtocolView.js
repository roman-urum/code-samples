'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel'
], function ($, _, Backbone, ProtocolModel) {
    return Backbone.View.extend({
        template: _.template($("#programProtocolTemplate").html()),

        className: 'program-protocol-ci',

        model: ProtocolModel,

        close: function () {
            this.remove();
        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.$el.find('.dropdown-toggle').dropdown();

            this.$el.attr('data-id', this.model.get('id')).data('dragObjectType', this.model.get('id'));
            this.$el.attr('data-dragObjectType', 'dayElement').data('dragObjectType', 'dayElement');

            this.initDragBindings();
            this.initDropBindings();

            return this;
        },

        events: {
            'click .js-delete-protocol': 'onDeleteProtocol',
            'click .js-delete-protocol-occurence': 'onDeleteProtocol',
            'click .js-delete-protocol-series': 'onDeleteRecurrence',
            'click .js-set-recurrence': 'onSetRecurrence',
            'click .js-edit-recurrence': 'onSetRecurrence'
        },

        onDeleteProtocol: function () {
            this.$el.trigger("deleteProtocol", this.model);
        },

        onDeleteRecurrence: function () {
            this.$el.trigger("deleteRecurrence", this.model);
        },

        onSetRecurrence: function () {
            this.$el.trigger("setRecurrence", this.model);
        },

        initDragBindings: function () {
            var self = this;

            this.$el.draggable({
                appendTo: 'body',
                cursor: "move",
                containment: "parent",
                revert: true,
                start: function (event, ui) {
                    self.$el.addClass('program-protocol-draggable-ci');
                },
                stop: function (event, ui) {
                    self.$el.removeClass('program-protocol-draggable-ci');
                }
            });
        },

        initDropBindings: function () {
            var self = this,
                $elementBox = this.$el;

            this.$el.droppable({
                over: function (event, ui) {
                    $elementBox.addClass('placeholder');
                },

                out: function () {
                    $elementBox.removeClass('placeholder');
                },

                drop: function (event, ui) {
                    $elementBox.removeClass('placeholder');

                    if (ui.draggable.data('dragObjectType') !== 'dayElement') {
                        return;
                    }

                    self.$el.trigger('setOrderInDay', [ui.draggable.data('id'), self.model.get('id')]);
                }
            });
        }
    });
});