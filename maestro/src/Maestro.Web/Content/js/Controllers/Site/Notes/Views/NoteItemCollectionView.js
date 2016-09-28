'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Notes/AppNamespace',
    'Controllers/Site/Notes/Views/NoteItemView'
], function ($, _, Backbone, app, NoteItemView) {
    return Backbone.View.extend({
        tagName: 'section',
        className: 'notes-body',
        defaultHeight: 80,
        currentCount: 0,
        isFirstNotes: false,
        loadMore: false,
        $spinner: $('<img src="/Content/img/spinner.gif" class="spinner spinner-small" />'),
        $document: $(document),
        $window: $(window),
        data: {
            q: '',
            patientId: '',
            notables: []
        },

        loadNotes: function (take) {
            this.collection.sync($.extend({}, this.data, {
                patientId: app.patientId,
                take: take
            }));
        },

        getCurrentCount: function () {
            return Math.round(this.$el.height() / this.defaultHeight) || 1;
        },

        initialize: function () {
            this.bind();

            this.collection.reset();

            setTimeout(function () {
                this.loadNotes(Math.round(this.getCurrentCount()) * 2);
            }.bind(this), 0);

            this.listen();
        },

        bind: function () {
            this.notesScroll = this.notesScroll.bind(this);
        },

        listen: function () {
            this.listenTo(this, 'show-notes', this.showNotes);
            this.listenTo(this.collection, 'add', this.addNotes);
            this.listenTo(this.collection, 'notes-loaded', this.notesLoaded);
            this.listenTo(this.collection, 'notes-loading', this.notesLoading);

        },

        unbindListeners: function () {
            this.$el
                .off('scroll.scroll-notes');
            this.$document
                .data('hasNotesEvent', false)
                .off('scroll.add-notes resize.add-notes');
            this.$window
                .data('hasNotesEvent', false)
                .off('scroll.add-notes resize.add-notes');
        },

        bindListeners: function () {
            this.$el.on('scroll.scroll-notes', this.notesScroll);

            if (!this.$document.data().hasNotesEvent) {
                this.$document
                    .data('hasNotesEvent', true)
                    .on('scroll.add-notes resize.add-notes', this.notesScroll);
            }
            if (!this.$window.data().hasNotesEvent) {
                this.$window
                    .data('hasNotesEvent', true)
                    .on('scroll.add-notes resize.add-notes', this.notesScroll);
            }
        },

        showNotes: function (data) {
            data = {
                q: data.q,
                patientId: data.patientId,
                notables: data.notables
            };

            if (_.isMatch(this.data, data)) {
                this.data = data;
                return;
            }

            this.collection.reset();
            this.$el.empty();

            this.data = data;
            this.loadNotes(this.getCurrentCount());
        },

        notesLoaded: function () {
            if (!this.isFirstNotes) {
                this.isFirstNotes = true;
                this.bindListeners();
                this.trigger('notes-built');
            }

            this.removeSpinner();

            if (this.loadMore) {
                this.loadMore = false;
                this.loadNotes(this.getCurrentCount());
            } else if (this.shouldLoad() && this.collection.hasMore() && !this.collection.isLoading) {
                this.loadNotes(this.getCurrentCount());
            }

            /*if(!this.collection.hasMore()) {
                this.unbindListeners();
            }*/
        },

        notesLoading: function () {
            this.renderSpinner();
        },

        addNotes: function (note) {
            this.renderNote(note);
        },

        shouldLoad: function () {
            return this.$el.height() + this.$el.scrollTop() >= this.el.scrollHeight / 2;
        },

        notesScroll: function () {
            if (this.shouldLoad() && this.collection.hasMore() && !this.collection.isLoading) {
                this.loadNotes(this.getCurrentCount());
            }
        },

        renderSpinner: function () {
            setTimeout(function () {
                this.$el.append(this.$spinner);
            }.bind(this), 0);
        },

        removeSpinner: function () {
            this.$el.children('.spinner').remove();
        },

        render: function () {
            this.renderSpinner();
            return this;
        },

        renderNote: function (note) {
            var noteItemView = new NoteItemView({ model: note });
            this.$el.append(noteItemView.render().el);

            return this;
        }
    });
});