angular.module('gitHubSampleApp')
    .controller('RepoDetailCtrl', [
        '$scope', 
        '$uibModal', 
        '$log', 
        '$location',
        '$routeParams',
        'repoService',
    function ($scope, $uibModal, $log, $location, $routeParams, repoService) {

        $scope.cancel = function () {
            $scope.navigationManager.goToListPage('/');
        };

        //$scope.repoDetail = repoService.getRepoById($routeParams.id);

        repoService
              .getRepoById($routeParams.id)
              .success(function (data, status, headers, config) {
                  $scope.repoDetail = data;
              });

        console.log($scope.repoDetail);
    }]);