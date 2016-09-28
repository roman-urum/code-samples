'use strict';

define([
    'underscore',
    'backbone'
], function (_, Backbone) {
    return Backbone.Model.extend({
        initialize: function () {

        },

        defaults: {
            name: '',
            fileName: '',
            contentType: '',
            tags: []
        },

        addAffectedAttribute: function (attributeName) {
            if (!this.affectedAttributes) this.affectedAttributes = [];

            if (!_.contains(this.affectedAttributes, attributeName)) {
                this.affectedAttributes.push(attributeName);
            }
        }
    });
});