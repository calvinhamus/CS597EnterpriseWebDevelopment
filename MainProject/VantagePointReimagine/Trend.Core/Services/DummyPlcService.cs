using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trend.Core.Data;

namespace Trend.Core.Services
{
    public interface IDummyPlcService
    {
        void StartDummyPlc(int PlcId);
        void StopDummyPlc(int PlcId);

    }

    public class DummyPlcService : IDummyPlcService
    {
        private TrendData db = new TrendData();
        private Timer t;
        private int plcId;


        public DummyPlcService()
        {
            t = new Timer(Callback, "Some state", -1,-1);
        }

        public void StartDummyPlc(int PlcId)
        {
            plcId = PlcId;
            t.Change(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
           // GenerateData(PlcId);
        }
        public void StopDummyPlc(int PlcId)
        {
          //  t.Change(-1, -1);
            t.Dispose();
           // GenerateData(PlcId);
        }

        private void Callback(object state)
        {
            var dataPoints = db.T_DataPoint.Where(x => x.T_PlcId == plcId).ToList();
            var rnd = new Random();
            var rndPoint = new Random();
            
            var r = rndPoint.Next(dataPoints.Count());
            var pointValue = new T_DataValue();

            pointValue.Value = rnd.Next(1, 100);
            pointValue.DateTime = DateTime.Now;
            pointValue.T_DataPoint = dataPoints[r].Id;

            db.T_DataValue.Add(pointValue);
            db.SaveChangesAsync();
        }
    }
}
