using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class ReturnChartData
    {
        public List<decimal> Values = new List<decimal>();//{ get; set; }

        public string DateTime { get; set; }

    }
}