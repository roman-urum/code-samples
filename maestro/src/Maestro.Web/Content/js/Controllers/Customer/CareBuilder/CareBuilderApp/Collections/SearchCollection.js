'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SearchModel'
], function ($, _, Backbone, SearchModel) {
    return Backbone.Collection.extend({
        model: SearchModel,

        url: function () {
            return '/CareBuilder/Search';
        }
    });
});