'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace',
    'Controllers/Helpers',

    'Controllers/Site/Patients/PatientsApp/Views/PatientSearchBoxAdvancedItemView'
], function ($, _, Backbone, app, Helpers, PatientSearchBoxAdvancedItemView) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'row',
        template: _.template($('#PatientSearchBoxAdvancedSearchTemplate').html()),
        events: {
            'click.clear-advanced-identifiers #clear-advanced-identifiers': 'clearIdentifiersAndTriggerSearch'
        },

        clearIdentifiers: function () {
            this.$identifiers.val('');
        },

        clearIdentifiersAndTriggerSearch: function () {
            this.clearIdentifiers();
            app.views.patientSearchBoxView.triggerChanges();
        },

        serializeIdentifiers: function () {
            return this.$identifiers.serializeArray()
                .map(function (identitifer) {
                    return identitifer.name + '=' + identitifer.value;
                })
                .join('&');
        },

        initialize: function () {
            this.sortCollection();
        },

        sortCollection: function () {
            this.collection = this.collection.sortBy(function (identifier) {
                return identifier.attributes.name;
            });
        },

        renderInputIdentifier: function ($inputWrapper, identifier) {
            var patientSearchBoxAdvancedItemView = new PatientSearchBoxAdvancedItemView({
                model: identifier
            });

            $inputWrapper.append(patientSearchBoxAdvancedItemView.render().el);
        },

        render: function () {
            this.$el.html(this.template());

            _.each(this.collection, this.renderInputIdentifier.bind(null, this.$el.find('#input-group-wrapper')));

            this.$identifiers = this.$el.find('input[type="text"]');

            return this;
        }
    });
});