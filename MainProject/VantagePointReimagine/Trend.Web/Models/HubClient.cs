using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trend.Web.Models
{
    public class HubClient
    {
        public string UserId { get; set; }
        public List<int> ChatIds { get; set; }
    }
}