(function () {
    'use strict';

    var app = angular
        .module('ang');
    app.controller('playersController', index);
    function index($scope, $http) {
        $http({
                method: 'get',
                url: "/en/Angular/GetResult"
            })
            .then(function(res) {
                $scope.model = res.data;
            });
    }
})();
