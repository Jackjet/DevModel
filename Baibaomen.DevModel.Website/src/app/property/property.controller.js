﻿
(function () {
    'use strict';

    angular
      .module('devModel')
      .controller('propertyCtrl', ['$scope', propertyCtrl]);

    function propertyCtrl($scope) {
        var vm = this;

        vm.title = '';

        activate();

        function activate() { }
    }
})();