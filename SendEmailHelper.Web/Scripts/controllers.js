'use strict';

/* Controllers */

function MessageListCtrl($scope, EmailByApplication) {
    EmailByApplication.get({ applicationId: 1 }, function(data) {
        $scope.messages = data.Messages;
    });
    //$http.get('/messages/applications/1').success(function (data) {
    //    $scope.messages = data.Messages;
    //});
    //$scope.phones = Phone.query();
    $scope.orderProp = 'To';
}

MessageListCtrl.$inject = ['$scope', 'EmailByApplication'];



function MessageDetailCtrl($scope, $routeParams, Email) {
    Email.get({ messageId: $routeParams.messageId }, function(message) {
        $scope.message = message;
        $scope.showCredential = message.Credential != null;
        $scope.message.Status.CreateDateVal = new Date(parseInt($scope.message.Status.CreateDate.substr(6)));
        $scope.message.CreateDateVal = new Date(parseInt($scope.message.CreateDate.substr(6)));
    });
    //$http.get('/messages/' + $routeParams.messageId).success(function(data) {
    //    $scope.message = data;
    //    $scope.showCredential = data.Credential != null;
    //    $scope.message.Status.CreateDateVal = new Date(parseInt($scope.message.Status.CreateDate.substr(6)));
    //    $scope.message.CreateDateVal = new Date(parseInt($scope.message.CreateDate.substr(6)));
    //});
}

MessageDetailCtrl.$inject = ['$scope', '$routeParams', 'Email'];
