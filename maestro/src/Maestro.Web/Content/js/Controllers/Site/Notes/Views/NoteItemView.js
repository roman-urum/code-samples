'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'note-block',
        template: _.template($('#notesItemTemplate').html()),
        $collapseButton: $('<button type="button" class="btn btn-primary btn-xs collapse-button">Collapse</button>'),
        classCollapsed: 'collapsed',
        text: {
            hide: 'Collapse',
            show: 'Expand'
        },

        events: {
            'click.collapse-block button.collapse-button': 'collapseEvent'
        },

        initialize: function () {
            this.bind();
        },

        bind: function () {
            this.collapseEvent = this.collapseEvent.bind(this);
        },

        collapseEvent: function (e) {
            if (!this.$button) {
                this.$button = $(e.currentTarget);
            }

            if (this.$el.hasClass(this.classCollapsed)) {
                this.$el.removeClass(this.classCollapsed);
                this.$button.text(this.text.show);
            } else {
                this.$el.addClass(this.classCollapsed);
                this.$button.text(this.text.hide);
            }
        },

        detectCollapse: function () {
            var $textBlock = this.$el.find('.note-block-text'),
                $text = this.$el.find('.note-block-full-text');

            if ($text.height() > $textBlock.height()) {
                this.$el
                    .addClass('shadow')
                    .append('<br/>')
                    .append('<button type="button" class="btn btn-primary btn-xs collapse-button">' + this.text.show + '</button>');
            }
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            setTimeout(this.detectCollapse.bind(this), 0);

            return this;
        }
    });
});