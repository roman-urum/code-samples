'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Notes/AppNamespace',
    'Controllers/Site/Notes/Models/NoteItemModel'
], function ($, _, Backbone, app, NoteItemModel) {
    return Backbone.Collection.extend({
        total: 0,
        xhr: new XMLHttpRequest(),
        model: NoteItemModel,

        initialize: function () {
            this.success = this.success.bind(this);
            this.afterSave = this.afterSave.bind(this);
        },

        url: function () {
            return app.link + '/Patients/Notes';
        },

        save: function (note) {
            return $.ajax({
                type: 'POST',
                url: this.url(),
                traditional: true,
                data: note
            }).then(this.afterSave);
        },

        afterSave: function (note) {
            var model = new NoteItemModel(note);

            this.models.unshift(model);
            this.total += 1;

            return model;
        },

        sync: function (data) {
            data = {
                data: data
            };

            if (this.isLoading) {
                this.xhr.abort();
            }

            this.isLoading = true;
            this.trigger('notes-loading');

            data.add = true;
            data.success = this.success;
            data.traditional = true;
            data.data.skip = this.models.length;

            this.xhr = Backbone.sync('read', this, data);
        },

        success: function (data) {
            var current = _.filter(data.results, function (item) {
                return !_.find(this.models, function (existingItem) {
                    return existingItem.attributes.id === item.id;
                });
            }, this);

            this.add(current);

            this.total = data.total;
            this.isLoading = false;
            this.trigger('notes-loaded');
        },

        hasMore: function () {
            return this.total > this.models.length;
        },

        getTotalCount: function () {
            return this.total;
        }
    });
});