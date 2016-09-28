'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/QuestionElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/MeasurementElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/AlertSeveritiesCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolQuestionListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolMeasurementListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolAssessmentListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeQuestionItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeMeasurementBranchView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerChoiceItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerScaleItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerOpenEndedView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/MeasurementBranchModalView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeQuestionModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeMeasurementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeAssessmentModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeTextAndMediaModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeAnswerChoiceModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeAnswerScaleModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolEditingModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementBranchModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/SelectionAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ScaleAnswerSetModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/CareElements/QuestionElementSelectionAnswersView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/QuestionElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTextMediaElementListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/SimulatorView',
    'BootstrapSlider'
], function (
        $,
        _,
        Backbone,
        BackboneBootstrapModal,
        BackboneCollectionBinder,
        app,
        Helpers,
        QuestionElementsCollection,
        MeasurementElementsCollection,
        AlertSeveritiesCollection,
        ProtocolQuestionListItemView,
        ProtocolMeasurementListItemView,
        ProtocolAssessmentListItemView,
        ProtocolTreeQuestionItemView,
        ProtocolTreeMeasurementBranchView,
        ProtocolTreeAnswerChoiceItemView,
        ProtocolTreeAnswerScaleItemView,
        ProtocolTreeAnswerOpenEndedView,
        MeasurementBranchModalView,
        ProtocolTreeQuestionModel,
        ProtocolTreeMeasurementModel,
        ProtocolTreeAssessmentModel,
        ProtocolTreeTextAndMediaModel,
        ProtocolTreeAnswerChoiceModel,
        ProtocolTreeAnswerScaleModel,
        ProtocolEditingModel,
        MeasurementBranchModel,
        SelectionAnswerSetModel,
        ScaleAnswerSetModel,
        QuestionElementSelectionAnswersView,
        SearchCollection,
        QuestionElementModel,
        ProtocolTextMediaElementListItemView,
        ProtocolSimulatorView
) {
    return Backbone.View.extend({
        model: ProtocolEditingModel,

        el: '#care-builder-container',

        initialize: function () {
            Helpers.renderSpinner(this.$el);

            this.placeholder = '<div class="placeholder js-placeholder">Your element will be added here</div>';
            this.placeholderBranchingProhibited = '<div class="placeholder placeholder-danger js-placeholder">Branching from multi-select questions is prohibited</div>';
            this.isProtocolChanged = false;
            this.initAlertIfProtocolChanged();

            Backbone.Validation.bind(this);
        },

        template: _.template($("#protocolViewTemplate").html()),
        templateQuestionItem: _.template($("#protocolTreeQuestionItemTemplate").html()),

        events: {
            'click .js-save-protocol': 'onSaveProtocol',
            'change .js-search-target': 'search',
            'change #search-protocol-elements-keyword': 'search',
            'change #search-protocol-elements-tags': 'search',
            'click .js-search-clear': 'searchClear',
            'click .js-is-ended-element': 'onEndProtocol',
            'click .js-is-ended-branch': 'onEndBranch',
            'click .js-delete-element': 'onDeleteElementProtocol',
            'click .js-add-measurement-branch': 'onAddMeasurementBranch',
            'click .protocol-item-element .js-set-alert': 'setAlertToAnswer',
            'click .protocol-item-element .js-unset-alert': 'unsetAlertFromAnswer',
            'click .js-delete-measurement-branch': 'onDeleteMeasurementBranch',
            'click .js-edit-measurement-branch': 'onEditMeasurementBranch',
            'click .btn-toggle-answers': 'onToggleAnswers',
            'click .js-simulate-protocol': 'simulateProtocol'
        },

        render: function (model, callback) {
            var self = this;

            if (model) {
                this.model = model;
            }

            Helpers.renderSpinner(this.$el);

            app.collections.alertSeverities = new AlertSeveritiesCollection();
            app.collections.alertSeverities.fetch({
                success: function () {
                    self.$el.html(self.template(self.model.attributes));
                    self.renderTitle();

                    Helpers.initTags($('.creation-tags'), $('.searching-tags'));

                    self.search();

                    $('.js-protocol-tree').append(self.placeholder);

                    self.setContainersHeight();

                    $(window).resize(function () {
                        if ($('.js-protocol-tree').length) {
                            self.setContainersHeight();
                            self.setContainersProperty();
                        }
                    });

                    $(window).scroll(function () {
                        if ($('.js-protocol-tree').length) {
                            self.setContainersProperty();
                        }
                    });

                    if (callback) {
                        callback();
                    }
                }
            });

            return this;
        },

        renderTitle: function () {
            if (this.model.get('id')) {
                var $title = $('#title-page'),
                    editTitle = $title.data("edit-title");

                $title.text(editTitle);
            }
        },

        setContainersHeight: function () {
            var $containerProtocol = $('.protocol-tree-list-container'),
                $containerElementsList = $('.protocol-elements-list-container'),
                windowHeight = $(window).height(),
                padding = 15;

            $containerElementsList.css('height', windowHeight - 2 * padding);
            $containerProtocol.css('min-height', windowHeight - 2 * padding);
        },

        setContainersProperty: function () {
            var $containerProtocol = $('.protocol-container'),
                $containerElements = $('.protocol-elements-container'),
                $containerElementsList = $('.protocol-elements-list-container'),
                // $footer = $('#footer'),
                containerProtocolOffsetTop = $containerProtocol.offset().top,
                containerElementsWidth = $containerElements.width(),
                containerElementsListHeight = $containerElementsList.height(),
                footerTop = containerProtocolOffsetTop + $containerProtocol.outerHeight(), // $footer.offset().top
                scrollTop = $(window).scrollTop(),
                padding = 15;

            $containerElementsList.css('width', containerElementsWidth + 'px');

            if (scrollTop > containerProtocolOffsetTop - padding) {
                $containerElementsList.addClass('protocol-elements-fixed');

                if ((containerElementsListHeight + 2 * padding + 7 + scrollTop) > footerTop) {
                    $containerElementsList.addClass('protocol-elements-fixed-bottom');
                } else {
                    $containerElementsList.removeClass('protocol-elements-fixed-bottom');
                }
            } else {
                $containerElementsList.removeClass('protocol-elements-fixed');
            }
        },

        search: function () {
            var searchCollection = new SearchCollection(),
                elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {

                    switch (model.get('type')) {
                        case 4:
                            {
                                return new ProtocolQuestionListItemView({
                                    model: new QuestionElementModel({
                                        id: model.get('id'),
                                        'questionElementString': { 'value': model.get('name') },
                                        'tags': model.get('tags')
                                    })
                                });
                            }
                        case 3:
                            {
                                return new ProtocolMeasurementListItemView({ model: model });
                            }
                        case 5:
                            {
                                return new ProtocolTextMediaElementListItemView({ model: model });
                            }
                        case 9:
                            {
                                return new ProtocolAssessmentListItemView({ model: model });
                            }
                    }
                });

            this.bindSearchResultCollection(searchCollection, elManagerFactory);
        },

        bindSearchResultCollection: function (collection, elManagerFactory) {
            var self = this,
                $searchResultContainer = this.$el.find('.js-search-result'),
                collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            Helpers.renderSpinner($searchResultContainer);

            collection.fetch({
                traditional: true,
                data: {
                    categories: this.getSearchCategories(),
                    q: this.getSearchKeyword(),
                    tags: this.getSearchTags()
                },
                success: function () {
                    $searchResultContainer.html('');
                    collectionBinder.bind(collection, $searchResultContainer);
                    self.runProtocolCreation();
                    self.setContainersProperty();
                }
            });
        },

        getSearchCategories: function () {
            var elementType = this.$el.find('.js-search-target').val();

            switch (elementType) {
                case 'questions':
                    {
                        return ["questionElement"];
                    }
                case 'measurements':
                    {
                        return ["measurementElement"];
                    }
                case 'textAndMediaElements':
                    {
                        return ["textMediaElement"];
                    }
                case 'assessments':
                    {
                        return ["assessmentElement"];
                    }
                case 'all':
                    {
                        return ["questionElement", "measurementElement", "textMediaElement", "assessmentElement"];
                    }
                default:
                    {
                        return [];
                    }
            }
        },

        getSearchKeyword: function () {
            return this.$el.find("#search-protocol-elements-keyword").val();
        },

        getSearchTags: function () {

            var searchTags = this.$el.find('#search-protocol-elements-tags').val();
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

        runProtocolCreation: function () {
            var self = this;

            $('.js-search-result .protocol-search-result-item-ci').draggable({
                appendTo: 'body',
                cursor: "move",
                cursorAt: { top: 15, left: 116 },
                // handle: '.protocol-handle',
                helper: 'clone',
                start: function (event, ui) {
                    if (!self.model.get("firstProtocolElementId")) {
                        self.isFirstProtocolElement = true;
                        self.firstProtocolElementCreation();
                    } else {
                        self.isFirstProtocolElement = false;
                        self.additionQuestion();
                        self.additionQuestionIntoAnswer();
                        self.additionQuestionIntoMeasurementBranch();
                        self.replaceFirstProtocolElement();
                    }
                },
                stop: function (event, ui) {
                    if (self.model.get("firstProtocolElementId")) {
                        $('.js-protocol-tree').find('.js-placeholder').remove();
                    }

                    self.setContainersProperty();
                }
            });
        },

        firstProtocolElementCreation: function () {
            var self = this;

            $(".js-placeholder").droppable({
                // tolerance: "touch",
                drop: function (event, ui) {
                    var elementId = ui.draggable.data('id'),
                        elementType = ui.draggable.data('elementType'),
                        spinnerContainer = '<div class="spinner">';

                    self.$thisDrop = $(this);

                    $('.js-protocol-tree').append(spinnerContainer);

                    Helpers.renderSpinner($('.spinner'));

                    $(".js-placeholder").remove();

                    self
                        .getProtocolElementModel(elementType, elementId)
                        .then(function() {
                            $('.js-save-protocol')
                                .removeClass('disabled')
                                .prop('disabled', false);
                        });

                    self.isProtocolChanged = true;
                }
            });
        },

        additionQuestion: function () {
            var self = this;

            $(".protocol-item-element-question").droppable({
                // tolerance: "touch",
                over: function (event, ui) {
                    $(this).closest('.protocol-item').after(self.placeholder);
                    self.setContainersProperty();
                },
                out: function () {
                    $(this).closest('.protocol-item').next('.js-placeholder').remove();
                    self.setContainersProperty();
                },
                drop: function (event, ui) {
                    var elementId = ui.draggable.data('id'),
                        elementType = ui.draggable.data('elementType'),
                        elementContainer = $(this).closest('.protocol-item');

                    self.addSpinnerForNewElement(elementContainer);

                    self.additionType = 1;
                    self.$thisDrop = $(this);
                    self.elementContainer = elementContainer;

                    self.getProtocolElementModel(elementType, elementId);
                    self.isProtocolChanged = true;
                }
            });
        },

        additionQuestionIntoAnswer: function () {
            var self = this;

            $(".protocol-item-element-answer:not(.protocol-item-disabled):not(.js-branching-prohibited)").droppable({
                // tolerance: "touch",
                over: function (event, ui) {
                    $(this).after(self.placeholder);
                    self.setContainersProperty();
                },
                out: function () {
                    $(this).next('.js-placeholder').remove();
                    self.setContainersProperty();
                },
                drop: function (event, ui) {
                    $(this).after('<ol class="protocol-list"/>');

                    var elementId = ui.draggable.data('id'),
                        elementType = ui.draggable.data('elementType'),
                        elementContainer = $(this).next('ol.protocol-list');

                    $(this).droppable('disable');
                    self.addSpinnerForNewElement(elementContainer);

                    self.additionType = 2;
                    self.$thisDrop = $(this);
                    self.elementContainer = elementContainer;

                    self.getProtocolElementModel(elementType, elementId);
                    self.isProtocolChanged = true;
                }
            });

            $(".protocol-item-element-answer.js-branching-prohibited").droppable({
                // tolerance: "touch",
                over: function (event, ui) {
                    $(this).after(self.placeholderBranchingProhibited);
                    self.setContainersProperty();
                },
                out: function () {
                    $(this).next('.js-placeholder').remove();
                    self.setContainersProperty();
                }
            });
        },

        additionQuestionIntoMeasurementBranch: function () {
            var self = this;

            $(".protocol-measurement-branch").droppable({
                over: function (event, ui) {
                    $(this).after(self.placeholder);
                    self.setContainersProperty();
                },
                out: function () {
                    $(this).next('.js-placeholder').remove();
                    self.setContainersProperty();
                },
                drop: function (event, ui) {
                    $('.js-placeholder').remove();
                    $(this).after('<ol class="protocol-list"/>');

                    var elementId = ui.draggable.data('id'),
                        elementType = ui.draggable.data('elementType'),
                        $nextElementContainer = $(event.target).next('ol.protocol-list');

                    $(this).droppable('disable');
                    self.addSpinnerForNewElement($nextElementContainer);

                    self.additionType = 3;
                    self.$thisDrop = $(this);
                    self.elementContainer = $nextElementContainer;

                    self.getProtocolElementModel(elementType, elementId);
                    self.isProtocolChanged = true;
                }
            });
        },

        replaceFirstProtocolElement: function () {
            var self = this;

            $(".js-protocol-first-element-drop-placeholder").droppable({
                over: function (event, ui) {
                    $(this).after(self.placeholder);
                },
                out: function () {
                    $(this).next('.js-placeholder').remove();
                },
                drop: function (event, ui) {
                    var elementId = ui.draggable.data('id'),
                        elementType = ui.draggable.data('elementType'),
                        elementContainer = $(event.target);

                    self.addSpinnerForNewElement(elementContainer);

                    self.isFirstProtocolElement = true;

                    self.getProtocolElementModel(elementType, elementId);
                    self.isProtocolChanged = true;
                }
            });
        },

        addSpinnerForNewElement: function (target) {
            var spinnerContainer = '<div class="spinner">';

            target.append(spinnerContainer);

            Helpers.renderSpinner($('.spinner'));
        },

        getProtocolElementModel: function (elementType, elementId) {
            var self = this,
                elementModel,
                deferred = $.Deferred();

            switch (elementType) {
                case 3:
                    elementModel = new ProtocolTreeMeasurementModel({ id: elementId });
                    break;
                case 9:
                    elementModel = new ProtocolTreeAssessmentModel({ id: elementId });
                    break;
                case 5:
                    elementModel = new ProtocolTreeTextAndMediaModel({ id: elementId });
                    break;
                default:
                    elementModel = new ProtocolTreeQuestionModel({ id: elementId });
            }

            elementModel.fetch({
                success: function () {
                    if (elementType === 5) {
                        elementModel.set('type', 2);
                    }

                    self.protocolModelUpdate(elementModel);

                    deferred.resolve();
                },
                error: function (e) {
                    deferred.reject(e);
                }
            });

            return deferred.promise();
        },

        generateProtocolElementJson: function (model) {
            var protocolElement = {
                    element: {
                        type: model.get('type')
                    },
                    elementId: model.get('id'),
                    id: Helpers.getGUID(),
                    sort: 0,
                    nextProtocolElementId: null,
                    branches: [],
                    isMainBranch: false
                },
                answerSet = model.get('answerSet');

            if (answerSet) {
                protocolElement.element.answerSet = answerSet;
            }

            if (model.get('type') === 1) {
                _.each(model.get('answerSet').selectionAnswerChoices, function (answerChoice) {
                    var branch = {
                        id: Helpers.getGUID(),
                        nextProtocolElementId: null,
                        conditions: [
                            {
                                operand: 'SelectionAnswerChoice',
                                operator: 'Equals',
                                value: answerChoice.id
                            }
                        ]
                    }

                    answerChoice.branchId = branch.id;
                    protocolElement.branches.push(branch);
                });
            }

            return protocolElement;
        },

        protocolModelUpdate: function (elementModel) {
            var self = this,
                protocolElements = self.model.get('protocolElements'),
                protocolElement = this.generateProtocolElementJson(elementModel);

            if (self.isFirstProtocolElement) {
                var existingFirstProtocolElement = self.model.get("firstProtocolElementId");

                if (existingFirstProtocolElement != undefined && existingFirstProtocolElement !== "") {
                    protocolElement.nextProtocolElementId = existingFirstProtocolElement;
                }

                protocolElement.isMainBranch = true;
                self.model.set("firstProtocolElementId", protocolElement.id);

            } else if (self.additionType === 1) {
                this.updateElementOrder(self.$thisDrop, protocolElement);

            } else if (self.additionType === 2) {
                this.updateBranchOrder(self.$thisDrop, protocolElement);

            } else if (self.additionType === 3) {
                var measurementBranchId = self.$thisDrop.data('branchid'),
                    measurementBranch = self.getBranchById(measurementBranchId);

                if (measurementBranch) {
                    protocolElement.nextProtocolElementId = measurementBranch.nextProtocolElementId;
                    measurementBranch.nextProtocolElementId = protocolElement.id;
                }
            }

            protocolElements.push(protocolElement);

            self.model.set('protocolElements', protocolElements);

            self.renderQuestionElement(elementModel, protocolElement);
            self.checkAlertsVisibility();
        },

        // Updates nextProtocolElementIds for parent and related elements.
        updateElementOrder: function ($rootElement, newElement) {
            var droppableProtocolElementId = $rootElement.data('protocolElementID'),
                protocolElements = this.model.get('protocolElements'),
                rootElement = _.findWhere(protocolElements, { id: droppableProtocolElementId }),
                rootNextElementId = rootElement.nextProtocolElementId;

            if (rootElement.nextProtocolElementId) {
                this.setNextElementIdForElement(newElement, rootElement.nextProtocolElementId);
            }

            if (rootElement.isMainBranch) {
                newElement.isMainBranch = true;
            }

            this.setNextElementIdForBranch(rootElement, rootNextElementId, newElement.id);
            this.hideEndProtocolControls($rootElement);
        },

        // Updates nextProtocolElementId for specified branch and child elements
        updateBranchOrder: function ($rootBranch, newProtocolElement) {
            var branchId = $rootBranch.data('branchid'),
                branch = this.getBranchById(branchId),
                protocolParentElementId = $rootBranch.closest('.protocol-list')
                    .siblings('.protocol-item-element-question')
                    .data('protocolElementID'),
                protocolElements = this.model.get('protocolElements'),
                protocolParentElement = _.findWhere(protocolElements, { id: protocolParentElementId });

            branch.nextProtocolElementId = newProtocolElement.id;
            this.setNextElementIdForElement(newProtocolElement, protocolParentElement.nextProtocolElementId);
            this.hideEndProtocolControls($('[data-branchid="' + branchId + '"]'));
        },

        // Sets specified nextProtocolElementId for element and for branches
        setNextElementIdForElement: function (protocolElement, nextProtocolElementId) {
            protocolElement.nextProtocolElementId = nextProtocolElementId;

            _.each(protocolElement.branches, function (branch) {
                branch.nextProtocolElementId = nextProtocolElementId;
            });
        },

        // Updates link to next element for specified element, branches and child elements
        setNextElementIdForBranch: function (protocolElement, currentNextElementId, newElementId) {
            var self = this,
                protocolElements = self.model.get('protocolElements');

            // Update next element for current element or set next element for child element
            if (protocolElement.nextProtocolElementId === currentNextElementId) {
                protocolElement.nextProtocolElementId = newElementId;

                if (!protocolElement.branches.length) {
                    this.enableEndProtocolControls($('[data-protocolelementid="' + protocolElement.id + '"]'));
                }
            } else {
                var nextElement = _.findWhere(protocolElements, { id: protocolElement.nextProtocolElementId });

                self.setNextElementIdForBranch(nextElement, currentNextElementId, newElementId);
            }

            // Update next element for branches of current element or set next element for child elements in branches
            _.each(protocolElement.branches, function (branch) {
                if (branch.nextProtocolElementId != null || branch.nextProtocolElementId === currentNextElementId) {
                    if (branch.nextProtocolElementId === currentNextElementId) {
                        branch.nextProtocolElementId = newElementId;
                        self.enableEndProtocolControls($('[data-branchid="' + branch.id + '"]'));
                    } else {
                        var nextElement = _.findWhere(protocolElements, { id: branch.nextProtocolElementId });

                        self.setNextElementIdForBranch(nextElement, currentNextElementId, newElementId);
                    }
                }
            });
        },

        // Enables "End protocol" menu option, removes icon with status
        enableEndProtocolControls: function ($elementContainer) {
            $elementContainer
                .find('.js-is-ended')
                .show()
                .removeClass('protocol-set-continue')
                .addClass('protocol-set-end')
                .html('End Protocol');
            $elementContainer.find('.element-status').remove();
        },

        // Enables "Continue protocol" menu option, adds icon with status
        enableContinueProtocolControls: function ($elementContainer) {
            $elementContainer
                .find('.js-is-ended')
                .show()
                .removeClass('protocol-set-end')
                .addClass('protocol-set-continue')
                .html('Continue Protocol');
            $elementContainer.append('<span class="element-status glyphicon glyphicon-remove-circle" />');
        },

        // hides elements related with isEnded state for specified element
        hideEndProtocolControls: function ($elementContainer) {
            $elementContainer
                .find('.js-is-ended')
                .hide();
            $elementContainer.find('.element-status').remove();
        },

        renderQuestionElement: function (elementModel, protocolElement) {
            var self = this,
                questionElementString = elementModel.get('questionElementString') ?
                    elementModel.get('questionElementString') :
                    { value: elementModel.get('name') },
                protocolElementId = protocolElement.id,
                answerSetType = elementModel.get('answerSet') ? elementModel.get('answerSet').type : elementModel.get('type'),
                elementContainer = self.elementContainer,
                questionBox;
            
            protocolElement.element.questionElementString = questionElementString;

            if (elementModel.get('media'))
                protocolElement.element.media = elementModel.get('media');
            if (elementModel.get('text')) {
                console.log(elementModel.toJSON());
                console.log(elementModel.get('text.value'));
                protocolElement.element.text = { value: elementModel.get('text').value };
            }

            if (elementModel.get('measurementType')) {
                protocolElement.element.measurementType = elementModel.get('measurementType');
                protocolElement.element.name = elementModel.get('name');
            }

            var templateQuestionItem = self.templateQuestionItem(protocolElement);

            if (self.isFirstProtocolElement) {
                questionBox = $('<li class="protocol-item protocol-item-question"></li>')
                    .html(templateQuestionItem)
                    .insertAfter('.protocol-first-element-drop-placeholder');
                self.$answerSetBox = questionBox.find('ol.protocol-list');
            } else if (self.additionType === 1) {
                questionBox = $('<li class="protocol-item"></li>').html(templateQuestionItem);
                elementContainer.after(questionBox);
                self.$answerSetBox = elementContainer.next().find('ol.protocol-list');
            } else if (self.additionType === 2 || self.additionType === 3) {
                questionBox = $('<li class="protocol-item"></li>').html(templateQuestionItem);
                elementContainer.append(questionBox);
                self.$answerSetBox = elementContainer.find('ol.protocol-list');
            }

            questionBox.find('.protocol-item-element')
                .attr('data-protocolElementID', protocolElementId)
                .data('protocolElementID', protocolElementId);

            if (answerSetType === 1) {
                self.renderScaleAnswerSet(elementModel.get('answerSet'));
            } else if (answerSetType === 0) {
                self.renderSelectionAnswerSet(elementModel.get('answerSet'), protocolElement, protocolElement.alerts);
            } else if (elementModel.get('type') === 1 && answerSetType === 2) {
                self.renderOpenEndedAnswerSet();
            }

            if (protocolElement.isMainBranch || this.getMainBranchElement(protocolElement.id).nextProtocolElementId == null) {
                this.hideEndProtocolControls($('[data-protocolelementid="' + protocolElement.id + '"]'));
            }

            self.setContainersProperty();
            $('.dropdown-toggle').dropdown();
            $('.spinner').remove();
        },

        renderSelectionAnswerSet: function (selectionAnswerSet, protocolElement, alerts) {
            var self = this,
                selectionAnswerChoices = selectionAnswerSet.selectionAnswerChoices;

            self.$answerSetBox.html('');
            
            _.each(selectionAnswerChoices, function (answer) {
                var alert = _getAlertByAnswerId(answer.id),
                    answerChoiceModel = new ProtocolTreeAnswerChoiceModel(answer),
                    treeAnswerChoiceView = new ProtocolTreeAnswerChoiceItemView({
                        model: answerChoiceModel,
                        protocolElement: protocolElement,
                        alert: alert,
                        isMultipleChoice: selectionAnswerSet.isMultipleChoice
                    }),
                    $answerElement = treeAnswerChoiceView.render().$el;

                self.$answerSetBox.append($answerElement);

                if (protocolElement.nextProtocolElementId == null) {
                    self.hideEndProtocolControls($answerElement);
                }
            });

            function _getAlertByAnswerId(answerId) {
                return !!_.find(alerts, function (alertMeta, index) {
                    return !!_.find(alertMeta.conditions, function (conditionMeta) {
                        return conditionMeta.value === answerId;
                    });
                });
            }
        },

        renderOpenEndedAnswerSet: function () {
            var answerSetView = new ProtocolTreeAnswerOpenEndedView();

            this.$answerSetBox.html(answerSetView.render().$el);
        },

        renderScaleAnswerSet: function (answerSet) {
            var answerScaleModel = new ProtocolTreeAnswerScaleModel(answerSet),
                treeAnswerScaleView = new ProtocolTreeAnswerScaleItemView({ model: answerScaleModel });

            this.$answerSetBox.html(treeAnswerScaleView.render().$el);

            $("input.scale").slider({
                handle: 'custom',
                tooltip: 'hide'
            });
        },

        onSaveProtocol: function () {
            var self = this,
                protocolName = this.$('#protocol-name').val(),
                protocolPrivacy = this.$el.find('#protocol-privacy').is(':checked');

            this.model.set('name', protocolName);
            this.model.set('isPrivate', protocolPrivacy);

            // Updating model with Tags
            var newTags = this.$el.find('#protocol-details-tags').tokenfield('getTokensList').split(', ');

            newTags = newTags.filter(function (element, index) {
                return element !== '';
            });

            this.model.set('tags', newTags);

            if (this.isModelValid()) {
                this.$('.js-save-protocol').button('loading');
                this.$('.js-alert-fail').addClass('hidden');

                this.model.save(null, {
                    success: function (model, response, options) {
                        self.renderTitle();

                        if (response.Id) {
                            self.$('.js-alert-create-success').removeClass('hidden');
                            app.router.navigate('EditProtocol/' + response.Id, {
                                trigger: false
                            });
                        } else {
                            self.$('.js-alert-update-success').removeClass('hidden');
                        }

                        setTimeout(function () {
                            self.$('.js-alert-create-success').addClass('hidden');
                            self.$('.js-alert-update-success').addClass('hidden');
                            self.$('.btn.js-save-protocol').button('reset');
                        }, 4000);

                        self.$el.find('.js-protocol-name').html(self.model.get('name'));
                        self.isProtocolChanged = false;

                        self.updateProtocolsCollection();
                    },
                    error: function (model, xhr, options) {
                        self.$('.btn.js-save-protocol').button('reset');
                        self.$('.js-alert-fail')
                            .html(xhr.responseJSON.ErrorMessage)
                            .removeClass('hidden');
                    }
                });
            }
        },

        //workaround to make protocols collection up to date
        updateProtocolsCollection: function () {
            var collection = app.collections.protocolsProgramsCollection;
            if (!collection) return;

            var protocolModel = collection.findWhere({id: this.model.get('id')});
            protocolModel.set(this.model.toJSON());
        },

        // Validates model and shows errors
        isModelValid: function () {
            var result = true;

            if (!this.model.isValid(true)) {
                result = false;
            }

            if (!this.model.isBranchesValid()) {
                result = false;

                $('.js-alert-fail')
                    .html($('#invalid-branches-error').val())
                    .removeClass('hidden');
            }

            return result;
        },

        onEndProtocol: function (ev) {
            var $this = $(ev.target),
                elementId = $this.data('elementid'),
                protocolElements = this.model.get('protocolElements'),
                protocolElement = _.findWhere(protocolElements, { id: elementId }),
                $elementBox = $this.closest('.protocol-item-element');

            if ($this.hasClass('protocol-set-end')) {
                protocolElement.nextProtocolElementId = null;
                this.enableContinueProtocolControls($elementBox);
            } else {
                var prevProtocolElement = this.getParentElement(elementId);

                protocolElement.nextProtocolElementId = prevProtocolElement.nextProtocolElementId;
                this.enableEndProtocolControls($elementBox);
            }
        },

        onEndBranch: function (ev) {
            var $this = $(ev.target),
                elementId = $this.data('elementid'),
                branchId = $this.data('branchid'),
                protocolElements = this.model.get('protocolElements'),
                protocolElement = _.findWhere(protocolElements, { id: elementId }),
                branch = _.findWhere(protocolElement.branches, { id: branchId }),
                $elementBox = $this.closest('.protocol-item-element');

            if ($this.hasClass('protocol-set-end')) {
                branch.nextProtocolElementId = null;
                this.enableContinueProtocolControls($elementBox);
            } else {
                branch.nextProtocolElementId = protocolElement.nextProtocolElementId;
                this.enableEndProtocolControls($elementBox);
            }
        },

        onDeleteElementProtocol: function (ev) {
            var $this = $(ev.target),
                elementId = $this.data('elementid'),
                protocolElements = this.model.get('protocolElements'),
                protocolElement = _.findWhere(protocolElements, { id: elementId });

            this.deleteLinksToElement(elementId, protocolElement.nextProtocolElementId);
            this.model.deleteProtocolElement(elementId, protocolElement.nextProtocolElementId);

            protocolElements = this.model.get('protocolElements');

            this.onDeleteElementProtocolUpdateHtml($this, protocolElements.length);
            this.model.set('protocolElements', protocolElements);
            this.isProtocolChanged = true;
        },

        onDeleteMeasurementBranch: function (ev) {
            var self = this,
                confirmationModal = new BackboneBootstrapModal({
                    content: 'Are you sure you want to delete measurement branch with all its content?',
                    showHeader: false,
                    okText: 'Confirm',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

            confirmationModal.on('ok', function () {
                var $this = $(ev.target),
                    branchId = $this.data('branchid'),
                    protocolElement = self.getElementByBranchId(branchId);

                self.model.deleteMeasurementBranch(branchId);
                $this.closest('li.protocol-item').remove();

                if (!protocolElement.branches.length && protocolElement.nextProtocolElementId != null) {
                    self.enableEndProtocolControls($('[data-protocolelementid="' + protocolElement.id + '"]'));
                }

                self.isProtocolChanged = true;
            });

            confirmationModal.open();
        },

        onDeleteElementProtocolUpdateHtml: function ($this, elementsLength) {
            var $protocolList = $this.closest('ol.protocol-list');

            $this.closest('li.protocol-item').remove();

            if ($protocolList.find(' > .protocol-item').length == 0) {
                $protocolList.siblings('.ui-droppable-disabled').droppable('enable');
                if (elementsLength) {
                    $protocolList.remove();
                }
            }

            if (!elementsLength) {
                $('.js-save-protocol')
                    .addClass('disabled')
                    .prop('disabled', true);

                $('.js-protocol-tree').append(this.placeholder);
            }
        },

        initAlertIfProtocolChanged: function () {
            var self = this;

            $(window).bind('beforeunload', function (e) {
                if (self.isProtocolChanged) {

                    var message = "You have made changes to the current tab. Are you sure you want to cancel and discard your changes?";
                    e.returnValue = message;

                    return message;
                }
            });
        },

        onAddMeasurementBranch: function (e) {
            var $addBtn = $(e.target),
                self = this,
                measurementType = $addBtn.attr('data-measurement-type'),
                measurementName = $addBtn.attr('data-measurement-name'),
                protocolElementId = $addBtn.attr('data-elementId'),
                protocolElement = _.findWhere(self.model.get('protocolElements'), { id: protocolElementId }),
                parentProtocolElement = this.getParentElement(protocolElementId),
                measurementBranchModel = new MeasurementBranchModel({
                    measurementType: measurementType,
                    name: measurementName,
                    id: Helpers.getGUID(),
                    nextProtocolElementId: protocolElement.isMainBranch ?
                        protocolElement.nextProtocolElementId :
                        parentProtocolElement.nextProtocolElementId,
                    elementId: protocolElement.id,
                }),
                measurementBranchModalView = new MeasurementBranchModalView({ model: measurementBranchModel }),
                modalView = new BackboneBootstrapModal({
                    content: measurementBranchModalView,
                    title: 'Add Custom Branch (' + measurementName + ')',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                }),
                $branchesBox = $addBtn.closest('.protocol-item').find('.protocol-list')[0];

            modalView.on("ok", function () {
                if (!measurementBranchModalView.validate(protocolElement)) {
                    modalView.preventClose();

                    return;
                }

                measurementBranchModel.initAlertSeverity();

                var protocolTreeMeasurementBranchView = new ProtocolTreeMeasurementBranchView({ model: measurementBranchModel }),
                    $branchElement = protocolTreeMeasurementBranchView.render().$el;

                $branchElement.appendTo($branchesBox);
                protocolElement.branches.push(measurementBranchModel.getBranchJson());

                if (!protocolElement.isMainBranch) {
                    protocolElement.nextProtocolElementId = parentProtocolElement.nextProtocolElementId;
                }

                self.hideEndProtocolControls($('[data-protocolelementid="' + protocolElement.id + '"]'));

                if (self.getMainBranchElement(protocolElement.id).nextProtocolElementId == null) {
                    self.hideEndProtocolControls($branchElement);
                }
            });

            modalView.open();
        },

        onEditMeasurementBranch: function (e) {
            var self = this,
                $editBtn = $(e.target),
                branchId = $editBtn.data('branchid'),
                protocolElement = this.getElementByBranchId(branchId),
                measurementBranch = this.getBranchById(branchId),
                measurementBranchModel = new MeasurementBranchModel({
                    measurementType: protocolElement.element.measurementType,
                    name: protocolElement.element.name,
                    id: measurementBranch.id,
                    nextProtocolElementId: measurementBranch.nextProtocolElementId,
                    conditions: measurementBranch.conditions,
                    elementId: protocolElement.id,
                    thresholdAlertSeverityId: measurementBranch.thresholdAlertSeverityId
                }),
                measurementBranchModalView = new MeasurementBranchModalView({
                    model: measurementBranchModel
                }),
                modalView = new BackboneBootstrapModal({
                    content: measurementBranchModalView,
                    title: 'Edit Custom Branch (' + measurementBranchModel.get('name') + ')',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

            modalView.on("ok", function () {
                if (!measurementBranchModalView.validate(protocolElement)) {
                    modalView.preventClose();

                    return;
                }

                measurementBranchModel.initAlertSeverity();

                var protocolTreeMeasurementBranchView = new ProtocolTreeMeasurementBranchView({ model: measurementBranchModel }),
                    $branchContainer = protocolTreeMeasurementBranchView.render().$el.find('.protocol-measurement-branch');

                $editBtn.closest('.protocol-measurement-branch').replaceWith($branchContainer);
                protocolElement.branches = _.filter(protocolElement.branches, function (branchEl) {
                    return branchEl.id !== branchId;
                });
                protocolElement.branches.push(measurementBranchModel.getBranchJson());

                if (protocolElement.isMainBranch) {
                    self.hideEndProtocolControls($branchContainer);
                }
            });

            modalView.open();
        },

        // Removes all links to element with specified id
        // Updates End protocol and Continue protocol controls
        deleteLinksToElement: function (removedElementId, nextProtocolElementId) {
            var firstProtocolElementId = this.model.get('firstProtocolElementId'),
                protocolElements = this.model.get('protocolElements'),
                self = this,
                removedElement = _.findWhere(protocolElements, { id: removedElementId }),
                parentElement = removedElement.isMainBranch ? removedElement : self.getParentElement(removedElementId);

            // For case if element is set as first protocol element
            if (firstProtocolElementId === removedElementId) {
                this.model.set('firstProtocolElementId', nextProtocolElementId);

                return;
            }

            // If deleted element is ended then prev elements should points to next branch
            if (!nextProtocolElementId) {
                nextProtocolElementId = parentElement.nextProtocolElementId;
            }

            // For case if element is linked in branches or in previous elements.
            _.each(protocolElements, function (element) {

                // For last elements in branches which linked with removed element
                if (element.nextProtocolElementId === removedElementId) {
                    element.nextProtocolElementId = nextProtocolElementId;

                    if (nextProtocolElementId != null && nextProtocolElementId === parentElement.nextProtocolElementId) {
                        self.enableEndProtocolControls($('[data-protocolelementid="' + element.id + '"]'));
                    }
                }

                // For last elements in main branch
                if (element.nextProtocolElementId == null && parentElement.nextProtocolElementId == null) {
                    self.hideEndProtocolControls($('[data-protocolelementid="' + element.id + '"]'));
                }

                _.each(element.branches, function (branch) {
                    if (branch.nextProtocolElementId === removedElementId) {
                        var $branchBox = $('[data-branchid="' + branch.id + '"]');

                        branch.nextProtocolElementId = nextProtocolElementId;
                        $branchBox.removeClass('protocol-item-disabled');

                        if (element.nextProtocolElementId != null && element.nextProtocolElementId === nextProtocolElementId) {
                            self.enableEndProtocolControls($branchBox);
                        }
                    }
                });
            });
        },

        // Returns root element from main thread for element with specified id.
        getMainBranchElement: function (elementId) {
            var protocolElements = this.model.get('protocolElements'),
                currElement = _.findWhere(protocolElements, { id: elementId }),
                parentElement;

            if (currElement.isMainBranch) {
                return currElement;
            }

            _.each(protocolElements, function (protocolElement) {
                if (protocolElement.nextProtocolElementId === elementId) {
                    parentElement = protocolElement;

                    return false;
                }

                _.each(protocolElement.branches, function (branch) {
                    if (branch.nextProtocolElementId === elementId) {
                        parentElement = protocolElement;

                        return false;
                    }
                });
            });

            return parentElement.isMainBranch ? parentElement : this.getMainBranchElement(parentElement.id);
        },

        // Returns parent element for branch with specified element.
        getParentElement: function (elementId) {
            var prevElementId = $('[data-protocolelementid="' + elementId + '"]')
                    .closest('.protocol-list')
                    .parent()
                    .closest('.protocol-list')
                    .siblings('.protocol-item-element-question')
                    .data('protocolelementid'),
                prevProtocolElement = _.findWhere(this.model.get('protocolElements'), { id: prevElementId });

            return prevProtocolElement;
        },

        getElementByBranchId: function (branchId) {
            var target;

            _.each(this.model.get('protocolElements'), function (protocolElement) {
                _.each(protocolElement.branches, function (branch) {
                    if (branch.id === branchId) {
                        target = protocolElement;
                    }
                });
            });

            return target;
        },

        getBranchById: function (id) {
            var target;

            _.each(this.model.get('protocolElements'), function (protocolElement) {
                _.each(protocolElement.branches, function (branch) {
                    if (branch.id === id) {
                        target = branch;
                    }
                });
            });

            return target;
        },

        setAlertToAnswer: function (e) {
            var elementId = $(e.target).data('question-id'),
                answerId = $(e.target).data('answer-id'),
                alertSeverityId = $(e.target).data('alert-severity-id'),
                protocolElements = this.model.get('protocolElements'),
                questionProtocolElement = _.findWhere(protocolElements, { id: elementId });

            var newAlertMeta = {
                alertSeverityId: alertSeverityId,
                conditions: [
                    {
                        operand: 'SelectionAnswerChoice',
                        operator: 'Equals',
                        value: answerId
                    }
                ]
            };

            questionProtocolElement.alerts = questionProtocolElement.alerts || [];
            questionProtocolElement.alerts.push(newAlertMeta);
            this.model.set('protocolElements', protocolElements);

            this.checkAlertsVisibility();
            this.isProtocolChanged = true;
        },

        unsetAlertFromAnswer: function (e) {
            var elementId = $(e.target).data('question-id'),
                answerId = $(e.target).data('answer-id'),
                protocolElements = this.model.get('protocolElements'),
                questionProtocolElement = _.findWhere(protocolElements, { id: elementId });

            //TODO: refactor this
            var newAlertsMeta = _.clone(questionProtocolElement.alerts);
            _.each(questionProtocolElement.alerts, function (alertMeta) {
                if (alertMeta.conditions[0].value === answerId) {
                    newAlertsMeta = _.without(newAlertsMeta, alertMeta);
                }
            });
            questionProtocolElement.alerts = newAlertsMeta;
            this.model.set('protocolElements', protocolElements);

            this.checkAlertsVisibility();
            this.isProtocolChanged = true;

        },

        checkAlertsVisibility: function () {
            var self = this,
                protocolElements = this.model.get('protocolElements');

            //display icons like all alerts are unset
            self.$el.find('.js-set-alert-li').show();
            self.$el.find('.js-unset-alert-li').hide();
            self.$el.find('[id^="answer-alert-icon"]').hide();

            //display icons of alerts that are set up
            _.each(protocolElements, function (protocolElement) {
                _.each(protocolElement.alerts || [], function (alert) {
                    var alertSeverity = app.collections.alertSeverities.where({ id: alert.alertSeverityId })[0],
                        alertColor = alertSeverity ? alertSeverity.get('colorCode') : '';

                    _.each(alert.conditions, function (condition) {
                        var alertId = condition.value,
                            $answerSettings = self.$el.find('#settings-answer-' + alertId + '-at-question-' + protocolElement.id);

                        $answerSettings.find('.js-set-alert-li').hide();
                        $answerSettings.find('.js-unset-alert-li').show();
                        self.$el.find('#answer-alert-icon-' + alertId + '-at-question-' + protocolElement.id)
                            .show()
                            .css('color', alertColor);
                    });
                });
            });
        },

        onToggleAnswers: function (e) {
            e.preventDefault();

            var text = $(e.target).text();

            $(e.target).text(text === "Show Answers" ? "Hide Answers" : "Show Answers");

            $(e.target)
                .closest('.protocol-item-element')
                .next('.protocol-list')
                .slideToggle(300);
        },

        simulateProtocol: function () {
            if (app.views.protocolSimulatorView)
                app.views.protocolSimulatorView.remove();
            app.views.protocolSimulatorView = new ProtocolSimulatorView({ model: this.model });
            this.$el.prepend(app.views.protocolSimulatorView.render().el);
            $("#care-builder-container input.scale").slider();
        }

    });
});