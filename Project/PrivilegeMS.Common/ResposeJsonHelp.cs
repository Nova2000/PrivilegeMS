using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrivilegeMS.Common
{
    public static class JsonHelper
    {
        /// <summary>
        ///统一返回数据格式
        /// </summary>
        /// <param name="Status">status</param>
        /// <param name="Data">data</param>
        /// <param name="Msg">msg</param>
        /// <returns></returns>
        public static string ResposeJson(int Status,string Data,string Msg)
        {
            string s = JsonConvert.SerializeObject(new
            {
                status = Status,
                data = Data,
                msg = Msg

            });
            return s;
        }
    }
}
