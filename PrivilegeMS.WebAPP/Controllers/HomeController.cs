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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            IBLL.IUserInfoService userInfoService = new UserInfoService();
            var user = userInfoService.LoadEntities(u => u.DelFlag == true);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}