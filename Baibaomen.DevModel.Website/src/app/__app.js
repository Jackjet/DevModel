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