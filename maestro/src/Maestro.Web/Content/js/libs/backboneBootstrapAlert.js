/**
 * Bootstrap Alert wrapper for use with Backbone.
 *
 * @author Renat Ganbarov <renat.ganbarov@gmail.com>
 */


(function (factory) {
    'use strict';
    if (typeof define === 'function' && define.amd) {
        // AMD is used - Register as an anonymous module.
        define(['jquery', 'underscore', 'backbone'], factory);
    } else if (typeof exports === 'object') {
        factory(require('jquery'), require('underscore'), require('backbone'));
    } else {
        // Neither AMD nor CommonJS used. Use global variables.
        if (typeof jQuery === 'undefined') {
            throw 'Backbone Bootstrap Alert requires jQuery to be loaded first';
        }
        if (typeof underscore === 'undefined') {
            throw 'Backbone Bootstrap Alert requires underscore.js to be loaded first';
        }
        if (typeof backbone === 'undefined') {
            throw 'Backbone Bootstrap Alert requires backbone.js to be loaded first';
        }
        factory(jQuery, underscore, Backbone);
    }
}(function ($, _, Backbone) {

//Set custom template settings

	var _interpolateBackup = _.templateSettings;
	_.templateSettings = {
		interpolate: /\{\{(.+?)\}\}/g,
		evaluate: /<%([\s\S]+?)%>/g
	};

	var template = _.template('\
		<% if (!autoClose) { %>\
			<a href="#" data-dismiss="alert" class="close">&times;</a>\
		<% } %>\
		<strong>{{title}}</strong>\
		{{message}}\
	');

  //Reset to users' template settings
  _.templateSettings = _interpolateBackup;

  	var Alert = Backbone.View.extend({

		className: "alert fade",

		initialize: function (options) {

			_.bindAll(this, "render", "remove");

			this.options = _.extend({
		        alert: 'info',
		        title: '',
		        message: '',
		        fixed: true,
		        autoClose: false
		      }, options);

		},

		render: function () {

			$('body > .alert').remove();

			var self = this,
				output = template({ title: this.options.title, message: this.options.message, autoClose: this.options.autoClose });

			this.$el.addClass("alert-" + this.options.alert).html(output).alert();

			if (this.options.fixed) {
				this.$el.addClass("fixed");
			}

			setTimeout(function () {
				self.$el.addClass("in");
			}, 20);


			this.isRendered = true;

			if (this.options.autoClose) {
				setTimeout(function () {
					self.$el.removeClass("in");
					setTimeout(function () {
						self.$el.remove();
					}, 1000);
				}, 3000);
			}

			return this;
		},

		remove: function () {
			var self = this;

			this.$el.removeClass("in");

			setTimeout(function () {
				self.$el.remove();
			}, 1000);
		},

		show: function(){
			if (!this.isRendered)
				$(document.body).append( this.render().el);
		}

	});

	return Alert;

}));