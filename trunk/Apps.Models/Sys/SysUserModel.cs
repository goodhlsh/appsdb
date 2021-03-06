﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace Apps.Models.Sys
{

    public class SysUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string TrueName { get; set; }        
        public string MobileNumber { get; set; }
        public string Token { get; set; }
        public string Card { get; set; }
        public string shfzh { get; set; }
        public bool State { get; set; }
        public Nullable<bool> IsAuth { get; set; }
        public string TId { get; set; }
        public string TName { get; set; }
        public string PId { get; set; }
        public string PName { get; set; }
        public string Jibie { get; set; }
        public int? TuiCount { get; set; }        
        public Nullable<decimal> FrozenMoney { get; set; }

    }
    public class SysUserChangePwd
    {
        public string UserId { get; set; }        
        public string NewPassword { get; set; }

    }
    public class SysUserLogingData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public partial class SysUserModel
    {
        [Display(Name = "ID")]        
        public override string Id { get; set; }
        [NotNullExpression]
        [Display(Name = "用户名")]
        public override string UserName { get; set; }
        [NotNullExpression]
        [StringLength(50,MinimumLength=5)]
        [System.Web.Mvc.Compare("ComparePassword", ErrorMessage = "两次密码不一致")]
        [Display(Name = "密码")]
        public override string Password { get; set; }
        [System.Web.Mvc.Compare("Password", ErrorMessage = "两次密码不一致")]
        [Display(Name = "确认密码")]
        public string ComparePassword { get; set; }
        [NotNullExpression]
        [Display(Name = "姓名")]
        public override string TrueName { get; set; }
        [Display(Name = "身份证")]
        public override string Card { get; set; }
        [Display(Name = "手机号码")]
        [NotNullExpression]
        public override string MobileNumber { get; set; }
        [Display(Name = "电话号码")]
        public override string PhoneNumber { get; set; }
        [Display(Name = "QQ")]
        public override string QQ { get; set; }
        [Display(Name = "Email")]
        public override string EmailAddress { get; set; }
        [Display(Name = "其他联系方式")]
        public override string OtherContact { get; set; }
        [Display(Name = "省份")]
        public override string Province { get; set; }
        [Display(Name = "城市")]
        public override string City { get; set; }
        [Display(Name = "地区")]
        public override string Village { get; set; }
        [Display(Name = "详细地址")]
        public override string Address { get; set; }
        [Display(Name = "状态")]
        public override bool State { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "创建时间")]
        public override DateTime? CreateTime { get; set; }
        [Display(Name = "创建人")]
        public override string CreatePerson { get; set; }
        [Display(Name = "性别")]
        public override string Sex { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]  
        public override DateTime? Birthday { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "加入时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public override DateTime? JoinDate { get; set; }
        [Display(Name = "婚姻状况")]
        public override string Marital { get; set; }
        [Display(Name = "党派")]
        public override string Political { get; set; }
        [Display(Name = "籍贯")]
        public override string Nationality { get; set; }
        [Display(Name = "所在地")]
        public override string Native { get; set; }
        [Display(Name = "学校")]
        public override string School { get; set; }
        [Display(Name = "专业")]
        public override string Professional { get; set; }
        [Display(Name = "学历")]
        public override string Degree { get; set; }
        [NotNullExpression]        
        [Display(Name = "部门")]
        public override string DepId { get; set; }
        public  string DepName { get; set; }
        [NotNullExpression]
        [Display(Name = "职位")]
        public override string PosId { get; set; }
        [Display(Name = "备注")]
        public override string Expertise { get; set; }
        [Display(Name = "是否在职")]
        public override bool? JobState { get; set; }
        [Display(Name = "照片")]
        public override string Photo { get; set; }
        [Display(Name = "附件")]
        public override string Attach { get; set; }
        [Display(Name = "角色")]
        public  string RoleName { get; set; }//拥有的用户

        public  string Flag { get; set; }//用户分配角色
        public  string PosName { get; set; }


        [MaxWordsExpression(50)]
        [Display(Name = "上级领导")]
        public override string Lead { get; set; }
        public override string LeadName { get; set; }
        [Display(Name = "是否可以自选领导")]
        public override Nullable<bool> IsSelLead { get; set; }

        [Display(Name = "工作日程汇报是否启用  启用后 他的上司领导将可以看到他的 工作日程汇报.")]
        public override Nullable<bool> IsReportCalendar { get; set; }

        [Display(Name = "开启 小秘书")]
        public override Nullable<bool> IsSecretary { get; set; }
        [Display(Name = "家庭电话")]
        public override string HomePhone { get; set; }
        [Display(Name = "微信账号")]
        public override string WXID { get; set; }
        [Display(Name = "个性签名")]
        public override string Signature { get; set; }
        [Display(Name = "二维码")]
        public override string QRCode { get; set; }
        [Display(Name = "身份证正面")]
        public override string IdentityCardFile { get; set; }
        [Display(Name = "身份证背面")]
        public override string IdentityCardBackFile { get; set; }
        [Display(Name = "是否认证")]
        public override Nullable<bool> IsAuth { get; set; }
        [Display(Name = "审核状态")]
        public override string AuditStatus { get; set; }
        [Display(Name = "备注")]
        public override string Note { get; set; }
        public override string SortCode { get; set; }
        public override string RecommendID { get; set; }
        [Display(Name = "推荐人")]
        public override string Recommendor { get => base.Recommendor; set => base.Recommendor = value; }
        public override Nullable<System.DateTime> RecommendTime { get; set; }
        public override string EditorID { get; set; }
        public override Nullable<System.DateTime> UpdateTime { get; set; }
        public override Nullable<bool> IsDeleted { get; set; }
        [Display(Name = "密保：提示问题")]
        public override string Questions { get; set; }
        [Display(Name = "密保：问题答案")]
        public override string Answer { get; set; }
        public override string LastLoginIP { get; set; }
        [Display(Name = "上次登录时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public override Nullable<System.DateTime> LastLoginTime { get; set; }
        public override string CurrentLoginIP { get; set; }
        [Display(Name = "登录时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public override Nullable<System.DateTime> CurrentLoginTime { get; set; }
        [Display(Name = "上次密码修改时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public override Nullable<System.DateTime> LastPasswdTime { get; set; }
        public SysJiaPu SysJiaPu { get; set; } 

    }
    //实名认证用户
    public class SysAuthUserModel
    {
        [Display(Name = "ID")]
        public string Id { get; set; }
        [Display(Name = "真实姓名")]
        public string TrueName { get; set; }
        [Display(Name = "身份证")]
        public string Card { get; set; }
        [Display(Name = "手机号码")]
        public string MobileNumber { get; set; }
    }
    public class SysUserEditModel
    {
        [Display(Name = "ID")]
        public  string Id { get; set; }

        [Display(Name = "用户名")]
        public  string UserName { get; set; }
     
        [Display(Name = "密码")]
        public  string Password { get; set; }
     
        [Display(Name = "确认密码")]
        public  string ComparePassword { get; set; }
        [NotNullExpression]
        [Display(Name = "真实姓名")]
        public  string TrueName { get; set; }
        [Display(Name = "身份证")]
        public  string Card { get; set; }
        [Display(Name = "手机号码")]
        public  string MobileNumber { get; set; }
        [Display(Name = "电话号码")]
        public  string PhoneNumber { get; set; }
        [Display(Name = "QQ")]
        public  string QQ { get; set; }
        [Display(Name = "Email")]
        public  string EmailAddress { get; set; }
        [Display(Name = "其他联系方式")]
        public  string OtherContact { get; set; }
        [Display(Name = "省份")]
        public  string Province { get; set; }

        [Display(Name = "城市")]
        public  string City { get; set; }

        [Display(Name = "地区")]
        public  string Village { get; set; }
        
        [Display(Name = "详细地址")]
        public  string Address { get; set; }
        [Display(Name = "状态")]
        public  bool State { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "创建时间")]
        public  DateTime? CreateTime { get; set; }
        [Display(Name = "创建人")]
        public  string CreatePerson { get; set; }
        [Display(Name = "性别")]
        public  string Sex { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string Birthday { get; set; }
        [DateExpression]//如果填写判断是否是日期
        [Display(Name = "加入日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public  string JoinDate { get; set; }
        [Display(Name = "婚姻状况")]
        public  string Marital { get; set; }
        [Display(Name = "党派")]
        public  string Political { get; set; }
        [Display(Name = "籍贯")]
        public  string Nationality { get; set; }
        [Display(Name = "所在地")]
        public  string Native { get; set; }
        [Display(Name = "学校")]
        public  string School { get; set; }
        [Display(Name = "专业")]
        public  string Professional { get; set; }
        [Display(Name = "学历")]
        public  string Degree { get; set; }
        [NotNullExpression]
        [Display(Name = "部门")]
        public  string DepId { get; set; }
        [NotNullExpression]
        [Display(Name = "职位")]
        public  string PosId { get; set; }
        [Display(Name = "Expertise")]
        public  string Expertise { get; set; }
        [Display(Name = "是否在职")]
        public  bool JobState { get; set; }
        [Display(Name = "头像")]
        public  string Photo { get; set; }
        [Display(Name = "附件")]
        public  string Attach { get; set; }
        [Display(Name = "角色")]
        public  string RoleName { get; set; }//拥有的用户
        public  string CityName { get; set; }
        public  string ProvinceName { get; set; }
        public  string VillageName { get; set; }
        public  string DepName { get; set; }
        public  string PosName { get; set; }

        
        [MaxWordsExpression(50)]
        [Display(Name = "上级领导")]
        public  string Lead { get; set; }
        public  string LeadName { get; set; }
        [Display(Name = "是否可以自选领导")]
        public  bool IsSelLead { get; set; }

        [Display(Name = "工作日程汇报是否启用  启用后 他的上司领导将可以看到他的 工作日程汇报.")]
        public  bool IsReportCalendar { get; set; }

        [Display(Name = "开启小秘书")]
        public  bool IsSecretary { get; set; }
        [Display(Name = "家庭电话")]
        public  string HomePhone { get; set; }
        [Display(Name = "微信账号")]
        public  string WXID { get; set; }
        [Display(Name = "个性签名")]
        public  string Signature { get; set; }
        [Display(Name = "二维码")]
        public  string QRCode { get; set; }
        [Display(Name = "身份证正面")]
        public  string IdentityCardFile { get; set; }
        [Display(Name = "身份证背面")]
        public  string IdentityCardBackFile { get; set; }
        [Display(Name = "是否认证")]
        public bool IsAuth { get; set; }
        [Display(Name = "审核状态")]
        public  string AuditStatus { get; set; }
        [Display(Name = "备注")]
        public  string Note { get; set; }
        public  string SortCode { get; set; }
        public  string RecommendID { get; set; }
        [Display(Name = "推荐人")]
        public string Recommendor { get; set; }
        public  DateTime? RecommendTime { get; set; }
        public  string EditorID { get; set; }
        public  DateTime? UpdateTime { get; set; }
        public  bool IsDeleted { get; set; }
        [Display(Name = "密保：提示问题")]
        public  string Questions { get; set; }
        [Display(Name = "密保：问题答案")]
        public  string Answer { get; set; }
        public  string LastLoginIP { get; set; }
        [Display(Name = "上次登录时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public  string LastLoginTime { get; set; }
        public  string CurrentLoginIP { get; set; }
        [Display(Name = "登录时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public  string CurrentLoginTime { get; set; }
        [Display(Name = "上次密码修改时间")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public  string LastPasswdTime { get; set; }
        public int? TuiCount { get; set; }
    }

    public class UserGroupModel
    {
        public string ContextId { get; set; }
    }

    public class SysOnlineUserModel
    {
        //ContextId
        public string ContextId { get; set; }
        //Online Status
        public int Status { get; set; }
        //用户ID
        public string UserId { get; set; }
        //用户名字
        public string TrueName { get; set; }
        //用户头像
        public string Photo { get; set; }
        //电话号码
        public string PhoneNumber { get; set; }
        //邮件地址
        public string Email { get; set; }
        //组织架构ID
        public string StructId { get; set; }
        //组织架构名字
        public string StructName { get; set; }
        //组织架构排序
        public int? Sort { get; set; }
        //职位名字
        public string PosName { get; set; }
    }
    public class SysUserForJiaPu
    {
        public string Id { get; set; }
        public string uid { get; set;}
        public string tid { get; set; }
        public string truename { get; set; }
        public string username { get; set; }
        public decimal fje { get; set; }
        public string isdone { get; set; }
    }
    public class SysAllChildUser
    {
        public string userId { get; set; }
        public string parentId { get; set; }
        public string trueName { get; set; }
        public string levelName { get; set; }
        public int levelMan { get; set; }
        public int levelMax { get; set; }
    }
    public class SysSons
    {
        public string zuname { get; set; }
        public string leftTrueName { get; set; }        
        public string rightTrueName { get; set; }
    }
    public class SysUserForTui
    {
        public string uid { get; set; }
        public string tid { get; set; }
    }
}
