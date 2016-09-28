Maestro.namespace("Maestro.controls");

// Modal to confirm saving changes.
Maestro.controls.ConfirmationModal = Backbone.View.extend({
    el: '#page-save',

    initialize: function () {
        _.extend(this, Backbone.Events);
    },

    events: {
        "click .submit-form-confirm": "confirm"
    },

    confirm: function () {
        this.trigger("confirm");
    },

    show: function () {
        this.$el.modal({
            keyboard: false,
            backdrop: 'static'
        });
    }
});