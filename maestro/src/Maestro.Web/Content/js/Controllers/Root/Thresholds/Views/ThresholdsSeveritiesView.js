'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Root/Thresholds/AppNamespace',
    'Controllers/Helpers',
    'Controllers/Constants',
    'BackboneBootstrapAlert',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesLayoutCollectionView',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesLayoutThView',
    'Controllers/Root/Thresholds/Views/ThresholdsSeveritiesCollectionView'
], function ($,
            _,
            Backbone,
            app,
            Helpers,
            Constants,
            BackboneBootstrapAlert,
            ThresholdsSeveritiesLayoutCollectionView,
            ThresholdsSeveritiesLayoutThView,
            ThresholdsSeveritiesCollectionView) {
    return Backbone.View.extend({

        template: _.template('<div class="alert alert-danger help-block-thresholds-general hidden" role="alert">\
                One or more values you entered is invalid. Please review threshold settings for specific error messages.\
                You may need to scroll to view the entire grid.\
            </div>\
            <div class="row row-nomargin-ci">\
                <div class="col-nopadding-ci col-thresholds col-thresholds-layout">\
                    <table id="table-layout-thresholds" class="table table-thresholds table-thresholds-alert-severities">\
                        <thead>\
                            <tr id="table-layout-thresholds-thead-tr">\
                                <th rowspan="2" class="th-name-of-measure">Measurement</th>\
                                <th rowspan="2" class="th-unit-of-measure">Unit</th>\
                            </tr>\
                        </thead>\
                        <tbody id="tbody-layout-thresholds"></tbody>\
                    </table>\
                </div>\
                <div class=" col-nopadding-ci col-thresholds col-thresholds-value">\
                    <table id="table-thresholds" class="table table-thresholds table-thresholds-alert-severities">\
                        <thead id="table-thresholds-thead" style="height: 41px;">\
                            <tr id="table-thresholds-thead-title-tr">\
                                <th class="th-low-limit">LOW</th>\
                                <th class="th-high-limit border-left">HIGH</th>\
                            <tr id="table-thresholds-thead-tr" class="tr-thresholds-severity" style="height: 41px;"></tr>\
                        </thead>\
                        <tbody id="tbody-thresholds"><tr></tr></tbody>\
                    </table>\
                </div>\
            </div>\
            <% if(obj.allowUnusedButton) { %>\
            <a class="btn btn-link js-show-unused-thresholds">Show Unused</a>\
            <a class="btn btn-link js-hide-unused-thresholds hidden">Hide Unused</a>\
            <% } %>'),

        events: {

            'click .js-show-unused-thresholds': 'showAllThresholds',
            'click .js-hide-unused-thresholds': 'hideAllThresholds'

        },

        selectors: {
            errorMsg: '.help-block-thresholds-general'
        },

        initialize: function () {

            _.bindAll(this, "updatePatientThreshold","savePatientThresholdsCollection","thresholdsTdHeight","checkThresholdsValidation");

            app.vent.unbind("updatePatientThreshold");
            app.vent.unbind("savePatientThresholdsCollection");
            app.vent.unbind("thresholdsTdHeight");
            app.vent.unbind('checkThresholdsValidation', this.checkThresholdsValidation );

            app.vent.bind("updatePatientThreshold", this.updatePatientThreshold);
            app.vent.bind("savePatientThresholdsCollection", this.savePatientThresholdsCollection);
            app.vent.bind("thresholdsTdHeight", this.thresholdsTdHeight);
            app.vent.bind('checkThresholdsValidation', this.checkThresholdsValidation );
            
        },

        checkThresholdsValidation: function () {
            app.isValid = this.thresholdsCollectionsIsValid();
        },

        render: function () {

            // app.views.thresholdsSeveritiesCollectionView = app.views.thresholdsSeveritiesCollectionView || {};

            app.views.thresholdsSeveritiesCollectionLowView = app.views.thresholdsSeveritiesCollectionLowView || {};
            app.views.thresholdsSeveritiesCollectionHighView = app.views.thresholdsSeveritiesCollectionHighView || {};

            this.thresholdsCollectionsRestore();
            this.thresholdsCollectionsStore();
            // this.$el.empty();
            this.$el.html( this.template( app ) );

            this.renderTresholdsLayout();
            this.renderThresholds();
            this.updatePatientThreshold();
            this.updatePatientThresholdTableStyle();

            return this;
        },

        renderThresholds: function () {

            this.collection.sortDirection('Desc');

            this.collection.each(function (alertSeverity, index) {

                this.renderTresholdsTheadLayout(alertSeverity);
                this.renderLowThresholds(alertSeverity, index+1);
            }, this);

            this.collection.sortDirection('Asc');

            this.collection.each(function (alertSeverity, index) {

                this.renderTresholdsTheadLayout(alertSeverity);
                this.renderHighThresholds(alertSeverity, index + 1 + this.collection.length + 1);
            }, this);

        },

        renderLowThresholds: function (alertSeverity, severityIndex) {

            app.views.thresholdsSeveritiesCollectionLowView[alertSeverity.get('id')] = new ThresholdsSeveritiesCollectionView({ collection: app.collections.thresholdsSeveritiesCollections[alertSeverity.get('id')], severity: 'low'});

            this.$el.find('#tbody-thresholds > tr').append(app.views.thresholdsSeveritiesCollectionLowView[alertSeverity.get('id')].render(severityIndex).el);

            return this;
        },

        renderHighThresholds: function (alertSeverity, severityIndex) {

            app.views.thresholdsSeveritiesCollectionHighView[alertSeverity.get('id')] = new ThresholdsSeveritiesCollectionView({ collection: app.collections.thresholdsSeveritiesCollections[alertSeverity.get('id')], severity: 'high'});

            this.$el.find('#tbody-thresholds > tr').append(app.views.thresholdsSeveritiesCollectionHighView[alertSeverity.get('id')].render(severityIndex).el);
            
            return this;
        },

        renderTresholdsLayout: function () {
            var self = this;
            //app.collections.thresholdsSeveritiesCollections - this is map (key, value) where key is severity id, value is collection of thresholds for the severity id from key
            //this.collection - collection of severities
            
            var thresholdsLayoutCollection = app.collections.thresholdsSeveritiesCollections[self.collection.first().get('id')];

            //iterate thresholdsLayoutCollection to detect overlaps
            thresholdsLayoutCollection.each(function(thresholdModel, index) {

                var defaultThresholds = _.map(self.collection.models, function (severityModel) {

                    var severityThreshold = app.collections.thresholdsSeveritiesCollections[severityModel.get('id')].findWhere({ name: thresholdModel.get('name') });
                    if (severityThreshold) {
                        if (severityThreshold.get('defaultThreshold') != null) {
                            return { minValue: severityThreshold.get('defaultThreshold').minValue, maxValue: severityThreshold.get('defaultThreshold').maxValue };
                        }
                    }
                }).filter(function (defaultThreshold) { return defaultThreshold != undefined && defaultThreshold.minValue != null && defaultThreshold.maxValue != null;; });

                var thresholds = _.map(self.collection.models, function (severityModel) {

                    var severityThreshold = app.collections.thresholdsSeveritiesCollections[severityModel.get('id')].findWhere({ name: thresholdModel.get('name') });
                    if (severityThreshold) {
                        return { minValue: severityThreshold.get('minValue'), maxValue: severityThreshold.get('maxValue') };
                    }

                }).filter(function (threshold) { return threshold != undefined && threshold.minValue != null && threshold.maxValue != null; });

                if (defaultThresholds.length > 0) {
                    var highestMinDefaultThresholdValue = _.max(defaultThresholds, function(defaultThreshold) { return defaultThreshold.minValue }).minValue;
                    if (_.some(defaultThresholds, function(defaultThreshold) { return defaultThreshold.maxValue < highestMinDefaultThresholdValue })) {
                        thresholdModel.set('overlap', true);
                    } else {
                        thresholdModel.set('overlap', false);
                    }
                }
                if (thresholds.length > 0) {
                    var highestMinThresholdValue = _.max(thresholds, function (threshold) { return threshold.minValue }).minValue;
                    if (_.some(thresholds, function(threshold) { return threshold.maxValue < highestMinThresholdValue })) {
                        thresholdModel.set('overlap', true);
                    } else {
                        thresholdModel.set('overlap', false);
                    }
                }
               
            });

            app.views.thresholdsSeveritiesLayoutCollectionView = new ThresholdsSeveritiesLayoutCollectionView({ collection: thresholdsLayoutCollection });
            self.$el.find('#tbody-layout-thresholds').append(app.views.thresholdsSeveritiesLayoutCollectionView.render().el);



            return this;
        },

        renderTresholdsTheadLayout: function (alertSeverity) {

            app.views.thresholdsSeveritiesLayoutThView = new ThresholdsSeveritiesLayoutThView({ model: alertSeverity });
            this.$el.find('#table-thresholds-thead-tr').append(app.views.thresholdsSeveritiesLayoutThView.render().el);

            return this;
        },

        updatePatientThreshold: function () {
            var self = this;

            if (app.collections.thresholdsCollection.isDisplay || !app.allowUnusedButton) {
                return false;
            }

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.each(function (model) {
                    model.set('isDisplay', false);
                });
            }, app.collections.thresholdsSeveritiesCollections);

            app.collections.thresholdsCollection.isShow = false;

            if (app.initCollections.devicesCollection) {
                app.initCollections.devicesCollection.each(function (model) {

                    _.each(model.get('peripherals'), function (peripheral, type) {

                        if (peripheral !== 'none') {
                            var thresholds = Helpers.getThresholdByPeripheral(type);

                            if (thresholds) {
                                if (Array.isArray(thresholds)) {
                                    $.each(thresholds, function (i, threshold) {
                                        self.updateModelThresholdsCollection(threshold);
                                    })
                                } else {
                                    self.updateModelThresholdsCollection(thresholds);
                                }
                            }
                        }
                    });
                });
            }

            if (app.collections.thresholdsCollection.isShow) {
                this.$el.find('.col-thresholds').show();
            } else {
                this.$el.find('.col-thresholds').hide();
            }

            this.thresholdsTdHeight();

        },

        updateModelThresholdsCollection: function (threshold) {

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {

                collection.each(function (model) {
                    if (model.get('name') == threshold.name) {
                        model.set('isDisplay', true);
                        app.collections.thresholdsCollection.isShow = true;
                    }
                });

            }, app.collections.thresholdsSeveritiesCollections);

        },

        showAllThresholds: function (e) {

            $('.js-hide-unused-thresholds').removeClass('hidden');
            $('.js-show-unused-thresholds').addClass('hidden');

            app.collections.thresholdsCollection.isDisplay = true;

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.each(function (model) {
                    model.set('isDisplay', true);
                });
            }, app.collections.thresholdsSeveritiesCollections);

            this.$el.find('.col-thresholds').show();

            this.thresholdsTdHeight();

        },

        hideAllThresholds: function () {

            $('.js-hide-unused-thresholds').addClass('hidden');
            $('.js-show-unused-thresholds').removeClass('hidden');

            // app.collections.thresholdsSeveritiesCollections.isDisplay = false;

            app.collections.thresholdsCollection.isDisplay = false;

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.each(function (model) {
                    model.set('isDisplay', false);
                });
            }, app.collections.thresholdsSeveritiesCollections);


            this.updatePatientThreshold();

            // this.thresholdsTdHeight();

        },

        savePatientThresholdsCollection: function () {
            var self = this,
                thresholdsCount = 0;

            var ThresholdsSavingModel = Backbone.Model.extend(),
                ThresholdsSavingView = Backbone.View.extend({
                    initialize: function () {
                        this.listenTo(this.model, 'change', this.collectionSaved);
                    },
                    render: function () {
                        return this;
                    },
                    collectionSaved: function () {

                        if (!this.model.get('count')) {

                            _.each(self.removedArray, function (array, severityId) {

                                _.each(array, function (item) {
                                    var model = item[0],
                                        modelIndex = item[1];

                                    model = model.unset('id');
                                    app.collections.thresholdsSeveritiesCollections[severityId].add(model, { at: modelIndex });

                                }, array);

                            }, self.removedArray);

                            self.thresholdsCollectionsStore();
                            app.vent.trigger("saveMeasurementCollection");

                        }
                    }
                });

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                thresholdsCount += collection.length;
            });

            app.models.thresholdsSavingModel = new ThresholdsSavingModel({ count: thresholdsCount });
            app.views.thresholdsSavingView = new ThresholdsSavingView({ model: app.models.thresholdsSavingModel });
            app.views.thresholdsSavingView.render();

            self.removedArray = {};

            if (this.thresholdsCollectionsIsValid()) {
                _.each(app.collections.thresholdsSeveritiesCollections, function (collection, severityId) {

                    self.removedArray[severityId] = [];

                    collection.each(function (model, i) {

                        if (model.get('minValue') && model.get('maxValue')) {
                            model.save(null, self.thresholdsSavingHandler);
                        } else if (!model.get('minValue') && !model.get('maxValue') && model.get('id')) {
                            self.removedArray[severityId].push([model, self.collection.indexOf(model)]);
                        } else {
                            var count = app.models.thresholdsSavingModel.get('count') - 1;
                            app.models.thresholdsSavingModel.set('count', count);
                        }

                    });

                    _.each(self.removedArray[severityId], function (item) {
                        item[0].destroy(self.thresholdsSavingHandler);
                    }, self.removedArray[severityId]);

                }, app.collections.thresholdsSeveritiesCollections);

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
                }))
                    .show();

            }
        },

        thresholdsCollectionsIsValid: function () {
            var valid = true;

            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.each(function (model) {
                    if (!model.isValid(true)) {
                        valid = false;
                    }
                });
            });

            this.thresholdsTdHeight();

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
        },

        thresholdsCollectionsStore: function () {
            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.store();
            });
        },

        thresholdsCollectionsRestore: function () {
            _.each(app.collections.thresholdsSeveritiesCollections, function (collection) {
                collection.restore();
            });
        },

        thresholdsHeadHeight: function () {
            var selectors = [
                    'thead#table-thresholds-thead',
                    'tr#table-layout-thresholds-thead-tr'
            ],
                $headers = [],
                heights = [],
                maxHeight = 0;

            _.each(selectors, function (selector) {
                $headers.push(this.$el.find(selector));
            }, this);

            _.each($headers, function ($header) {
                heights.push($header.height());
            });

            maxHeight = Math.max.apply(null, heights);

            if (maxHeight !== 0 && maxHeight !== Number.NEGATIVE_INFINITY) {
                _.each($headers, function ($header) {
                    if ($header.height() !== maxHeight) $header.height(maxHeight);
                });
            }
        },

        thresholdsTdHeight: function () {
            this.thresholdsHeadHeight();

            var thresholdsNames = Helpers.getVitalType('all');

            _.each(thresholdsNames, function (name) {
                var $item = $('td.td-' + name);

                $item.height('auto');
                var heights = $item.map(function () {
                    return $(this).height();
                }).get(),
                    maxHeight = Math.max.apply(null, heights);
                $item.height(maxHeight + 1);
            });

        },

        updatePatientThresholdTableStyle: function () {
            switch (this.collection.length) {

                case 1:

                    this.$el.find('.col-thresholds-layout').addClass('col-sm-6');
                    this.$el.find('.col-thresholds-value').addClass('col-sm-6');

                    break;

                case 2:

                    this.$el.find('.col-thresholds-layout').addClass('col-sm-5');
                    this.$el.find('.col-thresholds-value')
                        .addClass('col-sm-7')
                        .addClass('col-horizontal-scrolling-ci');

                    break;

                    // case 3:

                    //     this.$el.find('.col-thresholds-layout').addClass('col-sm-5');
                    //     this.$el.find('.col-thresholds-value')
                    //                     .addClass('col-sm-7')
                    //                     .addClass('col-horizontal-scrolling-ci');
                    //     this.$el.find('.table-thresholds-alert-severities').addClass('table-thresholds-multiple');
                    //     break;
                default:

                    this.$el.find('.col-thresholds-layout').addClass('col-sm-5');
                    this.$el.find('.col-thresholds-value')
                        .addClass('col-sm-7')
                        .addClass('col-horizontal-scrolling-ci');
                    this.$el.find('.table-thresholds-alert-severities').addClass('table-thresholds-multiple');
                    break;
            }

            var count = this.collection.length;

            this.$el.find('#table-thresholds-thead-title-tr th').attr({ colspan: count });
            this.$el.find('#table-thresholds-thead-tr th:eq(' + count + ')').addClass('border-left');
            this.$el.find('#tbody-thresholds td.td-nopadding:eq(' + count + ')').addClass('border-left');
        }

    });
});