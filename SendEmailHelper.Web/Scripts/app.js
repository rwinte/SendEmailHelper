angular.module('sendemail', ['sendemailServices']).
  config(['$routeProvider', function($routeProvider) {
  $routeProvider.
      when('/messages', {templateUrl: 'partials/message-list.html',   controller: MessageListCtrl}).
      when('/messages/:messageId', {templateUrl: 'partials/message-detail.html', controller: MessageDetailCtrl}).
      otherwise({redirectTo: '/messages'});
}]);