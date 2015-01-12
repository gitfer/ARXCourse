var app = angular.module('myDocumental', ['ngRoute', 'ngResource']);

angular.module('myDocumental').controller('mainController', ['$scope', '$resource', function ($scope, $resource) {
    $scope.prova = 'aaa';
    var Values = $resource('http://localhost:49860/api/values', {});
    Values.query({}, function (data) {
            console.log(data);
        });
}]);