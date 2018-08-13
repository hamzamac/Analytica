var app = angular.module('analyticaApp', []);
app.controller('analyticaCtrl', function ($scope, $http) {

    $scope.selectedDate;
    $scope.data;
    $scope.finter;

    $scope.dateToMonth = "MM";

    $scope.dateToYear = "YYYY";

    $scope.dateToYearAndMonth = "YYYY-MM";

    $scope.updateRadios = function () {
        let d = new Date($scope.selectedDate);
        $scope.dateToYear = d.getFullYear();
        $scope.dateToMonth = $scope.getMonthName(d.getMonth()+1);
        $scope.dateToYearAndMonth = d.getFullYear() + '-' + d.getMonth();
    }

    $scope.drawChart = function (data, targetId) {
        drwaBarChart(data, targetId);
    }

    $scope.getAll = function () {
        $http.get("api/words").then(function (response) {
            $scope.data = response.data;
            drwaBarChart($scope.data, "svg1");
        });
    }

    $scope.getByYear = function () { }

    $scope.geGetByMonth = function () { }

    $scope.getByMonthInAYear = function () { }

    $scope.search = function () {
        switch($scope.filter){
            case "year":
            break;
            case "month":
                break;
            default:
                ;
        }
    }

    $scope.getMonthName = function (monthNumber) {
        switch (monthNumber) {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            default:
                return "MM";
        }
    }

    //startup calls
    $scope.updateRadios();
    $scope.getAll();

});