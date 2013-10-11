'use strict';

/* Controllers */

function MessageListCtrl($scope, EmailByApplication) {
    $scope.messages = EmailByApplication.query({ applicationId: 1 });

    //, function(data) {
    //    $scope.messages = data.Messages;
    //});
    //$http.get('/messages/applications/1').success(function (data) {
    //    $scope.messages = data.Messages;
    //});
    //$scope.phones = Phone.query();
    $scope.orderProp = 'To';

    $scope.resendEmail = function(email) {
        email.$resend();
    };
}

MessageListCtrl.$inject = ['$scope', 'EmailByApplication'];



function MessageDetailCtrl($scope, $routeParams, Email) {
    $scope.addToHidden = true;
    $scope.addCcHidden = true;
    $scope.addBccHidden = true;
    $scope.addReplyToHidden = true;
    $scope.newTo = "";
    $scope.newCc = "";
    $scope.newBcc = "";
    $scope.newReplyTo = "";
    Email.get({ messageId: $routeParams.messageId }, function(message) {
        $scope.message = message;
        $scope.showCredential = message.Credential != null;
        $scope.message.Status.CreateDateVal = new Date(parseInt($scope.message.Status.CreateDate.substr(6)));
        $scope.message.CreateDateVal = new Date(parseInt($scope.message.CreateDate.substr(6)));
    });
    $scope.save = function() {
        Email.update($scope.message, function(result) {
            alert(result);
        });
    };
    $scope.removeTo = function (index) {
        $scope.message.To.splice(index, 1);
    };
    $scope.removeCc = function (index) {
        $scope.message.Cc.splice(index, 1);
    };
    $scope.removeBcc = function (index) {
        $scope.message.Bcc.splice(index, 1);
    };
    $scope.removeReplyTo = function (index) {
        $scope.message.ReplyTo.splice(index, 1);
    };
    $scope.showAddTo = function() {
        $scope.addToHidden = false;
    };
    $scope.showAddCc = function () {
        $scope.addCcHidden = false;
    };
    $scope.showAddBcc = function () {
        $scope.addBccHidden = false;
    };
    $scope.showAddReplyTo = function () {
        $scope.addReplyToHidden = false;
    };
    $scope.cancelAddTo = function () {
        $scope.addToHidden = true;
        $scope.newTo = "";
    };
    $scope.addTo = function () {
        $scope.addToHidden = true;
        $scope.message.To.push($scope.newTo);
    };
    $scope.addCc = function () {
        $scope.addCcHidden = true;
        $scope.message.Cc.push($scope.newCc);
    };
    $scope.addBcc = function () {
        $scope.addBccHidden = true;
        $scope.message.Bcc.push($scope.newBcc);
    };
    $scope.addReplyTo = function () {
        $scope.addReplyToHidden = true;
        $scope.message.ReplyTo.push($scope.newReplyTo);
    };
    //$http.get('/messages/' + $routeParams.messageId).success(function(data) {
    //    $scope.message = data;
    //    $scope.showCredential = data.Credential != null;
    //    $scope.message.Status.CreateDateVal = new Date(parseInt($scope.message.Status.CreateDate.substr(6)));
    //    $scope.message.CreateDateVal = new Date(parseInt($scope.message.CreateDate.substr(6)));
    //});
}

MessageDetailCtrl.$inject = ['$scope', '$routeParams', 'Email'];
