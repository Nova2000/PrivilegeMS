using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrivilegeMS.IBLL;
using PrivilegeMS.BLL;
using Newtonsoft.Json;
namespace PrivilegeMS.WebAPP.Controllers
{
    public class RoleController : Controller
    {
        IBLL.IRoleInfoService roleInfoService = new BLL.RoleInfoService();
        IBLL.IActionInfoService ActionInfoService = new BLL.ActionInfoService();
        JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None
        };
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
        //GET 获取角色列表
        [HttpGet]
        public ActionResult RoleList()
        {
            var role = roleInfoService.LoadEntities(r => r.DelFlag == true);
            //JsonSerializerSettings setting = new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //    Formatting = Formatting.None
            //};
            var ret = JsonConvert.SerializeObject(role, setting);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddRole(string name,string remark,int sort)
        {
            Model.RoleInfo roleInfo = new Model.RoleInfo();
            roleInfo.Remark = remark;
            roleInfo.Sort = sort;
            roleInfo.Name = name;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = true;
            var role = roleInfoService.AddEntity(roleInfo);
            if (role.ID!=0&&role!=null)
            {
                return Content("ok");
            }
            return Content("no");
        }
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            var roleinfo = roleInfoService.LoadEntities(r => r.ID == id && r.DelFlag == true).FirstOrDefault();
            if (roleinfo!=null)
            {
                roleinfo.DelFlag = false;
                var flag = roleInfoService.EditEntity(roleinfo);
                if (flag)
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }
        [HttpPost]
        public ActionResult EditRole(string name,string remark,int id,int sort)
        {
            var roleInfo = roleInfoService.LoadEntities(r => r.ID == id && r.DelFlag == true).FirstOrDefault();
            if (roleInfo!=null)
            {
                roleInfo.Name = name;
                roleInfo.Remark = remark;
                roleInfo.Sort = sort;
                roleInfo.ModifiedTime = DateTime.Now;
                var flag = roleInfoService.EditEntity(roleInfo);
                if (flag)
                {
                    return Content("ok");
                }
            }
            return Content("no");
        }
        //获取角色未分配权限
        public ActionResult NoActioninfo(int id)
        {
            //int id = int.Parse(Request["id"]);
            var roleinfo = roleInfoService.LoadEntities(r => r.ID == id && r.DelFlag == true).FirstOrDefault();
            if (roleinfo != null)
            {
                var roleInfoAction = (from a in roleinfo.ActionInfo
                                      select a.ID).ToList();
                var actioninfo = ActionInfoService.LoadEntities(a => a.DelFlag == true && !roleInfoAction.Contains(a.ID));
                var ret = JsonConvert.SerializeObject(actioninfo, setting);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            return Content("no");
           
        }
        //获取角色已有权限
        public ActionResult Actioninfo(int id)
        {
            //int id = int.Parse(Request["id"]);
            var roleinfo = roleInfoService.LoadEntities(r => r.ID == id && r.DelFlag == true).FirstOrDefault();
            if (roleinfo != null)
            {
                var roleInfoAction = (from a in roleinfo.ActionInfo
                                      select a.ID).ToList();
                var actioninfo = ActionInfoService.LoadEntities(a => a.DelFlag == true && roleInfoAction.Contains(a.ID));
                var ret = JsonConvert.SerializeObject(actioninfo, setting);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            return Content("no");

        }
        //设置角色权限
        public ActionResult SetRoleAction(int id,string idList)
        {
            string[] IdListS = idList.Substring(1, idList.Length - 2).Split(',');
            List<int> IdList = new List<int>();
            foreach (var item in IdListS)
            {
                IdList.Add(Convert.ToInt32(item));
            }
            if (roleInfoService.SetRoleActionInfo(id,IdList))
            {
                return Content("ok");
            }
            return Content("no");
        }
    }
}