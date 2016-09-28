'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramElementModel'
], function ($, _, Backbone, ProgramElementModel) {
    return Backbone.Collection.extend({
        model: ProgramElementModel,

        comparator: function (model) {
            return model.get('sort');
        },

        addElementToOrder: function (elementModel) {
            var isExists = false,
                maxSort = 0;

            this.each(function (element) {
                var sort = element.get('sort');

                if (element.get('id') === elementModel.get('id')) {
                    elementModel.set('sort', sort);
                    isExists = true;
                }

                if (sort > maxSort) {
                    maxSort = sort;
                }
            });

            if (!isExists) {
                elementModel.set('sort', maxSort + 1);

                this.add(new ProgramElementModel({
                    id: elementModel.get('id'),
                    name: elementModel.get('name'),
                    sort: elementModel.get('sort')
                }));
            }
        },

        setOrder: function (elementId, newSort) {
            var elementModel = this.where({ id: elementId })[0],
                currentSort = elementModel.get('sort'),
                iSort = currentSort > newSort ? newSort : newSort + 1,
                nextElement = this.where({ sort: iSort })[0],
                currentElement;

            elementModel.set('sort', iSort);

            while (nextElement != undefined) {
                currentElement = nextElement;
                iSort++;
                nextElement = this.where({ sort: iSort })[0];
                currentElement.set('sort', iSort);
            }

            this.sort();
        },

        deleteElementByProtocolId: function (protocolId) {
            var elementToRemove;

            this.each(function (programElement) {
                if (programElement.get('id') === protocolId) {
                    elementToRemove = programElement;
                }
            });

            if (elementToRemove) {
                this.remove(elementToRemove);
            }
        }
    });
});