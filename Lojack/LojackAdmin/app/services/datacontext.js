(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, ['common', datacontext]);

    function datacontext(common) {
        var $q = common.$q;

        var service = {
            getPeople: getPeople,
            getMessageCount: getMessageCount,
            getHistory: getHistory,
            getNewRecordCount : getNewRecordCount
        };

        return service;

        function getMessageCount() { return $q.when(10); }
        function getNewRecordCount() { return $q.when(25);}
        function getPeople() {
            var people = [
                { firstName: 'John', lastName: 'Papa', age: 25, location: 'Florida' },
                { firstName: 'Ward', lastName: 'Bell', age: 31, location: 'California' },
                { firstName: 'Colleen', lastName: 'Jones', age: 21, location: 'New York' },
                { firstName: 'Madelyn', lastName: 'Green', age: 18, location: 'North Dakota' },
                { firstName: 'Ella', lastName: 'Jobs', age: 18, location: 'South Dakota' },
                { firstName: 'Landon', lastName: 'Gates', age: 11, location: 'South Carolina' },
                { firstName: 'Haley', lastName: 'Guthrie', age: 35, location: 'Wyoming' }
            ];
            return $q.when(people);
        }
        function getHistory() {
            var lojackAuditProcess = [
                { processDate: '08/14/14', recordsProcessed: 62000, newRecords: 25 },
                { processDate: '08/15/14', recordsProcessed: 62000, newRecords: 25 },
                { processDate: '08/16/14', recordsProcessed: 62025, newRecords: 5 },
                { processDate: '08/17/14', recordsProcessed: 62036, newRecords: 4 },
                { processDate: '08/18/14', recordsProcessed: 62100, newRecords: 36 }
            ];
            return $q.when(lojackAuditProcess);
        }
    }
})();