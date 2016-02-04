angular
    .module('gitHubSampleApp.Service', [])
    .factory('repoService', [
        '$http',
        function ($http) {

            var baseUrl = '/';

            repoList = [];

            // Load fake data
            function loadAllRepos() {
                console.log('Executing loadAllRecords');

                // load regular repos
                for (var i = 0; i < 10; i++) {
                    repoList.push({
                        id: i,
                        name: 'repository' + i,
                        description: 'lorem ipsum lorem ipsum lorem ipsum lorem ipsum',
                        owner: 'Felipe Cembranelli',
                        language: 'C#',
                        lastUpdate: '02/01/2016',
                        imgPath: 'http://localhost:52521/Content/Image/github-512.png',
                        isFavoriteRepo: 'false',
                        contributors: 'João da Silva'
                    });
                }

                // load favorites
                for (var i = 11; i < 20; i++) {
                    repoList.push({
                        id: i,
                        name: 'favorite' + i,
                        description: 'lorem ipsum lorem ipsum lorem ipsum lorem ipsum',
                        owner: 'Felipe Cembranelli',
                        language: 'C#',
                        lastUpdate: '02/01/2016',
                        imgPath: 'http://localhost:52521/Content/Image/github-512.png',
                        isFavoriteRepo: 'true',
                        contributors: 'João da Silva'
                    });
                }
            }

            loadAllRepos();

            // define Services
            return {

                // return all repos
                getAllRepos: function () {
                    return repoList;
                },


                // return repo by id
                getRepoById: function (id) {
                    console.log('GetRepoById: ' + id);

                    var i = 0, len = repoList.length;
                    for (; i<len; i++) {
                        if (+repoList[i].id == +id) {
                            console.log('found');
                            return repoList[i];
                        }
                    }
                    return null;
                },

                // return repo by id
                getAllFavoritesRepos: function () {

                    favoritesRepoList = [];

                    var i = 0, len = repoList.length;
                    for (; i < len; i++) {
                        if (repoList[i].isFavoriteRepo == 'true') {
                            favoritesRepoList.push(repoList[i]);
                        }
                    }
                    return favoritesRepoList;
                },

                //getAllPostsBiggerThan: function () {
                //    return $http({
                //        method: 'GET',
                //        url: baseUrl + '/GetAllPostsBiggerThan/0'
                //    });
                //},


                //updatePost: function (post) {
                //    return $http({
                //        method: 'PUT',
                //        url: baseUrl + '/UpdatePost',
                //        data: post
                //    });
                //},

            };
        }]);
