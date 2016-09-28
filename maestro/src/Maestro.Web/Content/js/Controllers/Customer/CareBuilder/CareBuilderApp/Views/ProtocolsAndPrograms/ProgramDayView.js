'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'BackboneCollectionBinder',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDayModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramProtocolView'
], function ($, _, Backbone, BackboneBootstrapModal, BackboneCollectionBinder, AppGlobalVariables, ProgramDayModel, ProgramElementModel, ProgramProtocolView) {
    return Backbone.View.extend({
        className: 'program-day-ci',

        model: ProgramDayModel,

        collectionBinder: undefined,

        initialize: function () {
            var elManagerFactory = new BackboneCollectionBinder.ViewManagerFactory(
                    function (model) {
                        return new ProgramProtocolView({ model: model });
                    });

            this.collectionBinder = new BackboneCollectionBinder(elManagerFactory);

            this.model.get('dayElements').on('add remove', this.checkDayElements, this);
            this.model.get('dayElements').on('sort', this.renderDayElements, this);
        },

        close: function () {
            this.remove();
            this.collectionBinder.unbind();
            this.model.get('dayElements').unbind('add remove', this.checkDayElements);
            this.model.get('dayElements').unbind('sort', this.renderDayElements);
        },

        template: _.template($("#programDayTemplate").html()),

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.renderDayElements();
            this.initDropBindings();
            this.checkDayElements();

            return this;
        },

        renderDayElements: function () {
            var dayElements = this.model.get('dayElements'),
                $customOrderIcon = this.$el.find('.js-custom-order');

            this.collectionBinder.unbind();
            this.collectionBinder.bind(dayElements, this.$el.find('.js-protocols'));

            if (dayElements.isCustomOrderUsed()) {
                $customOrderIcon.removeClass('hidden');
            } else {
                $customOrderIcon.addClass('hidden');
            }
        },

        events: {
            'deleteProtocol': 'showDeleteProtocolConfirmationModal',
            'setRecurrence': 'onSetRecurrence',
            'setOrderInDay': 'onSetOrderInDay',
            'click .js-custom-order': 'onClickResetCustomOrder'
        },

        checkDayElements: function () {
            if (this.model.get('dayElements').length === 0) {
                this.$el.addClass('empty');
            } else {
                this.$el.removeClass('empty');
            }
        },

        initDropBindings: function () {
            var self = this;

            this.$el.droppable({
                over: function (event, ui) {
                    var protocolId = ui.draggable.data('id'),
                        $protocolsBox = self.$el.find('.js-protocols');

                    if (self.hasProtocol(protocolId)) {
                        $protocolsBox.addClass('drop-not-allowed');
                    } else {
                        $protocolsBox.addClass('placeholder');
                    }
                },

                out: function () {
                    var $protocolsBox = self.$el.find('.js-protocols');

                    $protocolsBox.removeClass('placeholder');
                    $protocolsBox.removeClass('drop-not-allowed');
                },

                drop: function (event, ui) {
                    var $protocolsBox = self.$el.find('.js-protocols'),
                        dayElements = self.model.get('dayElements'),
                        programElement = new ProgramElementModel({
                            id: ui.draggable.data('id'),
                            name: {
                                value: ui.draggable.data('name')
                            }
                        });

                    $protocolsBox.removeClass('drop-not-allowed');
                    $protocolsBox.removeClass('placeholder');

                    if (!self.hasProtocol(programElement.get('id')) && ui.draggable.data('dragObjectType') !== 'dayElement') {
                        AppGlobalVariables.collections.ProgramElements.addElementToOrder(programElement);
                        dayElements.addDayElement(programElement);
                        self.renderDayElements();
                        self.$el.trigger('addProtocolToDay');
                    }
                }
            });
        },

        hasProtocol: function (protocolId) {
            var dayElements = this.model.get('dayElements'),
                result = false;

            dayElements.each(function (element) {
                if (element.get('id') == protocolId) {
                    result = true;
                }
            });

            return result;
        },

        showDeleteProtocolConfirmationModal: function (event, dayElementModel) {
            var protocolName = dayElementModel.get('name').value,
                dayNumber = this.model.get('number'),
                message = dayElementModel.get('recurrenceId') != null ?
                    'Are you sure you want to delete ' + protocolName + ' occurrence from Day ' + dayNumber + '?' :
                    'Are you sure you want to delete ' + protocolName + ' from Day ' + dayNumber + '?',
                confirmationModal = new BackboneBootstrapModal({
                    content: message,
                    showHeader: false,
                    okText: 'Delete',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                }),
                self = this;

            confirmationModal.on('ok', function () {
                var dayElements = self.model.get('dayElements');

                dayElements.remove(dayElementModel);
                self.$el.trigger('dayElementDeleted', dayElementModel);
            });

            confirmationModal.open();
        },

        onSetRecurrence: function (event, elementModel) {
            this.$el.trigger('setDayRecurrence', [elementModel, this.model.get('number')]);
        },

        onSetOrderInDay: function (event, dragElementId, dropElementId) {
            this.model.get('dayElements').setOrderInDay(dragElementId, dropElementId);
            this.renderDayElements();
            AppGlobalVariables.views.ProgramView.isProgramChanged = true;
        },

        onClickResetCustomOrder: function () {
            var self = this,
                message = 'Day ' + this.model.get('number') + ' has custom order configured. Do you want to reset it?',
                confirmationModal = new BackboneBootstrapModal({
                    content: message,
                    showHeader: false,
                    okText: 'Reset',
                    modalOptions: {
                        backdrop: 'static',
                        keyboard: false
                    }
                });

            confirmationModal.on('ok', function () {
                self.model.get('dayElements').resetCustomOrder();
                self.renderDayElements();
            });

            confirmationModal.open();
        }
    });
});