'use strict';

define([
    'jquery',
    'underscore',
    'backbone'
], function (
    $,
    _,
    Backbone
) {
    return Backbone.Collection.extend({

        initialize: function () {
            Backbone.Collection.prototype.initialize.apply(this, arguments);

            this.isFetched = false;
        },

        fetch: function (options) {
            options = options || {};

            var self = this;
            var successCb = options.success;

            self.isFetched = false;

            options.success = function () {
                self.each(function (model) {
                    model.isFetched = true;
                    //model.trigger('fetched');
                });

                self.isFetched = true;
                self.trigger('fetched');

                if (_.isFunction(successCb)) {
                    successCb.apply(self, arguments);
                }
            };

            return Backbone.Collection.prototype.fetch.call(this, options);
        }

    });
});