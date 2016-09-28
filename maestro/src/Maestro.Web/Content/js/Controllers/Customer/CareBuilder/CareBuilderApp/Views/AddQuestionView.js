'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneModelBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/SelectAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/QuestionModalScaleAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/QuestionModalSelectionAnswerSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/QuestionModalOpenEndedSetView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/AudioOptionsView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/OpenEndedAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/UploadFileModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ScaleAnswerChoiceCollection',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneModelBinder,
    app,
    SelectAnswerSetView,
    QuestionModalScaleAnswerSetView,
    QuestionModalSelectionAnswerSetView,
    QuestionModalOpenEndedSetView,
    AudioOptionsView,
    QuestionElementModel,
    SelectionAnswerSetModel,
    ScaleAnswerSetModel,
    OpenEndedAnswerSetModel,
    UploadFileModel,
    ScaleAnswerChoiceCollection,
    Helpers
) {
    return Backbone.View.extend({
        model: QuestionElementModel,

        initialize: function () {

            Backbone.Validation.bind(this);

        },

        selectAnswerSetView: null,

        template: _.template($("#addQuestionTemplate").html()),

        modelBinder: new BackboneModelBinder(),

        events: {
            "change #answer-type": "search",
            "change #search-keyword": "search",
            "change #search-tags": "search",
            'click .js-search-clear': 'searchClear',
            "click .js-change-answer-set": "OnClickChangeAnswerSet",
            'fileUploaded': 'onFileUploaded',
            'removeAudioFile': 'onRemoveAudioFile'
        },

        render: function () {
            var bindings = {
                    'questionElementString.value': '[data-name="questionElementString.value"]',
                    'questionElementString.pronunciation': '[data-name="pronunciation"]',
                    'externalId': '[data-name="externalId"]',
                    'tags': '[data-name="tags"]'
                };

            var viewHtmlContent = this.template(this.model.attributes);

            var theresAnInternalIdInputInViewContent = $('[data-name="internalId"]', viewHtmlContent).length > 0;
            if (theresAnInternalIdInputInViewContent) {
                bindings['internalId'] = '[data-name="internalId"]';
            }

            this.audioOptionsView = new AudioOptionsView({ model: this.model.get('questionElementString') });
            this.$el.html(viewHtmlContent);
            this.$el.find("#audio-options-container").html(this.audioOptionsView.render().$el);
            this.modelBinder.bind(this.model, this.el, bindings);
            this.$changeAnswerSetControlBox = this.$el.find('.js-change-answer-set-row');
            this.renderSelectAnswerSetView();

            return self;
        },

        close: function() {
            this.audioOptionsView.stopAudio();
            this.$el.find('.creation-tags').tokenfield('destroy');
        },

        // event handlers
        OnClickChangeAnswerSet: function () {
            this.renderSelectAnswerSetView();
        },

        search: function () {
            var searchData = {
                "searchCategories": this.getSearchCategories(),
                "searchKeyword": this.getSearchKeyword(),
                "searchTags": this.getSearchTags()
            };

            this.selectAnswerSetView.SearchAnswerSets(searchData);
        },

        getSearchCategories: function () {
            var selectedAnswersetType = this.$el.find('#answer-type').val();

            return selectedAnswersetType === 'All' ? ["ScaleAnswerSet", "SelectionAnswerSet", "OpenEndedAnswerSet"] : [selectedAnswersetType];
        },

        getSearchKeyword: function () {
            return this.$el.find('#search-keyword').val();
        },

        getSearchTags: function () {
            var searchTags = this.$el.find('#search-tags').val();
            searchTags = (searchTags && searchTags !== '0') ? searchTags : [];

            searchTags = searchTags.filter(function (element, index) {
                return element !== '';
            });

            if (searchTags.length) {
                this.$el.find('.js-search-clear').prop('disabled', false);
            } else {
                this.$el.find('.js-search-clear').prop('disabled', true);
            }

            return searchTags;
        },

        searchClear: function () {
            this.$el.find('.searching-tags')
                .val(0)
                .trigger("chosen:updated")
                .change()
            ;
        },

        onFileUploaded: function (event, audioFileModel) {
            this.model.set('questionElementString.audioFileMedia', audioFileModel);
        },

        onRemoveAudioFile: function () {
            this.model.set('questionElementString.audioFileMedia', null);
        },

        OnSelectAnswerSet: function () {

            if (this.model.get('answerSet').get('type') == 2) {
                this.model.get('answerSet').set('answerSetType', 'OpenEndedAnswerSet');
            }

            var self = this,
                answerSetType = this.model.get('answerSet').get('answerSetType'),
                answerSetSettings = {
                    el: self.$el.find('#select-answer-set'),
                    model: this.model.get('answerSet')
                },
                answerSetDetailsView;


            if (this.model.get('answerSet').get('type') === 0) {
                _.each(this.model.get('answerSet').get('selectionAnswerChoices').models, function (model, i) {
                    model.set('index', i + 1);
                });
            }

            switch (answerSetType) {
                case 'SelectionAnswerSet':

                    this.initializeSelectionAnswerChoices(answerSetSettings.model.get('selectionAnswerChoices'));
                    answerSetDetailsView = new QuestionModalSelectionAnswerSetView(answerSetSettings);
                    break;

                case 'ScaleAnswerSet':

                    answerSetSettings.model.set('scaleAnswerChoices', this.initializeScaleAnswerChoices(answerSetSettings.model));
                    answerSetDetailsView = new QuestionModalScaleAnswerSetView(answerSetSettings);
                    break;

                case 'OpenEndedAnswerSet':

                    answerSetDetailsView = new QuestionModalOpenEndedSetView(answerSetSettings);
                    break;

            }

            answerSetDetailsView.render();
            answerSetDetailsView.on("OnChangeAnswerSet", function () { self.renderSelectAnswerSetView(); });

            this.$changeAnswerSetControlBox.show();
            this.trigger('OnShowAnswerSetDetails');
        },

        OnSubmitQuestion: function (modalView, isNew) {
            modalView.preventClose();

            // Updating model with Tags
            var newTags = modalView.$el.find('.creation-tags').tokenfield('getTokensList').split(', ');

            newTags = newTags.filter(function (element, index) {
                return element !== '';
            });

            this.model.set('tags', newTags);

            if (!this.model.isModelValid()) {
                return;
            }

            this.model.save(null,
            {
                success: function (model, response, options) {
                    if (isNew === true) {
                        modalView.$el.find('.js-alert-create-success').removeClass('hidden');
                    } else {
                        modalView.$el.find('.js-alert-update-success').removeClass('hidden');
                    }

                    setTimeout(function () {
                        modalView.close();

                        app.vent.trigger("reRenderCareElements", {careElementType: "3"});

                    }, 2000);
                },
                skipParseOnSuccess: true
            });

        },

        // help methods
        renderSelectAnswerSetView: function () {
            var self = this;

            this.$changeAnswerSetControlBox.hide();

            if (this.selectAnswerSetView == null) {
                this.selectAnswerSetView = new SelectAnswerSetView({ el: this.$el.find('#select-answer-set') });
            }

            this.selectAnswerSetView.render();

            this.selectAnswerSetView.on('OnSelectAnswerSet', function (selectedAnswerset) {
                var type = selectedAnswerset.get('type'),
                    id = selectedAnswerset.get('id'),
                    answerSetModel;

                switch (type) {
                    case 6:
                        answerSetModel = new ScaleAnswerSetModel({ id: id });
                        break;

                    case 7:

                        answerSetModel = new SelectionAnswerSetModel({ id: id });
                        break;

                    case 8:

                        answerSetModel = new OpenEndedAnswerSetModel({ id: id });
                        break;

                }

                Helpers.renderSpinner(self.$el.find('#select-answer-set'));

                if (answerSetModel != undefined) {
                    answerSetModel.fetch({
                        success: function () {
                            self.model.set('answerSet', answerSetModel);
                            self.model.set('answerSetId', answerSetModel.get('id'));

                            var answerChoices = self.model.getAnswerChoices();

                            if (answerChoices != undefined) {
                                self.model.set('answerChoiceIds', answerChoices.models);
                            }

                            self.OnSelectAnswerSet();
                        }
                    });
                }
            });

            this.trigger('OnShowAnswerSetsList');

            $('#answer-type').trigger('change');
        },

        initializeSelectionAnswerChoices: function (selectionAnswerChoices) {
            var answerChoiceCollection = this.model.get('answerChoiceIds');

            if (answerChoiceCollection && answerChoiceCollection.length) {
                _.each(selectionAnswerChoices.models, function (el) {
                    var answerChoiceModel = _.findWhere(answerChoiceCollection, { id: el.get('id') });

                    if (answerChoiceModel != undefined) {
                        el.set('internalId', answerChoiceModel.internalId);
                        el.set('externalId', answerChoiceModel.externalId);
                        el.set('internalScore', answerChoiceModel.internalScore);
                        el.set('externalScore', answerChoiceModel.externalScore);
                    }
                });
            }
        },

        initializeScaleAnswerChoices: function (scaleAnswerSetModel) {
            var lowValue = scaleAnswerSetModel.get('lowValue'),
                highValue = scaleAnswerSetModel.get('highValue'),
                result = new ScaleAnswerChoiceCollection(),
                answerChoiceIds = this.model.get('answerChoiceIds');

            for (var i = lowValue; i <= highValue; i++) {
                var model = {
                    value: i
                };

                if (answerChoiceIds != undefined) {
                    $.each(answerChoiceIds, function (index, ids) {
                        if (model.value === ids.value) {
                            model.internalId = ids.internalId;
                            model.externalId = ids.externalId;
                            model.internalScore = ids.internalScore;
                            model.externalScore = ids.externalScore;
                        }
                    });
                }

                result.add(model);
            }

            return result;
        }
    });
});