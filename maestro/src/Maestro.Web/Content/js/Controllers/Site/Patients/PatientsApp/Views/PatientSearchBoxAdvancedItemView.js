'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'Controllers/Site/Patients/PatientsApp/AppNamespace'
], function ($, _, Backbone, app) {
    return Backbone.View.extend({
        tagName: 'div',
        className: 'col-sm-4',
        template: _.template($('#PatientSearchBoxAdvancedSearchItemTemplate').html()),
        events: {
            'click.clear-identifier .clear-identifier': 'clearIdentifier'
        },

        clearIdentifier: function () {
            this.$input.val('');
            app.views.patientSearchBoxView.triggerChanges();
        },

        initialize: function () {
        },

        render: function () {
            this.$el.html(this.template(this.model.attributes));
            this.$input = this.$el.find('input[type="text"]');

            this.$input.inputmask({
                clearMaskOnLostFocus: false,
                mask: this.model.attributes.inputMask
            });

            return this;
        }
    });
});