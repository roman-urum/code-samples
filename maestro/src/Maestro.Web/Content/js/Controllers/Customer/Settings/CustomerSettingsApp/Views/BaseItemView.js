'use strict';

define([
    'jquery',
    'underscore',
    'backbone.marionette'
], function (
    $,
    _,
    Marionette
) {
    return Marionette.ItemView.extend({

        useSpinnerClass: 'use-spinner',

        initialize: function (options) {
            options = options || {};
            if (!options.model) throw 'Model is required';

            var self = this;
            if (this.useSpinner) {
                this.$el.addClass(this.useSpinnerClass);
            }

            this.model = options.model;
            this.listenTo(this.model, 'fetched', function () {
                self.render();
            });
        },

        render: function () {
            //run default render functionality
            Marionette.ItemView.prototype.render.apply(this, arguments);

            if (this.useSpinner && !this.model.isFetched) {
                this.showSpinner();
            }
        },

        showSpinner: function () {
            var tplFunc = _.template($('#spinnerTemplate').html());
            this.$el.append(tplFunc());
        }

    });
});