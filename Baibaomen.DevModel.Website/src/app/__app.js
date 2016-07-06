(function(){
    'use strict';

    
    angular.module('shared', [
            // Angular modules
            'ngAnimate',
            //'ngSanitize',
            // 3rd Party Modules
            'ui.bootstrap',
            'ui.router',
            //'ui.select',
            //'ui.alias',
    ]);

    angular.module('devModel', [
            // Angular modules
            'ngAnimate',
            'ngSanitize',
            // 3rd Party Modules
            'ui.bootstrap',
            'ui.router',
            //'ui.select',
            //'ui.alias',
            'jsonFormatter',
            'shared',
            'angularMoment']);

    app.config(['$httpProvider', '$stateProvider', '$urlRouterProvider', '$sceProvider', '$compileProvider', '$anchorScrollProvider', configRoutes]);

    function configRoutes($httpProvider, $stateProvider, $urlRouterProvider, $sceProvider, $compileProvider, $anchorScrollProvider) {
        $anchorScrollProvider.disableAutoScrolling();

        $httpProvider.defaults.withCredentials = true;

        $stateProvider
            .state('list', {
                url: '/list',
                templateUrl: 'app/zixun-list/zixun-list.html',
                controller: 'zixunListCtrl',
                controllerAs: 'vm'
            });

        $urlRouterProvider.otherwise('/list');
    }
})();
