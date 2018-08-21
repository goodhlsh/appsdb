using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
   public partial interface ISysWalletBLL
    {
        /// <summary>
        /// 获取某人当前余额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        SysWalletModel GetWallByUserID(string userID);
        /// <summary>
        /// 获取某人余额账单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<SysWalletModel> GetAllWallByUserID(string userID);
        /// <summary>
        ///  获取所有人当前余额
        /// </summary>
        /// <returns></returns>
        List<P_Sys_GetUserWallet_Result> GetUserWallet();
       
    }
}
