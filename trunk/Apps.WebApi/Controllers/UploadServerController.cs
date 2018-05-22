using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps.WebApi.Controllers
{
    [HandleError]
    public class UploadServerController : Controller
    {
        // GET: UploadServer
        public JsonResult Index()
        {
            result<string> ret = new result<string>();
            List<string> fileName = new List<string>();
            fileName.Add(" Count : " + Request.Files.Count);
            ret.code = "404";
            if (Request.Files.Count > 0)
            {
                string path = Server.MapPath("~/files/");
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        string saveName = System.IO.Path.GetRandomFileName() + System.IO.Path.GetExtension(Request.Files[i].FileName);
                        Request.Files[i].SaveAs(path + saveName);
                        fileName.Add(path + saveName);
                    }
                    ret.code = "200";
                }
                catch
                {
                    ret.code = "500";
                }
            }
            ret.data = fileName;
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
    }

    public class result<T>
    {
        public string code { get; set; }
        public string des
        {
            get
            {
                return "200 : 上传成功 , 404 : 上传文件未找到 , 500 : 上传文件失败";
            }
        }
        public List<T> data { get; set; }
    }
}