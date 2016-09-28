'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Helpers',
    'BackboneBootstrapAlert',
    'Controllers/Root/Thresholds/Models/ThresholdModel',
    'Controllers/Root/Thresholds/Views/ThresholdsCollectionItemView'
], function ($, _, Backbone, app, Helpers, BackboneBootstrapAlert, ThresholdModel, ThresholdsCollectionItemView) {
    return Backbone.View.extend({
        template: _.template('<div class="alert alert-danger help-block-thresholds-general hidden" role="alert">\
                                One or more values you entered is invalid. Please review threshold settings for specific error messages.\
                                You may need to scroll to view the entire grid.\
                            </div>\
                            <table class="table table-thresholds">\
                                <thead>\
                                    <tr>\
                                        <th >Measurement</th>\
                                        <th class="th-unit-of-measure">Unit</th>\
                                        <th class="th-low-limit">Low</th>\
                                        <th class="th-high-limit">High</th>\
                                    </tr>\
                                </thead>\
                                <tbody id="tbody-thresholds"></tbody>\
                            </table>\
                            <% if(obj.allowUnusedButton) { %>\
                            <a class="btn btn-link js-show-unused-thresholds">Show Unused</a>\
                            <a class="btn btn-link js-hide-unused-thresholds hidden">Hide Unused</a>\
                            <% } %>'),

        templateCap: _.template('<div>NO THRESHOLDS</din>'),

        selectors: {
            errorMsg: '.help-block-thresholds-general'
        },

        events: {
            'click .js-show-unused-thresholds': 'showAllThresholds',
            'click .js-hide-unused-thresholds': 'hideAllThresholds'
        },

        initialize: function () {

            _.bindAll(this, "updatePatientThreshold","savePatientThresholdsCollection","checkThresholdsValidation");

            app.vent.unbind("updatePatientThreshold");
            app.vent.unbind("savePatientThresholdsCollection");
            app.vent.unbind("checkThresholdsValidation" );

            app.vent.bind("updatePatientThreshold", this.updatePatientThreshold);
            app.vent.bind("savePatientThresholdsCollection", this.savePatientThresholdsCollection);
            app.vent.bind('checkThresholdsValidation', this.checkThresholdsValidation );
            
        },

        checkThresholdsValidation: function () {
            app.isValid = this.thresholdsCollectionIsValid();
        },

        render: function () {
            this.$el.empty();
            this.$el.html( this.template( app ) );
            this.collection.each(this.renderThreshold, this);

            this.updatePatientThreshold();
            return this;
        },

        renderThreshold: function (threshold) {
            app.views.thresholdsCollectionItemView = new ThresholdsCollectionItemView({ model: threshold });
            this.$el.find('#tbody-thresholds').append(app.views.thresholdsCollectionItemView.render().el);

            return this;
        },


        updatePatientThreshold: function () {
            var self = this;

            if (app.collections.thresholdsCollection.isDisplay || !app.allowUnusedButton) {
                return false;
            }

            this.collection.each(function (model) {
                model.set('isDisplay', false);
            });


            if (app.initCollections.devicesCollection) {
                app.initCollections.devicesCollection.each(function (model) {

                    _.each(model.get('peripherals'), function (peripheral, type) {

                        if (peripheral !== 'none') {
                            var thresholds = Helpers.getThresholdByPeripheral(type);

                            if (thresholds) {
                                if (Array.isArray(thresholds)) {
                                    $.each(thresholds, function (i, threshold) {
                                        self.updateModelThresholdsCollection(threshold)
                                    })
                                } else {
                                    self.updateModelThresholdsCollection(thresholds)
                                }
                            }
                        }
                    });
                });
            }

        },

        updateModelThresholdsCollection: function (threshold) {

            this.collection.each(function (model) {

                if (model.get('name') == threshold.name) {
                    model.set('isDisplay', true);
                }

            });

        },

        showAllThresholds: function (e) {

            $('.js-hide-unused-thresholds').removeClass('hidden');
            $('.js-show-unused-thresholds').addClass('hidden');

            app.collections.thresholdsCollection.isDisplay = true;

            this.collection.each(function (model) {
                model.set('isDisplay', true);
            });
        },

        hideAllThresholds: function () {

            $('.js-show-unused-thresholds').removeClass('hidden');
            $('.js-hide-unused-thresholds').addClass('hidden');

            app.collections.thresholdsCollection.isDisplay = false;

            this.updatePatientThreshold();

        },

        savePatientThresholdsCollection: function () {
            var self = this;

            var ThresholdsSavingModel = Backbone.Model.extend();
            var ThresholdsSavingView = Backbone.View.extend({
                initialize: function () {
                    this.listenTo(this.model, 'change', this.collectionSaved);
                },
                render: function () {
                    return this;
                },
                collectionSaved: function () {

                    if (!this.model.get('count')) {

                        _.each(self.removedArray, function (item) {
                            var model = item[0],
                                modelIndex = item[1];

                            model = model.unset('id');

                            self.collection.add(model, { at: modelIndex })

                        });
                        self.collection.store();
                        app.vent.trigger("saveMeasurementCollection");

                    }
                }
            });

            app.models.thresholdsSavingModel = new ThresholdsSavingModel({ count: this.collection.length });
            app.views.thresholdsSavingView = new ThresholdsSavingView({ model: app.models.thresholdsSavingModel });
            app.views.thresholdsSavingView.render();

            self.removedArray = [];

            if (this.thresholdsCollectionIsValid()) {

                //$('.js-patient-measurements-save').data('loading-text', 'Updating...').button('loading');

                this.collection.each(function (model, i) {

                    if (model.get('minValue') && model.get('maxValue')) {
                        model.save(null, self.thresholdsSavingHandler)
                    } else if (!model.get('minValue') && !model.get('maxValue') && model.get('id')) {

                        self.removedArray.push([model, self.collection.indexOf(model)]);

                    } else {
                        var count = app.models.thresholdsSavingModel.get('count') - 1;
                        app.models.thresholdsSavingModel.set('count', count);
                    }

                });

                _.each(self.removedArray, function (item) {
                    item[0].destroy(self.thresholdsSavingHandler);
                });

            }
        },

        thresholdsSavingHandler: {
            success: function (model, response, options) {
                var count = app.models.thresholdsSavingModel.get('count') - 1;
                app.models.thresholdsSavingModel.set('count', count);
            },
            error: function (model, xhr, options) {
                (new BackboneBootstrapAlert({
                    alert: 'danger',
                    title: 'Error: ',
                    message: xhr.responseJSON.ErrorMessage
                })).show();
            }
        },

        thresholdsCollectionIsValid: function () {
            var valid = true;

            this.collection.each(function (model) {
                if (!model.isValid(true)) {
                    valid = false;
                }
            });

            var $errMsg = this.$(this.selectors.errorMsg);
            if (valid) {
                $errMsg.addClass('hidden');
            } else {
                $errMsg.removeClass('hidden');
            }

            if (_.isFunction(app.events.afterValidation)) {
                app.events.afterValidation(valid);
            }

            return valid;
        }
    });
});
