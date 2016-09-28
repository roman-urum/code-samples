'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'backbone-nested',
    'Controllers/Customer/Settings/CustomerSettingsApp/Application',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneNested,
    App,
    Helpers
) {
    return Backbone.NestedModel.extend({
        initialize: function () {
            _.extend(this, new Backbone.Memento(this));
        },

        fetch: function (options) {
            options = options || {};

            var self = this;
            var successCb = options.success;

            self.isFetched = false;

            options.success = function () {
                self.isFetched = true;
                self.trigger('fetched');
                if (_.isFunction(successCb)) {
                    successCb.apply(self, arguments);
                }
            };

            Backbone.NestedModel.prototype.fetch.call(this, options);
        },

        parse: function (response) {
            return Helpers.convertKeysToCamelCase(response);
        }

    });
});