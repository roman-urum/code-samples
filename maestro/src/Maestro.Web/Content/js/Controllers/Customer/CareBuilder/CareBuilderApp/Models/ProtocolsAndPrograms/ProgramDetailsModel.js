'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Constants',
    'Controllers/Helpers',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramWeeksCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramDaysCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/ProgramDayElementsCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Collections/ProtocolsAndPrograms/RecurrencesCollection',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramWeekModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramDayModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramModel',
    'Controllers/Customer/CareBuilder/CareBuilderApp/Models/ProtocolsAndPrograms/ProgramElementModel',
    'backbone-nested'
], function (
    $,
    _,
    Backbone,
    Constants,
    Helpers,
    ProgramWeeksCollection,
    ProgramDaysCollection,
    ProgramElementsCollection,
    ProgramDayElementsCollection,
    RecurrencesCollection,
    ProgramWeekModel,
    ProgramDayModel,
    ProgramModel,
    ProgramElementModel
) {
    return ProgramModel.extend({
        initialize: function () {
            if (this.get('weeks') === undefined) {
                this.set('weeks', new ProgramWeeksCollection());
            }

            if (this.get('programElements') === undefined) {
                this.set('programElements', new ProgramElementsCollection());
            }

            if (this.get('recurrences') === undefined) {
                this.set('recurrences', new RecurrencesCollection());
            }

            // Memento initializing
            _.extend(this, new Backbone.Memento(this));
        },

        defaults: {
            id: null,
            name: '',
            tags: []
        },

        validation: {
            name: [
                {
                    required: true,
                    msg: 'Please enter Program name'
                }, {
                    maxLength: 50,
                    msg: 'Program name shouldn\'t exceed 50 letters'
                }
            ],

            tags: function (tags) {
                if (!Helpers.isTagsValid(tags)) {
                    return 'Incorrect tag value. Tags can consist of alphanumeric symbols, dot, dash, underscore.';
                }

                if (Helpers.hasDuplicates(tags)) {
                    return globalStrings.DuplicatedTags_ErrorMessage;
                }
            },

            weeks: function(weeks) {

                var totalAmountOfDayElements = 0;

                weeks.eachDay(function (day) {
                    var dayElements = day.get('dayElements');day.get('number');

                    totalAmountOfDayElements += dayElements.length;                    
                });

                if (totalAmountOfDayElements <= 0) {
                    return 'You cannot save a program without at least one protocol.';
                }

            }

        },

        parse: function (response, options) {
            response = Helpers.convertKeysToCamelCase(response);

            if (response.programElements != undefined) {
                response.weeks = this.parseWeeksCollection(response);
                response.programElements = this.parseElementsCollection(response);
                response.recurrences = new RecurrencesCollection(response.recurrences);
            }

            return response;
        },

        parseElementsCollection: function (response) {
            var collection = new ProgramElementsCollection();

            $.each(response.programElements, function (elementIndex, programElementDto) {
                collection.add({
                    id: programElementDto.protocolId,
                    name: programElementDto.protocolName,
                    sort: programElementDto.sort
                });
            });

            return collection;
        },

        parseWeeksCollection: function (response) {
            var weeks = new ProgramWeeksCollection(),
                maxDay = 0,
                maxWeek,
                weekIndex = 1;

            $.each(response.programElements, function (elementIndex, programElementDto) {
                $.each(programElementDto.programDayElements, function (dayElementIndex, programDayElementDto) {
                    if (programDayElementDto.day > maxDay) {
                        maxDay = programDayElementDto.day;
                    }
                });
            });

            maxWeek = maxDay / Constants.daysInWeek >> 0;

            if (maxWeek * Constants.daysInWeek < maxDay) {
                maxWeek++;
            }

            for (weekIndex; weekIndex <= maxWeek; weekIndex++) {
                var week = this.parseWeek(weekIndex, response);

                weeks.add(week);
            }

            return weeks;
        },

        parseWeek: function (weekIndex, response) {
            var dayIndex = 1,
                days = new ProgramDaysCollection();

            for (dayIndex; dayIndex <= Constants.daysInWeek; dayIndex++) {
                var day = this.parseDay(dayIndex, weekIndex, response);

                days.add(day);
            }

            return new ProgramWeekModel(
            {
                number: weekIndex,
                days: days
            });
        },

        parseDay: function (dayIndex, weekIndex, response) {
            var day = dayIndex + (weekIndex - 1) * Constants.daysInWeek,
                dayElements = [];

            $.each(response.programElements, function (elementIndex, programElementDto) {
                $.each(programElementDto.programDayElements, function (dayElementIndex, programDayElementDto) {
                    if (programDayElementDto.day == day) {
                        dayElements.push({
                            id: programElementDto.protocolId,
                            name: programElementDto.protocolName,
                            sort: programElementDto.sort,
                            daySort: programDayElementDto.sort,
                            recurrenceId: programDayElementDto.recurrenceId
                        });
                    }
                });
            });

            return new ProgramDayModel(
            {
                number: day,
                dayElements: new ProgramDayElementsCollection(dayElements)
            });
        },

        toJSON: function () {
            var programElements = [],
                self = this;

            this.get('weeks').eachDay(function (day) {
                var dayElements = day.get('dayElements'),
                    dayIndex = day.get('number');

                if (dayElements.length > 0) {
                    dayElements.each(function (element) {
                        self.addProtocolDayToJson(programElements, element, dayIndex);
                    });
                }
            });

            return {
                name: this.get('name'),
                tags: this.get('tags'),
                programElements: programElements,
                recurrences: this.get('recurrences').toJSON()
            };
        },

        addProtocolDayToJson: function (programElements, element, day) {
            var isProtocolExists = false;

            $.each(programElements, function (index, programElement) {
                if (programElement.protocolId == element.get('id')) {
                    programElement.programDayElements.push({
                        day: day,
                        recurrenceId: element.get('recurrenceId'),
                        sort: element.get('daySort')
                    });

                    isProtocolExists = true;
                }
            });

            if (!isProtocolExists) {
                programElements.push({
                    protocolId: element.get('id'),
                    sort: element.get('sort'),
                    programDayElements: [
                        {
                            day: day,
                            recurrenceId: element.get('recurrenceId'),
                            sort: element.get('daySort')
                        }
                    ]
                });
            }
        },

        initRecurrence: function (recurrenceModel, dayElement) {
            var reccurenceDays = recurrenceModel.getRecurrenceDays(),
                recurrenceId = Helpers.getGUID(),
                protocolId = dayElement.get('id');

            recurrenceModel.set('id', recurrenceId);
            this.get('recurrences').add(recurrenceModel);
            this.addWeeks(recurrenceModel.get('endDay'));

            this.get('weeks').eachDay(function (day) {
                var dayNumber = day.get('number'),
                    dayElements = day.get('dayElements');

                if (jQuery.inArray(dayNumber, reccurenceDays) >= 0) {
                    var existingElement;

                    dayElements.each(function (dayElement) {
                        if (dayElement.get('id') === protocolId) {
                            existingElement = dayElement;
                        }
                    });

                    if (existingElement !== undefined && existingElement !== null) {
                        dayElements.remove(existingElement);
                    }

                    dayElements.add(new ProgramElementModel({
                        id: protocolId,
                        day: dayNumber,
                        recurrenceId: recurrenceId,
                        name: dayElement.get('name'),
                        sort: dayElement.get('sort')
                    }));
                }
            });
        },

        addWeeks: function (endDay) {
            var maxWeek = (endDay / Constants.daysInWeek >> 0) + 1,
                weeks = this.get('weeks');

            while (maxWeek > weeks.length) {
                weeks.add({ number: weeks.length + 1 });
            }
        }
    });
});