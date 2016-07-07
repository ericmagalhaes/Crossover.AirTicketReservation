var homeModule = angular.module('airTicket.authentication',[]);

homeModule.service('AuthenticationService', ['$http', '$q','WebApiUrl','localStorageService','$httpParamSerializer','$location',function ($http, $q,webApiUrl,localStorageService,$httpParamSerializer,$location) {
    var loginService = {
        webApiUrl:webApiUrl,
		loginUser: function (email,password,provider) {
			var authPromise = $q.defer();
			var authData = {
                grant_type:'password',
		        username: email,
		        password: password
			};
		    var authLoginApi = webApiUrl + 'Token';
		    $http.post(authLoginApi, $httpParamSerializer(authData)).success(function (response) {
		        this.accessToken(response.access_token);
		        authPromise.resolve(response);
		    }).error(function(reason) {
		        authPromise.reject(reason);
		    });
		    return authPromise.promise;
		},
		registerUser: function (email, password, repassword) {

		    var authPromise = $q.defer();
		    var authRegisterApi = webApiUrl + 'api/account/register';
		    var authData = {
		        Email: email,
		        Password: password,
                ConfirmPassword:repassword
		    };
		    $http.post(authRegisterApi,authData).success(function (response) {
		        authPromise.resolve(response);
		    }).error(function (reason) {
		        authPromise.reject(reason);
		    });
		    return authPromise.promise;
		},
        availableExternalLogins: function() {
            var authPromise = $q.defer();
            var localHost = $location.protocol() + '://' + $location.host() + (($location.port() !== 80) ? ':' + $location.port() : '');
            localHost = localHost + '/index.html';
            var authRegisterApi = webApiUrl + 'api/account/externalLogins?returnUrl=' + localHost + '&generateState=true';
            $http.get(authRegisterApi).success(function (response) {
                authPromise.resolve(response);
            }).error(function (reason) {
                authPromise.reject(reason);
            });
            return authPromise.promise;
        },
        accessToken: function (token) {

            if (!token)
                return localStorageService.get('access_token');
            localStorageService.set('access_token',token.substr(13));
        }
    }
    return loginService;
}]);

homeModule.controller('LoginController', ['AuthenticationService','$state', function (authenticationService,$state) {
    var self = this;
    self.username = null;
    self.password = null;
    self.provider = null;
    self.providers = [];
    self.webApiUrl = authenticationService.webApiUrl;
    self.login = function() {
        if (!self.dataForm.$valid)
            return;
        authenticationService.loginUser(self.username, self.password).then(function(response) {
            $state.go('app.home');
        }, function(reason) {
            self.messages = reason.modelState[""];
        });

    }
    function init() {
        authenticationService.availableExternalLogins().then(function(response) {
            self.providers = response;
        }, function(reason) {
            
        });
    }

    init();
}]);
homeModule.controller('RegisterController', ['AuthenticationService','$state', function (authenticationService,$state) {
    var self = this;
    self.username = null;
    self.password = null;
    self.repassword = null;
    self.provider = null;
    self.messages = null;
    self.webApiUrl = authenticationService.webApiUrl;
    self.register = function () {

        if (!self.dataForm.$valid)
            return;

        authenticationService.registerUser(self.username, self.password,self.repassword).then(function (response) {
            $state.go('page.login');
        }, function(reason) {
            self.messages = reason.modelState[""];
        });
    }
    function init() {
        authenticationService.availableExternalLogins().then(function (response) {
            self.providers = response;
        }, function (reason) {

        });
    }

    init();
}]);
homeModule.controller('ExternalLoginController', ['AuthenticationService', '$state','$stateParams', function (authenticationService, $state,$stateParams) {
    var self = this;
    function init() {
        authenticationService.accessToken($stateParams.access_token);
        $state.go('app.home');
    }

    init();
}]);