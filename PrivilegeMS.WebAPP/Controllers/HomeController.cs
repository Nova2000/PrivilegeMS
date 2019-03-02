using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivilegeMS.BLL;
using PrivilegeMS.IBLL;
using PrivilegeMS.Model;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService userInfoService = new UserInfoService();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
       
       
    }
}