'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/MultipleChoiceCollectionView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/ScaleView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/MeasurementView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/TextAndMediaView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/OpenEndedView'
], function (
    $,
    _,
    Backbone,
    app,
    MultipleChoiceCollectionView,
    ScaleView,
    MeasurementView,
    TextAndMediaView,
    OpenEndedView
) {
    return Backbone.View.extend({

        template: _.template('<div class="simulator-content-question pull-left">\
                                <div class="question-text">\
                                    <div class="text"><%=value%></div>\
                                </div>\
                                <div class="question-nav">\
                                    <a class="nav-next-question js-next-question">Next Step</a>\
                                </div>\
                            </div>\
        '),

        events: {

            'click .js-select-answer': 'selectMultipleChoiceAnswer',
            'click .js-next-question': 'renderNextProtocolElement'

        },

        initialize: function () {
            this.listenTo(this.model, 'change:isDisplay', this.toggleProtocolElement);
        },

        render: function () {
            this.renderProtocolElement(this.model.attributes);
            this.toggleProtocolElement();
            return this;
        },

        renderProtocolElement: function (currentElement) {

            switch (currentElement.element.type) {
                case 1:

                    switch (currentElement.element.answerSet.type) {
                        case 0:
                            this.renderMultipleChoice(currentElement.element);
                            break;
                        case 1:
                            this.renderScale(currentElement.element);
                            break;
                        case 2:
                            this.renderOpenEnded(currentElement.element);
                            break;
                    }
                    break;
                case 2:
                    this.renderTextAndMedia(currentElement.element);
                    break;
                case 3:
                    this.renderMeasurement(currentElement.element);
                    break;
            }

        },

        renderMultipleChoice: function (element) {
            var MultipleChoiceCollection = Backbone.Collection.extend({});
            app.collections.multipleChoiceCollection = new MultipleChoiceCollection(element.answerSet.selectionAnswerChoices);
            app.views.multipleChoiceCollectionView = new MultipleChoiceCollectionView({ collection: app.collections.multipleChoiceCollection });

            this.$el.html(this.template(element.questionElementString));
            this.$el.append(app.views.multipleChoiceCollectionView.render().el);

        },

        renderScale: function (element) {
            var ScaleModel = Backbone.Model.extend({});
            app.models.scaleModel = new ScaleModel(element);
            app.views.scaleView = new ScaleView({ model: app.models.scaleModel });

            this.$el.html(this.template(element.questionElementString));
            this.$el.append(app.views.scaleView.render().el);
            this.$el.find('.js-next-question').addClass('selected');

            return this;
        },

        renderOpenEnded: function (element) {
            var OpenEndedModel = Backbone.Model.extend({});
            app.models.openEndedModel = new OpenEndedModel(element);
            app.views.openEndedView = new OpenEndedView({ model: app.models.openEndedModel });

            // this.$el.html( this.template(element.questionElementString) );
            this.$el.html(app.views.openEndedView.render().el);
            this.$el.find('.js-next-question').addClass('selected');

            return this;

        },

        renderMeasurement: function (element) {
            var MeasurementModel = Backbone.Model.extend({});
            app.models.measurementModel = new MeasurementModel(element);
            app.views.measurementView = new MeasurementView({ model: app.models.measurementModel });

            // this.$el.html( this.template(element.questionElementString) );
            this.$el.html(app.views.measurementView.render().el);
            this.$el.find('.js-next-question').addClass('selected');

            return this;

        },

        renderTextAndMedia: function (element) {
            var TextAndMediaModel = Backbone.Model.extend({});
            app.models.textAndMediaModel = new TextAndMediaModel(element);
            app.views.textAndMediaView = new TextAndMediaView({ model: app.models.textAndMediaModel });

            this.$el.append(app.views.textAndMediaView.render().el);
            this.$el.find('.js-next-question').addClass('selected');

            return this;
        },

        selectMultipleChoiceAnswer: function (e) {
            var answerId = $(e.currentTarget).data('id');
            this.$el.find('.js-next-question').data('id', answerId);
            this.$el.find('.js-select-answer').removeClass('selected');
            $(e.currentTarget).addClass('selected');
            this.$el.find('.js-next-question').addClass('selected');
            return this;
        },

        renderNextProtocolElement: function (e) {
            var answerId = $(e.currentTarget).data('id'),
                currentElement,
                nextProtocolElementId,
                branches = this.model.get('branches');

            if (!$(e.currentTarget).hasClass('selected')) return false;

            if (branches.length) {

                _.each(branches, function (branch) {
                    if (branch.conditions[0].value == answerId) {
                        return nextProtocolElementId = branch.nextProtocolElementId;
                    }
                });
            }

            if (!nextProtocolElementId) {
                nextProtocolElementId = this.model.get('nextProtocolElementId');
            }

            currentElement = app.collections.protocolElementsCollection.find(function (model) {
                return model.get('isDisplay') == true;
            });
            currentElement.set('isDisplay', false);

            if (nextProtocolElementId) {
                currentElement = app.collections.protocolElementsCollection.find(function (model) {
                    return model.get('id') == nextProtocolElementId;
                });
                currentElement.set('isDisplay', true);
            } else {
                this.$el.html('<h1 class="the-end">Thank you!</h1>').removeClass('hidden');
            }
        },

        toggleProtocolElement: function () {
            if (this.model.get('isDisplay'))
                this.$el.removeClass('hidden');
            else
                this.$el.addClass('hidden');
        }

    });
});