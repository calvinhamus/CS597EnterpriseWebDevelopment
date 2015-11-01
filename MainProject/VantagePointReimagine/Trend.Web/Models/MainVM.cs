using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trend.Core.Data;

namespace Trend.Web.Models
{
    public class MainVM
    {
        public List<T_Plc> Plcs { get; set; }
        public List<T_SavedChart> Charts { get; set; }
    }
}