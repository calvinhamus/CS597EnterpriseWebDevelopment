using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trend.Web.Controllers;

namespace Trend.Web.Areas.Admin.Controllers
{
    [RequireHttps]
    [Authorize(Roles ="Admin")]
    public class BaseAdminController : BaseAuthenticatedController
    {
        // GET: Admin/BaseAdmin
       
    }
}