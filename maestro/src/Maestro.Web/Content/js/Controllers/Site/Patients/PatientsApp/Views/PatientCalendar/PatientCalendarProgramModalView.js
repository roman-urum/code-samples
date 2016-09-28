'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'BackboneModelBinder'
], function ($, _, Backbone, app, Helpers, moment, BackboneModelBinder) {
    return Backbone.View.extend({

        template: _.template($('#patientCalendarProgramModalTemplate').html()),

        modelBinder: new BackboneModelBinder(),

        initialize: function () {
            this.listenTo(this.model, 'change:startDate change:endDay', this.updateEndDate);
            this.listenTo(this.model, 'change:timeType', this.updateProgramTimeInputState);
            this.listenTo(this.model, 'change:validHours', this.updateExpireMinutes);

            Backbone.Validation.bind(this);
        },

        render: function () {
            this.$el.html(this.template($.extend({}, this.model.attributes, {
                preferredSessionTime: moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("h:mm A")
            })));

            this.$el.find('#startDate-datetimepicker').datetimepicker({
                format: "YYYY-MM-DD",
                minDate: moment().format('YYYY-MM-DD')
            }).on('dp.change', function (e) {
                var startDate = $(this).find('#startDateDp').val();

                $(this).siblings('#startDate').val(startDate).trigger('change');
            });

            this.$el.find('#startDateDp').inputmask({
                mask: '9999-99-99',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            this.$el.find('#programTime-time').datetimepicker({
                format: 'LT'
            }).on('dp.change', function (e) {
                var time = $(this).find('#programTimeTp').val();

                $(this).siblings('#programTime').val(moment(time, ["h:mm A"]).format("HH:mm")).trigger('change');
            });

            this.$el.find('#programTimeTp').inputmask({
                mask: '9{1,2}:99 aM',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            this.modelBinder.bind(this.model, this.el);

            this.updateProgramTimeInputState();

            return this;
        },

        updateEndDate: function () {
            var startDate = this.model.get('startDate'),
                endDay = this.model.get('endDay') * 1,
                endDate = moment(startDate).add((endDay - 1), 'days').format('YYYY-MM-DD');

            this.model.set('estimatedEndDate', endDate);
        },

        updateProgramTimeInputState: function () {
            if (this.model.get('timeType') == 'defaultTime') {
                this.$el.find('#programTimeTp').prop('disabled', true);
                this.$el.find('#programTimeTp').val(moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("h:mm A")).trigger('change');
            } else {
                this.$el.find('#programTimeTp').prop('disabled', false);
            }
        },

        updateExpireMinutes: function () {
            var validHours = this.model.get('validHours');

            this.model.set('expireMinutes', validHours * 60);
        }
    });
});