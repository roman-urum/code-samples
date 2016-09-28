'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    './BaseModel'
], function (
    $,
    _,
    Backbone,
    BaseModel
) {
    return BaseModel.extend({
        url: function () {
            var conditionId = this.get('id'),
                url = '/Settings/CustomerConditions';

            if (conditionId) {
                url += ('?id=' + conditionId);
            }

            return url;
        },

        defaults: {
            name: '',
            description: '',
            tags: []
        },

        validation: {
            name: [{
                required: true,
                msg: 'Please enter Condition Name'
            }, {
                maxLength: 200,
                msg: 'Condition Name shouldn\'t exceed 200 letters'
            }],
            description: [{
                required: true,
                msg: 'Please enter Condition Description'
            }, {
                maxLength: 1024,
                msg: 'Condition Description shouldn\'t exceed 1024 letters'
            }]
        }
    });
});