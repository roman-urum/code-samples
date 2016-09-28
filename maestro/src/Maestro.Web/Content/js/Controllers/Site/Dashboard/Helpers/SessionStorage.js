'use strict';

define([
    'underscore'
], function(_) {
    var fakeStorage = (function() {
        var _storage = {};

        return Object.defineProperties({
            getItem: function(key) {
                return _.constant(_storage[key])();
            },
            setItem: function(key, value) {
                _storage[key] = _.constant(value)();
            },
            removeItem: function(key) {
                delete _storage[key];
            },
            clear: function() {
                _storage = {};
            }
        }, {
            length: {
                get: function() {
                    var size = 0, key;

                    for (key in _storage) {
                        if (_storage.hasOwnProperty(key)) {
                            size += 1;
                        }
                    }

                    return size;
                },
                configurable: true,
                enumerable: true
            }
        });
    }());

    //return sessionStorage || fakeStorage;
    return fakeStorage;
});