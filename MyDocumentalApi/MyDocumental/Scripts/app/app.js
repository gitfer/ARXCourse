var app = angular.module('myDocumental', ['ngRoute', 'ngResource']);

angular.module('myDocumental').controller('mainController', ['$scope', '$resource', '$log', function ($scope, $resource, $log) {
    $scope.prova = 'aaa';
    var Values = $resource('http://localhost:49860/api/v1/values', {});
    Values.query({}, function (data) {
        $scope.valori = data;
    });

    var profileHub = $.connection.profileHub;
    profileHub.client.broadcastMessage = function (name, message) {
        $log.info('Received from server', message);
    };

    $.connection.hub.start().done(function () {
        $scope.sendMessageToServer = function () {
            $scope.myInput = $scope.myInput || '';
            if ($scope.myInput === '') {
                alert('Inserisci dati');
                return;
            }
            profileHub.server.send($scope.myInput,$scope.myInput);
        };
    });
}]);