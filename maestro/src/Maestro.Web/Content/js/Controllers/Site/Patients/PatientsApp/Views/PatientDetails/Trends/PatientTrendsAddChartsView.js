'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers
) {
    return Backbone.View.extend({

        template: _.template($('#patientTrendsAddChartsViewTemplate').html()),

        charts: null,
        selectedCharts: null,
        addChartsModal: null,

        events: {
            'click .js-show-vitals': 'showVitals',
            'click .js-show-questions': 'showQuestions',
            'click .js-add-chart': 'addChart',
            'click .js-remove-chart': 'removeChart',
            'click .js-move-up': 'moveChartUp',
            'click .js-move-down': 'moveChartDown'
        },

        initialize: function (options) {
            options = options || {};
            var self = this;

            this.questionsFetched = false;

            //parent data
            this.vitals = options.vitals.map(function (model) {
                return {
                    name: model.get('name'),
                    label: model.get('label')
                }
            });
            this.questions = options.questions.map(function (model) {
                return {
                    name: model.get('questionId'),
                    label: model.get('questionText')
                }
            });
            this.selectedCharts = options.selectedCharts.map(function (model) {
                return {
                    name: model.chartName,
                    label: model.chartLabel
                }
            });

            //if questions collection is not fetched listen to its changes
            if (options.questions.isFetched) {
                this.questionsFetched = true;
            } else {
                this.listenTo(options.questions, 'add', function () {
                    this.questionsFetched = true;
                    this.questions = options.questions.map(function (model) {
                        return {
                            name: model.get('questionId'),
                            label: model.get('questionText')
                        }
                    });
                    this._calculateAvailableCharts();
                    this.render();
                });
            }

            //temprorary data
            this.currentTabIndex = 0;
            this.availableVitals = [];
            this.availableQuestions = [];
            this._calculateAvailableCharts();

            this.bind("ok", function () { self.onSave(); });
        },

        _calculateAvailableCharts: function () {
            var self = this;

            //availableVitals are vitals which do not present in selectedCharts
            this.availableVitals = _.filter(this.vitals, function (vitalMeta) {
                return !_.findWhere(self.selectedCharts, { name: vitalMeta.name });
            });

            this.availableQuestions = _.filter(this.questions, function (questionMeta) {
                return !_.findWhere(self.selectedCharts, { name: questionMeta.name });
            });

            this.availableCharts = (this.currentTabIndex === 0) ? this.availableVitals : this.availableQuestions;
        },

        render: function () {
            //render general template
            var data = {
                availableCharts: this.availableCharts,
                selectedCharts: this.selectedCharts
            };
            this.$el.html(this.template(data));

            //render spinner for questions list
            if (this.currentTabIndex === 1 && !this.questionsFetched) {
                Helpers.renderSpinner(this.$('.charts-list-container'));
            }

            //render active class for btns
            if (this.currentTabIndex === 0) {
                this.$('.js-show-vitals').addClass('active').siblings().removeClass('active');
            } else {
                this.$('.js-show-questions').addClass('active').siblings().removeClass('active');
            }

            return this;
        },

        onSave: function (modal) {
            app.vent.trigger('charts:selected', this.selectedCharts);
        },

        showVitals: function (e) {
            $(e.target).addClass('active').siblings().removeClass('active');
            this.currentTabIndex = 0;
            this._calculateAvailableCharts();
            this.render();
        },

        showQuestions: function (e) {
            this.currentTabIndex = 1;
            this._calculateAvailableCharts();
            this.render();
        },

        addChart: function (e) {
            e.preventDefault();
            var index = $(e.target).data('index');

            var chartMeta = (this.currentTabIndex === 0) ? this.availableVitals[index] : this.availableQuestions[index];
            this.selectedCharts.push(chartMeta);
            this._calculateAvailableCharts();

            this.render();
        },

        removeChart: function (e) {
            e.preventDefault();
            var index = $(e.target).data('index');

            this.selectedCharts.splice(index, 1);
            this._calculateAvailableCharts();

            this.render();
        },

        moveChartUp: function (e) {
            var index = $(e.target).data('index');

            if (index > 0) {
                var temp = this.selectedCharts[index - 1];
                this.selectedCharts[index - 1] = this.selectedCharts[index];
                this.selectedCharts[index] = temp;
                this.render();
            }
        },

        moveChartDown: function (e) {
            var index = $(e.target).data('index');

            if (index < this.selectedCharts.length - 1) {
                var temp = this.selectedCharts[index + 1];
                this.selectedCharts[index + 1] = this.selectedCharts[index];
                this.selectedCharts[index] = temp;
                this.render();
            }
        },

        remove: function () { }
    });
});
