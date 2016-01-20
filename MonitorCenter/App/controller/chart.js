
 
angular.module('app')
  .controller('ChartCtrl', function ($scope, $http, $interval, $rootScope, timerCancelService, $routeParams) {

      if (!$routeParams.serviceName) {
          return;
      }
         
      $scope.timeSteps = [];
      $scope.timeSteps.push( { text: "时", value: 3600 });
      $scope.timeSteps.push( { text: "分", value: 60 });
      $scope.timeSteps.push( { text: "秒", value: 1 });
      $scope.selectedStep = $scope.timeSteps[1];
        
        $scope.$watch("selectedStep", function (newValue, oldValue, scope) {
             getRate(newValue.value);
            //console.log(newValue);
        });
        
        function getRate(step) {
            timerCancelService.cancelTimer();
            var interTime = 1000;
            var url = "api/Monitor/InitialChart";
            var recentUrl = "api/Monitor/GetServiceRate";
            if (step) {
                url += "?step=" + step;
                recentUrl += "?step="+step;
                interTime *= step;
            }
            var serviceName = "&serviceName=" + $routeParams.serviceName;
            url += serviceName;
            recentUrl += serviceName;
            $http.get(url).success(function (data) {

                var series = $scope.chartConfig.series[0];
                var point = [];
                data.forEach(function (d) {
                    point.push({ x: new Date(d.TimeStamp), y: d.Rate });
                });

                series.data = point;
                 var timer = $interval(function () {
                    $http.get(recentUrl).success(function (rate) {
                        if (series.data.length >= 10) {
                            series.data.shift();
                        }
                        series.data.push({ x: new Date(rate.TimeStamp).getTime(), y: rate.Rate });
                    });
                }, interTime);
                timerCancelService.addTimer(timer);
            });
        };
 
        //getRate(60);
     
      //$interval(function () {
           
      //      var series = $scope.chartConfig.series[0];
      //      var x = (new Date()).getTime(), // current time
      //                            y = Math.random();
      //    series.data.shift();
      //      series.data.push({ x: x, y: y });
      //  },1000);
 
      $scope.test = "sdfasdfasdf";

      $scope.addPoints = function () {
          var seriesArray = $scope.chartConfig.series
          var x = Math.random(), // current time
                              y = Math.random();
          seriesArray[0].addPoint([x, y], true, true);
           
      };
     
      $scope.addSeries = function () {
          var rnd = []
          for (var i = 0; i < 10; i++) {
              rnd.push(Math.floor(Math.random() * 20) + 1)
          }
          $scope.chartConfig.series.push({
              data: rnd
          })
      }

      $scope.removeRandomSeries = function () {
          var seriesArray = $scope.chartConfig.series;
          var rndIdx = Math.floor(Math.random() * seriesArray.length);
          seriesArray.splice(rndIdx, 1);
      }

      $scope.toggleLoading = function () {
          this.chartConfig.loading = !this.chartConfig.loading
      }

      $scope.chartConfig = {
          options: {
              chart: {
                  type: 'spline',
                  animation: Highcharts.svg, // don't animate in old IE
                  marginRight: 10,
              }
          },
          
          title: {
              text: '速率分布'
          },
          xAxis: {

              type: 'datetime',
              tickPixelInterval: 150
          },
          yAxis: {
              title: {
                  text: '速率'
              },
              plotLines: [{
                  value: 0,
                  width: 1,
                  color: '#808080'
              }]
          },
          tooltip: {
              formatter: function () {
                  return '<b>' + this.series.name + '</b><br/>' +
                      Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                      Highcharts.numberFormat(this.y, 2);
              }
          },
          legend: {
              enabled: true
          },
          exporting: {
              enabled: true
          },
          series: [{
              name: 'ryan',
              data: (function () {
 
                  //// generate an array of random data
                  //var data = [],
                  //    time = (new Date()).getTime(),
                  //    i;

                  //for (i = -19; i <= 0; i += 1) {
                  //    data.push({
                  //        x: time + i * 1000,
                  //        y: Math.random()
                  //    });
                  //}
                  //return data;
              }())
          }]
      }
      
     
  });