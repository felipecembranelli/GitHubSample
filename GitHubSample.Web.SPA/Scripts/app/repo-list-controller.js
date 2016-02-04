angular.module('gitHubSampleApp')
    .controller('ListReposCtrl',[
        '$scope',
        '$log',
        '$location',
        'repoService',
    function ($scope, $log, $location, repoService) {

        $scope.repos = [];

        $scope.repos = repoService.getAllRepos();

        $scope.viewRepoDetail = function (repoId) {
            console.log(repoId);
            $scope.navigationManager.goToListPage('/RepoDetail/' + repoId);
        };
    }]);