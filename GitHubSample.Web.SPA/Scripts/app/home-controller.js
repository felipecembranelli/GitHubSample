angular
    .module('gitHubSampleApp')
    .controller('HomeCtrl',[
        '$scope',
        '$log',
        '$location',
    function ($scope, $log, $location) {

        $scope.navigationManager = navigationManagerFactory();

        function navigationManagerFactory() {
            var listPath = '/';
            return {
                setListPage: function () {
                    listPath = $location.path();
                },
                goToListPage: function (listPath) {
                    $location.path(listPath);
                }
            };
        }
    }]);