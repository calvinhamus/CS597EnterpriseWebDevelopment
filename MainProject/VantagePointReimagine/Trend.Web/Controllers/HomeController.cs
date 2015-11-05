﻿using System;
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
           
            vm.Plcs = plcs.Select(x =>x.T_Plc).ToList();
            return View("Index",vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult StockTicker()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetChartData(GetDataModel data)
        {
            var points = db.T_DataValue.Where(x => x.DateTime <= data.EndDate && x.DateTime >= data.StartDate).Where(y=> data.DataPointIds.Contains(y.T_DataPoint));
            return Json(points);
        }

    }
}