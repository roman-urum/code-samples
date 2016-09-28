'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',
    'moment',
    'Controllers/Constants',
    'BackboneModelBinder',
    'BackboneBootstrapAlert',
    'BackboneBootstrapModal',
    'Controllers/Site/Patients/PatientsApp/Collections/PatientIdentifiersCollection',
    'Controllers/Site/Patients/PatientsApp/Views/PatientIdentifiersCollectionView'
], function ($,
             _,
             Backbone,
             app,
             Helpers,
             moment,
             Constants,
             BackboneModelBinder,
             BackboneBootstrapAlert,
             BackboneBootstrapModal,
             PatientIdentifiersCollection,
             PatientIdentifiersCollectionView) {
    return Backbone.View.extend({
        modelBinder: new BackboneModelBinder(),

        el: '#patients-container',

        events: {
            'click .js-patient-info-save': 'savePatientEditInfo',
            'click .js-patient-info-cancel': 'cancelPatientEditInfo'
        },

        initialize: function () {
            Backbone.Validation.bind(this);
            this.listenTo(this.model, 'change', this.patientEditInfoChanged);
            this.listenTo(this.model, 'change:status', this.patientStatusChanged);
        },

        render: function () {

            var self = this,
                patientIdentifiersArray = $.extend(true, [], Constants.customer.patientIdentifiers);

            this.modelBinder.bind(this.model, this.el);

            _.each(patientIdentifiersArray, function (identifier) {
                var id = _.findWhere(this.model.get('identifiers'), { name: identifier.name });
                if (id) {
                    identifier.value = id.value;
                }
            }, this);

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
                defaultDate: self.model.get('birthDate'),
                maxDate: moment()
            }).on('dp.change', function (e) {
                var date = $(this).find('#birthDate-dp').val(),
                    age = moment().diff(date, 'years');

                date = date ? moment(date).format('YYYY-MM-DD') : '';

                $(this).siblings('#birthDate').val(date).trigger('change');

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
                defaultDate: self.model.get('birthDate') + ' ' + self.model.get('preferredSessionTime')
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
            //     $(this).find('input[type="radio"]:checked').parent().click();
            // });

            $('#phoneHome, #phoneWork').inputmask({
                mask: '(999) 999-9999',
                showMaskOnHover: false,
                clearIncomplete: true
            });                        

            $('#timeZone').chosen();

            // this.updateDateTimePicker();
            this.categoriesOfCare();

            this.isChanged = false;

            return this;
        },

        savePatientEditInfo: function (e) {
            e.preventDefault();

            var self = this,
                identifier,
                identifiers = [];

            if (self.patientInfoIsValid()) {
                app.collections.patientIdentifiersCollection.each(function (model) {
                    identifier = {};
                    if (model.get('value')) {
                        identifier.name = model.get('name');
                        identifier.value = model.get('value');
                        identifier.isPrimary = model.get('isPrimary');
                        identifiers.push(identifier);
                    }
                });

                self.model.set('identifiers', identifiers);

                var doSavePatient = function () {
                    $('.js-patient-info-save').data('loading-text', 'Updating...').button('loading');

                    self.model.save({ siteId: app.siteId }, {
                        success: function (model, response, options) {

                            self.model.store();
                            self.isChanged = false;

                            var alert = new BackboneBootstrapAlert({
                                alert: 'success',
                                message: 'Patient was successfully updated',
                                autoClose: true
                            })
                            .show();

                            //we fetch devices colelction to keep devices up to date in case of patient inactivating  
                            self.fetchDeviceCollection();

                            var href = $(e.currentTarget).attr('href');

                            app.router.navigate(href, {
                                trigger: true
                            });

                        },
                        error: function (model, xhr, options) {

                            $('.js-patient-info-save').button('reset');

                            var alert = new BackboneBootstrapAlert({
                                alert: 'danger',
                                title: 'Error: ',
                                message: xhr.responseJSON.ErrorMessage
                            })
                            .show();
                        }
                    });
                }

                if (self.isStatusChanged && self.model.get('status') == Constants.patientStatuses.INACTIVE) {

                    var confirmInactivatePatientModal = new BackboneBootstrapModal({
                        title: "Confirm inactivate patient",
                        content: "<h4>Do you want make patient inactive?</h4>",
                        modalOptions: {
                            backdrop: 'static',
                            keyboard: false
                        },
                        okText: 'OK'
                    });

                    confirmInactivatePatientModal.open();

                    self.listenTo(confirmInactivatePatientModal, "ok", doSavePatient);

                } else {
                    doSavePatient();
                }
            }
        },

        fetchDeviceCollection: function () {

            app.collections.devicesCollection.isFetched = false;
            app.collections.devicesCollection.fetch({
                success: function () {
                    app.collections.devicesCollection.isFetched = true;
                    app.collections.devicesCollection.trigger('fetched');
                }
            });

        },

        cancelPatientEditInfo: function (e) {
            e.preventDefault();

            var self = this;

            if (this.isChanged) {

                var modalView = new BackboneBootstrapModal({
                    content: 'You have unsaved changes. If you leave the page, these changes will be lost.',
                    title: 'Confirm Navigation',
                    okText: 'Leave this page',
                    cancelText: 'Stay on this page',
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

        patientEditInfoChanged: function () {
            this.isChanged = true;
        },

        patientStatusChanged: function () {
            this.isStatusChanged = true;
        },

        // updateDateTimePicker: function () {
        //     var $date = $('#birthDate-datetimepicker'),
        //         date = $date.siblings('#birthDate').val(),
        //         age = moment().diff(date, 'years');

        //     // $date.find('#birthDate-dp').val(moment(date).format('MM/DD/YYYY')).trigger('change');
        //     $('input[name="age"]').val(age);

        //     var $time = $('#preferred-session-time'),
        //         time = $time.siblings('#preferredSessionTime').val();

        //     $time.find('#preferredSessionTime-dp').val( moment(time + ' ' + date).format('h:mm A') );
        // },

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
                    .chosen()
                ;

            } else {
                $('#form-group-categories-of-care').addClass('hidden');
            }

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
        }

    });
});