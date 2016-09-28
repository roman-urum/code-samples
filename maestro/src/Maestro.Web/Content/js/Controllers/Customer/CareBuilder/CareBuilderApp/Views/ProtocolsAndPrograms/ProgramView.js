'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'BackboneModelBinder',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDetailsModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramWeekModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/RecurrenceModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramWeekView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/RecurrenceModalView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProtocolsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramProtocolListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/OrganizeListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/SearchCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    BackboneBootstrapModal,
    BackboneModelBinder,
    BackboneCollectionBinder,
    AppGlobalVariables,
    ProgramDetailsModel,
    ProgramWeekModel,
    RecurrenceModel,
    ProgramWeekView,
    RecurrenceModalView,
    ProtocolsCollection,
    ProgramElementsCollection,
    ProgramProtocolListItemView,
    OrganizeListItemView,
    SearchCollection,
    ProtocolModel,
    Helpers) {

    return Backbone.View.extend({
        el: '#care-builder-container',

        model: ProgramDetailsModel,

        modelBinder: undefined,

        collectionBinder: undefined,

        template: _.template($("#programViewTemplate").html()),

        initialize: function () {
            var self = this;

            this.modelBinder = new BackboneModelBinder();
            this.isProgramChanged = false;
            this.initAlertIfProgramChanged();

            AppGlobalVariables.collections.ProgramElements = this.model.get('programElements');
            AppGlobalVariables.collections.Weeks = this.model.get('weeks');
            AppGlobalVariables.collections.ProgramElements.on('sort', function () {
                self.renderOrganizeList();
                self.isProgramChanged = true;
            }, this);

            Backbone.Validation.bind(this);
        },

        events: {
            'click .js-save-program': 'onSaveProgram',
            'addProtocolToDay': 'allowSaving',
            'deleteRecurrence': 'onDeleteRecurrence',
            'dayElementDeleted': 'onElementDelete',
            'setDayRecurrence': 'onSetRecurrence',
            'change #search-program-elements-keyword': 'renderProtocolsList',
            'change #search-program-elements-tags': 'renderProtocolsList',
            'click .js-search-clear': 'searchClear'
            
        },


        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.modelBinder.bind(this.model, this.el, BackboneModelBinder.createDefaultBindings(this.el, 'data-name'));

            this.renderTitle();
            this.renderWeeks();

            Helpers.initTags($('.creation-tags'), $('.searching-tags'));

            this.renderProtocolsList();
            this.renderOrganizeList();
            this.setContainersHeight();

            $(window).resize(this.setContainersHeight);

            if (this.model.get('id') == null || !this.model.isValid(true)) {
                this.$el.find('.js-save-program').prop('disabled', true);
            }

            return this;
        },

        setContainersHeight: function () {
            var $containerProgram = $('.program-weeks-ci'),
                $containerElementsList = $('.program-elements-list-container'),
                windowHeight = $(window).height(),
                programTabsHeight = $('.nav-tabs-program-ci').outerHeight(),
                padding = 15,
                containerHeight = windowHeight - 2 * padding;

            $containerProgram.css('height', containerHeight);
            $containerElementsList.css('height', containerHeight - programTabsHeight + 98);

        },

        renderTitle: function () {
            if (this.model.get('id')) {
                var $title = $('#title-page'),
                    editTitle = $title.data("edit-title");

                $title.text(editTitle);
            }
        },

        renderWeeks: function () {
            var self = this,
                weeks = this.model.get('weeks'),
                elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(
                    function (model) {
                        return new ProgramWeekView({ model: model });
                    }),
                collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            collectionBinder.bind(weeks, this.$el.find('.js-weeks'));

            while (weeks.length < 4) {
                weeks.add(new ProgramWeekModel({ number: weeks.length + 1 }));
            }

            this.$el.find('.js-weeks').scroll(function () {
                self.updateWeeksCount();
            });
        },

        renderOrganizeList: function () {
            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {
                return new OrganizeListItemView({ model: model });
            }),
                collectionBinder = new BackboneCollectionBinder(elManagerFactory),
                $elementsContainer = this.$el.find('.js-organize');

            $elementsContainer.html('');
            collectionBinder.bind(AppGlobalVariables.collections.ProgramElements, this.$el.find('.js-organize'));
        },

        renderProtocolsList: function () {
            var $searchResultContainer = this.$el.find('.js-search-result'),
                searchCollection = new SearchCollection(),
                elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(function (model) {
                    return new ProgramProtocolListItemView({
                        model: new ProtocolModel({
                            id: model.get('id'),
                            name: { value: model.get('name') },
                            shortName: model.get('name'),
                            tags: model.get('tags')
                        })
                    });
                }),
                collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            Helpers.renderSpinner($searchResultContainer);

            searchCollection.fetch({
                traditional: true,
                data: {
                    categories: ["protocol"],
                    q: this.getSearchKeyword(),
                    tags: this.getSearchTags()
                },
                success: function (result) {
                    $searchResultContainer.html('');
                    collectionBinder.bind(searchCollection, $searchResultContainer);
                },
                error: function () {
                    console.log('error while fetch protocols');
                }
            });
        },

        getSearchKeyword: function () {
            return this.$el.find('#search-program-elements-keyword').val();
        },

        getSearchTags: function () {

            var searchTags = this.$el.find('#search-program-elements-tags').val();
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
                .change();
        },

        updateWeeksCount: function () {
            var $container = this.$el.find('.js-weeks'),
                containerHeight = $container.height(),
                scrollPosition = $container.scrollTop(),
                scrollHeight = $container.prop('scrollHeight'),
                weeks = this.model.get('weeks');

            if (scrollHeight - scrollPosition < containerHeight + 30) {
                weeks.add(new ProgramWeekModel({ number: weeks.length + 1 }));
            }
        },

        onSaveProgram: function () {
            var self = this;

            // Updating model with Tags
            var newTags = this.$el.find('#program-details-tags').tokenfield('getTokensList').split(', ');

            newTags = newTags.filter(function (element, index) {
                return element !== '';
            });

            this.model.set('tags', newTags);

            if (this.model.isValid(true)) {
                $('.js-save-program').button('loading');
                $('.js-alert-fail').addClass('hidden');

                this.model.save(null, {
                    success: function (model, response, options) {
                        self.renderTitle();

                        if (response.Id) {
                            $('.js-alert-create-success').removeClass('hidden');
                            AppGlobalVariables.router.navigate('EditProgram/' + response.Id, {
                                trigger: false
                            });
                        } else {
                            $('.js-alert-update-success').removeClass('hidden');
                        }

                        setTimeout(function () {
                            $('.js-alert-create-success').addClass('hidden');
                            $('.js-alert-update-success').addClass('hidden');
                            $('.btn.js-save-program').button('reset');
                        }, 4000);

                        self.isProgramChanged = false;
                        self.$el.find('.js-program-name').html(self.model.get('name'));

                        self.updateProgramsCollection();
                    },
                    error: function (model, xhr, options) {
                        $('.btn.js-save-program').button('reset');
                        $('.js-alert-fail')
                            .html(xhr.responseJSON.ErrorMessage)
                            .removeClass('hidden');
                    }
                });
            }
        },

        //workaround to make protocols collection up to date
        updateProgramsCollection: function () {
            var collection = AppGlobalVariables.collections.protocolsProgramsCollection;
            if (!collection) return;

            var programModel = collection.findWhere({ id: this.model.get('id') });
            if (!programModel) return;

            programModel.set(this.model.toJSON());
        },

        allowSaving: function () {
            this.$el.find('.js-save-program').prop("disabled", false);
            this.isProgramChanged = true;
        },

        onDeleteRecurrence: function (event, dayElementModel) {
            var confirmationModal = new BackboneBootstrapModal({
                content: 'Are you sure you want to delete ' + dayElementModel.get('name').value + ' series?',
                showHeader: false,
                okText: 'Delete',
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            }),
                self = this;

            confirmationModal.on('ok', function () {
                self.model.get('weeks').deleteElementsByRecurrenceId(dayElementModel.get('recurrenceId'));
                self.onElementDelete(null, dayElementModel);
                self.isProgramChanged = true;
            });

            confirmationModal.open();
        },

        onElementDelete: function (event, dayElementModel) {
            var deletedElementId = dayElementModel.get('id'),
                deletedElementRecurrenceId = dayElementModel.get('recurrenceId'),
                weeks = this.model.get('weeks');

            if (!weeks.hasElement(deletedElementId)) {
                this.model.get('programElements').deleteElementByProtocolId(deletedElementId);
            }

            if (deletedElementRecurrenceId !== null &&
                weeks.getElementsCountByRecurrence(deletedElementRecurrenceId) <= 1) {
                var recurrences = this.model.get('recurrences'),
                    recurrence = recurrences.where({ id: deletedElementRecurrenceId })[0];

                recurrences.remove(recurrence);
                weeks.removeRecurrenceFromElements(deletedElementRecurrenceId);
            }

            if (weeks.isEmpty()) {
                this.$el.find('.js-save-program').prop("disabled", true);
            }

            this.isProgramChanged = true;
        },

        onSetRecurrence: function (event, elementModel, number) {
            var self = this,
                recurrenceId = elementModel.get('recurrenceId'),
                recurrences = self.model.get('recurrences'),
                modalTitle,
                recurrenceModel;

            if (recurrenceId == null) {
                modalTitle = 'Set Recurrence';
                recurrenceModel = new RecurrenceModel({
                    protocolName: elementModel.get('name').value,
                    startDay: number,
                    endDay: number + 1
                });
            } else {
                modalTitle = 'Edit Recurrence';
                recurrenceModel = recurrences.where({ id: recurrenceId })[0];
                recurrenceModel.set('protocolName', elementModel.get('name').value);
            }

            var recurrenceView = new RecurrenceModalView({
                model: recurrenceModel
            });
            var modal = new BackboneBootstrapModal({
                content: recurrenceView,
                title: modalTitle,
                modalOptions: {
                    backdrop: 'static',
                    keyboard: false
                }
            });

            modal.on('ok', function () {
                var weeks = self.model.get('weeks'),
                    protocolId = elementModel.get('id');

                if (!recurrenceModel.isValid(true)) {
                    modal.preventClose();

                    return;
                }

                if (weeks.hasCollisions(protocolId,
                    recurrenceModel.get('startDay'),
                    recurrenceModel.get('endDay'),
                    number,
                    recurrenceId)) {
                    recurrenceView.$el.find('.help-block-endDay')
                        .html('Health Protocol ' +
                            elementModel.get('name').value +
                            ' is already scheduled for some days between ' + recurrenceModel.get('startDay') +
                            ' and ' + recurrenceModel.get('endDay') + ' days of the Program')
                        .removeClass('hidden');
                    modal.preventClose();

                    return;
                }

                if (recurrenceId != null) {
                    weeks.deleteElementsByRecurrenceId(recurrenceId);
                    recurrences.remove(recurrenceModel);
                } else {
                    weeks.deleteElementByProtocolId(protocolId, number);
                }

                self.model.initRecurrence(recurrenceModel, elementModel);
                self.isProgramChanged = true;
            });

            modal.open();
        },

        initAlertIfProgramChanged: function () {
            var self = this;

            $(window).bind('beforeunload', function (e) {
                if (self.isProgramChanged) {

                    var message = "You have made changes to the current tab. Are you sure you want to cancel and discard your changes?";
                    e.returnValue = message;

                    return message;
                }
            });
        }

    });
});