'use strict';

/**
 * @ngdoc function
 * @name angularjsApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the angularjsApp
 */
var app = angular.module('app');
app.controller('MainCtrl', function ($scope, $http, $interval, timerCancelService, $location) {
    $scope.Title = "bingo mainpage";
    $scope.gotochart = function(servicename) {
        $location.path("/chart/" + servicename);
    };
    $scope.services = [];
    $http.get("api/Monitor/HealthStateMock").success(function (data, status, headers, config) {
        
        $scope.services = data;
        $scope.services.forEach(function(s) {
            s.ServiceCheckUrls.forEach(function (u) {
               // $http({ method: 'POST', url: "api/Monitor/GetHealthInfo", data: { 'url': u.URL, 'seconds': u.TimeLimit } }).success

                var url = "api/Monitor/GetHealthInfo?url=" + u.Url + "&seconds=" + u.TimeLimit;
                
                 var timer= $interval(function () {
                    $http.get(url).success(function (d) {
                        u.IsGood = true;
                        u.Rate = d;
                    }).error(function (d) {
                        u.IsGood = false;
                        u.Rate = d;
                    });
                }, 1500);
                timerCancelService.addTimer(timer);
            });
        });
    }).error(function (data, status, headers, config) {
        switch (status) {
            case 404:
                break;
            default:
        }
    });


    
});

app.service("timerCancelService", function ($rootScope, $interval) {

    this.addTimer = function(timer) {
        if (!$rootScope.timers) {
            $rootScope.timers = [];
        }
        $rootScope.timers.push(timer);
    };
    this.cancelTimer = function() {
        if ($rootScope.timers) {
            $rootScope.timers.forEach(function (t) {
                $interval.cancel(t);
            });
        }
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

