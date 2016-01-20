(function () {
    'use strict';

   var app= angular.module('app', [
        'ngAnimate',
        'ngRoute'
    ]).config(
    function ($routeProvider) {
        $routeProvider
          .when('/', {
              templateUrl: 'views/main.html',
              controller: 'MainCtrl'
              
          })
          .when('/about', {
              templateUrl: 'views/about.html',
              controller: 'AboutCtrl',
              controllerAs: 'about'
            })
          .otherwise({
              redirectTo: '/'
          });
    }
    );
   app.run(function ($rootScope, $log, $timeout) {
        
       $rootScope.$on("$routeChangeStart", function (event, current,
       previous, rejection) {
           $rootScope.loading = true;
          
       });
       $rootScope.$on("$routeChangeError", function (event, current,
previous, rejection) {
           $window.location.href = "error.html";
       });
       $rootScope.$on("$routeChangeSuccess", function(event, current,
previous, rejection) {
$rootScope.loading = false;
       });
   });
})();

'use strict';

/**
 * @ngdoc function
 * @name angularjsApp.controller:AboutCtrl
 * @description
 * # AboutCtrl
 * Controller of the angularjsApp
 */
angular.module('app')
  .controller('AboutCtrl', function ($scope) {
        $scope.Title = "this is ryan";
    });

'use strict';

/**
 * @ngdoc function
 * @name angularjsApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the angularjsApp
 */
var app = angular.module('app');
  app.controller('MainCtrl', function ($scope,$http) {
      $scope.Title = "bingo mainpage";
      $scope.appTitle = "[Packt] Parking";
      $scope.cars = [];
      $scope.colors = ["White", "Black", "Blue", "Red",
"Silver"];
      $scope.park = function (car) {
          car.entrance = new Date();
          $scope.cars.push(car);
          delete $scope.car;
      };
      $http.get("api/Monitor/GetServiceHealth").success(function (data, status, headers, config) {
          if (status) {
              var head = headers;
              var con = config;
          }
          $scope.apiResult = data;
      }).error(function(data, status, headers, config) {
          switch (status) {
          case 404:
               break;
          default:
          }
      });
  });
   
  app.directive("alert", function () {
      return {
          restrict: 'E',
          templateUrl: "App/Template/alert.html",
          replace: true
      };
  });
 
//app.constant("parkConfig", {
//    parkingRate: 10
//});
//app.service("parkingService", function(parkConfig) {
//    this.calculateTicket = function (car) {
//        var departHour = new Date().getHours();
//        var entranceHour = car.entrance.getHours();
//        var parkingPeriod = departHour-entranceHour;
//        var parkingPrice = parkingPeriod * parkConfig.parkingRate;
//        return {
//            period: parkingPeriod,
//            price: parkingPrice
//        };
//    };
//});

//app.provider("parkingService", function (parkingConfig) {
//    var _parkingRate = parkingConfig.parkingRate;
//    var _calculateTicket = function (car) {
//        var departHour = new Date().getHours();
//        var entranceHour = car.entrance.getHours();
//        var parkingPeriod = departHour-entranceHour;
//        var parkingPrice = parkingPeriod * _parkingRate;
//        return {
//            period: parkingPeriod,
//            price: parkingPrice
//        };
//    };
//    this.setParkingRate = function (rate) {
//        _parkingRate = rate;
//    };
//    this.$get = function () {
//        return {
//            calculateTicket: _calculateTicket
//        };
//    };
//});

//app.config(function (parkingService) {
//    parkingService.setParkingRate(3);
//});

//app.factory("parkingFacade", function($http) {

//    var _getCars = function() {
//        return $http.get("/cars");
//    };
//    var _getCar = function(id) {
//        return $http.get("/cars/" + id);
//    };
//    var _saveCar = function(car) {
//        return $http.post("/cars", car);
//    };
//    var _updateCar = function(car) {
//        return $http.put("/cars" + car.id, car);
//    };
//    var _deleteCar = function(id) {
//        return $http.delete("/cars/" + id);
//    };
//    return {
//        getCars: _getCars,
//        SaveCar: _saveCar
//    };
//});
//app.run(function($http) {
//    $http.default.cache = true;
//});

//app.factory('httpUnauthorizedInterceptor', function ($q,
//$rootScope, $location, $window) {
     
//    return {
      
//        'responseError': function (rejection) {
//            if (rejection.status === 401) {
//                $rootScope.login = true;
//            } 
//            return $q.reject(rejection);
//        }
//    }
//});

//app.config(function ($httpProvider) {
//    $httpProvider.interceptors.push('httpUnauthorizedInterceptor');
//});

