(function () {
    'use strict';

    angular.module('devModel').factory('devModelApi', ['$http', '$q', '$location', '$timeout', '$cache', devModelApi]);

    function devModelApi() {
        var service = {
            getProperties: getProperties,
            getProperty: getProperty,
            saveProperty: saveProperty,
            deleteProperty:deleteProperty
        };

        return service;

        function getProperties() {
        }

        function getProperty(propertyId) {

        }

        function saveProperty(property) {
        }

        function deleteProperty(property) {

        }

        function httpExecute(requestUrl, method, data) {

            if (consts.isMocking) {
                var deferred = $q.defer();
                $timeout(function () {
                    deferred.resolve(mock[method + ':' + requestUrl]);
                }, 0);
                return deferred.promise;
            }

            return $http({
                url: consts.baseUrl + requestUrl,
                method: method,
                //data: data,
                //headers: requestConfig.headers
                data: data
            }).then(function (response) {
                //set mock.
                //mock[method + ':' + requestUrl] = response.data;
                console.log('**response from EXECUTE ' + method + ' ' + requestUrl, response);

                return response.data;

            }, function (err) {
                console.log('err when ' + method + ' ' + requestUrl, err);
                return $q.reject(err);
            });
        }

        function httpGet(url) {
            return httpExecute(url, 'GET');
        }

        function httpPatch(url, data) {
            return httpExecute(url, 'PUT', data);
        }

        function httpPost(url, data) {
            return httpExecute(url, 'POST', data);
        }

        function httpDelete(url) {
            return httpExecute(url, 'DELETE');
        }

        function saveItem(url, item) {
            if (item.id) {
                return httpPatch(url + '/' + item.id, item);
            } else {
                return httpPost(url, item);
            }
        }
    }
})();