'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/AnswerSetsModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/OpenEndedAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectAnswerSetSelectionAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectAnswerSetScaleAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectAnswerSetOpenEndedAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Helpers'
], function ($, _, Backbone,
    AnswerSetsModel,
    SelectionAnswerSetModel,
    ScaleAnswerSetModel,
    OpenEndedAnswerSetModel,
    SelectAnswerSetSelectionAnswerSetView,
    SelectAnswerSetScaleAnswerSetView,
    SelectAnswerSetOpenEndedAnswerSetView,
    SearchCollection,
    Helpers) {
    return Backbone.View.extend({

        template: _.template($("#selectAnswerSetTemplate").html()),

        // checkListAnswerSetTemplate: _.template($("#checkListAnswerSetTemplate").html()),

        events: {
            "click .js-select-answer-set": "OnSelectAnswerSet"
        },

        initialize: function () {
            this.answerSets = new AnswerSetsModel();
        },

        render: function () {
            var self = this;
            self.$el.html(self.template());
            self.DisplayAnswerSets();

            return self;
        },



        SearchAnswerSets: function (searchData) {
            var self = this;

            Helpers.renderSpinner(self.$el.find("#question-accordion"));

            this.searchCollection = new SearchCollection();

            this.searchCollection.fetch({
                traditional: true,
                data: {
                    categories: searchData.searchCategories,
                    q: searchData.searchKeyword,
                    tags: searchData.searchTags
                },
                success: function (result) {
                    self.$el.find("#question-accordion").html("");

                    $.each(result.models, function (index, item) {
                        self.RenderAnswerSet(item);
                    });
                },
                error: function () {
                    console.log('error while fetch answersets');
                }
            });
        },

        RenderAnswerSet: function (searchResultItem) {
            var model,
                view,
                type = searchResultItem.get('type');

            if (type === 7) {
                model = new SelectionAnswerSetModel({
                    id: searchResultItem.get('id'),
                    name: searchResultItem.get('name'),
                    tags: searchResultItem.get('tags')
                });

                view = new SelectAnswerSetSelectionAnswerSetView({ model: model });
            } else if (type === 6) {
                model = new ScaleAnswerSetModel({
                    id: searchResultItem.get('id'),
                    name: searchResultItem.get('name'),
                    tags: searchResultItem.get('tags')
                });

                view = new SelectAnswerSetScaleAnswerSetView({ model: model });
            } else if (type === 8) {
                model = new OpenEndedAnswerSetModel({
                    id: searchResultItem.get('id'),
                    name: searchResultItem.get('name'),
                    tags: searchResultItem.get('tags')
                });

                view = new SelectAnswerSetOpenEndedAnswerSetView({ model: model });
            }

            if (view != undefined) {
                this.$el.find("#question-accordion").append(view.render().$el);
            }
        },

        DisplayAnswerSets: function () {
            //var self = this;

            //self.$el.find("#question-accordion").html("");

            //$("input.scale").slider({
            //    handle: 'custom',
            //    tooltip: 'hide'
            //});
        },

        OnSelectAnswerSet: function (e) {
            var selectedAnswerSetId = $(e.target).attr('answer-set-id'),
                selectedAnswerSet = this.searchCollection.where({ id: selectedAnswerSetId })[0];

            this.trigger("OnSelectAnswerSet", selectedAnswerSet);
        }
    });
});