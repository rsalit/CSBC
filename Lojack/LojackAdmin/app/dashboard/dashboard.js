(function () {
    'use strict';
    var controllerId = 'dashboard';
    angular.module('app').controller(controllerId, ['common', 'datacontext', dashboard]);

    function dashboard(common, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.news = {
            title: 'BWI Admin',
            description: 'Lojack is a set of downloads - this could be used for other things as well'
        };
        vm.messageCount = 0;
        vm.newReocrdCount = 0;
        vm.people = [];
        vm.audit = [];
        vm.title = 'Dashboard';

        activate();

        function activate() {
            var promises = [getMessageCount(), getPeople(), getNewRecordCount(), getAuditPartials()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Dashboard View'); });
        }

        function getMessageCount() {
            return datacontext.getMessageCount().then(function (data) {
                return vm.messageCount = data;
            });
        }
        function getNewRecordCount() {
            return datacontext.getNewRecordCount().then(function (data) {
                return vm.newRecordCount = data;
            });
        }

        function getPeople() {
            return datacontext.getPeople().then(function (data) {
                return vm.people = data;
            });
        }
        function getHistory() {
            return datacontext.getAuditPartials().then(function (data) {
                return vm.audit = data;
            });
        }
    }
})();