angular.module('sendemailServices', ['ngResource']).
    factory('Email', function($resource) {
        return $resource('/messages/:messageId', {}, {            
            'update': { method: 'PUT', url: '/messages/' }
        });
    }).
    factory('EmailByApplication', function($resource) {
        return $resource('/messages/applications/:applicationId', {}, {
            'resend': { method: 'PUT', url: '/messages/', params: { ResendEmail: true } }
        });
    });