using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class ValueAndLabel
    {
        public List<List<decimal>> Values { get; set; }
        public List<string> Labels { get; set; }
    }
}