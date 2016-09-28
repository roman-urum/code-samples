'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/Views/PatientConditionView'
], function ($, _, Backbone, PatientConditionView) {
    return Backbone.View.extend({

        tagName: 'ul',

        className: 'list-unstyled',

        initialize: function () {
            this.listenTo(this.collection, 'change', this.render);
        },

        render: function () {
            var self = this;

            this.collection.each(function (model) {
                var conditionView = new PatientConditionView({ model: model });

                self.$el.append(conditionView.render().$el);
            });

            return this;
        }
    });
});