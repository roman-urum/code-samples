'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/TextMediaElementModel'
], function ($, _, Backbone, TextMediaElementModel) {
    return Backbone.Collection.extend({
        model: TextMediaElementModel,

        url: function () {
            return '/CareBuilder/TextMediaElements';
        }
    });

});