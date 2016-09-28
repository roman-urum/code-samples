'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapAlert',

    'Controllers/Site/Notes/AppNamespace',

    'Controllers/Site/Notes/Views/NoteItemView',
    'Controllers/Site/Notes/Views/NoteSearchBoxView',
    'Controllers/Site/Notes/Views/NoteItemCollectionView',
    'Controllers/Site/Notes/Collections/NoteItemCollection',
    'Controllers/Site/Notes/Views/NewNoteMeasurementView',
    'Controllers/Site/Notes/Models/NewNoteMeasurementModel',
    'Controllers/Site/Notes/Views/NewNoteBehaviorView',
    'Controllers/Site/Notes/Models/NewNoteBehaviorModel'
], function($,
            _,
            Backbone,
            BackboneBootstrapAlert,
            app,
            NoteItemView,
            NoteSearchBoxView,
            NoteItemCollectionView,
            NoteItemCollection,
            NewNoteMeasurementView,
            NewNoteMeasurementModel,
            NewNoteBehaviorView,
            NewNoteBehaviorModel) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'notes-board building',
        classVisible: 'visible',
        classInvisible: 'invisible',
        classBuilding: 'building',
        isNotablesBuilt: false,
        isNotesBuilt: false,
        $noteContainer: $(),
        $notesBody: $(),
        $textBox: $(),

        maxLength: 1000,
        defaultTop: 65,
        notesCount: 0,
        isVisible: false,
        isSaving: false,
        $document: $(document),
        $window: $(window),

        healthSessionElementId: null,
        mainWindowWidth: 1200,
        vitalId: null,

        template: _.template($('#notesBoardTemplate').html()),
        notablesTemplate: _.template($('#notableListTemplate').html()),

        events: {
            'click.close-notes .close-notes': 'hide',
            'click #new-note-button': 'showNewNote',
            'click #hide-new-note': 'toggleNewNote',
            'click #save-new-note': 'saveNote'
        },

        saveNote: function(e) {
            if(e && e.preventDefault) {
                e.preventDefault();
            }

            var data = {
                PatientId: app.patientId,
                Text: this.$textBox.val() || '',
                Notables: app.views.noteSearchBoxView.filterNotables(this.$selectPicker.selectpicker('val') || [])
            };

            if(data.Text.length >= this.maxLength) {
                (new BackboneBootstrapAlert({
                    autoClose: true,
                    alert: 'danger',
                    message: 'Note message should be less than ' + this.maxLength + ' symbols'
                }))
                    .show();

                return;
            }

            if(!data.Text.length) {
                (new BackboneBootstrapAlert({
                    autoClose: true,
                    alert: 'danger',
                    message: 'Note message should not be empty'
                }))
                    .show();

                return;
            }

            if (this.healthSessionElementId) {
                data.HealthSessionElementId = this.healthSessionElementId;
            }

            /*if (this.vitalId) {
                data.VitalId = this.vitalId;
            }*/

            if (this.measurementId) {
                data.MeasurementId = this.measurementId;
            }

            if (this.isSaving) {
                return;
            }

            this.isSaving = true;
            this.$textBox[0].disabled = true;
            app.collections.noteItemCollection.save(data).then(this.afterSave);
        },

        clearNote: function() {
            this.$textBox.val('');
            this.$selectPicker.selectpicker('deselectAll');
        },

        clearIds: function() {
            this.healthSessionElementId = null;
            //this.vitalId = null;
            this.measurementId = null;
        },

        afterSave: function(note) {
            var noteItemView = new NoteItemView({
                model: note
            });

            this.$notesBody.prepend(noteItemView.render().el);

            this.isSaving = false;

            this.clearIds();

            this.notesCount += 1;
            this.renderNotesCount();

            this.$textBox.trigger('blur');
            this.$textBox[0].disabled = false;

            this.toggleNewNote();
            this.clearNote();
        },

        showNewNote: function(e) {
            e.preventDefault();

            this.$el.find('.details-container').empty();
            this.applyTextBoxHeight();
            this.toggleNewNote();
        },

        toggleNewNote: function(e) {
            if (e && e.preventDefault) {
                e.preventDefault();
            }

            if (this.isSaving) {
                return;
            }
            this.$noteContainer.toggleClass('visible');
            this.clearNote();
        },

        initialize: function() {
            this.bind();

            this.resize();

            this.listen();
        },

        detectPosition: function() {
            var areaWidth = (this.$window.width() - this.mainWindowWidth) / 2,
                itemWidth = this.$el.width();

            this.$el.css({
                right: areaWidth > itemWidth? areaWidth - itemWidth : 0
            })
        },

        bind: function() {
            this.resize = this.resize.bind(this);
            this.afterSave = this.afterSave.bind(this);
            this.detectPosition = this.detectPosition.bind(this);
        },

        listen: function() {
            this.$document.on('scroll resize', this.resize);
            this.$window.on('scroll resize', this.resize);
            this.$window.on('resize.detectNoteBoarPosition', this.detectPosition);
        },

        destroyViews: function() {
            if (app.views.noteSearchBoxView) {
                app.views.noteSearchBoxView.undelegateEvents();
                app.views.noteSearchBoxView.remove();
            }
            if (app.views.noteItemCollectionView) {
                app.views.noteItemCollectionView.undelegateEvents();
                app.views.noteItemCollectionView.unbindListeners();
                app.views.noteItemCollectionView.remove();
            }
            if (app.collections.noteItemCollection) {
                app.collections.noteItemCollection.xhr.abort();
                app.collections.noteItemCollection.reset();
            }

            this.isNotablesBuilt = this.isNotesBuilt = false;
        },

        buildViews: function() {
            app.views.noteSearchBoxView = new NoteSearchBoxView();
            app.views.noteItemCollectionView = new NoteItemCollectionView({
                collection: app.collections.noteItemCollection = new NoteItemCollection()
            });

            this.listenTo(app.views.noteSearchBoxView, 'notables-built', this.notablesBuilt);
            this.listenTo(app.views.noteItemCollectionView, 'notes-built', this.notesBuilt);
        },

        notablesBuilt: function(notables) {
            this.$selectPicker = this.$el
                .find('#new-notables-list')
                .html(this.notablesTemplate({
                    notables: notables
                }))
                .find('select')
                .selectpicker()
                .on('change', app.views.noteSearchBoxView.toggleSelectAll);

            this.$textBox = this.$el.find('#new-note-field');
            this.applyTextBoxHeight();


            this.isNotablesBuilt = true;
            this.hideSpinner();
        },

        notesBuilt: function() {
            this.isNotesBuilt = true;
            this.renderNotesCount();
            this.hideSpinner();
        },

        renderNotesCount: function() {
            if (!this.notesCount) {
                this.notesCount = app.collections.noteItemCollection.getTotalCount();
            }

            this.$el.find('.notes-count').text(this.notesCount === 1 ? this.notesCount + ' note' : this.notesCount + ' notes');
        },

        hideSpinner: function() {
            if (this.isNotablesBuilt && this.isNotesBuilt) {
                this.$el.removeClass(this.classBuilding);
            }
        },

        resize: function() {
            var scrollTop = this.$document.scrollTop();

            this.$el.css({top: scrollTop < this.defaultTop ? this.defaultTop - scrollTop : 0});

            if (this.isVisible) {
                this.applyTextBoxHeight();
            }
        },

        hide: function() {
            delete app.patientId;
            this.isVisible = false;

            this.destroyViews();
            this.$el
                .removeClass(this.classVisible)
                .addClass(this.classInvisible);
        },

        showAnyway: function() {
            this.isVisible = true;

            this.buildViews();
            this.render();

            this.detectPosition();
            app.views.noteSearchBoxView.show();
        },

        show: function(patientId) {
            app.patientId = patientId;
            this.clearIds();

            if (this.isVisible) {
                this.destroyViews();
                this.showAnyway();

            } else {
                this.showAnyway();

                this.$el.removeClass(this.classInvisible);
                setTimeout(function() {
                    this.$el.addClass(this.classVisible);
                }.bind(this), 0);
            }
        },

        isPanelVisible: function() {
            return this.isVisible;
        },

        detectTextBoxHeight: function() {
            var height = 0,
                $siblings = $('.new-note-text-box', this.$noteContainer).siblings();

            $siblings.each(function(index) {
                height += $siblings.eq(index).outerHeight(true);
            });

            return this.$noteContainer.height() - height;
        },

        applyTextBoxHeight: function() {
            $('.new-note-text-box', this.$noteContainer).css({
                height: this.detectTextBoxHeight()
            });
        },

        getDetailsContainer: function() {
            return this.$el.find('.details-container');
        },

        renderMeasurement: function(data) {
            var newNoteMeasurementView = new NewNoteMeasurementView({
                model: new NewNoteMeasurementModel(data)
            });

            this.getDetailsContainer().html(newNoteMeasurementView.render().el);
        },

        renderBehavior: function(data) {
            var newNoteBehaviorView = new NewNoteBehaviorView({
                model: new NewNoteBehaviorModel(data)
            });

            this.getDetailsContainer().html(newNoteBehaviorView.render().el);
        },

        renderCurrent: function(type, data) {
            switch (type) {
                case app.types.BEHAVIOUR:
                {
                    this.renderBehavior(data);
                    break;
                }
                case app.types.MEASUREMENT:
                {
                    this.renderMeasurement(data);
                    break;
                }
            }
        },

        add: function(patientId, itemType, contextType, data) {
            app.patientId = patientId;

            this.clearIds();

            if (!this.isVisible) {
                this.showAnyway();
            }

            switch (contextType) {
                case app.types.VITAL:
                {
                    //this.vitalId = data.id;
                    this.measurementId = data.measurementId;
                    this.renderCurrent(itemType, data);
                    break;
                }
                case app.types.HEALTHSESSIONELEMENT:
                {
                    this.healthSessionElementId = data.id;
                    this.renderCurrent(itemType, data);
                    break;
                }
            }

            this.applyTextBoxHeight();
            this.$noteContainer.addClass('visible');

            this.$el.removeClass(this.classInvisible);
            setTimeout(function() {
                this.$el.addClass(this.classVisible);
            }.bind(this), 0);
        },

        render: function() {
            this.$el
                .empty()
                .addClass(this.classBuilding);

            this.$el
                .html(this.template())
                .find('#notes-search-box')
                .html(app.views.noteSearchBoxView.render().el);

            this.$el
                .find('#notes-body')
                .replaceWith(app.views.noteItemCollectionView.render().el);

            this.$noteContainer = this.$el.find('.new-note-container');
            this.$notesBody = this.$el.find('.notes-body');

            $('body').append(this.el);

            return this;
        }
    });
});