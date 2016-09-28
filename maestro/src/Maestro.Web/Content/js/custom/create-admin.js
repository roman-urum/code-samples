Maestro.namespace("Maestro.pages.createadmin");

Maestro.pages.createadmin = Backbone.View.extend({
    el: '#create-admin-user',

    events: {
        'click #cancel-new-admin': 'onCancelCreateAdmin'
    },

    onCancelCreateAdmin: function (event) {
        location.href = $(event.target).attr('href');
    }
});
(function () {
    var editAdminView = new Maestro.pages.createadmin();
})()