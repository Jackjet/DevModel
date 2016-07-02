(function () {
    'use strict';

    angular
      .module('devModel')
      .controller('loginCtrl', ['$scope', loginCtrl]);

    function loginCtrl($scope) {
        var vm = this;

        vm.title = '';

        activate();

        function activate() { }
    }
})();