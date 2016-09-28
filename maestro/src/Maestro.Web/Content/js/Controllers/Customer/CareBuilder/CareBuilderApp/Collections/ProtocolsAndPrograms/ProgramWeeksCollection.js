'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Customer/CareBuilder/CareBuilderApp/AppGlobalVariables',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramWeekModel'
], function ($, _, Backbone, AppGlobalVariables, ProgramWeekModel) {
    return Backbone.Collection.extend({
        model: ProgramWeekModel,

        isEmpty: function () {
            var result = true;

            this.eachDay(function (day) {
                if (day.get('dayElements').length > 0) {
                    result = false;
                }
            });

            return result;
        },

        hasElement: function (protocolId) {
            var result = false;

            this.eachElement(function (dayElement) {
                if (dayElement.get('id') === protocolId) {
                    result = true;
                }
            });

            return result;
        },

        hasCollisions: function (protocolId, startDay, endDay, skipDay, recurrenceId) {
            var result = false;

            this.eachDay(function (day) {
                var dayNumber = day.get('number');

                if (dayNumber !== skipDay &&
                    dayNumber >= startDay && dayNumber <= endDay) {
                    day.get('dayElements').each(function (dayElement) {
                        if (dayElement.get('id') === protocolId &&
                        (recurrenceId == null || recurrenceId !== dayElement.get('recurrenceId'))) {
                            result = true;
                        }
                    });
                }
            });

            return result;
        },

        getElementsCountByRecurrence: function (recurrenceId) {
            var result = 0;

            this.eachElement(function (dayElement) {
                if (dayElement.get('recurrenceId') === recurrenceId) {
                    result++;
                }
            });

            return result;
        },

        removeRecurrenceFromElements: function (recurrenceId) {
            this.eachDay(function (day) {
                var dayElements = day.get('dayElements'),
                    recurrenceElement = dayElements.where({ recurrenceId: recurrenceId })[0];

                if (recurrenceElement != undefined) {
                    dayElements.remove(recurrenceElement);
                    dayElements.add({
                        id: recurrenceElement.get('id'),
                        name: recurrenceElement.get('name'),
                        sort: recurrenceElement.get('sort')
                    });
                }
            });
        },

        deleteElementsByRecurrenceId: function (recurrenceId) {
            this.eachDay(function (day) {
                var dayElements = day.get('dayElements'),
                    dayToRemove;

                dayElements.each(function (dayElement) {
                    if (dayElement.get('recurrenceId') === recurrenceId) {
                        dayToRemove = dayElement;
                    }
                });

                dayElements.remove(dayToRemove);
            });
        },

        deleteElementByProtocolId: function (protocolId, protocolDayNumber) {
            this.eachDay(function (day) {
                var dayNumber = day.get('number');

                if (dayNumber === protocolDayNumber) {
                    day.get('dayElements').deleteElementByProtocolId(protocolId);
                }
            });
        },

        refreshOrder: function () {
            this.eachDay(function (day) {
                var isSortChanged = false,
                    dayElements = day.get('dayElements');

                dayElements.each(function (dayElement) {
                    var globalElement = AppGlobalVariables.collections.ProgramElements.where({ id: dayElement.get('id') })[0];

                    if (globalElement.get('sort') !== dayElement.get('sort')) {
                        dayElement.set('sort', globalElement.get('sort'));
                        isSortChanged = true;
                    }
                });

                if (isSortChanged) {
                    dayElements.sort();
                }
            });
        },

        // helpers
        eachDay: function (callback) {
            if (callback) {
                this.each(function (week) {
                    week.get('days').each(function (day) {
                        callback(day);
                    });
                });
            }
        },

        eachElement: function (callback) {
            if (callback) {
                this.eachDay(function (day) {
                    day.get('dayElements').each(function (dayElement) {
                        callback(dayElement);
                    });
                });
            }
        }
    });
});