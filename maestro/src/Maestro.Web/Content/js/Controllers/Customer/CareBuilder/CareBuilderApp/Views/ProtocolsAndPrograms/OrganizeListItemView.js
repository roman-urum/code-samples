'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProtocolModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/ProtocolsAndPrograms/ProgramProtocolListItemView'
], function (
    $,
    _,
    Backbone,
    AppGlobalVariables,
    ProtocolModel,
    ProgramProtocolListItemView
) {
    return ProgramProtocolListItemView.extend({
        render: function () {
            ProgramProtocolListItemView.prototype.render.apply(this);
            this.initDropBindings();

            return this;
        },

        initDropBindings: function () {
            var self = this,
                $elementBox = this.$el;

            this.$el.droppable({
                over: function (event, ui) {
                    $elementBox.addClass('placeholder');
                },

                out: function () {
                    $elementBox.removeClass('placeholder');
                },

                drop: function (event, ui) {
                    $elementBox.removeClass('placeholder');
                    AppGlobalVariables.collections.ProgramElements.setOrder(ui.draggable.data('id'), self.model.get('sort'));
                    AppGlobalVariables.collections.Weeks.refreshOrder();
                }
            });
        }
    });
});