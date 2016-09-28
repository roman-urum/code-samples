Maestro.namespace('Maestro.pages');

Maestro.pages.ViewPatient = Backbone.View.extend({
    el: ".maestro-edit-wrap",

    initialize: function() {
        this.patientId = this.$el.find("#Id").val();
    },

    events: {
        'click .expand-patient': 'expand',
        'click .remove-manager': 'removeCareManager'
    },

    expand: function(event) {
        var $target = $(event.target);

        if ($target.html() === 'Collapse') {
            $target.html('Expand');
        } else {
            $target.html('Collapse');
        }
        $target.closest('.expand-patient-row').siblings('.patient-details').slideToggle();
        $target.closest('.expand-patient-row').siblings('.patient-header').toggleClass('open');
    },

    removeCareManager: function (event) {
        var $target = $(event.target).parent(),
            $container = $target.parents(".user-tag"),
            managerId = $target.attr("manager-id");

        $.post("/CareManagers/Remove", {
            managerId: managerId,
            patientId: this.patientId
        }, function(data) {
            if (data.success) {
                $container.remove();
            }
        });
    }
});

$(function () {
    var createPatientView = new Maestro.pages.ViewPatient();
});