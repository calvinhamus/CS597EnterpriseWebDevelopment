using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

using Trend.Core.Data;
using Microsoft.AspNet.SignalR.Hubs;

namespace Trend.Web.SignalRChart
{
    [HubName("signalrchart")]
    public class ChartHub : Hub
    {
        private readonly ChartService _chartService;

        public ChartHub() :
            this(ChartService.Instance)
        {

        }

        public ChartHub(ChartService chartService)
        {
            _chartService = chartService;
        }
        public void AddToChart(int id)
        {
            throw new NotImplementedException();
        }
        public T_SavedChart LoadChart(int chartId)
        {
            throw new NotImplementedException();
        }
        public bool SaveChart()
        {
            throw new NotImplementedException();
        }
        public string GetMarketState()
        {
            _chartService.Ready();
            return "Hello";
        }
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}