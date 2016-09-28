'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'BackboneBootstrapModal',
    'BackboneBootstrapAlert',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionModel',
    'Controllers/Site/Patients/PatientsApp/Models/PatientHealthSessionTerminateModel'
], function (
        $,
        _,
        Backbone,
        app,
        Helpers,
        moment,
        BackboneBootstrapModal,
        BackboneBootstrapAlert,
        PatientHealthSessionModel,
        PatientHealthSessionTerminateModel
) {
    return Backbone.View.extend({

        className: 'dropdown',

        recurrenceRules: [],

        isRecurring: false,

        template: _.template('<a data-toggle="dropdown" class="dropdown-program hidden"></a><ul class="dropdown-menu" role="menu"></ul>'),

        events: {
            'click .js-edit-session': 'editSession',
            'click .js-view-session': 'viewSession',
            'click .js-delete-session': 'deleteSession',
            'click .js-terminate-session': 'terminateSession'
        },

        initialize: function () {

            // _.bindAll(this, "viewProgramShown", "rescheduleProgramSave", "terminateProgramSave");
        },

        render: function (options) {

            this.$el.html(this.template());
            this.$el.find(".dropdown-program").dropdown("toggle");
            this.$el.css({
                display: "block",
                left: options.contextEvent.offsetX,
                top: options.contextEvent.offsetY,
            });

            this.isRecurring = this.model.get('recurrenceRules').length ? true : false;

            this.renderContext();

            return this;
        },

        renderContext: function () {
            var recurrenceRules = this.model.get('recurrenceRules') ? this.model.get('recurrenceRules')[0] : false,
        		startDate = recurrenceRules && recurrenceRules.startDate,
                endDate = recurrenceRules && recurrenceRules.endDate,
        		due = this.model.get('due'),
        		expireMinutes = this.model.get('expireMinutes'),
        		frequencyArray = ['daily', 'weekly', 'monthly'];

            var todayValue = moment().format('x'),
        		occurredDayValue = this.isRecurring ? moment(startDate).format('x') : moment(due).format('x'),
        		isOccurred = (todayValue > occurredDayValue) ? true : false;
            
            this.model.set({
                isRecurring: this.isRecurring,
                expireHours: expireMinutes / 60,
                startDate: this.isRecurring ? moment(startDate).format('YYYY-MM-DD') : moment(due).format('YYYY-MM-DD'),
                startDateDp: this.isRecurring ? moment(startDate).format('MM/DD/YYYY') : moment(due).format('MM/DD/YYYY'),
                sessionTime: this.isRecurring ? moment(startDate).format('HH:mm') : moment.parseZone(due).format('HH:mm'),
                sessionTimeTp: this.isRecurring ? moment(startDate).format('h:mm A') : moment.parseZone(due).format('h:mm A')
            });

            this.model.set({
                timeType: (moment(app.models.patientModel.get('preferredSessionTime'), ["HH:mm"]).format("HH:mm") == this.model.get('sessionTime')) ? 'defaultTime' : 'differentTime'
            });
            
            if (this.isRecurring) {
                this.recurrenceRules = _.extend({}, recurrenceRules);

                this.recurrenceRules = _.extend(this.recurrenceRules, {
                    endDate: moment(recurrenceRules.endDate).format('YYYY-MM-DD'),
                    endDateDp: moment(recurrenceRules.endDate).format('MM/DD/YYYY'),
                    frequency: frequencyArray[recurrenceRules.frequency - 1],
                    intervalDaily: (recurrenceRules.frequency == 1) ? recurrenceRules.interval : 4,
                    intervalWeekly: (recurrenceRules.frequency == 2) ? recurrenceRules.interval : 1,
                    intervalMonthly: (recurrenceRules.frequency == 3) ? recurrenceRules.interval : 1
                });

                var completedDayValue = moment(endDate).format('x'),
                    isCompleted = (todayValue > completedDayValue) ? true : false;

            }
            
            if (!isOccurred) {
                this.$el.find('.dropdown-menu').html('<li><a tabindex="-1" class="js-edit-session">Edit Session</a></li>' +
                                                        '<li><a tabindex="-1" class="js-delete-session">Delete Session</a></li>');
            } else {
                if (this.isRecurring && !isCompleted) {
                    this.$el.find('.dropdown-menu').html('<li><a tabindex="-1" class="js-terminate-session">Terminate Session</a></li>'+
                                                         '<li><a tabindex="-1" class="js-edit-session">View Session</a></li>');
                } else {
                    this.$el.find('.dropdown-menu').html('<li><a tabindex="-1" class="js-view-session">View Session</a></li>');
                }
            }
        },

        editSession: function () {
            app.views.patientCalendarView.editHealthSessionCalendar({ isEdit: true, isDefault: false });

            if (app.collections.patientProtocolSearchCollection.isFetched) {

                this.updateSessionModel();
            } else {

                this.listenTo(app.collections.patientProtocolSearchCollection, 'fetched', this.updateSessionModel);
            }
        },

        viewSession: function () {
            app.views.patientCalendarView.editHealthSessionCalendar({ isEdit: true, isDefault: false });

            if (app.collections.patientProtocolSearchCollection.isFetched) {

                this.updateViewSessionModel();
            } else {

                this.listenTo(app.collections.patientProtocolSearchCollection, 'fetched', this.updateViewSessionModel);
            }
        },

        updateViewSessionModel: function () {

            this.updateSessionModel();

            $('[href="#tab-session-creation"]').addClass('disabled');
            $('#tab-session-schedule input').prop('readonly', true);
            $('#tab-session-schedule input[type="checkbox"]').prop('disabled', true);
            $('#tab-session-schedule input[type="radio"]').prop('disabled', true);
            $('#tab-session-schedule #recurring-session-rules .btn-sm').addClass('disabled');
            $('#tab-session-schedule .js-remove-protocol').addClass('disabled');
            $('#tab-session-schedule .js-session-protocol-handle').addClass('disabled');
            $('#tab-session-schedule .js-create-session').remove();

        },

        updateSessionModel: function () {
            this.stopListening(app.collections.patientProtocolSearchCollection);

            if (this.model.get('expireMinutes') === null) {
                this.model.set({expireHours: null, isNeverExpiring: true});
            }

            var protocols = _.extend({}, this.model.get('protocols')),
                protocolCollection = app.models.patientHealthSessionModel.get('protocols'),
                patientHealthSessionModel = _.extend({}, this.model.attributes),
                protocolSearchModel;

            delete patientHealthSessionModel.protocols;

            app.models.patientHealthSessionModel.set(patientHealthSessionModel);
            app.models.patientHealthSessionRecurringRulesModel.set(this.recurrenceRules);

            _.each(protocols, function (protocol) {
                protocolSearchModel = app.collections.patientProtocolSearchCollection.findWhere({ id: protocol.protocolId });
                protocolSearchModel.set({
                    'order': protocol.order,
                    'isAdded': true
                });

                protocolCollection.add(protocolSearchModel);

            });

            if (this.isRecurring) {

                if (this.recurrenceRules.weekDays.length) {

                    _.each(this.recurrenceRules.weekDays, function (number) {
                        var recurrungWeekModel = app.collections.patientHealthSessionRecurrungWeekCollection.findWhere({ number: number });
                        recurrungWeekModel.set('isSelected', true);
                    });
                }

                if (this.recurrenceRules.monthDays.length) {

                    _.each(this.recurrenceRules.monthDays, function (number) {
                        var recurrungMonthModel = app.collections.patientHealthSessionRecurrungMonthCollection.findWhere({ number: number });
                        recurrungMonthModel.set('isSelected', true);
                    });
                }
            }


        },

        deleteSession: function (e) {

            e.preventDefault();

            app.models.patientHelthSessionDeleteModel = new PatientHealthSessionModel();
            app.models.patientHelthSessionDeleteModel.set(this.model.attributes);

            var name = this.model.get('name');

            var confirmModal = new BackboneBootstrapModal({
                title: 'Delete Session',
                content: '<div class="alert alert-danger hidden" role="alert"></div><h3>Are you sure want to delete the ' + name + '?</h3>',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
            .open()
            .on('ok', function () {

                confirmModal.$el.find('.btn.ok').data('loading-text', 'Deleting...').button('loading');

                app.models.patientHelthSessionDeleteModel.destroy({

                    success: function (model, response, options) {

                        confirmModal.close();

                        app.vent.trigger("updateCurrentMonth");

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Session was successfully deleted',
                            autoClose: true
                        }).show();
                    },
                    error: function (model, xhr, options) {

                        confirmModal.$el.find('.btn.ok').button('reset');

                        var errorAlert = confirmModal.$el.find('.alert-danger');
                        errorAlert.html(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : "Response status: " + xhr.statusText);
                        errorAlert.removeClass('hidden');

                    }

                });

                confirmModal.preventClose();

            });


        },

        terminateSession: function (e) {
            e.preventDefault();

            app.models.patientHelthSessionTerminateModel = new PatientHealthSessionTerminateModel();
            app.models.patientHelthSessionTerminateModel.set({
                calendarItemId: this.model.get('id'),
                terminationUtc: moment().format('YYYY-MM-DD') + ' 00:00:00'
            });

            var name = this.model.get('name');

            var confirmModal = new BackboneBootstrapModal({
                title: 'Terminate Session',
                content: '<div class="alert alert-danger hidden" role="alert"></div><h3>Are you sure want to terminate the ' + name + '?</h3>',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
            .open()
            .on('ok', function () {

                confirmModal.$el.find('.btn.ok').data('loading-text', 'Terminating...').button('loading');

                app.models.patientHelthSessionTerminateModel.save({}, {

                    success: function (model, response, options) {

                        confirmModal.close();

                        app.vent.trigger("updateCurrentMonth");

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Session was successfully terminated',
                            autoClose: true
                        }).show();
                    },
                    error: function (model, xhr, options) {

                        confirmModal.$el.find('.btn.ok').button('reset');

                        var errorAlert = confirmModal.$el.find('.alert-danger');
                        errorAlert.html(xhr.responseJSON ? xhr.responseJSON.ErrorMessage : "Response status: " + xhr.statusText);
                        errorAlert.removeClass('hidden');

                    }

                });

                confirmModal.preventClose();

            });



        }








    });
});