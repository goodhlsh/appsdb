using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Apps.WebApi.Controllers
{
    public class UploadServerApiController : ApiController
    {
        [HttpPost]
        public string UploadImg()
        {
            string result = "";
            HttpFileCollection filelist = HttpContext.Current.Request.Files;
            if (filelist != null && filelist.Count > 0)
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    HttpPostedFile file = filelist[i];
                    String Tpath =  DateTime.Now.ToString("yyyy-MM-dd") ;
                    string filename = file.FileName;
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string FilePath = HttpContext.Current.Server.MapPath("~/uploadfile/") + Tpath + "\\";
                    DirectoryInfo di = new DirectoryInfo(FilePath);
                    if (!di.Exists) { di.Create(); }
                    try
                    {
                        file.SaveAs(FilePath + FileName + filename);
                        result = (FilePath + FileName + filename).Replace("\\", "/");
                    }
                    catch (Exception ex)
                    {
                        result = "a";// 上传文件写入失败：" + ex.Message;
                    }
                }
            }
            else
            {
                result = "b";// 上传的文件信息不存在！";
            }

            return result;
        }
    }
}
