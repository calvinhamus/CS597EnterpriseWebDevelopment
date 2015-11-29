using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trend.Core.Data;
using Trend.Web.Models;
using Microsoft.AspNet.Identity;

namespace Trend.Web.Controllers
{
    public class HomeController : BaseAuthenticatedController
    {
        private TrendData db = new TrendData();

        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var vm = new MainVM();
            var plcs = db.T_UserPlc.Where(y => y.T_UserId.Equals(user)).ToList();
            var charts = db.T_SavedChart.Where(y => y.T_UserId.Equals(user)).ToList();

            vm.Plcs = plcs.Select(x =>x.T_Plc).ToList();
            vm.Charts = charts;
            return View("Index",vm);
        }

       
        public ActionResult StockTicker()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetChartData(GetDataModel data)
        {
            var valueAndLabel = new ValueAndLabel();
            valueAndLabel.Labels = new List<string>();
            var temp = new List<List<T_DataValue>>();
            foreach (var point in data.DataPointIds )
            {
                var values = (from t in db.T_DataValue
                              where t.DateTime <= data.EndDate
                              && t.DateTime >= data.StartDate
                              && t.T_DataPoint == point
                              select t).ToList();
                temp.Add(values);
            }

            var list = new List<List<decimal>>();
            for (var x = 0; x < temp.Count; x++)
            {
                
                for (var y = 0; y < temp[x].Count; y++)
                {
                    list.Add(new List<decimal>());
                    list[y].Add(temp[x][y].Value);
                    valueAndLabel.Labels.Add(temp[x][y].DateTime.ToString());
                }
            }
            list.RemoveAll(x => x.Count == 0);
            valueAndLabel.Labels.RemoveRange(list.Count, valueAndLabel.Labels.Count - list.Count);
          
            valueAndLabel.Values = list;
            return Json(valueAndLabel);


        }
    }
}