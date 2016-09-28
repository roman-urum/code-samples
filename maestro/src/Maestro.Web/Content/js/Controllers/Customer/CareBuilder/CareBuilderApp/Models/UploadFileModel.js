'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Helpers',
    'backbone-model-file-upload'
], function (
    $,
    _,
    Backbone,
    Helpers
) {
    return Backbone.Model.extend({
        url: '/CareBuilder/UploadFile',

        fileAttribute: 'mediaFile',

        parse: function (response, options) {
            var result = Helpers.convertKeysToCamelCase(response);

            return result;
        }
    });
});