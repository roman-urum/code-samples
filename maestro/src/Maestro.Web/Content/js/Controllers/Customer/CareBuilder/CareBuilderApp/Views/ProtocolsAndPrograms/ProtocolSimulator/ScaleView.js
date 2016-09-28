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
    app
) {
    return Backbone.View.extend({

        template: _.template('<input class="scale" type="text" data-slider-min="<%=answerSet.lowValue%>" data-slider-max="<%=answerSet.highValue%>" data-slider-step="1" data-slider-value="0" data-slider-orientation="vertical" data-slider-reversed="true"/>'),

        className: 'simulator-content-answer pull-right',

        initialize: function () {

        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        }
    });
});