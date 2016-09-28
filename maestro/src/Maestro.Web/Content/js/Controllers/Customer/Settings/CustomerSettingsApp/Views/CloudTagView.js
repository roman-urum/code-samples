'use strict';

define([
    'jquery',
    'underscore',
    './BaseItemView'
], function (
    $,
    _,
    BaseItemView
) {
    return BaseItemView.extend({
        tagName: 'a',

        render: function() {
            this.$el.attr('rel', this.model.get('rate'));
            this.$el.html(this.model.get('name'));

            return this;
        }
    });
});