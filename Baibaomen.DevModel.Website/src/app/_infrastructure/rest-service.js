
(function () {
    'use strict';

    angular
      .module('devModel')
      .factory('rest', ['http', restService]);

    var _itemUrl;

    function restService(http) {
        var service = function (itemUrl) {
            _itemUrl = itemUrl;

            return {
                get: get,
                post: post,
                patch: patch,
                del:del
            }
        }

        return service;

        function get(/*optional*/ id) {
            return http.get(_itemUrl + (id ? ('/' + id) : '?$count=true'));
        }

        function post(id, item) {
            return http.post(_itemUrl + '/' + id, item);
        }

        function patch(id, item) {
            return http.patch(_itemUrl + '/' + id, item);
        }

        function del(id, recordVersion) {
            return http.del(_itemUrl + '/' + id + (recordVersion? ('?recordVersion=' + encodeURIComponent(recordVersion)):''));
        }
    }
})();
