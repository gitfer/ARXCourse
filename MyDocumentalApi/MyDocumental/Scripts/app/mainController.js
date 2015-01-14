
app.controller('mainController', ['$scope', '$resource', '$log', '$translate', 'arxivarAppConfig', function ($scope, $resource, $log, $translate, arxivarAppConfig) {

    $scope.languages = [{ label: 'Inglese', val: 'en' }, { label: 'Italiano', val: 'it' }];
    $scope.languageSelected = $scope.languages[1];
    
    $scope.$watch('languageSelected', function (oldVal, newVal) {
        if (newVal) {
            $translate.use($scope.languageSelected.val);
        }
    });

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