using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Trend.Core.Data;
using Trend.Web.Models;
using Microsoft.AspNet.Identity;
using Trend.Core.Helpers;

namespace Trend.Web.SignalRChart
{
    public class ChartService 
    {
        private TrendData db = new TrendData();

        private readonly static Lazy<ChartService> _instance = new Lazy<ChartService>(
            () => new ChartService(GlobalHost.ConnectionManager.GetHubContext<ChartHub>().Clients));

        private readonly object _marketStateLock = new object();
        private readonly object _updateStockPricesLock = new object();

        private readonly ConcurrentDictionary<int, T_DataValue> _stocks = new ConcurrentDictionary<int, T_DataValue>();

        // Stock can go up or down by a percentage of this factor on each change
        private readonly double _rangePercent = 0.002;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(500);
        private readonly Random _updateOrNotRandom = new Random();

        private Timer _timer;
        private volatile bool _updatingStockPrices;

        private ChartService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
           // _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);
            // LoadDefaultStocks();
        }

        public static ChartService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }


        internal ReturnPointData GetPoint(int dataPointId)
        {
            var point = db.T_DataPoint.Where(x => x.Id.Equals(dataPointId)).FirstOrDefault();
            var pointAndStrokeColor = RandomColorGenerator.GetRandomColor();
            return new ReturnPointData
            {
                StrokeColor = pointAndStrokeColor,
                PointColor = pointAndStrokeColor,
                Label = point.Name,
                PointHighlightFill = "#fff",
                PointHighlightStroke = "rgba(151,187,205,1)",
                PointStrokeColor = "#fff",
                FillColor = RandomColorGenerator.GetRandomColor()
            };
        }

        internal void SaveChart(string username)
        {
            //var user = User.Identity.GetUserId();
            var user = db.AspNetUsers.FirstOrDefault(x => x.UserName == username);
            var chart = new T_SavedChart
            {
                T_UserId = user.Id,
                Created = DateTime.Now,
                Name = ""


            };

            throw new NotImplementedException();
        }


      
        public void Stop()
        {
            _updatingStockPrices = false;
            _timer?.Dispose();
        }
     
    }
}