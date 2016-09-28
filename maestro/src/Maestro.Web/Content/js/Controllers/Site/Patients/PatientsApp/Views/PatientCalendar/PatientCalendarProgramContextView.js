'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'BackboneBootstrapAlert',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarProgramModalView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramTerminateModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarProgramTerminateModalView',
    'Controllers/Site/Patients/PatientsApp/Models/PatientCalendarProgramDeleteModel',
    'Controllers/Site/Patients/PatientsApp/Views/PatientCalendar/PatientCalendarProgramDeleteModalView'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        moment,
        BackboneBootstrapAlert,
        BackboneBootstrapModal,
        PatientCalendarProgramModel,
        PatientCalendarProgramModalView,
        PatientCalendarProgramTerminateModel,
        PatientCalendarProgramTerminateModalView,
        PatientCalendarProgramDeleteModel,
        PatientCalendarProgramDeleteModalView
) {
    return Backbone.View.extend({

        className: 'dropdown',

        template: _.template('<a data-toggle="dropdown" class="dropdown-program hidden"></a><ul class="dropdown-menu" role="menu"></ul>'),

        events: {
            'click .js-reschedule-program': 'rescheduleProgram',
            'click .js-view-program': 'viewProgram',
            'click .js-terminate-program': 'terminateProgram',
            'click .js-delete-program': 'deleteProgram'
        },

        initialize: function () {

            _.bindAll(this, "viewProgramShown", "rescheduleProgramSave", "terminateProgramSave");
        },

        render: function (options) {

            var calendarProgramId = this.model.get('calendarProgramId');

            this.$el.html(this.template());
            this.$el.find(".dropdown-program").dropdown("toggle");
            this.$el.css({
                display: "block",
                left: options.contextEvent.offsetX,
                top: options.contextEvent.offsetY,
            });

            app.models.patientCalendarProgramModel = app.collections.patientCalendarProgramsCollection.findWhere({ id: calendarProgramId });

            var programStartDate = app.models.patientCalendarProgramModel.get('startDate'),
                programExpireMinutes = app.models.patientCalendarProgramModel.get('expireMinutes'),
                programEndDay = app.models.patientCalendarProgramModel.get('endDay');

            app.models.patientCalendarProgramModel.set({
                'startDate': moment(programStartDate).format('YYYY-MM-DD'),
                'startDateDp': moment(programStartDate).format('YYYY-MM-DD'),
                'programTime': moment(programStartDate).format('h:mm A'),
                'programTimeTp': moment(programStartDate).format('h:mm A'),
                'validHours': programExpireMinutes / 60,
                'estimatedEndDate': moment(programStartDate).add((programEndDay - 1), 'days').format('YYYY-MM-DD'),
                'timeType': (moment(programStartDate).format('hh:mm') == '08:30') ? 'defaultTime' : 'differentTime'
            });

            this.renderContext();

            return this;
        },

        renderContext: function () {
            var todayDate = moment().format('YYYY-MM-DD'),
                todayValue = moment(todayDate).format('x'),
                startDay = app.models.patientCalendarProgramModel.get('startDay') * 1,
                endDay = app.models.patientCalendarProgramModel.get('endDay') * 1 - 1,
                startDate = app.models.patientCalendarProgramModel.get('startDate'),
                startDateValue = moment(startDate).format('x'),
                finishDateValue = moment(startDate).add((endDay - startDay), 'days'),
                isStarted = startDateValue <= todayValue && todayValue <= finishDateValue,
                isFinished = (todayValue > finishDateValue) ? true : false;

            if (isStarted || isFinished) {
                this.$el.find('.dropdown-menu').html('<li><a tabindex="-1" class="js-view-program">View Program</a></li>');
            } else {
                this.$el.find('.dropdown-menu').html('<li><a tabindex="-1" class="js-reschedule-program">Reschedule Program</a></li>' +
                                                     '<li><a tabindex="-1" class="js-delete-program">Delete</a></li>');
            }

            if (isStarted && !isFinished) {
                this.$el.find('.dropdown-menu').append('<li><a tabindex="-1" class="js-terminate-program">Terminate Program</a></li>');
            }

        },

        showProgramModal: function (functionContent, functionShown, functionOk, functionCancel, title) {
            var programName = this.model.get('name'),
                title = title ? title + ' ' + programName + ' Program' : programName + ' Program';

            this.programModalView = new BackboneBootstrapModal({
                title: title, // (' + + 'days)'
                content: functionContent,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
            .open()
            .on('shown', function () {
                if (typeof (functionShown) == "function") {
                    functionShown();
                }
            })
            .on('ok', function () {
                if (typeof (functionOk) == "function") {
                    functionOk();
                }
            })
            .on('cancel', function () {
                if (typeof (functionCancel) == "function") {
                    functionCancel();
                }
            });

        },

        rescheduleProgram: function () {

            this.$el.remove();

            this.showProgramModal(
                new PatientCalendarProgramModalView({ model: app.models.patientCalendarProgramModel }),
                this.rescheduleProgramShown,
                this.rescheduleProgramSave,
                this.rescheduleProgramCancel,
                'Schedule'
            );

        },

        rescheduleProgramShown: function () {
            app.models.patientCalendarProgramModel.store();
        },

        rescheduleProgramSave: function () {

            var self = this;

            if (app.models.patientCalendarProgramModel.isValid(true)) {

                this.programModalView.$el.find('.btn.ok').data('loading-text', 'Updating...').button('loading');
                this.programModalView.$el.find('.alert-danger').addClass('hidden');

                var programTime = app.models.patientCalendarProgramModel.get('programTime'),
                    startDate = app.models.patientCalendarProgramModel.get('startDate');

                // app.models.patientCalendarProgramModel.store();
                app.models.patientCalendarProgramModel.set('startDate', startDate + ' ' + programTime)
                app.models.patientCalendarProgramModel.save(null, {
                    success: function (model, response, options) {

                        self.programModalView.close();

                        app.vent.trigger("updateCurrentMonth");

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Program was successfully updated',
                            autoClose: true
                        })
                        .show();

                    },
                    error: function (model, xhr, options) {

                        app.models.patientCalendarProgramModel.restore();
                        self.programModalView.$el.find('.btn.ok').button('reset');

                        var errorAlert = self.programModalView.$el.find('.alert-danger');
                        errorAlert.html(xhr.responseJSON.ErrorMessage);
                        errorAlert.removeClass('hidden');

                    }
                });

            }

            this.programModalView.preventClose();
        },

        rescheduleProgramCancel: function () {
            app.models.patientCalendarProgramModel.restore();
        },

        viewProgram: function () {
            this.$el.remove();
            this.showProgramModal(
                new PatientCalendarProgramModalView({ model: app.models.patientCalendarProgramModel }),
                this.viewProgramShown,
                null,
                null,
                null
            );
        },

        viewProgramShown: function () {
            this.programModalView.$el.find(':input').prop('disabled', true);
        },

        terminateProgram: function () {
            var self = this,
                programName = this.model.get('name'),
                calendarProgramId = this.model.get('calendarProgramId'),
                startDate = app.models.patientCalendarProgramModel.get('startDate'),
                endDay = app.models.patientCalendarProgramModel.get('endDay') * 1 - 1,
                nowDate = moment().format('YYYY-MM-DD'),
                endDate = moment(startDate).add(endDay, 'days'),
                enabledDates = Helpers.getDates(nowDate, endDate);

            app.models.patientCalendarProgramTerminateModel = new PatientCalendarProgramTerminateModel();
            app.models.patientCalendarProgramTerminateModel.set({
                enabledDates: enabledDates,
                terminationDp: enabledDates[0],
                terminationUtc: enabledDates[0],
                calendarProgramId: calendarProgramId
            });

            this.$el.remove();

            this.showProgramModal(
                new PatientCalendarProgramTerminateModalView({ model: app.models.patientCalendarProgramTerminateModel }),
                null,
                this.terminateProgramSave,
                null,
                'Terminate'
            );

        },

        terminateProgramSave: function () {
            var self = this;

            this.programModalView.$el.find('.btn.ok').data('loading-text', 'Updating...').button('loading');
            this.programModalView.$el.find('.alert-danger').addClass('hidden');

            app.models.patientCalendarProgramTerminateModel.save(null, {
                success: function (model, response, options) {

                    self.programModalView.close();

                    app.vent.trigger("updateCurrentMonth");

                    var alert = new BackboneBootstrapAlert({
                        alert: 'success',
                        message: 'Program was successfully updated',
                        autoClose: true
                    })
                    .show();

                },
                error: function (model, xhr, options) {

                    self.programModalView.$el.find('.btn.ok').button('reset');

                    var errorAlert = self.programModalView.$el.find('.alert-danger');
                    errorAlert.html(xhr.responseJSON.ErrorMessage);
                    errorAlert.removeClass('hidden');

                }
            });

            this.programModalView.preventClose();

        },

        deleteProgram: function () {
            app.models.patientCalendarProgramDeleteModel = new PatientCalendarProgramDeleteModel();
            app.models.patientCalendarProgramDeleteModel.set({
                id: this.model.get('calendarProgramId'),
                calendarProgramId: this.model.get('calendarProgramId')
            });

            app.views.patientCalendarProgramDeleteModalView = new PatientCalendarProgramDeleteModalView();

            var deleteModalView = new BackboneBootstrapModal({
                title: 'Delete ' + this.model.get('name') + ' Program',
                content: app.views.patientCalendarProgramDeleteModalView,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                },
                okText: 'Delete'
            }).open().on('ok', function () {

                deleteModalView.$el.find('.btn.ok').data('loading-text', 'Updating...').button('loading');

                deleteModalView.$el.find('.alert-danger').addClass('hidden');
                app.models.patientCalendarProgramDeleteModel.destroy({
                    success: function (model, response, options) {
                        deleteModalView.close();

                        app.vent.trigger("updateCurrentMonth");

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Program was successfully deleted',
                            autoClose: true
                        }).show();
                    },
                    error: function (model, xhr, options) {

                        deleteModalView.$el.find('.btn.ok').button('reset');

                        var errorAlert = deleteModalView.$el.find('.alert-danger');
                        errorAlert.html(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : "Response status: " + xhr.statusText);
                        errorAlert.removeClass('hidden');

                    }

                });

                deleteModalView.preventClose();

            }).on('cancel', function () {
                //console.log('delete canceled');
            });
        }

    });
});