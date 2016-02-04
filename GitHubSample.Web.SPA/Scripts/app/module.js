angular.module('gitHubSampleApp', [
        'ngSanitize',
        'ngRoute',
        'ngMessages',
        'ui.bootstrap',
        'gitHubSampleApp.Service',
    ])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Home/ListRepos',
            controller: 'ListReposCtrl'
        });
        $routeProvider.when('/RepoDetail/:id', {
            templateUrl: '/Home/RepoDetail',
            controller: 'RepoDetailCtrl'
        });
        $routeProvider.when('/FavoritesRepos/', {
            templateUrl: '/Home/FavoritesRepos',
            controller: 'ListReposFavoritesCtrl'
        });
        //$routeProvider.otherwise({
        //    redirectTo: '/'
        //});

        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
        //$locationProvider.html5Mode(false).hashPrefix('!');
        //$locationProvider.html5Mode(true);

    }]).run(function ($rootScope, $location) {
        $rootScope.$on("$routeChangeStart", function (event, next, current) {

            console.log(current);
        });
    });;
