using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Trend.Core.Data;
using Microsoft.AspNet.SignalR.Hubs;
using Trend.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Trend.Web.SignalRChart
{

    [HubName("signalrchart")]
    public class ChartHub : Hub  
    {
        public List<HubClient> HubClients = new List<HubClient>();
        private readonly ChartService _chartService;
        private DummyPlcService _dummyPlcService;

        public ChartHub() :
            this(ChartService.Instance, DummyPlcService.Instance)
        {

        }
        public override Task OnConnected()
        {

            var user =  Context.User.Identity.Name;
            var id = Context.ConnectionId;
            Clients.Caller.HubClient = AddUserToHub(id, user);
            Clients.Caller.initialized();

            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            // Add your own code here.
            // For example: in a chat application, mark the user as offline, 
            // delete the association between the current connection id and user name.
            var id = Context.ConnectionId;
           
            HubClients.Remove(HubClients.Find(x => x.UserId == id));
            return base.OnDisconnected(stopCalled);
        }

        //public override Task OnReconnected()
        //{
        //    // Add your own code here.
        //    // For example: in a chat application, you might have marked the
        //    // user as offline after a period of inactivity; in that case 
        //    // mark the user as online again.
        //    return base.OnReconnected();
        //}
    
        public ChartHub(ChartService chartService, DummyPlcService dummyPlcService)
        {
            _chartService = chartService;
            _dummyPlcService = dummyPlcService;
        }
        public void AddToChart(string clientId, int dataPointId)
        {
            var  hubClient = Clients.Caller.HubClient;

            var points = hubClient["DataPointIds"];
            points.Add(dataPointId);

            hubClient["DataPointIds"] = points;
            Clients.Caller.HubClient = hubClient;

            var point = _chartService.GetPoint(dataPointId);
            
            Clients.Caller.addToLegend(point);

        }
        public void RemoveFromChart(string clientId, long dataPointId)
        {
            var hubClient = Clients.Caller.HubClient;

            var points = hubClient["DataPointIds"];
            points.Remove(dataPointId);

            hubClient["DataPointIds"] = points;
            Clients.Caller.HubClient = hubClient;
        }
        public T_SavedChart LoadChart(int chartId)
        {
            throw new NotImplementedException();
        }
        public bool SaveChart(string clientId)
        {
            var hubClient = Clients.Caller.HubClient;
            _chartService.SaveChart(hubClient["UserName"]);
            throw new NotImplementedException();
        }
        public string StartChartData(string clientId)
        {
            var hubClient = Clients.Caller.HubClient;
            //var client = GetUser(clientId);
           
            //_chartService.Ready();
            var points = hubClient["DataPointIds"];
            _dummyPlcService.StartDummyPlc(points);
            return "Ready";
        }
        public string StopChartData(string clientId)
        {
            //_chartService.Stop();
            _dummyPlcService.StopDummyPlc(0);
            return "Stopped";
        }
        private HubClient AddUserToHub(string clientId, string userName)
        {

            var holder = new List<int>();
           // holder.Add(2);
          //  holder.Add(3);
            var hubClient = new HubClient
            {
                UserId = clientId,
                UserName = userName,
                DataPointIds = holder
            };
            return hubClient;

        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}