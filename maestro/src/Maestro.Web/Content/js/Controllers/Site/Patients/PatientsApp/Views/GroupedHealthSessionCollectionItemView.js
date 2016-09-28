'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Site/Patients/PatientsApp/Models/InvalidateMeasurementModel',
    'Controllers/Site/Patients/PatientsApp/Collections/UngroupedHealthSessionCollection',
    'Controllers/Site/Patients/PatientsApp/Views/UngroupedHealthSessionCollectionView'
], function ($, _, Backbone, app, Helpers, moment, InvalidateMeasurementModel, UngroupedHealthSessionCollection, UngroupedHealthSessionCollectionView) {
    return Backbone.View.extend({

        className: 'panel panel-default',

        template: _.template('<div class="panel-heading collapsed panel-heading-toggle-ci" data-toggle="collapse" data-parent="#accordion" href="#<%=id%>">\
                                <table class="table">\
                                    <tr>\
                                        <td class="col-md-2"><%=scheduledDate%></td>\
                                        <td class="col-md-2"><%=scheduledTime%></td>\
                                        <td class="col-md-4"><%=name%></td>\
                                        <td class="col-md-4"></td>\
                                    </tr>\
                                </table>\
                            </div>\
                            <div id="<%=id%>" class="panel-collapse collapse">\
                                <table class="table table-striped js-answers"></table>\
                            </div>'),

        events: {

        },

        initialize: function () {

        },

        render: function () {
            var completed = this.model.get('completed'),
                healthSessionType = this.model.get('healthSessionType');

            this.model.set('scheduledDate', moment.parseZone(completed).format('MM/DD/YYYY'));
            this.model.set('scheduledTime', moment.parseZone(completed).format('h:mm A (Z)'));

            if (healthSessionType) {

                var measurementId = this.model.get('id');

                this.model.set('answered', completed);

                app.collections.groupedHealthSessionInnerCollection[measurementId] = new UngroupedHealthSessionCollection(this.model.attributes);
                app.views.groupedHealthSessionInnerCollectionView[measurementId] = new UngroupedHealthSessionCollectionView({ collection: app.collections.groupedHealthSessionInnerCollection[measurementId] });

                this.$el.html(app.views.groupedHealthSessionInnerCollectionView[measurementId].render().el);

            } else {
                this.$el.html(this.template(this.model.attributes));
                this.renderElements();
            }

            return this;
        },

        renderElements: function () {
            var self = this,
                elements = this.model.get('elements'),
                calendarItemId = this.model.get('calendarItemId');

            _.each(elements, function (element) {
                element.calendarItemId = calendarItemId;
            });

            app.collections.groupedHealthSessionInnerCollection[calendarItemId] = new UngroupedHealthSessionCollection(elements);
            app.views.groupedHealthSessionInnerCollectionView[calendarItemId] = new UngroupedHealthSessionCollectionView({ collection: app.collections.groupedHealthSessionInnerCollection[calendarItemId] });

            this.$el.find('.js-answers').after(app.views.groupedHealthSessionInnerCollectionView[calendarItemId].render().el);
            this.$el.find('.js-answers').remove();
        }
    });
});