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
    'Controllers/Site/Patients/PatientsApp/Models/InvalidateMeasurementModel',
    'Controllers/Site/Notes/NotesApp'
], function ($, _, Backbone, app, Helpers, moment, BackboneBootstrapModal, BackboneBootstrapAlert, InvalidateMeasurementModel, notes) {
    return Backbone.View.extend({

        tagName: 'tr',

        templateMeasurement: _.template('<td class="col-md-2"><div style="padding: 5px 0"><%=answeredDate%></div></td>\
                                        <td class="col-md-2"><div style="padding: 5px 0"><%=answeredTime%></div></td>\
                                        <td class="col-md-4">\
                                            <% _.each( vitals, function (vital) { %>\
                                                <div style="padding: 5px 0"><%=vital.name%></div>\
                                            <% }); %>\
                                        </td>\
                                        <td class="col-md-4">\
                                            <div class="dropdown pull-right">\
                                                <a class="btn btn-xs btn-link dropdown-toggle" data-toggle="dropdown">\
                                                    <span class="glyphicon glyphicon-cog"></span>\
                                                </a>\
                                                <ul class="dropdown-menu dropdown-menu-right">\
                                                    <li><a class="js-ignore-reading">Ignore Reading</a></li>\
                                                </ul>\
                                            </div>\
                                            <% if(measurementId) { //TODO: do something with "Blood Pressure" %>\
                                                <a role="button" data-type="measurement" data-id="<%- measurementId %>" class="btn btn-success btn-xs add-note pull-right" style="margin: 3px 3px 0 0;"><i class="fa fa-book"></i></a>\
                                            <% } %>\
                                            <% _.each( vitals, function (vital) { %>\
                                                <div style="padding: 5px 50px 5px 0;">\
                                                    <% if( typeof(isAutomated) !== "undefined" && !isAutomated ) {%>*<%}%>\
                                                    <% if( vital.value ) {%>\
                                                        <span <% if( vital.vitalAlert && vital.vitalAlert.alertSeverity ) {%> style="color:<%=vital.vitalAlert.alertSeverity.colorCode%>" <%}%>>\
                                                            <%=vital.value%> <%=vital.unit%>\
                                                        </span>\
                                                    <%} else {%>\
                                                        <% if( vital.vitalAlerts ) {%>\
                                                            <span <% if( vital.vitalAlerts.systolic && vital.vitalAlerts.systolic.alertSeverity ) {%> style="color:<%=vital.vitalAlerts.systolic.alertSeverity.colorCode%>"<%}%>>\
                                                                <%=vital.values.systolic%>\
                                                            </span>\
                                                            /\
                                                            <span <% if( vital.vitalAlerts.diastolic && vital.vitalAlerts.diastolic.alertSeverity ) {%> style="color:<%=vital.vitalAlerts.diastolic.alertSeverity.colorCode%>"<%}%>>\
                                                                <%=vital.values.diastolic%> <%=vital.unit%>\
                                                            </span>\
                                                        <%}else{%>\
                                                            <span <% if( vital.vitalAlert && vital.vitalAlert.alertSeverity ) {%> style="color:<%=vital.vitalAlert.alertSeverity.colorCode%>" <%}%>>\
                                                                <%=vital.value%> <%=vital.unit%>\
                                                            </span>\
                                                        <%}%>\
                                                    <%}%>\
                                                </div>\
                                            <% }); %>\
                                        </td>'),

        templateQuestion: _.template('<td class="col-md-2"><%=answeredDate%></td>\
                                        <td class="col-md-2"><%=answeredTime%></td>\
                                        <td class="col-md-4"><%=text%></td>\
                                        <td class="col-md-4">\
                                            <span <% if( healthSessionElementAlert ) {%>style="color:<%=healthSessionElementAlert.alertSeverity.colorCode%>"<%}%>>\
                                                <% _.each(values, function(value, index){ %>\
                                                    <% if(value.type == 0 ){%><%=value.text%><%}%>\
                                                    <% if(value.type == 1 ){%><%=value.value%><%}%>\
                                                    <% if(value.type == 2 ){%><%=value.value%><%}%>\
                                                    <% if (index != values.length - 1) {%> , <%}%>\
                                                <% }) %>\
                                            </span>\
                                            <a role="button" data-type="question" class="btn btn-success btn-xs add-note pull-right" style="margin-right: 26px;"><i class="fa fa-book"></i></a>\
                                        </td>'),

        templateTextMedia: _.template('<td class="col-md-2"><%=answeredDate%></td>\
                                            <td class="col-md-2"><%=answeredTime%></td>\
                                            <td class="col-md-4">\
                                                <% if(text){ %>\
                                                    <%=text%><br>\
                                                <% } %>\
                                                <% if(mediaId){ %>\
                                                    <span class="glyphicon glyphicon-play-circle"></span>\
                                                    <%=mediaName%>\
                                                <% } %>\
                                            </td>\
                                            <td class="col-md-4"></td>'),

        templateAssessment: _.template('<td class="col-md-2"><%=answeredDate%></td>\
                                            <td class="col-md-2"><%=answeredTime%></td>\
                                            <td class="col-md-4">\
                                                <% if(text){ %>\
                                                    <%=text%><br>\
                                                <% } %>\
                                                <% _.each(assessmentMedia, function(media){ %>\
                                                    <% if( media.assessmentMediaUrl ){%>\
                                                        <a class="js-play-assessment" data-media="<%=media.assessmentMediaUrl%>">\
                                                            <span class="glyphicon glyphicon-play"></span>\
                                                            <%=media.originalFileName%>\
                                                        </a>\
                                                    <% } else { %>\
                                                        <span class="glyphicon glyphicon-play glyphicon-play-disabled-ci">\
                                                            <span class="glyphicon glyphicon-ban-circle"></span>\
                                                        </span>\
                                                        <%=media.originalFileName%>\
                                                    <% } %>\
                                                <% }) %>\
                                            </td>\
                                            <td class="col-md-4"></td>'),

        events: {
            'click .js-ignore-reading': 'ignoreReading',
            'click .js-play-assessment': 'toggleAssessment',
            'click.add-note .add-note': 'addNote'
        },

        initialize: function () {

        },

        render: function () {

            var healthSessionType = this.model.get('healthSessionType'),
                answered = this.model.get('answered');

            if (answered) {
                this.model.set('answeredDate', moment.parseZone(answered).format('MM/DD/YYYY'));
                this.model.set('answeredTime', moment.parseZone(answered).format('hh:mm A (Z)'));

                if (healthSessionType) {

                    var vitals = this.model.get('vitals');
                    this._bloodPressureVitals(vitals);

                    //workaround to unify access to measurementId in notes module
                    this.model.set('measurementId', this.model.get('id'));

                    this.$el.html(this.templateMeasurement(this.model.attributes));

                } else {

                    switch (this.model.get('type')) {
                        case 1:
                            this.renderQuestionItem();
                            break;
                        case 2:
                            this.renderTextMediaItem();
                            break;
                        case 3:
                            this.renderMeasurementItem();
                            break;
                        case 4:
                            this.renderAssessmentItem();
                            break;
                    }
                }

            }

            return this;
        },

        renderQuestionItem: function () {
            this.$el.html(this.templateQuestion(this.model.attributes));
        },

        renderTextMediaItem: function () {
            this.$el.html(this.templateTextMedia(this.model.attributes));
        },

        renderMeasurementItem: function () {
            var values = this.model.get('values'),
                vitals = [];

            _.each(values, function (value) {
                this._bloodPressureVitals(value.value.vitals);
                vitals = vitals.concat(value.value.vitals);

                //workaround to unify access to measurementId in notes module
                this.model.set('measurementId', value.value.id);

                var isAutomated = value.value.isAutomated;
                if (isAutomated !== undefined) {
                    this.model.set('isAutomated', isAutomated);
                }

            }, this);

            this.model.set('vitals', vitals);

            this.$el.html(this.templateMeasurement(this.model.attributes));
        },

        renderAssessmentItem: function () {

            var values = this.model.get('values'),
                assessmentMedia = [];

            _.each(values, function (value) {
                assessmentMedia = assessmentMedia.concat(value.assessmentMedia);
            }, this);

            this.model.set('assessmentMedia', assessmentMedia);

            this.$el.html(this.templateAssessment(this.model.attributes));

        },

        ignoreReading: function () {

            var self = this,
                values = this.model.get('values'),
                measurementId = values ? values[0].value.id : this.model.get('id');

            var ignoreReadingModalView = new BackboneBootstrapModal({
                title: 'Ignore This Reading', // (' + + 'days)'
                content: 'Are you sure you want to ignore this reading?',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            })
                .open()
                .on('shown', function () {

                })
                .on('ok', function () {

                    var calendarItemId = self.model.get('calendarItemId'),
                        elementId = calendarItemId ? calendarItemId : measurementId;

                    app.models.invalidateMeasurementModel = new InvalidateMeasurementModel();
                    app.models.invalidateMeasurementModel.set({
                        'patientId': app.models.patientModel.get('id'),
                        'measurementId': measurementId
                    });

                    ignoreReadingModalView.$el.find('.btn.ok').data('loading-text', 'Saving...').button('loading');

                    ignoreReadingModalView.$el.find('.alert-danger').addClass('hidden');

                    app.models.invalidateMeasurementModel.save(null, {
                        success: function (model, response, options) {

                            ignoreReadingModalView.close();

                            if (app.collections.groupedHealthSessionInnerCollection && app.collections.groupedHealthSessionInnerCollection[elementId]) {
                                app.collections.groupedHealthSessionInnerCollection[elementId].remove(self.model);
                            }

                            app.collections.ungroupedHealthSessionCollection.remove(self.model);

                            var alert = new BackboneBootstrapAlert({
                                alert: 'success',
                                message: 'Measurement was successfully updated',
                                autoClose: true
                            })
                                .show();

                        },
                        error: function (model, xhr, options) {

                            ignoreReadingModalView.$el.find('.btn.ok').button('reset');

                            var errorAlert = ignoreReadingModalView.$el.find('.alert-danger');
                            errorAlert.html(xhr.responseJSON.ErrorMessage);
                            errorAlert.removeClass('hidden');
                        }
                    });


                    ignoreReadingModalView.preventClose();

                })
                .on('cancel', function () {

                });

        },

        toggleAssessment: function (e) {
            var $element = $(e.currentTarget),
                mediaUrl = $element.data('media');

            if ($element.hasClass('isPlaing')) {
                this._stopAssessment();
            } else {
                this._stopAssessment();

                app.media = new Audio(mediaUrl);
                app.media.play();

                $element.addClass('isPlaing')
                    .find('.glyphicon-play')
                    .removeClass('glyphicon-play')
                    .addClass('glyphicon-stop');
            }
        },

        _stopAssessment: function () {
            if (app.media) {
                app.media.pause();

                $('.isPlaing')
                    .removeClass('isPlaing')
                    .find('.glyphicon-stop')
                    .removeClass('glyphicon-stop')
                    .addClass('glyphicon-play');
            }
        },

        _bloodPressureVitals: function (vitals) {

            var systolicBloodPressure = _.findWhere(vitals, { name: "Systolic Blood Pressure" }),
                diastolicBloodPressure = _.findWhere(vitals, { name: "Diastolic Blood Pressure" });

            if (systolicBloodPressure || diastolicBloodPressure) {

                var unit = systolicBloodPressure ? systolicBloodPressure.unit : diastolicBloodPressure.unit,
                    systolicBloodPressureValue = systolicBloodPressure ? systolicBloodPressure.value : '—',
                    diastolicBloodPressureValue = diastolicBloodPressure ? diastolicBloodPressure.value : '—',
                    systolicBloodPressureAlert = systolicBloodPressure ? systolicBloodPressure.vitalAlert : null,
                    diastolicBloodPressureAlert = diastolicBloodPressure ? diastolicBloodPressure.vitalAlert : null;

                vitals.unshift({
                    name: "Blood Pressure",
                    unit: unit,
                    //TODO: temporary solution, need for adding note to vital
                    id: ( systolicBloodPressure && systolicBloodPressure.id ) ? systolicBloodPressure.id : diastolicBloodPressure.id,
                    values: {
                        'systolic': systolicBloodPressureValue,
                        'diastolic': diastolicBloodPressureValue
                    },
                    vitalAlerts: {
                        'systolic': systolicBloodPressureAlert,
                        'diastolic': diastolicBloodPressureAlert
                    }
                });

                systolicBloodPressure && vitals.splice(_.indexOf(vitals, systolicBloodPressure), 1);
                diastolicBloodPressure && vitals.splice(_.indexOf(vitals, diastolicBloodPressure), 1);
            }
        },

        addNote: function (e) {
            e.preventDefault();

            var $button = $(e.currentTarget),
                types = notes.types(),
                data = {};

            switch ($button.attr('data-type')) {
                case 'question':
                    {
                        data = notes.parse.from.detailedData(types.BEHAVIOUR, this.model.attributes);

                        notes.add(app.models.patientModel.get('id'), types.BEHAVIOUR, types.HEALTHSESSIONELEMENT, data);

                        break;
                    }
                case 'measurement':
                    {
                        data = notes.parse.from.detailedData(types.MEASUREMENT, $.extend(
                            {},
                            this.model.attributes,
                            {
                                vital: this.model.get('vitals')[0]
                            }));

                        notes.add(app.models.patientModel.get('id'), types.MEASUREMENT, types.VITAL, data);

                        break;
                    }
            }
        }

    });

});