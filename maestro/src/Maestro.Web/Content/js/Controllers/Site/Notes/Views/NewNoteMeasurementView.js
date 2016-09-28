'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'note-measurement-block',
        template: _.template($('#addNoteMeasurementTemplate').html()),

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});