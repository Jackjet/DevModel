/// <reference path="../../lib/node_modules/jquery/dist/jquery.js" />

(function () {
    'use strict';

    angular
      .module('devModel')
      .factory('propertyService', ['http','rest', propertyService]);

    function propertyService(http,rest) {
        var theRest = rest('property');

        var service = {
            hiFromProperty:hiFromProperty
        };
        $.extend(service, theRest);

        return service;

        function hiFromProperty() {
            console.log('hi from property');
        }
    }
})();
