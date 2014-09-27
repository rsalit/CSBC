(function () {
    'use strict';

    var serviceId = 'datacontext';
    angular.module('app').factory(serviceId, ['common', 'entityManagerFactory', datacontext]);

    function datacontext(common, emFactory) {
        var EntityQuery = breeze.EntityQuery;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(serviceId);
        var logError = getLogFn(serviceId, 'error');
        var logSuccess = getLogFn(serviceId, 'success');
        var manager = emFactory.newManager();
        var primePromise;
        var $q = common.$q;

        var entityNames = {
            audit: 'Audit',
  
        };
        var service = {

            getPeople: getPeople,
            getMessageCount: getMessageCount,
            getHistory: getHistory,
            getNewRecordCount: getNewRecordCount,
            getAuditPartials: getAuditPartials,
            prime: prime
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

        function getAuditPartials() {
            var audit = [];
            var orderBy = 'ProcessDateTime';
            return EntityQuery.from('LojackAuditProcess')
                .select('ProcessDateTime, RecordsProcessed, TotalRecords')
                .orderBy(orderBy)
                .toType('LojackOrderProcess')
                .using(manager).execute()
                .to$q(querySucceeded, _queryFailed);

            function querySucceeded(data) {
                audit = data.results;
                log('Retrieved [Audit Partials] from remote data source', audit.length, true);
                return audit;
            }
        }
        function prime() {
            if (primePromise) return primePromise;

            primePromise = $q.all([getAuditPartials()])
                .then(extendMetadata)
                .then(success);
            return primePromise;

            function success() {
                //setLookups();
                log('Primed the data');
            }

            function extendMetadata() {
                var metadataStore = manager.metadataStore;
                var types = metadataStore.getEntityTypes();
                types.forEach(function (type) {
                    if (type instanceof breeze.EntityType) {
                        set(type.shortName, type);
                    }
                });

                var auditEntityName = entityNames.audit;
                ['Audit'].forEach(function (r) {
                    set(r, auditEntityName);
                });

                function set(resourceName, entityName) {
                    metadataStore.setEntityTypeForResourceName(resourceName, entityName);
                }
            }

        }
    }
})();