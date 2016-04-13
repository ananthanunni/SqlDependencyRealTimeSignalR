/// <reference path="../knockout-3.4.0.debug.js" />

function GridViewModel() {
    var self = this;

    self.listData = ko.observableArray([]);

    self.loadData = function () {
        $.get('Home/GetDevTestList', function (data) {
            console.log('init list loaded.');
            if (data != null)
                self.listData(data);
        });
    }

    self.addItem = function (item) {
        var currentItem = ko.utils.arrayFirst(self.listData(), function (data) {
            return data.ID == item.ID;
        });

        if (currentItem == null)
            self.listData.push(item);
    }

    self.removeItem = function (id) {
        self.listData.remove(function (item) {
            return item.ID == id;
        });
    }

    self.doAjaxDelete = function (data, clickData) {
        $.get('Home/Delete/' + data.ID, function () {
            console.log('doAjaxDelete callback invoked.');
        });
    }

    self.formatDate = function (dateInfo) {
        if (dateInfo == null || dateInfo == '')
            return '';

        var dateValue = parseInt(dateInfo.match(/\d+/ig));
        return (new Date(dateValue)).toDateString();
    }

    self.updateItem = function (id, item) {
        var currentItem = ko.utils.arrayFirst(self.listData(), function (data) {
            return data.ID == id;
        });

        self.listData.replace(currentItem, item);
    }
}

var vm = null;
$(document).ready(function () {
    vm = new GridViewModel();
    vm.loadData();

    ko.applyBindings(vm);

    var trackerHub = $.connection.dbNotificationsHub;

    // register client methods
    trackerHub.client.RelayDbChanges = function (data) {
        processUpdate(data);
    };

    trackerHub.client.RelayDbError = function (data) {
        console.log('db error occured');
        console.log(data);
    };

    trackerHub.connection.start()
    .done(function (data) {
        console.log('signalr connection initialized');
    })
    .fail(function (data) {
        console.log('signalr connection failed.');
    });
});

function processUpdate(data) {
    var eventType = {
        0: 'None',
        1: 'Delete',
        2: 'Insert',
        3: 'Update'
    };

    switch (data.ChangeType) {
        case 0:
            console.log('unknown change type.');
            break;

        case 1:
            vm.removeItem(data.Record.ID);
            break;

        case 2:
            vm.addItem(data.Record);
            break;

        case 3:
            vm.updateItem(data.Record.ID, data.Record);
            break;

        default:
            debugger;
    }
}