var app = angular.module('myDocumental', ['ngRoute', 'ngResource', 'pascalprecht.translate', 'myDocumental.config']);

app.config(['$translateProvider', 'ARXivarAppConfig', function ($translateProvider, ARXivarAppConfig) {
    $translateProvider.useUrlLoader(ARXivarAppConfig.rootApi+'/language/');
    $translateProvider.preferredLanguage('it');
    //$translateProvider.useCookieStorage();
}]);