using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class LargeDataReturn
    {
        public ReturnPointData PointData { get; set; }
        public List<string> Labels { get; set; }
        public List<int> Data { get; set; }
    }
}