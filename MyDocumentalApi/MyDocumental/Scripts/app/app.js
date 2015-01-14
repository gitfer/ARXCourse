var app = angular.module('myDocumental', ['ngRoute', 'ngResource', 'pascalprecht.translate', 'myDocumental.config']);

app.factory('_', ['$window',
    function ($window) {
        return $window._;
    }
]);

app.config(['$translateProvider', 'arxivarAppConfig', function ($translateProvider, arxivarAppConfig) {
    $translateProvider.useUrlLoader(arxivarAppConfig.rootApi + '/languages/');
    $translateProvider.preferredLanguage('it');
}]);