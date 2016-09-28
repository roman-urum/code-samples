'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/ProtocolElementsCollectionItemView'
], function (
    $,
    _,
    Backbone,
    app,
    ProtocolElementsCollectionItemView
) {
    return Backbone.View.extend({

        render: function () {
            this.collection.each(this.renderElement, this);
            return this;
        },

        renderElement: function (element) {
            element.set('isDisplay', false);
            app.views.protocolElementsCollectionItemView = new ProtocolElementsCollectionItemView({ model: element });
            this.$el.append(app.views.protocolElementsCollectionItemView.render().el);

            return this;
        }
    });
});