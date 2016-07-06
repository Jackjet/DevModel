
(function () {
    'use strict';

    angular
      .module('devModel')
      .controller('propertyCtrl', ['propertyService', propertyCtrl]);

    function propertyCtrl(propertyService) {
        var vm = this;

        vm.title = '';

        activate();

        function activate() {
            propertyService.get().then(function (data) {
                vm.properties = data;
            });
        }
    }
})();
