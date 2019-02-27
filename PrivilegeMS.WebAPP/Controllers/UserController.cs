using PrivilegeMS.BLL;
using PrivilegeMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class UserController : Controller
    {
        IBLL.IRUserInfoActionInfoService rUserInfoActionInfoService = new BLL.RUserInfoActionInfoService();
        IBLL.IActionInfoService actionInfoService = new BLL.ActionInfoService();
        IBLL.IUserInfoService userInfoService = new UserInfoService();
        IBLL.IRoleInfoService roleInfoService = new RoleInfoService();
        //解决Json序列化的时候，带有索引属性的对象产生双向引用的BUG
        JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None
        };

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
            var user = userInfoService.LoadEntities(u => u.DelFlag == true).Select(u => new
            {
                ID = u.ID,
                Name = u.Name,
                SubTime = u.SubTime,
                AccountNum = u.AccountNum,
                Pwd = u.Pwd,
                ModifiedTime = u.ModifiedTime
            });
            var ret = JsonConvert.SerializeObject(user, Formatting.Indented);
            return Content(ret);
        }
        // POST 添加用户
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
        //获取已有角色身份
        public ActionResult GetUsetRoleList(int id)
        {
            var userinfo = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (userinfo!=null)
            {
                var userRoleIdList = (from a in userinfo.RoleInfo select a.ID).ToList();
                var roleinfo = roleInfoService.LoadEntities(r => r.DelFlag == true&& userRoleIdList.Contains(r.ID)).Select(r => new { ID = r.ID, Name = r.Name, SubTime = r.SubTime, Remark = r.Remark,Sort=r.Sort });
                string jsonTxt = JsonConvert.SerializeObject(roleinfo, Newtonsoft.Json.Formatting.Indented);
                return Content(jsonTxt);
            }
            return Content("no");
        }
        //获取未有角色身份
        public ActionResult GetUsetOutsideRoleList(int id)
        {
            var userinfo = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (userinfo != null)
            {
                var userRoleIdList = (from a in userinfo.RoleInfo select a.ID).ToList();
                var roleinfo = roleInfoService.LoadEntities(r => r.DelFlag == true&& !userRoleIdList.Contains(r.ID)).Select(r => new { ID = r.ID, Name = r.Name, SubTime = r.SubTime, Remark = r.Remark, Sort = r.Sort });
                string jsonTxt = JsonConvert.SerializeObject(roleinfo, Newtonsoft.Json.Formatting.Indented);
                return Content(jsonTxt);
            }
            return Content("no");
        }
        //修改用户角色身份
        public ActionResult EditUserRole(string idList,int id)
        {
            string[] IdListS = idList.Substring(1, idList.Length - 2).Split(',');
            if (IdListS[0]!="")
            {
                List<int> IdList = new List<int>();
                foreach (var item in IdListS)
                {
                    IdList.Add(Convert.ToInt32(item));
                }
                if (userInfoService.SetUserRoleInfo(id, IdList))
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }

        //展示用户未启用/仅有的权限
        public ActionResult GetUserWithoutAction(int ID)
        {
            var userInfo = userInfoService.LoadEntities(u => u.ID == ID && u.DelFlag == true).FirstOrDefault();
            if (userInfo!=null)
            {
                var userInfoActionInfoList = (from a in userInfo.RUserInfoActionInfo
                                              select a.ActionInfoID).ToList();
                if (userInfoActionInfoList!=null)
                {
                    var actionInfoList = actionInfoService.LoadEntities(a => a.DelFlag == true && !userInfoActionInfoList.Contains(a.ID)).Select(a => new
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Url = a.Url,
                        HttpMethod = a.HttpMethod,
                        SubTime = a.SubTime,
                        Remark = a.Remark,
                        ActionType = a.ActionType,
                        ModifiedTime = a.ModifiedTime,
                        Sort = a.Sort
                    });
                    if (actionInfoList != null)
                    {
                        string jsonTxt = JsonConvert.SerializeObject(actionInfoList, Formatting.Indented);
                        return Content(jsonTxt);
                    }
                }
            }
            return Content("no");
        }
        //展示用户已有权限
        public ActionResult UserAction(int id)
        {
            var userInfo = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (userInfo!=null)
            {
                var userInfoActionInfoList = (from a in userInfo.RUserInfoActionInfo
                                      where a.IsPass == true
                                      select a.ActionInfoID).ToList();
                if (userInfoActionInfoList != null)
                {
                    var actionInfoList = actionInfoService.LoadEntities(a => a.DelFlag == true&&userInfoActionInfoList.Contains(a.ID)).Select(a => new
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Url = a.Url,
                        HttpMethod = a.HttpMethod,
                        SubTime = a.SubTime,
                        Remark = a.Remark,
                        ActionType = a.ActionType,
                        ModifiedTime = a.ModifiedTime,
                        Sort = a.Sort
                    });
                    if (actionInfoList!=null)
                    {
                        string jsonTxt = JsonConvert.SerializeObject(actionInfoList, Formatting.Indented);
                        return Content(jsonTxt);
                    }

                }
            }
            return Content("no");
        }
        //展示用户已禁用权限
        public ActionResult UserPorhibitAction(int id)
        {
            var userInfo = userInfoService.LoadEntities(u => u.ID == id && u.DelFlag == true).FirstOrDefault();
            if (userInfo != null)
            {
                var userInfoActionInfoList = (from a in userInfo.RUserInfoActionInfo
                                              where a.IsPass == false
                                              select a.ActionInfoID).ToList();
                if (userInfoActionInfoList != null)
                {
                    var actionInfoList = actionInfoService.LoadEntities(a => a.DelFlag == true && userInfoActionInfoList.Contains(a.ID)).Select(a => new
                    {
                        ID = a.ID,
                        Name = a.Name,
                        Url = a.Url,
                        HttpMethod = a.HttpMethod,
                        SubTime = a.SubTime,
                        Remark = a.Remark,
                        ActionType = a.ActionType,
                        ModifiedTime = a.ModifiedTime,
                        Sort = a.Sort
                    });
                    if (actionInfoList != null)
                    {
                        string jsonTxt = JsonConvert.SerializeObject(actionInfoList, Formatting.Indented);
                        return Content(jsonTxt);
                    }

                }
            }
            return Content("no");
        }
        //为用户添加/禁用权限
        public ActionResult EditUserAction(int userID,int actionID,bool isPass)
        {
            if (userInfoService.SetUserActionInfo(actionID,userID, isPass))
            {
                if (isPass)
                {
                    return Content("启用成功");
                }
                else
                {
                    return Content("禁用成功");
                }
            }
            else
            {
                return Content("修改失败");
            }
        }
        
        //为用户清理权限
        public ActionResult DelUserAction(int userID,int actionID)
        {
            var rUserInfoActionInfo = rUserInfoActionInfoService.LoadEntities(r => r.UserInfoID == userID && r.ActionInfoID == actionID).FirstOrDefault();
            if (rUserInfoActionInfo!=null)
            {
                if (rUserInfoActionInfoService.DeleteEntity(rUserInfoActionInfo))
                {
                    return Content("删除成功");
                }
                else
                {
                    return Content("删除失败");
                }
            }
            return Content("数据不存在");
        }
    }
}