using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class ReturnPointData
    {
        //public string DataPointName { get; set; }
        //public string PlcName { get; set; }
        //public string HexColor { get; set; }

        public string Label { get; set; }
        public string FillColor { get; set; }
        public string StrokeColor { get; set; }
        public string PointColor { get; set; }
        public string PointStrokeColor { get; set; }
        public string PointHighlightFill { get; set; }
        public string PointHighlightStroke { get; set; }
    }
}