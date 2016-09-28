'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramWeekModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramDayView'
], function ($, _, Backbone, BackboneCollectionBinder, ProgramWeekModel, ProgramDayView) {
    return Backbone.View.extend({
        className: 'program-week-ci',

        model: ProgramWeekModel,

        collectionBinder: undefined,

        initialize: function () {

            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(
                function (model) {
                    return new ProgramDayView({ model: model });
                });
            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);
        },

        template: _.template($("#programWeekTemplate").html()),

        events: {

        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.collectionBinder.bind(this.model.get('days'), this.$el.find('.js-days'));

            return this;
        }
    });
});