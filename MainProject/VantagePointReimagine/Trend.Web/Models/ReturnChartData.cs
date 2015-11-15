using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class ReturnChartData
    {

        public string DataPoint { get; set; }
        public string PlcName { get; set; }
        public decimal Value { get; set; }

        public string DateTime { get; set; }

       


    }
}