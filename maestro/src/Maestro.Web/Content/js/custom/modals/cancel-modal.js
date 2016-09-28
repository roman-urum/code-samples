Maestro.namespace("Maestro.controls");

// Modal to confirm saving changes.
Maestro.controls.CancelModal = Backbone.View.extend({
    el: '#page-cancel',

    initialize: function () {
        _.extend(this, Backbone.Events);
    },

    events: {
        "click .cancel-form-confirm": "confirm"
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