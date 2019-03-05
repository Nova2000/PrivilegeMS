using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class ActionController : BaseController
    {
        IBLL.IActionInfoService actionInfoService = new BLL.ActionInfoService();
        // GET: Action
        public ActionResult Index()
        {
            return View();
        }
        //获取所有权限列表
        [HttpGet]
        public ActionResult GetAllActionList()
        {
            var ActionInfoList = actionInfoService.LoadEntities(a => a.DelFlag == true).Select(a => new
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
            if (ActionInfoList!=null)
            {
                string jsonTxt = JsonConvert.SerializeObject(ActionInfoList, Newtonsoft.Json.Formatting.Indented);
                return Content(jsonTxt);
            }
            return Content("no");
        }
    }
}