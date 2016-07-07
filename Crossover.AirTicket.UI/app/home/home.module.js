var homeModule = angular.module('airTicket.home',[]);
homeModule.service('MenuService', ['$http', 'WebApiUrl','$q', function($http, webApiUrl,$q) {
        var menuService = {
            
            loadMenu : function() {
                var q = $q.defer();
                var menuUrl = webApiUrl + 'api/menu';
                $http.get(menuUrl).then(function (response) {
                    q.resolve(response.data);
                }, function (reason) {
                    q.reject(reason);
                });
                return q.promise;
            }
        }
        return menuService;
    }
]);
homeModule.controller('LayoutController', ['MenuService','$state', function (menuService,$state) {

    var self = this;
    self.menuItems = [];
    self.route = $state.current.name;
    function init() {
        menuService.loadMenu().then(function(menuItems) {
            self.menuItems = menuItems;
        }, function(reason) {
            console.log(reason);
        });
    }

    init();
}]);
