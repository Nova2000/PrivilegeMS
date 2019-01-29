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
        IBLL.IUserInfoService userInfoService = new UserInfoService();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserList()
        {
            var user = userInfoService.LoadEntities(u => u.DelFlag == true);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddUser(string name,string accountNum,string pwd)
        {
            Model.UserInfo userInfo = new UserInfo()
            {
                Name = name,
                AccountNum = accountNum,
                Pwd = pwd,
                SubTime = DateTime.Now,
                DelFlag=true
            };
            var user = userInfoService.AddEntity(userInfo);
            if (user.ID!=0&&user!=null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public ActionResult DeleteUser(int id)
        {
            
            var user = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (user!=null)
            {
                user.DelFlag = false;
                var delflag = userInfoService.EditEntity(user);
                if (delflag)
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }
    }
}