/// <reference path="../__0constants.js" />

(function () {
    'use strict';

    angular
      .module('shared')
      .factory('http', ['$http', '$q', '$timeout', httpService]);

    function httpService($http, $q, $timeout) {
        var _restBase;
        var service = {
            get: get,
            post: post,
            patch: patch,
            del: del,
            save: save
        };

        return service;

        var mock = [];
        function httpExecute(requestUrl, method, data) {
            if (consts.isMocking) {
                var toReturn = mock[method + ':' + requestUrl];
                if (toReturn) {
                    var deferred = $q.defer();
                    $timeout(function () {
                        deferred.resolve(toReturn);
                    }, 0);
                    return deferred.promise;
                }
            }

            return $http({
                url: consts.baseUrl + requestUrl,
                method: method,
                //data: data,
                //headers: requestConfig.headers
                data: data
            }).then(function (response) {
                //set mock.
                if (consts.isMocking) {
                    mock[method + ':' + requestUrl] = response.data;
                }
                console.log('**response from EXECUTE ' + method + ' ' + requestUrl, response);

                return response.data;

            }, function (err) {
                console.error('err when ' + method + ' ' + requestUrl, err);
                return $q.reject(err);
            });
        }

        function get(url) {
            return httpExecute(url, 'GET');
        }

        function patch(url, data) {
            return httpExecute(url, 'PUT', data);
        }

        function post(url, data) {
            return httpExecute(url, 'POST', data);
        }

        function del(url) {
            return httpExecute(url, 'DELETE');
        }

        function save(url, item) {
            if (item.id) {
                return httpPatch(url + '/' + item.id, item);
            } else {
                return httpPost(url, item);
            }
        }

        function restFuncs(restBase) {
            _restBase = restBase;
        }
    }
})();
