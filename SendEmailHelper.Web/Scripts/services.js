angular.module('sendemailServices', ['ngResource']).
    factory('Email', function($resource) {
        return $resource('/messages/:messageId', {}, {            
            
        });
    }).
    factory('EmailByApplication', function($resource) {
        return $resource('/messages/applications/:applicationId', {}, {
            'query': { method: 'GET', isArray: true }
        });
    });