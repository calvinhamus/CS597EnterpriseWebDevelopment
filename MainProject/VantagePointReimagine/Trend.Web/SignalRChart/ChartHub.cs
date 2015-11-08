using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Trend.Core.Data;
using Microsoft.AspNet.SignalR.Hubs;
using Trend.Web.Models;
using System.Threading.Tasks;


namespace Trend.Web.SignalRChart
{

    [HubName("signalrchart")]
    public class ChartHub : Hub  
    {
        public List<HubClient> HubClients = new List<HubClient>();
        private readonly ChartService _chartService;

        public ChartHub() :
            this(ChartService.Instance)
        {

        }
        public override Task OnConnected()
        {
            var user = Context.User;
            string name;
            if (user.Identity.IsAuthenticated)
            {
                name = user.Identity.Name;
            }

            var id = Context.ConnectionId;
            AddUserToHub(id);

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
    
        public ChartHub(ChartService chartService)
        {
            _chartService = chartService;
        }
        public void AddToChart(string clientId, int chartId)
        {
             var client = GetUser(clientId);
            client.ChatIds.Add(chartId);
         
        }

        private HubClient GetUser(string clientId)
        {
            return HubClients.Find(x => x.UserId == clientId);
           // throw new NotImplementedException();
        }

        public void RemoveFromChart(string clientId, int chartId)
        {
            var client = GetUser(clientId);
            client.ChatIds.Remove(chartId);
        }
        public T_SavedChart LoadChart(int chartId)
        {
            throw new NotImplementedException();
        }
        public bool SaveChart(string clientId)
        {
            var client = GetUser(clientId);
            _chartService.SaveChart(client);
            throw new NotImplementedException();
        }
        public string StartChartData(string clientId)
        {     
            _chartService.Ready();
            return "Ready";
        }

        private void AddUserToHub(string clientId)
        {
            var hubClient = new HubClient
            {
                UserId = clientId
            };
            HubClients.Add(hubClient);

        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}