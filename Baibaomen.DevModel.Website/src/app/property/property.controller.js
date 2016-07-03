
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
            propertyService.get(2).then(function (data) {
                vm.properties = data;

                propertyService.del(2, data.recordVersion);
            });
        }
    }
})();
