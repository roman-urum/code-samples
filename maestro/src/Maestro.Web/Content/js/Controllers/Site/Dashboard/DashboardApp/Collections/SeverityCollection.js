'use strict';

define([
    'Controllers/Site/Dashboard/DashboardApp/Models/FilterTypeModel'
], function (FilterTypeModel) {
    return Backbone.Collection.extend({
        model: FilterTypeModel,
        comparator: function(item) {
            return -item.get('severity');
        }
    });
});