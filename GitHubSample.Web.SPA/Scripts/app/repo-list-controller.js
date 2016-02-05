angular.module('gitHubSampleApp')
    .controller('ListReposCtrl',[
        '$scope',
        '$log',
        '$location',
        'repoService',
    function ($scope, $log, $location, repoService) {

        $scope.repos = [];

        repoService
              .getAllRepos()
              .success(function (data, status, headers, config) {
                  $scope.repos = data;
              });


        $scope.viewRepoDetail = function (repoId) {
            console.log(repoId);
            $scope.navigationManager.goToListPage('/RepoDetail/' + repoId);
        };
    }]);