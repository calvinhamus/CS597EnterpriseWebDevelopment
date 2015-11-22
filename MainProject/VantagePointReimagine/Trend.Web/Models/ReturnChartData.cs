using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class ReturnChartData
    {
        public List<decimal> Values = new List<decimal>();//{ get; set; }
        public List<string> Labels = new List<string>();
        public string DateTime { get; set; }

    }
}