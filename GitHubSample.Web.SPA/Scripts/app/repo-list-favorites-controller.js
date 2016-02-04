angular.module('gitHubSampleApp')
    .controller('ListReposFavoritesCtrl',[
        '$scope',
        '$log',
        '$location',
        'repoService',
    function ($scope, $log, $location, repoService) {

        $scope.repos = [];

        $scope.repos = repoService.getAllFavoritesRepos();

        $scope.viewRepoDetail = function (repoId) {
            console.log(repoId);
            $scope.navigationManager.goToListPage('/RepoDetail/' + repoId);
        };
    }]);