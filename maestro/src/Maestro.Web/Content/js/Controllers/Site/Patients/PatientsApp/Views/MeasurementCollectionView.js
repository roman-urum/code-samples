'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'BackboneBootstrapAlert',
    'Controllers/Site/Patients/PatientsApp/Views/MeasurementCollectionItemView'
], function ($, _, Backbone, app, BackboneBootstrapAlert, MeasurementCollectionItemView) {
    return Backbone.View.extend({

        events: {

        },

        initialize: function () {

            _.bindAll(this, "saveMeasurementCollection");
            
            app.vent.unbind("saveMeasurementCollection");
            app.vent.unbind("collectionModelSetValue");

            app.vent.bind("saveMeasurementCollection", this.saveMeasurementCollection);
            app.vent.bind("collectionModelSetValue", this.collectionModelSetValue);

            this.listenTo(this.collection, 'add remove', this.render);
        },

        render: function () {
            this.$el.empty();
            this.collection.each(this.renderDeviceMeasurement, this);
            return this;
        },

        renderDeviceMeasurement: function (device) {
            if (device.get('status') == 0 || device.get('status') == 1) {

                app.views.measurementCollectionItemView = new MeasurementCollectionItemView({ model: device });
                this.$el.append(app.views.measurementCollectionItemView.render().el);

            }

            return this;
        },

        saveMeasurementCollection: function () {
            console.log('start executing saveMeasurementCollection...');
            var self = this;

            this.updateSettings();

            console.log( this.collection );

            this.collection.update({
                success: function (model, response, options) {
                    // self.model.store();

                    var alert = new BackboneBootstrapAlert({
                        alert: 'success',
                        message: 'Patient Measurements were updated successfully',
                        autoClose: true
                    })
                        .show();

                    self.collection.store();
                    app.vent.trigger('measurementSavedSuccess');
                },
                error: function (model, response, options) {
                    $('.js-patient-measurements-save').button('reset');

                    var alert = new BackboneBootstrapAlert({
                        alert: 'danger',
                        title: 'Error: ',
                        message: model.responseJSON.ErrorMessage
                    })
                    .show();
                }


            });

        },
        collectionModelSetValue: function () {

            app.vent.trigger('updatePatientThreshold');

        },

        updateSettings: function(){

            var settings = {
                bloodGlucose: ['isBloodGlucoseAutomated','isBloodGlucoseManual'],
                bloodPressure: ['isBloodPressureAutomated','isBloodPressureManual'],
                peakFlow: ['isPeakFlowAutomated','isPeakFlowManual'],
                pedometer: ['isPedometerAutomated','isPedometerManual'],
                pulseOx: ['isPulseOxAutomated','isPulseOxManual'],
                temperature: ['isTemperatureAutomated','isTemperatureManual'],
                weight: ['isWeightAutomated','isWeightManual']
            };

            this.collection.each(function(device){

                var peripherals = device.get('peripherals');

                if( peripherals ){

                    var newSettings = {};

                    _.each( peripherals , function(setting, peripheral){

                        if( setting == 'both' ){

                            newSettings[ settings[ peripheral ][0] ] = true;
                            newSettings[ settings[ peripheral ][1] ] = true;

                        }else if( setting == 'auto' ){

                            newSettings[ settings[ peripheral ][0] ] = true;
                            newSettings[ settings[ peripheral ][1] ] = false;

                        }else if( setting == 'manual' ){

                            newSettings[ settings[ peripheral ][0] ] = false;
                            newSettings[ settings[ peripheral ][1] ] = true;

                        }else{

                            newSettings[ settings[ peripheral ][0] ] = false;
                            newSettings[ settings[ peripheral ][1] ] = false;

                        }

                    });

                    var currentSettings = device.get('settings');

                    newSettings = _.extend( currentSettings, newSettings );

                    device.set('settings', newSettings );

                }

            });

        }

    });
});