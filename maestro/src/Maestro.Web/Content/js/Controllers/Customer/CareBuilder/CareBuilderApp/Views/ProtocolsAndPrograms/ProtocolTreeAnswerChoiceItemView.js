'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables'
], function (
    $,
    _,
    Backbone,
    app
) {
    return Backbone.View.extend({
        tagName: 'li',

        className: 'protocol-item protocol-item-answer',

        template: _.template($("#protocolTreeAnswerChoiceItemTemplate").html()),

        events: {

        },

        initialize: function (options) {
            this.protocolElement = options.protocolElement;
            this.isMultipleChoice = options.isMultipleChoice;
        },

        render: function () {
            var data = _.extend({
                elementId: this.protocolElement.id,
                nextProtocolElementId: this.protocolElement.nextProtocolElementId,
                alertSeverities: app.collections.alertSeverities.sort().toJSON(),
                isMultipleChoice: this.isMultipleChoice
            },
                this.model.attributes
            );

            this.$el.html(this.template(data));
            this.$el.find('.protocol-item-element-answer').data('AnswerChoiceId', this.model.attributes.id).attr('data-AnswerChoiceId', this.model.attributes.id);

            return this;
        }
    });
});