
app.controller('mainController', ['$scope', '$resource', '$log', '$translate', 'ARXivarAppConfig', function ($scope, $resource, $log, $translate, ARXivarAppConfig) {

    $scope.languages = ['en', 'it'];
    $scope.languageSelected = $scope.languages[$scope.languages.indexOf(ARXivarAppConfig.defaultLanguage)];
    $translate.use($scope.languageSelected);
    
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
            profileHub.server.send($scope.myInput, $scope.myInput);
        };
    });
}]);