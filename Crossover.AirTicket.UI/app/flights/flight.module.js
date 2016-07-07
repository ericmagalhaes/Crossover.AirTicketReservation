var homeModule = angular.module('airTicket.flights',[]);

homeModule.service('FlightService', ['$http', '$q','WebApiUrl','localStorageService','$httpParamSerializer','$location',function ($http, $q,webApiUrl,localStorageService,$httpParamSerializer,$location) {
    var flightService = {
        webApiUrl:webApiUrl,
		airports: function () {
			var q = $q.defer();
		    var airportsUrl = webApiUrl + 'airports';
		    $http.get(airportsUrl).success(function (response) {
		        q.resolve(response.data);
		    }).error(function(reason) {
		        q.reject(reason);
		    });
		    return q.promise;
		},
		
    }
    return flightService;
}]);

homeModule.controller('FlightController', ['FlightService','$state', function (flightService,$state) {
    var self = this;
    
    function init() {
        flightService.airports().then(function(response) {
            self.providers = response;
        }, function(reason) {
            
        });
    }

    init();
}]);
