using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivilegeMS.BLL;
using PrivilegeMS.IBLL;
using PrivilegeMS.Model;
using Newtonsoft.Json;

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
        /// <summary>
        /// 可以按照 【用户--角色--权限】这条路线找出登录用户的权限，放在集合中
        /// 
        /// </summary>
        /// <returns></returns>
       public ActionResult GetMenus()
        {
            int userID = Convert.ToInt32(Session["userID"]);
            var userInfo = userInfoService.LoadEntities(u => u.ID == userID && u.DelFlag == true).FirstOrDefault();
            if (userInfo!=null)
            {
                var userRoleInfo = userInfo.RoleInfo;
                //1.【用户--角色--权限】
                var loginUserMenuActions = (from r in userRoleInfo
                                            from a in r.ActionInfo
                                            where a.ActionType == 1
                                            select a
                                          ).ToList();
                //2.【用户--权限】
                var userActions = from a in userInfo.RUserInfoActionInfo
                                  select a.ActionInfo;
                var userMenuAction = (from a in userActions
                                      where a.ActionType == 1
                                      select a).ToList();

                //3.讲两个集合合并成一个集合
                loginUserMenuActions.AddRange(userMenuAction);

                //4.把禁止的权限从总的集合中清除
                var forbidActions = (from a in userInfo.RUserInfoActionInfo
                                     where a.IsPass == false
                                     select a.ActionInfoID).ToList();

                var loginUserAllowAction = loginUserMenuActions.Where(a => !forbidActions.Contains(a.ID));

                //5.把集合去重
                var MenuAction = loginUserAllowAction.Distinct(new EqualityComparer());

                var lastActionInfo = MenuAction.Select(a => new
                {
                    Name = a.Name,
                    Url = a.Url
                });
                string jsontxt = JsonConvert.SerializeObject(lastActionInfo, Formatting.Indented);
                return Content(jsontxt);
            }
            return Content("no");
        }
       
    }
    /// <summary>
    /// 对 ActionInfo 去重
    /// </summary>
    public class EqualityComparer : IEqualityComparer<ActionInfo>
    {
        public bool Equals(ActionInfo x, ActionInfo y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(ActionInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}