/// <reference path="../__0constants.js" />

(function () {
    'use strict';

    angular.module('devModel').factory('devModelApi', ['http','rest', devModelApi]);

    function devModelApi(http, rest) {

        var theRest = rest('property');

        var service = {
            getProperties: getProperties,
            getProperty: getProperty,
            saveProperty: saveProperty,
            deleteProperty:deleteProperty
        };

        return service;

        function getProperties() {
            return http.get('property');
        }

        function getProperty(propertyId) {
            return http.get('property')
        }

        function saveProperty(property) {
        }

        function deleteProperty(property) {

        }

    }
})();