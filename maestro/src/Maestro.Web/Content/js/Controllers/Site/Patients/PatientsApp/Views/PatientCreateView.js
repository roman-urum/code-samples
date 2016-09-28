'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'BackboneModelBinder',
    'BackboneBootstrapAlert',
    'Controllers/Constants',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientIdentifiersCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientIdentifiersCollectionView',
    'BackboneBootstrapModal'
], function ($,
             _,
             Backbone,
             app,
             Helpers,
             moment,
             BackboneModelBinder,
             BackboneBootstrapAlert,
             Constants,
             PatientIdentifiersCollection,
             PatientIdentifiersCollectionView,
             BackboneBootstrapModal) {

    return Backbone.View.extend({

        modelBinder: new BackboneModelBinder(),

        el: '#patients-container',

        template: _.template($('#patientCreateTemplate').html()),

        events: {
            'click .js-patient-info-save': 'savePatientInfo',
            'click .js-patient-info-cancel': 'cancelPatientEditInfo'
        },

        initialize: function () {
            Backbone.Validation.bind(this);
        },

        render: function () {

            var self = this,
                patientIdentifiersArray = $.extend(true, [], Constants.customer.patientIdentifiers);

            this.$el.html(this.template(this.model.attributes));

            this.modelBinder.bind(this.model, this.el);

            // _.each( patientIdentifiersArray, function(identifier){
            //     var id = _.findWhere( this.model.get('identifiers'), { name: identifier.name } );
            //     if( id ){
            //         identifier.value = id.value;
            //     }
            // }, this);

            app.collections.patientIdentifiersCollection = new PatientIdentifiersCollection(patientIdentifiersArray);
            if (app.views.patientIdentifiersCollectionView)
                app.views.patientIdentifiersCollectionView.remove();
            app.views.patientIdentifiersCollectionView = new PatientIdentifiersCollectionView({ collection: app.collections.patientIdentifiersCollection });
            this.$el.find('#patient-identifiers').html(app.views.patientIdentifiersCollectionView.render().el);

            this.$el.find('[data-inputmask-mask]').inputmask({
                showMaskOnHover: false,
                clearIncomplete: true
            });

            $('#birthDate-datetimepicker').datetimepicker({
                format: "MM/DD/YYYY",
                maxDate: moment()
            }).on('dp.change', function (e) {
                var date = $(this).find('#birthDate-dp').val(),
                    age = moment().diff(date, 'years');

                date = date ? moment(date).format('YYYY-MM-DD') : '';

                $(this).siblings('#birthDate').val(moment(date).format('YYYY-MM-DD')).trigger('change');

                $('input[name="age"]').val(age);

            }).on('click', function (e) {
                $(this).siblings('.help-block-birthDate').addClass('hidden');
            });

            $('#birthDate-dp').inputmask({
                mask: '99/99/9999',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            $('#preferred-session-time').datetimepicker({
                format: 'h:mm A',
                defaultDate: '09/20/1988 ' + self.model.get('preferredSessionTime')
            }).on('dp.change', function (e) {

                var time = $(this).find('#preferredSessionTime-dp').val();
                $(this).siblings('#preferredSessionTime').val(time).trigger('change');

            }).on('click', function (e) {
                $(this).siblings('.help-block-preferred-session-time').addClass('hidden');
                $(this).closest('.form-group').removeClass('has-error');
            });

            $('#preferredSessionTime-dp').inputmask({
                mask: '9{1,2}:99 aM',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            // $('[data-toggle="buttons"]').each(function () {
            //     $(this).find('label.btn:first').click();
            // });

            $('#phoneHome, #phoneWork').inputmask({
                mask: '(999) 999-9999',
                showMaskOnHover: false,
                clearIncomplete: true
            });

            this.updateTimeZone();

            // this.updateDateTimePicker();
            this.categoriesOfCare();

            return this;
        },

        savePatientInfo: function (e) {
            e.preventDefault();
            var self = this,
                identifier,
                identifiers = [];

            if (this.patientInfoIsValid()) {

                app.collections.patientIdentifiersCollection.each(function (model) {
                    identifier = {};
                    if (model.get('value')) {
                        identifier.name = model.get('name');
                        identifier.value = model.get('value');
                        identifiers.push(identifier);
                    }

                });

                this.model.set('identifiers', identifiers);

                $('.js-patient-info-save').data('loading-text', 'Saving...').button('loading');

                this.model.save({ siteId: app.siteId }, {
                    processData: true,
                    success: function (model, response, options) {

                        app.patientId = self.model.get('id');

                        var alert = new BackboneBootstrapAlert({
                            alert: 'success',
                            message: 'Patient was successfully created',
                            autoClose: true
                        })
                        .show();

                        var href = $(e.currentTarget).attr('href').replace('/id/','/' + app.patientId + '/');

                        app.router.navigate(href, {
                            trigger: true
                        });

                    },
                    error: function (model, xhr, options) {

                        $('.js-patient-info-save').button('reset');

                        var alert = new BackboneBootstrapAlert({
                            alert: 'danger',
                            title: 'Error: ',
                            message: xhr.responseJSON.ErrorMessage,
                        })
                        .show();
                    }
                });
            }

        },

        cancelPatientEditInfo: function (e) {
            e.preventDefault();
            var self = this;
            if (self.model.changed) {
                
                new BackboneBootstrapModal({
                    content: '<h3>You have made changes to the current tab. Are you sure you want to cancel and discard your changes?<h3>',
                    okText: 'Yes',
                    cancelText: 'No',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                })
                .open()
                .on('ok', function () {
                    self.cancelTriggerRouter(e);
                });

            } else {
                self.cancelTriggerRouter(e);
            }


        },

        cancelTriggerRouter: function (e) {
            e.preventDefault();

            if (!e.altKey && !e.ctrlKey && !e.metaKey && !e.shiftKey) {
                var href = $(e.currentTarget).attr('href');

                app.router.navigate(href, {
                    trigger: true
                });
            }
        },

        updateTimeZone: function () {
            $('#timeZone').prop('disabled', true);
            $('#timeZone option[value="' + Helpers.getTimeZoneId() + '"]').attr('selected', true);
            $('#timeZone').prop('disabled', false);
            $('#timeZone').change();
        },

        categoriesOfCare: function () {

            if (Constants.site.categoriesOfCare.length) {

                var categoriesOfCare = this.model.get('categoriesOfCare'),
                    options = '<option value=""></option>';

                _.each(Constants.site.categoriesOfCare, function (category) {
                    options += '<option value="' + category.id + '">' + category.name + '</option>';
                });

                $('#categoriesOfCare')
                    .prop('disabled', false)
                    .prop('multiple', true)
                    .html(options)
                    .val(categoriesOfCare)
                    .chosen();

            } else
                $('#form-group-categories-of-care').addClass('hidden');
        },

        patientInfoIsValid: function () {
            var patientIdIsValid = this.patientIdentifiersCollectionIsValid(),
                patientModelIsValid = this.model.isValid(true);

            if (patientModelIsValid && patientIdIsValid)
                return true;

            return false;

        },

        patientIdentifiersCollectionIsValid: function () {
            var valid = true;

            app.collections.patientIdentifiersCollection.each(function (model) {
                if (!model.isValid(true)) {
                    valid = false;
                }
            });

            return valid;
        },

        // updateDateTimePicker: function () {
        //     var $time = $('#preferred-session-time'),
        //         time = $time.siblings('#preferredSessionTime').val();

        //     $time.find('#preferredSessionTime-dp').val(time);
        // }
    });
});