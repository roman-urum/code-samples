'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'moment',
    'Controllers/Helpers',
    'BackboneBootstrapAlert',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarCollectionItemEventView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramModel',
    'Controllers/Site/Patients/PatientsApp/Models/ProgramModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarProgramModalView'
], function (
        $,
        _,
        Backbone,
        app,
        moment,
        Helpers,
        BackboneBootstrapAlert,
        BackboneBootstrapModal,
        PatientCalendarCollectionItemEventView,
        PatientCalendarProgramModel,
        ProgramModel,
        PatientCalendarProgramModalView
 ) {
    return Backbone.View.extend({
        className: 'calendar-day',

        template: _.template('<span class="calendar-day-value<%if( isToday ){%> calendar-day-today<%}%>"><%=day%></span>'),

        events: {

        },

        initialize: function () {

        },

        render: function () {

            this.$el.html(this.template(this.model.attributes));

            if (!this.model.get('isMonth'))
                this.$el.addClass('calendar-day-empty');

            this.model.get('calendarEvents').each(this.renderEvent, this);
            this.initDroppable();

            return this;
        },

        renderEvent: function (event) {
            app.views.patientCalendarCollectionItemEventView = new PatientCalendarCollectionItemEventView({ model: event });
            this.$el.append(app.views.patientCalendarCollectionItemEventView.render().el);

            return this;
        },

        initDroppable: function () {
            var self = this;

            this.$el.droppable({
                over: function (event, ui) {
                    var programId = ui.draggable.data('id'),
                        isAvailable = self.model.get('isAvailable');

                    if (isAvailable) {
                        // if (self.hasProtocol(programId))
                        //     self.$el.addClass('hover-droppable-disabled');
                        // else
                            self.$el.addClass('hover-droppable');
                    }
                },
                out: function () {
                    self.$el.removeClass('hover-droppable')
                            .removeClass('hover-droppable-disabled');
                },
                drop: function (event, ui) {
                    var programId = ui.draggable.data('id'),
                        duration = ui.draggable.data('duration'),
                        isAvailable = self.model.get('isAvailable');

                    // self.$el.removeClass('hover-droppable')
                    //         .removeClass('hover-droppable-disabled');
                    if (isAvailable /*&& !self.hasProtocol(programId)*/)
                        self.renderProgramModal(programId, self.model.get('date'), duration);
                }
            });

            return this;
        },

        renderProgramModal: function (programId, startDate, duration) {

            app.models.patientCalendarProgramModel = new PatientCalendarProgramModel({
                programId: programId,
                startDate: startDate,
                startDateDp: startDate,
                estimatedEndDate: moment(startDate).add((duration - 1), 'days').format('YYYY-MM-DD'),
                programTime: app.models.patientModel.get('preferredSessionTime'), 
                programTimeTp: moment( app.models.patientModel.get('preferredSessionTime') , ["HH:mm"]).format("h:mm A"),
                endDay: duration
            });

            var programModalView = new BackboneBootstrapModal({
                title: 'Schedule Program',
                content: new PatientCalendarProgramModalView({ model: app.models.patientCalendarProgramModel }),
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
            .open()
            .on('shown', function () {

            })
            .on('ok', function () {

                if (app.models.patientCalendarProgramModel.isValid(true)) {
                    programModalView.$el.find('.btn.ok').data('loading-text', 'Creating...').button('loading');

                    programModalView.$el.find('.alert-danger').addClass('hidden');

                    var programTime = app.models.patientCalendarProgramModel.get('programTime'),
                        startDate = app.models.patientCalendarProgramModel.get('startDate');

                    app.models.patientCalendarProgramModel.store();
                    app.models.patientCalendarProgramModel.set('startDate', startDate + ' ' + programTime)

                    app.models.patientCalendarProgramModel.save(null, {
                        success: function (model, response, options) {

                            programModalView.close();

                            app.vent.trigger("updateCurrentMonth");

                            var alert = new BackboneBootstrapAlert({
                                alert: 'success',
                                message: 'Program was successfully added',
                                autoClose: true
                            })
                            .show();

                        },
                        error: function (model, xhr, options) {

                            app.models.patientCalendarProgramModel.restore();
                            programModalView.$el.find('.btn.ok').button('reset');

                            var errorAlert = programModalView.$el.find('.alert-danger');
                            errorAlert.html(xhr.responseJSON.ErrorMessage);
                            errorAlert.removeClass('hidden');

                        }

                    });

                }

                programModalView.preventClose();

            })
            .on('cancel', function () {
                app.models.patientCalendarProgramModel.restore();
            });
        },

        hasProtocol: function (programId) {
            var dayEvents = this.model.get('calendarEvents'),
                result = false;

            dayEvents.each(function (event) {
                if (event.get('programId') == programId) {
                    result = true;
                };
            });

            return result;
        }
    });
});