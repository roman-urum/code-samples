'use strict';

define([
    'jquery',
    'underscore',
    'backbone',
    'BackboneBootstrapModal',
    'BackboneBootstrapAlert',
    './OneWayChatView'
], function(
    $, 
    _, 
    Backbone, 
    BackboneBootstrapModal,
    BackboneBootstrapAlert,
    OneWayChatView
) {

    var chatModal = null;
    var siteId = null;
    var patientId = null;

    function url() {
        return siteId ? '/' + siteId + '/Patients/ChatMessage' : '';
    }

    function showModal(options) {
        options = options || {};
        if (!options.siteId) throw 'SiteId is required';
        if (!options.patientId) throw 'PatientId is required';
        siteId = options.siteId;
        patientId = options.patientId;

        var firstName = options.patientFirstName;
        var lastName = options.patientLastName;
        var title = firstName ?
            'Send message to ' + firstName + ' ' + lastName :
            'Send message';

        chatModal = new BackboneBootstrapModal({
            content: new OneWayChatView({
                model: new Backbone.Model()
            }),
            title: title,
            okText: 'Send',
            cancelText: 'Cancel',
            modalOptions: {
                backdrop: 'static'
            }
        });

        chatModal.on('ok', function () {
            var msg = this.options.content.getMessageText();
            var alert;
            if (!msg) {
                chatModal.preventClose();
                alert = new BackboneBootstrapAlert({
                    alert: 'warning',
                    fixed: false,
                    message: 'Please enter message text',
                    autoClose: true
                });
                chatModal.$content.children('.one-way-chat-content').prepend(alert.render().el);
            } else if (msg.length >= 1000) {
                chatModal.preventClose();
                alert = new BackboneBootstrapAlert({
                    alert: 'warning',
                    fixed: false,
                    message: 'Message text length should be below 1000',
                    autoClose: true
                });
                chatModal.$content.children('.one-way-chat-content').prepend(alert.render().el);
            } else {
                _sendMessage(msg);
            }
        });
        chatModal.open();
    }

    function closeModal() {}

    function _sendMessage(msg) {
        $.ajax({
            url: url(),
            method: 'POST',
            data: {
                "PatientId": patientId,
                "Message": msg
            },
            success: function (resp) {
                var alert = new BackboneBootstrapAlert({
                    alert: 'success',
                    message: 'Message was sent successfully',
                    autoClose: true
                }).show();
            },
            error: function (err) {
                var alert = new BackboneBootstrapAlert({
                    alert: 'danger',
                    message: 'Error while sending message: ' + err.code,
                    autoClose: true
                }).show();
            }
        });
    }

    return {
        showModal: showModal,
        closeModal: closeModal,
        _sendMessage: _sendMessage
    };
});