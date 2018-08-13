var app = angular.module('analyticaApp', []);
app.controller('analyticaCtrl', function ($scope, $http) {

    $scope.selectedDate;
    $scope.data;
    $scope.filter;

    $scope.dateToMonthName = "MM";

    $scope.dateToMonth = "MM";

    $scope.dateToYear = "YYYY";

    $scope.dateToYearAndMonth = "YYYY-MM";

    $scope.updateRadios = function () {
        let d = new Date($scope.selectedDate);
        $scope.dateToYear = d.getFullYear();
        $scope.dateToMonth = d.getMonth() + 1;
        $scope.dateToMonthName = $scope.getMonthName(d.getMonth() + 1);
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

    $scope.getByYear = function (year) {
        $http.get("api/words/year/" + year).then(function (response) {
            $scope.data = response.data;
            drwaBarChart($scope.data, "svg1");
        });
    }

    $scope.geGetByMonth = function (month) {
        $http.get("api/words/month/" + month).then(function (response) {
            $scope.data = response.data;
            drwaBarChart($scope.data, "svg1");
        });
    }

    $scope.getByMonthInAYear = function (year,month) {
        $http.get("api/words/year/" + year + "/month/" + month).then(function (response) {
            $scope.data = response.data;
            drwaBarChart($scope.data, "svg1");
        });
    }

    $scope.submit = function () {
        
        switch($scope.filter){
            case "year":
                $scope.getByYear($scope.dateToYear);
            break;
            case "month":
                $scope.geGetByMonth($scope.dateToMonth);
                break;
            case "yearAndMonth":
                $scope.getByMonthInAYear($scope.dateToYear, $scope.dateToMonth)
                break;
            default:
                $scope.getAll();
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
    $scope.getAll();

});