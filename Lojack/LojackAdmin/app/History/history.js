(function () {
    'use strict';
    var controllerId = 'history';
    angular.module('app').controller(controllerId, ['common', 'datacontext', history]);

    function history(common, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.lojackAuditProcess = [];
        vm.title = 'History';

        activate();

        function activate() {
            var promises = [getHistory()];
            common.activateController([promises], controllerId)
                .then(function () { log('Activated History View'); });
        }
        function getHistory() {
            return datacontext.getHistory().then(function (data) {
                return vm.lojackAuditProcess = data;
            });
        }
    }
})();