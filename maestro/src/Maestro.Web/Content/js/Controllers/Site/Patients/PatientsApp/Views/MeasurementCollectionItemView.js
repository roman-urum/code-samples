'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'BackboneModelBinder'
], function ($, _, Backbone, app, BackboneModelBinder) {
    return Backbone.View.extend({

        modelBinder: undefined,

        tagName: 'table',

        className: 'table table-striped',

        template: _.template($('#patientMeasurementTemplate').html()),

        events: {

        },

        initialize: function () {

            this.listenTo(this.model, 'set', this.modelSetValue);

            this.listenTo(this.model, 'change', this.modelSetValue );
            this.listenTo(this.model, 'change', this.updatePeripherals );

            this.modelBinder = new BackboneModelBinder();

        },

        render: function () {
            var self = this;
            var settings = {
                bloodGlucose: ['isBloodGlucoseAutomated','isBloodGlucoseManual'],
                bloodPressure: ['isBloodPressureAutomated','isBloodPressureManual'],
                peakFlow: ['isPeakFlowAutomated','isPeakFlowManual'],
                pedometer: ['isPedometerAutomated','isPedometerManual'],
                pulseOx: ['isPulseOxAutomated','isPulseOxManual'],
                temperature: ['isTemperatureAutomated','isTemperatureManual'],
                weight: ['isWeightAutomated','isWeightManual']
            };

            var peripherals = this.getPeripherals( settings );

            this.model.set('peripherals', peripherals );

            this.$el.attr('id', this.model.get('id'));

            this.$el.html( this.template( this.model.attributes ) );

            this.modelBinder.bind(this.model, this.el, {
                "settings.bloodGlucosePeripheral": "[name='" + this.model.get('id') + ".settings.bloodGlucosePeripheral']",
                "peripherals.weight": "[name='"  + this.model.get('id') + ".peripherals.weight']",
                "peripherals.bloodPressure": "[name='" + this.model.get('id') + ".peripherals.bloodPressure']",
                "peripherals.pulseOx": "[name='" + this.model.get('id') + ".peripherals.pulseOx']",
                "peripherals.bloodGlucose": "[name='" + this.model.get('id') + ".peripherals.bloodGlucose']",
                "peripherals.peakFlow": "[name='" + this.model.get('id') + ".peripherals.peakFlow']",
                "peripherals.temperature": "[name='" + this.model.get('id') + ".peripherals.temperature']",
                "peripherals.pedometer": "[name='" + this.model.get('id') + ".peripherals.pedometer']"
            });

            this.updatePeripherals();

            return this;
        },

        getPeripherals: function( peripherals ){

            var currentPeripherals = {},
                settings = this.model.get('settings');

            _.each(peripherals, function( peripheral, key ){

                if( settings[ peripheral[0] ] && settings[ peripheral[1] ]  ){

                    currentPeripherals[key] = 'both';

                }else if( settings[ peripheral[0] ] ){

                    currentPeripherals[key] = 'auto';

                }else if( settings[ peripheral[1] ] ){

                    currentPeripherals[key] = 'manual';

                }else{

                    currentPeripherals[key] = 'none';
                }

            });

            return currentPeripherals;
        },

        modelSetValue: function () {
            app.vent.trigger("collectionModelSetValue");
            app.vent.trigger("patientEditMeasurementsChanged");
        },

        updatePeripherals: function(){

            var peripherals = this.model.get('peripherals');
            if( peripherals.bloodGlucose == 'both' || peripherals.bloodGlucose == 'auto'  ){
                this.$el.find('.js-glucometer-device-type').removeClass('hidden');
            }else{
                this.$el.find('.js-glucometer-device-type').addClass('hidden');
            }

        }
    });

});