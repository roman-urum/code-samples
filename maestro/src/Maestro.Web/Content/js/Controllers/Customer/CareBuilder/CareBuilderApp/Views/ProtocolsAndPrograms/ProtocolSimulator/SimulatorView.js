'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProtocolSimulator/ProtocolElementsCollectionView'
], function (
    $,
    _,
    Backbone,
    app,
    Helpers,
    ProtocolElementsCollectionView
) {
    return Backbone.View.extend({
        className: 'b-protocol-simulator',

        template: _.template($("#protocolSimulatorTemplate").html()),

        initialize: function () {

        },

        events: {

        },

        render: function () {
            this.$el.append(this.template());
            this.protocolRun();

            return this;
        },

        protocolRun: function () {
            var protocolElements = this.model.get('protocolElements');
            var ProtocolElementsCollection = Backbone.Collection.extend({});

            app.collections.protocolElementsCollection = new ProtocolElementsCollection(protocolElements);
            app.views.protocolElementsCollectionView = new ProtocolElementsCollectionView({ collection: app.collections.protocolElementsCollection });

            this.$el.find('.simulator-content').html(app.views.protocolElementsCollectionView.render().el);

            var firstProtocolElementId = this.model.get('firstProtocolElementId'),
                firstProtocolElement = app.collections.protocolElementsCollection.find(function (model) {
                    return model.get('id') == firstProtocolElementId;
                });

            firstProtocolElement.set('isDisplay', true);
        }
    });
});