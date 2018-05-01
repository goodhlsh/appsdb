using Apps.Common;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Apps.WebApi.Core
{
    [SupportFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseApiController : ApiController
    {

        protected virtual HttpRequestBase httpRequestBase
        {
            get {
                var context = (HttpContextBase)Request.Properties[ConfigPara.MS_HttpContext];
                return context.Request;
            }
        }
        public bool ValidateSQL(string sql, ref string msg)
        {
            if (sql.ToLower().IndexOf("delete") > 0)
            {
                msg = "查询参数中含有非法语句DELETE";
                return false;
            }
            if (sql.ToLower().IndexOf("update") > 0)
            {
                msg = "查询参数中含有非法语句UPDATE";
                return false;
            }

            if (sql.ToLower().IndexOf("insert") > 0)
            {
                msg = "查询参数中含有非法语句INSERT";
                return false;
            }

            if (sql.ToLower().IndexOf("drop") > 0)
            {
                msg = "查询参数中含有非法语句drop";
                return false;
            }
            return true;
        }
    }
}