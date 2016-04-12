using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SqlChangeTrackingPoc.Service;
using SqlChangeTrackingPoc.Models.DevTest;
using TableDependency.EventArgs;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Web.Hubs
{
    public class DbNotificationsHub : Hub
    {
        public void RelayDbChanges(Service.RecordChangedEventArgs<DevTestModel> model)
        {
            Clients.All.RelayDbChanges(model);
        }

        public void RelayError(ErrorEventArgs args)
        {
            Clients.All.RelayDbError(args);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}