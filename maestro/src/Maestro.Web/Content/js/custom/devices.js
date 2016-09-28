Maestro.namespace('Maestro.pages');


Maestro.pages.ViewDevices = Backbone.View.extend({
    el: "#ci-devices",
    initialize: function () {
        this.$("#modal-qrcode").modal('hide');
        this.$("#modal-new-activation").modal('hide');
        this.$("#modal-remove-device").modal('hide');
        this.deviceDecommissionModal = this.$("#modal-device-decommission");
        this.deviceDecommissionModal.modal('hide');
    },

    events: {
        "click .device-info-btn": "onClickDeviceIdBtn",
        "click .create-new-activation-btn": "OnClickNewActivationBtn",
        "click .btn-remove-device": "OnClickRemoveDeviceBtn",
        "click .btn-device-decommission": "OnClickDeviceDecommissionBtn"
    },

    onClickDeviceIdBtn: function (event) {
        this.$("#modal-qrcode #device-qrcode-img").attr("src", $(event.target).closest('a').attr("href"));
        var self = this;
        this.$("#modal-qrcode .btn-primary").click(function() {
            self.$("#modal-qrcode").modal("hide");
        });
        this.$("#modal-qrcode").modal({
            "show": true
        });
        return false;
    },

    OnClickNewActivationBtn: function (event) {

        var createDeviceUrl = $(event.target).attr("href");

        var confirmModal = new Backbone.BootstrapModal({
            content: 'Please confirm creating new activation',
            title: 'New Activation',
            modalOptions: {
                backdrop: 'static',
                keyboard: false
            },
            okText: 'Ok'
        });

        confirmModal.on('ok', function() {

            confirmModal.preventClose();
            confirmModal.$(".btn-primary").text('loading...');
            confirmModal.$(".btn-primary").addClass('disabled');
            window.location = createDeviceUrl;

        });

        confirmModal.open();

        return false;
        
    },

    OnClickRemoveDeviceBtn: function(event) {

        var removeDeviceUrl = $(event.target).attr("href");

        var confirmModal = new Backbone.BootstrapModal({
            content: 'Please confirm removing device',
            title: 'Removing Device',
            modalOptions: {
                backdrop: 'static',
                keyboard: false
            },
            okText: 'Ok'
        });

        confirmModal.on('ok', function() {

            confirmModal.preventClose();
            confirmModal.$(".btn-primary").text('loading...');
            confirmModal.$(".btn-primary").addClass('disabled');
            window.location = removeDeviceUrl;

        });

        confirmModal.open();

        return false;
        
    },

    OnClickDeviceDecommissionBtn: function(event) {
        var deviceDecommissionUrl = $(event.target).attr("href");

        var self = this;

        this.deviceDecommissionModal.find(".btn-cancel").click(function () {
            self.deviceDecommissionModal.modal('hide');
        });

        this.deviceDecommissionModal.find(".btn-ok").click(function () {
            window.location = deviceDecommissionUrl;
        });

        this.deviceDecommissionModal.modal({
            "show": true
        });

        return false;
    }
});


$(function () {
    var devicesView = new Maestro.pages.ViewDevices();
});