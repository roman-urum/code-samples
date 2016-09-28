/**
 * Backbone ZoomCalls.
 *
 * @author Renat Ganbarov <renat.ganbarov@gmail.com>
 */
 
 (function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD is used - Register as an anonymous module.
        define(['jquery', 'backbone'], factory);
    } else if (typeof exports === 'object') {
        factory(require('jquery'), require('backbone'));
    } else {
        // Neither AMD nor CommonJS used. Use global variables.
        if (typeof jQuery === 'undefined') {
            throw 'Backbone Zoomcalls requires jQuery to be loaded first';
        }
        if (typeof Backbone === 'undefined') {
            throw 'Backbone Zoomcalls requires backbone.js to be loaded first';
        }
        factory(jQuery, Backbone);
    }
}(function ($, Backbone) {

 	var Zoom = Backbone.View.extend({

		initialize: function (options) {

			_.bindAll(this, "waitForStart");

            this.options = _.extend({
                maxTries: 60,
                started: function () { },
                timeout: function () { },
                waiting: function (m, t) { }
            }, options);

            this.tries = 0;
            this.meeting = null;
		},

        createMeeting: function (){
            var self = this;

            $.ajax({
                type: 'POST',
                url: '/' + this.options.siteId + '/Patients/VideoCall',
                data: {
                    patientId: this.options.patientId
                },
                success: function (responce, status, xhr) {
                    self.meeting = self.convertKeysToCamelCase( responce );

                    window.open(self.meeting.startUrl, 'startZoom');
                    self.waitForStart();

                }
            });

        },

        getMeeting: function (){
            var self = this,
                meeting;

            $.ajax({
                type: 'GET',
                async: false,
                url: '/' + this.options.siteId + '/Patients/VideoCall',
                data: { 
                    patientId: this.options.patientId,
                    id: this.meeting.id,
                    hostId: this.meeting.hostId
                },
                success: function (responce, status, xhr) {
                    meeting = self.convertKeysToCamelCase( responce );
                }
            });

            return meeting;
        },

        waitForStart: function(){
            var meeting = this.getMeeting();

            if (meeting.status == 1) {
                this.options.started();
                this.notifyPatient();
            } else if (this.tries++ > this.options.maxTries) {
                this.options.timeout();
            } else {
                setTimeout( this.waitForStart, 1000, this );

                if ( this.options.waiting )
                    this.options.waiting();
            }
        },

        notifyPatient: function(){
            $.ajax({
                type: 'POST',
                async: false,
                url: '/' + this.options.siteId + '/Patients/SendVideoCallNotification',
                data: {
                    meetingId: this.meeting.id,
                    patientId: this.options.patientId
                },
                success: function (data, status, xhr) {}
            });
        },

        render: function () {
            return this;
        },

        convertKeysToCamelCase: function (target) {
            var self = this;

            if (!target || typeof target !== "object") return target;

            if (target instanceof Array) {
                return $.map(target, function (value) {
                    return self.convertKeysToCamelCase(value);
                });
            }

            var newObj = {};

            $.each(target, function (key, value) {
                key = key.charAt(0).toLowerCase() + key.slice(1);
                newObj[key] = self.convertKeysToCamelCase(value);
            });

            return newObj;
        }


    });

    return Zoom;

}));