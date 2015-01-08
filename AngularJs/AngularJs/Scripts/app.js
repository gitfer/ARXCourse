var app = angular.module('arxivar', ['ngRoute', 'ngResource', 'ngSanitize']);

app.config(function($routeProvider) {
    $routeProvider
      .when('/', {
      })
      .when('/argomenti', {
          templateUrl: 'views/argomenti.html',
          controller: 'argomentiCtrl'
      })
      .when('/argomenti/:id', {
          templateUrl: 'views/argomento.html',
          controller: 'argomentoDettaglioCtrl',
          //resolve: {
          //    serverModels: ['$route', 'templateService', function ($route, templateService) {
          //        return templateService.loadModel($route.current.params.template);
          //    }]
          //}
      })
      .when('/error', {
          templateUrl: 'views/404.html'
      })
      .otherwise({
          redirectTo: '/error'
      });
});

app.controller('argomentiCtrl', ['$scope', function($scope) {
    $scope.argomenti = [
        {
            id: 1,
            nome: 'databinding'
        },
        {
            id: 2,
            nome: 'filtri'
        },
        {
            id: 3,
            nome: 'servizi'
        },
        {
            id: 4,
            nome: 'risorse'
        },
        {
            id: 5,
            nome: 'direttive'
        }
    ];
    this.prova = 'ciao';
}]);

app.controller('argomentoDettaglioCtrl', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.argomenti = [
        {
            id: 1,
            nome: 'databinding e varie',
            temi: ['one/two way databinding', '{{ }} vs ng-bind', 'binding di properties non definite sullo scope muoiono silently', 'DOM inheritance between controllers', 'comunicazioni tra controllers emit/broadcast o servizi', 'controllerAs syntax', 'lifecycle https://docs.angularjs.org/guide/scope']
        },
        {
            id: 2,
            nome: 'filtri',
            temi: ['filtri predefiniti vs filtri custom', 'pipe di filtri', 'uso programmatico dei filtri']
        },
        {
            id: 3,
            nome: 'servizi',
            temi: ['dependency injection', 'servizi predefiniti vs servizi custom', 'lifetime', 'tipologie: http://stackoverflow.com/questions/15666048/service-vs-provider-vs-factory']
        },
        {
            id: 4,
            nome: 'risorse',
            temi: ['metodi out of the box', 'vincoli', 'restangular', '$http']
        },
        {
            id: 5,
            nome: 'direttive',
            temi: ['direttive predefinite vs direttive custom', 'webcomponents', 'restrict', 'transclusion', 'compile vs linking function e rispettivi parametri']
        }
    ];
    Array.prototype.findElementBy = function(obj) {
        for (var i = 0, len = this.length; i < len; i++) {
            if (this[i][obj.property] === obj.value) {
                console.log(this[i][obj.property], obj.value, this[i]);
                return this[i];
            }
        }
        return undefined;
    };
    $scope.argomento = $scope.argomenti.findElementBy({ property: 'id', value: parseInt($routeParams.id) });
    
}]);
