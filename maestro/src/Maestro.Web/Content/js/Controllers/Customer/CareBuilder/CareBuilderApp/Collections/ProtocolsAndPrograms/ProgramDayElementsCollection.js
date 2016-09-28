'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramElementsCollection'
], function ($, _, Backbone, ProgramElementsCollection) {
    return ProgramElementsCollection.extend({

        comparator: function (model) {
            var daySort = model.get('daySort');

            return daySort == null ? model.get('sort') : daySort;
        },

        setOrderInDay: function (dragElementId, dropElementId) {
            if (!this.isCustomOrderUsed()) {
                this.initDayOrder();
            }

            var elementModel = this.where({ id: dragElementId })[0],
                dropElement = this.where({ id: dropElementId })[0],
                dropElementSort = dropElement.get('daySort'),
                currentSort = elementModel.get('daySort'),
                iSort = currentSort > dropElementSort ? dropElementSort : dropElementSort + 1,
                nextElement = this.where({ daySort: iSort })[0],
                currentElement;

            elementModel.set('daySort', iSort);

            while (nextElement != undefined) {
                currentElement = nextElement;
                iSort++;
                nextElement = this.where({ daySort: iSort })[0];
                currentElement.set('daySort', iSort);
            }

            this.sort();
        },

        initDayOrder: function () {
            var sort = 1;
            this.each(function (dayElement) {
                dayElement.set('daySort', sort);
                sort++;
            });
        },

        isCustomOrderUsed: function () {
            return this.length > 0 && this.where({ daySort: null }).length === 0;
        },

        addDayElement: function (elementModel) {
            if (this.isCustomOrderUsed()) {
                var maxSortValue = Math.max.apply(Math, this.toJSON().map(function (element) {
                    return element.daySort;
                }));
                elementModel.set('daySort', maxSortValue + 1);
            }

            this.add(elementModel);
        },

        resetCustomOrder: function () {
            this.each(function (dayElement) {
                dayElement.set('daySort', null);
            });
            this.sort();
        }
    });
});