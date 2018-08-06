using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public partial interface ISysJiaPuBLL
    {
        SysJiaPuRModel CreateTree(SysJiaPuRModel node, string id);
        SysJiaPuModel GetRefSysJiaPu(string userID);
       
        //家谱
        int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE);

        List<SysAllChildUser> GetAllChildren(string uid);
        List<string> GetRealSon(string userID);
        char[,] GetZmp15(string userID);
        List<SysSons> GetAllSon(string queryStr, int skip, int limit);
    }
}
