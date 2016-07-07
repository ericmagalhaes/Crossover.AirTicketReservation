var core = angular.module('airTicket.core', ['ui.router', 'LocalStorageModule','ui.bootstrap']);
core.constant('WebApiUrl', 'http://localhost:34135/');
core.config(['$stateProvider', '$locationProvider', '$urlRouterProvider','localStorageServiceProvider','$httpProvider',
    function ($stateProvider, $locationProvider, $urlRouterProvider, localStorageServiceProvider,$httpProvider) {

        
        $locationProvider.html5Mode(false);
        $urlRouterProvider.otherwise('/login');

        $stateProvider
            .state('app', {
                abstract: true,
                templateUrl: 'views/module.layout.html',
                controller: 'LayoutController',
                controllerAs:'vm'
            })
            .state('app.home', {
                url: '/dashboard',
                templateUrl: 'views/module.home.html'
                
            })
            .state('app.flight', {
                url: '/flights',
                templateUrl: 'views/module.flight.html'

            })
            .state('page', {
                abstract: true,
                templateUrl: 'views/page.layout.html'
            })
            .state('page.login', {
                url:'/login',
                templateUrl: 'views/page.login.html',
                controller: 'LoginController',
                controllerAs:'vm'
            })
            .state('page.register', {
                url: '/register',
                templateUrl: 'views/page.register.html',
                controller: 'RegisterController',
                controllerAs: 'vm'

            }).state('page.externalLogin', {
                url: '/:access_token',
                templateUrl: 'views/page.external.html',
                controller: 'ExternalLoginController',
                controllerAs: 'vm'

            });

        $httpProvider.interceptors.push(['localStorageService','$q','WebApiUrl',function (localStorageService,$q,webApiUrl) {
            return {
                'request': function (config) {
                    if (config.url.indexOf(webApiUrl) >= 0) {
                        var token = localStorageService.get('access_token');
                        if (token != undefined) {
                            config.headers['Authorization'] = 'Bearer ' + token;
                        }
                    }
                    return config;

                },

                'response': function (response) {
                    return response;
                }
            };
        }]);

    }
]);
core.run(['$rootScope', 'localStorageService','WebApiUrl','$http', function ($rootScope, localStorageService,webApiUrl,$http) {
    function init() {
       
    }

    init();
}]);
