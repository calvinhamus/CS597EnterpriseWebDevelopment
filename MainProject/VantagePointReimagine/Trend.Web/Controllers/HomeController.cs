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
            
            var y = (from t in db.T_DataValue
                     where t.DateTime <= data.EndDate
                     && t.DateTime >= data.StartDate
                     select new ValueAndLabel
                     {
                         Value = t.Value,
                         Label = t.DateTime.ToString()

                     });
            var points = db.T_DataValue.Where(x => x.DateTime <= data.EndDate && x.DateTime >= data.StartDate).Where(y=> data.DataPointIds.Contains(y.T_DataPoint)).OrderBy(y =>y.T_DataPoint).ToList();
           
            return Json(points.ToList());
        }

    }
}