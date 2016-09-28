'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/MultipleChoiceCollectionItemView'
], function (
    $,
    _,
    Backbone,
    app,
    MultipleChoiceCollectionItemView
) {
    return Backbone.View.extend({

        className: 'simulator-content-answer pull-right',

        render: function () {
            this.collection.each(this.renderAnswer, this);
            return this;
        },

        renderAnswer: function (answer) {
            app.views.multipleChoiceCollectionItemView = new MultipleChoiceCollectionItemView({ model: answer });
            this.$el.append(app.views.multipleChoiceCollectionItemView.render().el);

            return this;
        }

    });
});