angular
    .module('gitHubSampleApp.Service', [])
    .factory('repoService', [
        '$http',
        function ($http) {

            var baseUrl = '/';

            // define Services
            return {

                getAllRepos: function () {
                    return $http({
                        method: 'GET',
                        url: baseUrl + 'api/GitHubRepos/GetAllGitHubRepos/'
                    });
                },

                // return repo by id
                getRepoById: function (id) {
                    return $http({
                        method: 'GET',
                        url: baseUrl + 'api/GitHubRepos/GetGitHubRepoById/' + id
                    });
                },

                //// return repo by id
                //getAllFavoritesRepos: function () {

                //    favoritesRepoList = [];

                //    var i = 0, len = repoList.length;
                //    for (; i < len; i++) {
                //        if (repoList[i].isFavoriteRepo == 'true') {
                //            favoritesRepoList.push(repoList[i]);
                //        }
                //    }
                //    return favoritesRepoList;
                //},

            };
        }]);
