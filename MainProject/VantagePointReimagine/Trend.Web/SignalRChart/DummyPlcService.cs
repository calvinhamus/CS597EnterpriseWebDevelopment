﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trend.Core.Data;
using Trend.Core.Helpers;
using Trend.Web.Models;

namespace Trend.Web.SignalRChart
{
    public interface IDummyPlcService
    {
        void StartDummyPlc(List<object> PlcIds);
        void StopDummyPlc(int PlcId);

    }

    public class DummyPlcService : IDummyPlcService
    {
        private readonly static Lazy<DummyPlcService> _instance = new Lazy<DummyPlcService>(
          () => new DummyPlcService(GlobalHost.ConnectionManager.GetHubContext<ChartHub>().Clients));
        //private readonly object _marketStateLock = new object();
        private readonly object _updateDataPointLock = new object();
        private volatile bool _updatingDataPoint;
        // private readonly ConcurrentDictionary<int, T_DataValue> _dataPoints = new ConcurrentDictionary<int, T_DataValue>();
        private List<T_DataPoint> _dataPoints = new List<T_DataPoint>();
        private List<ReturnChartData> _returnDataPoints = new List<ReturnChartData>();
        private TrendData db = new TrendData();
        private Timer t;

        public DummyPlcService()
        {
            //Clients = clients;

        }

        public DummyPlcService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            
        }
        public static DummyPlcService Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public void StartDummyPlc(int dataPointId)
        {
            _dataPoints.Clear();
            _returnDataPoints.Clear();
            //TODO fix this

            StartTimer();
        }
        public void StartDummyPlc(List<object> dataPointIds)
        {
            _dataPoints.Clear();
            _returnDataPoints.Clear();
            _dataPoints = FindDataPoint(dataPointIds);

            StartTimer();

        }

        private List<T_DataPoint> FindDataPoint(List<object> dataPointIds)
        {
            var temp = new List<int>();
            dataPointIds.ForEach(point => temp.Add((Convert.ToInt32(point))));
            //var points = db.T_DataValue.Where(x => x.DateTime <= data.EndDate && x.DateTime >= data.StartDate).Where(y => data.DataPointIds.Contains(y.T_DataPoint));
            var points =  db.T_DataPoint.Where(x => temp.Contains(x.Id)).ToList();
            //points.ForEach(point => _returnDataPoints.Add(new ReturnChartData
            //{
            //    DataPointId = point.Id,
            //    DateTime = DateTime.Now.ToString(),
            //    Value = 0,
            //    DataPoint = point.Name,
            //    PlcName = point.T_Plc.Name,
            //}));

           
            return points;

        }

        public void StopDummyPlc(int PlcId)
        {
            t.Change(Timeout.Infinite, Timeout.Infinite);
            t.Dispose();          
        }

        private void Callback(object state)
        {
            lock (_updateDataPointLock)
            {
                if (!_updatingDataPoint)
                {
                    _updatingDataPoint = true;
                    TryUpdateDataChart(_dataPoints);
                    //for (int i = 0; i < _dataPoints.Count; i++)
                    //{
                    //    TryUpdateDataChart(_dataPoints[i]);
                    //   // _returnDataPoints[i].Value = TryUpdateDataChart(_dataPoints[i]);
                    //}
                    //foreach (var datapoint in _dataPoints)
                    //{
                    //    TryUpdateDataChart(datapoint);

                    //}

                    // Clients.All.updateChart(_returnDataPoints);
                    _updatingDataPoint = false;
                }
            }

           
        }
        private async void TryUpdateDataChart(List<T_DataPoint> _dataPoints)
        {

            var rnd = new Random();
            var values = new ReturnChartData();
            // Randomly choose whether to udpate this stock or not
            foreach ( var point in _dataPoints)
            {
                var pointValue = new T_DataValue();
                pointValue.Value = rnd.Next(1, 100);
                pointValue.DateTime = DateTime.Now;
                pointValue.T_DataPoint = point.Id;
                db.T_DataValue.Add(pointValue);

                values.Values.Add(pointValue.Value);
                values.DateTime = pointValue.DateTime.ToString();
            }
            Clients.All.updateChart(values);
            await db.SaveChangesAsync();
            
           
        }

        private async Task<decimal> TryUpdateDataChart(T_DataPoint point)
        {
            // Randomly choose whether to udpate this stock or not

            var rnd = new Random();
            var pointValue = new T_DataValue();

            pointValue.Value = rnd.Next(1, 100);
            pointValue.DateTime = DateTime.Now;
            pointValue.T_DataPoint = point.Id;
            db.T_DataValue.Add(pointValue);
            await db.SaveChangesAsync();

            //var value = new ReturnChartData
            //{
            //    DateTime = pointValue.DateTime.ToString(),
            //    Value = pointValue.Value,
            //    DataPoint = point.Name,
            //    PlcName = point.T_Plc.Name,

            //};
            return pointValue.Value;
            // Clients.All.updateChart(value);
            //return true;
        }
        private void StartTimer()
        {
            t = new Timer(Callback, "Some state", -1, -1);
            // plcId = PlcId;
            t.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
        }
        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }
    }
}
