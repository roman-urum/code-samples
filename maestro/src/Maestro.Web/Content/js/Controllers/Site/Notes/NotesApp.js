'use strict';

define([
    'jquery',
    'Controllers/Site/Notes/Views/NotesMainView',
    'Controllers/Site/Notes/AppNamespace',
    'Controllers/Site/Notes/DataParser',

    'bootstrap',
    'bootstrap-select'
], function ($, NotesMainView, app, parser) {
    app.views.notesMainView = new NotesMainView();

    return {
        hide: app.views.notesMainView.hide.bind(app.views.notesMainView),
        show: app.views.notesMainView.show.bind(app.views.notesMainView),
        isVisible: app.views.notesMainView.isPanelVisible.bind(app.views.notesMainView),
        add: app.views.notesMainView.add.bind(app.views.notesMainView),
        types: app.extendedTypes.bind(app),
        parse: parser
    };
});