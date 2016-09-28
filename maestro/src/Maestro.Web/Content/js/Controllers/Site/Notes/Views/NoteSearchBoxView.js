'use strict';

define([
    'jquery',
    'underscore',
    'backbone',

    'Controllers/Helpers',
    'Controllers/Site/Notes/AppNamespace'
], function ($, _, Backbone, helpers, app) {
    var notablesCache = {
        isLoaded: false,
        notables: [],
        suggestedNotables: []
    };

    return Backbone.View.extend({
        tagName: 'div',
        className: 'search-box',
        $selectPicker: $(),
        patientId: '',
        controls: {
            $selectPicker: $(),
            $textBox: $(),
            $searchButton: $()
        },
        events: {
            'click.search-notes #note-search-button': 'showNotes',
            'keypress.search-notes #note-search-field': 'showNotesByEnter'
        },

        template: _.template($('#notesSearchBoxTemplate').html()),

        showNotesByEnter: function (e) {
            if (e.keyCode === 13) {
                this.showNotes();
            }
        },

        showNotes: function () {
            var data = {
                q: this.controls.$textBox.val(),
                notables: _.filter(this.controls.$selectPicker.val(), function (notable) {
                    return notable !== 'All';
                }) || []
            };

            app.views.noteItemCollectionView.trigger('show-notes', data);
        },

        initialize: function () {
            this.bind();
        },

        bind: function () {
            this.renderBox = this.renderBox.bind(this);
            this.showNotes = this.showNotes.bind(this);
        },

        toggleSelectAll: function toggleSelectAll() {
            var $control = $(this),
                values = $control.val(),
                $options = $control.find('option'),
                allOptionIsSelected = (values || []).indexOf('All') > -1,
                $selectedOptions = $control.find('option:selected[value!=All]'),
                valuesOf = function (elements) {
                    return $.map(elements, function (element) {
                        return element.value;
                    });
                };

            if ($control.data().allOptionIsSelected !== allOptionIsSelected) {
                $control.selectpicker('val', allOptionIsSelected ? valuesOf($options) : []);
            } else {
                if (allOptionIsSelected && values && values.length !== $options.size()) {
                    $control.selectpicker('val', valuesOf($selectedOptions));
                    allOptionIsSelected = false;
                } else if (!allOptionIsSelected && values && values.length === $options.size() - 1) {
                    $control.selectpicker('val', valuesOf($options));
                    allOptionIsSelected = true;
                }
            }

            if (allOptionIsSelected) {
                $control
                    .next('.bootstrap-select')
                    .find('.filter-option.pull-left')
                    .text($control.val().length - 1 + ' items selected');
            }

            $control.data({
                allOptionIsSelected: allOptionIsSelected
            });
        },

        detectControls: function () {
            this.controls = {
                $selectPicker: this.$el.find('.selectpicker').selectpicker(),
                $textBox: this.$el.find('#note-search-field'),
                $searchButton: this.$el.find('#note-search-button'),
                $addNoteButton: this.$el.find('#new-note-button')
            };

            if (this.notables.length > 1) {
                this.controls.$selectPicker.on('change', this.toggleSelectAll);
                this.controls.$selectPicker.on('change', this.showNotes);
            }
        },

        loadNotables: function () {
            var deferred = $.Deferred();

            if (!notablesCache.isLoaded) {
                $.when(
                    $.get(app.link + '/Patients/Notables', {
                        patientId: app.patientId
                    }),
                    $.get(app.link + '/Patients/SuggestedNotables')
                )
                    .then(this.cacheNotables)
                    .then(function () {
                        return deferred.resolve(notablesCache.notables);
                    });
            } else {
                deferred.resolve(notablesCache.notables);
            }

            return deferred.promise();
        },
        cacheNotables: function (notables, suggestedNotables) {
            notablesCache.isLoaded = true;
            notablesCache.notables = notables[0];
            notablesCache.suggestedNotables = suggestedNotables[0];
        },

        show: function () {
            this.loadNotables().then(this.renderBox);
        },

        filterNotables: function (notables) {
            return _.filter(notables, function (notable) {
                return notable !== 'All';
            })
        },

        renderBox: function (notables) {
            this.notables = this.filterNotables(notables) || [];

            this.$el
                .html(this.template({
                    notables: this.notables
                }));

            this.trigger('notables-built', notablesCache.suggestedNotables);

            this.detectControls();
        },

        render: function () {
            helpers.renderSpinner(this.$el);

            return this;
        }
    });
});

