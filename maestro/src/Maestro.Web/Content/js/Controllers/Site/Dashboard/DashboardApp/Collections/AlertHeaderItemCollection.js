'use strict';

define([
    'Controllers/Site/Dashboard/DashboardApp/Models/PatientHeaderItemModel'
], function (PatientHeaderItemModel) {
    return Backbone.Collection.extend({
        model: PatientHeaderItemModel,
        maxLength: 5,

        initialize: function () {
            this.on('add', this.checkSize);
        },

        checkSize: function (item) {
            if (this.length > this.maxLength) {
                this.pop(item);
            }
        }
    });
});