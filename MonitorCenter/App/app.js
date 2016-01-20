(function () {
    'use strict';

   var app= angular.module('app', [
        'ngAnimate',
        'ngRoute', 'highcharts-ng'
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
               
          })
          .when('/chart/:serviceName', {
              templateUrl: 'views/chart.html',
              controller: 'ChartCtrl',
            })
          .otherwise({
              redirectTo: '/'
          });
    }
    );
   app.run(function ($rootScope, timerCancelService,$interval, $log, $timeout) {
        
       $rootScope.$on("$routeChangeStart", function (event, current,
       previous, rejection) {
           $rootScope.loading = true;
           if (timerCancelService) {
               timerCancelService.cancelTimer();
           }
            
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
