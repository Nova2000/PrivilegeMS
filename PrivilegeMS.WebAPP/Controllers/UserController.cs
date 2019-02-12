using PrivilegeMS.BLL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class UserController : Controller
    {
        IBLL.IUserInfoService userInfoService = new UserInfoService();
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //GET 获取用户列表
        [HttpGet]
        public ActionResult UserList()
        {
            var user = userInfoService.LoadEntities(u => u.DelFlag == true);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// POST 添加用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accountNum"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUser(string name, string accountNum, string pwd)
        {
            Model.UserInfo userInfo = new UserInfo()
            {
                Name = name,
                AccountNum = accountNum,
                Pwd = pwd,
                SubTime = DateTime.Now,
                DelFlag = true
            };
            var user = userInfoService.AddEntity(userInfo);
            if (user.ID != 0 && user != null)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        // POST 删除用户
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {

            var user = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (user != null)
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
        //修改用户
        [HttpPost]
        public ActionResult EditUser(int id, string name, string pwd)
        {
            var user = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (user != null)
            {
                user.Name = name;
                user.Pwd = pwd;
                user.ModifiedTime = DateTime.Now;
                var editflag = userInfoService.EditEntity(user);
                if (editflag)
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }
    }
}