'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/QuestionElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolQuestionListItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeQuestionItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeMeasurementBranchView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerChoiceItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerScaleItemView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolTreeAnswerOpenEndedView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolEditingView',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeQuestionModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeAnswerChoiceModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolTreeAnswerScaleModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/MeasurementBranchModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers'
], function (
        $,
        _,
        Backbone,
        ProtocolModel,
        QuestionElementsCollection,
        ProtocolQuestionListItemView,
        ProtocolTreeQuestionItemView,
        ProtocolTreeMeasurementBranchView,
        ProtocolTreeAnswerChoiceItemView,
        ProtocolTreeAnswerScaleItemView,
        ProtocolTreeAnswerOpenEndedView,
        ProtocolEditingView,
        ProtocolTreeQuestionModel,
        ProtocolTreeAnswerChoiceModel,
        ProtocolTreeAnswerScaleModel,
        MeasurementBranchModel,
        AppGlobalVariables,
        Helpers
    ) {
    return Backbone.View.extend({
        el: '#care-builder-container',

        initialize: function () {
            Helpers.renderSpinner(this.$el);
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
                    var alertSeverity = AppGlobalVariables.collections.alertSeverities.where({ id: alert.alertSeverityId })[0],
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

        render: function (protocolId) {
            var self = this;

            this.model = new ProtocolModel({ id: protocolId });

            Helpers.renderSpinner($('.js-protocol-tree'));

            self.model.fetch({
                success: function () {
                    console.log(self.model.attributes);
                    self._clearView(self.protocolEditingView);
                    self.protocolEditingView = new ProtocolEditingView({ model: self.model });
                    self.protocolEditingView.render(null, function () {
                        _.each(self.model.attributes.protocolElements, function (protocolElement) {
                            if (!protocolElement.elementId) {
                                protocolElement.elementId = protocolElement.element.id;
                            }

                            if (protocolElement.element.type === 2
                                || protocolElement.element.type === 3
                                || protocolElement.element.type === 4) {

                                protocolElement.element.questionElementString = { value: protocolElement.element.name };
                            }
                        });

                        self.renderProtocolTree();
                        self.checkAlertsVisibility();

                        $(".protocol-item-element").droppable();
                    });
                }
            });
        },

        renderProtocolTree: function () {
            var self = this,
                protocolElements = this.model.get('protocolElements'),
                firstProtocolElementId = this.model.get('firstProtocolElementId'),
                nextProtocolElementId = firstProtocolElementId,
                elementContainer = self.$el.find('.js-protocol-tree');

            this.initProtocolElements(firstProtocolElementId);
            elementContainer.find('.js-placeholder').remove();

            while (nextProtocolElementId) {
                nextProtocolElementId = self.renderProtocolTreeElement(protocolElements, nextProtocolElementId, elementContainer);
            }

            $("input.scale").slider({
                handle: 'custom',
                tooltip: 'hide'
            });

            $('.dropdown-toggle').dropdown();

            self.disableAnswers();
        },

        initProtocolElements: function (firstProtocolElementId) {
            var protocolElements = this.model.get('protocolElements'),
                mainBranchElement = _.findWhere(protocolElements, { id: firstProtocolElementId });

            while (mainBranchElement != undefined) {
                mainBranchElement.isMainBranch = true;

                mainBranchElement = _.findWhere(protocolElements, { id: mainBranchElement.nextProtocolElementId });
            }

            _.each(protocolElements, function (protocolElement) {
                if (!protocolElement.isMainBranch) {
                    protocolElement.isMainBranch = false;
                }

                _.each(protocolElement.branches, function (branch) {
                    branch.id = Helpers.getGUID();
                });
            });
        },

        renderProtocolTreeElement: function (protocolElements, nextProtocolElementId, elementContainer) {
            var self = this,
                questionProtocolElement = _.findWhere(protocolElements, { id: nextProtocolElementId }),
                questionModel = new ProtocolTreeQuestionModel(questionProtocolElement),
                questionElementId = questionModel.get('id'),
                treeQuestionView = new ProtocolTreeQuestionItemView({ model: questionModel }),
                answerSet = questionModel.get('element').answerSet,
                branches = questionModel.get('branches'),
                alerts = questionModel.get('alerts'),
                $elementBox = treeQuestionView.render().$el;
            
            nextProtocolElementId = questionProtocolElement.nextProtocolElementId;
            elementContainer.append($elementBox);
            elementContainer
                .find('.protocol-item#' + questionElementId + ' > .protocol-item-element')
                .attr('data-protocolElementID', questionElementId)
                .data('protocolElementID', questionElementId);

            if (questionProtocolElement.nextProtocolElementId == null
                && this.protocolEditingView.getMainBranchElement(questionProtocolElement.id).nextProtocolElementId == null) {
                this.protocolEditingView.hideEndProtocolControls($elementBox);
            }

            var answerSetBox = $('ol#' + questionElementId + '-container');

            if (answerSet) {
                self.renderProtocolTreeAnswers(answerSet, answerSetBox, questionProtocolElement, alerts);
            }

            if (branches.length) {
                _.each(branches, function (branch) {
                    var branchNextProtocolElementId = branch.nextProtocolElementId,
                        answerBox;

                    if (questionProtocolElement.element.type === 3) {
                        var $measurementBranch = self.renderMeasurementBranch(treeQuestionView,
                            questionProtocolElement, branch, branchNextProtocolElementId);

                        if ($measurementBranch == null) {
                            branchNextProtocolElementId = null;
                        } else {
                            answerBox = $measurementBranch.find('.protocol-list').first();
                        }
                    } else {
                        var branchConditions = branch.conditions[0];

                        answerBox = $('#' + questionElementId + ' .protocol-item-element-answer[data-branchid="' + branch.id + '"]').closest('.protocol-item');
                        answerBox.append('<ol class="protocol-list" id="' + questionElementId + '-' + branchConditions.value + '" />');
                        answerBox = answerBox.find('#' + questionElementId + '-' + branchConditions.value);
                    }

                    while (nextProtocolElementId !== branchNextProtocolElementId && branchNextProtocolElementId !== null) {
                        branchNextProtocolElementId = self.renderProtocolTreeElement(protocolElements, branchNextProtocolElementId, answerBox);
                    }
                });
            }

            return nextProtocolElementId;
        },

        renderMeasurementBranch: function (treeQuestionView, questionProtocolElement, branch, branchNextProtocolElementId) {
            var measurementModel = new MeasurementBranchModel({
                measurementType: questionProtocolElement.element.measurementType,
                name: questionProtocolElement.element.name,
                id: branch.id,
                nextProtocolElementId: branchNextProtocolElementId,
                conditions: branch.conditions,
                elementId: questionProtocolElement.id,
                thresholdAlertSeverityId: branch.thresholdAlertSeverityId
            }),
                measurementView = new ProtocolTreeMeasurementBranchView({ model: measurementModel }),
                measurementBranchesBox = treeQuestionView.$el.find('.protocol-list').first();

            if (measurementModel.isValid()) {
                branch.id = measurementModel.get('id');
                measurementView.render().$el.appendTo(measurementBranchesBox);

                return measurementView.$el;
            }

            return null;
        },

        renderProtocolTreeAnswers: function (answerSet, answerSetBox, protocolElement, alerts) {
            var self = this;

            if (answerSet.type === 1) {
                var answerScaleModel = new ProtocolTreeAnswerScaleModel(answerSet),
                    treeAnswerScaleView = new ProtocolTreeAnswerScaleItemView({ model: answerScaleModel });

                answerSetBox.append(treeAnswerScaleView.render().$el);
            } else if (answerSet.type === 2) {
                var answerSetView = new ProtocolTreeAnswerOpenEndedView();

                answerSetBox.append(answerSetView.render().$el);
            } else {
                _.each(answerSet.selectionAnswerChoices, function (answer) {
                    var $answerBox = self.renderSelectionAnswerChoice(answer, protocolElement, alerts, answerSet.isMultipleChoice);

                    answerSetBox.append($answerBox);
                });
            }
        },

        renderSelectionAnswerChoice: function (answer, protocolElement, alerts, isMultipleChoice) {
            var alert = _getAlertByAnswerId(answer.id),
                branch = _getBranchByAnswerId(answer.id),
                answerChoiceModel = new ProtocolTreeAnswerChoiceModel(answer),
                treeAnswerChoiceView = new ProtocolTreeAnswerChoiceItemView({
                    model: answerChoiceModel,
                    protocolElement: protocolElement,
                    alert: alert,
                    isMultipleChoice: isMultipleChoice
                });

            answerChoiceModel.set('nextProtocolElementId', branch.nextProtocolElementId);
            answerChoiceModel.set('branchId', branch.id);
            var $answerBox = treeAnswerChoiceView.render().$el;

            if (branch.nextProtocolElementId !== protocolElement.nextProtocolElementId && branch.nextProtocolElementId != null
                || branch.nextProtocolElementId == null && protocolElement.nextProtocolElementId == null) {
                this.protocolEditingView.hideEndProtocolControls($answerBox);
            }

            return $answerBox;

            function _getAlertByAnswerId(answerId) {
                return !!_.find(alerts, function (alertMeta, index) {
                    return !!_.find(alertMeta.conditions, function (conditionMeta) {
                        return conditionMeta.value === answerId;
                    });
                });
            }

            // Returns existed branch for answer or creates new
            function _getBranchByAnswerId(answerId) {
                var result = _.find(protocolElement.branches, function (branch, index) {
                    return _.find(branch.conditions, function (condition) {
                        return condition.value === answerId;
                    });
                });

                if (result == undefined) {
                    result = {
                        id: Helpers.getGUID(),
                        nextProtocolElementId: null,
                        conditions: [
                            {
                                operand: 'SelectionAnswerChoice',
                                operator: 'Equals',
                                value: answerId
                            }
                        ]
                    };

                    protocolElement.branches.push(result);
                }

                return result;
            }
        },

        disableAnswers: function () {
            var protocolItemAnswers = $('.js-protocol-tree').find('.protocol-item-answer');

            protocolItemAnswers.each(function () {
                if ($(this).find('.protocol-item').length) {
                    $(this).children('.protocol-item-element-answer').addClass('protocol-item-disabled');
                }
            });
        },

        close: function () {
            if (this.protocolEditingView) {
                this._clearView(this.protocolEditingView);
            }
        },

        _clearView: function (view) {
            if(AppGlobalVariables.views.contentTypeModalView) {
                AppGlobalVariables.views.contentTypeModalView.close();
            }
            if (view) {
                view.unbind();
                view.undelegateEvents();
            }
        }
        
    });
});