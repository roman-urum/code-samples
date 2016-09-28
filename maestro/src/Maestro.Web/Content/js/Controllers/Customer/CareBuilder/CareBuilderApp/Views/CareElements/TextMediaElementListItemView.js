'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/TextMediaElementModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Views/TextMediaElementView',
    'Controllers/Helpers'
], function (
    $,
    _,
    Backbone,
    AppGlobalVariables,
    TextMediaElementModel,
    TextMediaElementView,
    Helpers
) {
    return Backbone.View.extend({
        className: 'panel panel-default',

        model: TextMediaElementModel,

        template: _.template($('#textMediaElementListItemTemplate').html()),
        templateMedia: _.template($('#textMediaElementItemMediaTemplate').html()),


        render: function () {
            this.$el.html(this.template(this.model.attributes));

            return this;
        },

        initialize: function () {
            this.isCollapsed = true;
        },

        events: {
            'click .js-collapsed': 'loadDetailedModel',
            'click .js-edit-text-media-element': 'onClickEditTextMediaElementBtn'
        },

        loadDetailedModel: function (e) {
            e.preventDefault();

            var self = this;

            if (this.isCollapsed) {
                Helpers.renderSpinner(this.$el.find('.panel-body .text-media-text'));

                this.model.fetch({
                    reset: true,
                    data: { id: self.model.get('id') },
                    success: function () {
                        var mediaModel = self.model.get('media');

                        self.isCollapsed = false;
                        self.$el.find('.panel-body .text-media-text').html(self.model.get('text.value'));

                        if (self.model.get('text'))
                            self.$el.find('.panel-body').html('<span>' + self.model.get('text.value') + '</span>');
                        else
                            self.$el.find('.panel-body').html('');

                        if (mediaModel) {
                            self.$el.find('.panel-body').append(self.templateMedia(mediaModel.attributes));
                        }

                    }
                });
            }
        },

        onClickEditTextMediaElementBtn: function () {
            var self = this;

            // Saving original model state
            self.model.store();

            var textMediaElementView = new TextMediaElementView({ model: self.model });

            var modalView = textMediaElementView.getModalView({ title: 'Edit Text And Media' });

            textMediaElementView.on('OnTextMediaElementSaved', function (isNew) {
                console.log('isNew: ', isNew);
                self.render();
            });

            modalView.open();

            modalView.on('cancel', function () {
                // Restoring original model state
                self.model.restart();
            });

            return false;
        }
    });
});