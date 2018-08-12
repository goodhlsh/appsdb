using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_ActivesModel
    {
        public override string Id { get; set; }
        [Display(Name = "标题")]
        public override string Title { get; set; }
        [Display(Name = "内容")]
        public override string Cont { get; set; }
        [Display(Name = "适用对象")]
        public override string Fors { get; set; }
        [Display(Name = "开始日期")]
        public override Nullable<System.DateTime> BeginDate { get; set; }
        [Display(Name = "结束日期")]
        public override Nullable<System.DateTime> EndDate { get; set; }
        [Display(Name = "是否显示")]
        public override Nullable<bool> IsShow { get; set; }
        [Display(Name = "创建日期")]
        public override Nullable<System.DateTime> CreateTime { get; set; }
        [Display(Name = "更新日期")]
        public override Nullable<System.DateTime> UpdateTime { get; set; }
        [Display(Name = "活动类型")]
        public override string ActType { get; set; }
        [Display(Name = "是否置顶")]
        public override Nullable<bool> ToTop { get; set; }
        [Display(Name = "是否推荐")]
        public override Nullable<bool> Promoted { get; set; }
        [Display(Name = "图片")]
        public override string PicShow { get; set; }
        [Display(Name = "缩略图")]
        public override string Thumbnail { get; set; }
        [Display(Name = "显示顺序")]
        public override Nullable<int> ShunXu { get; set; }
    }

    public class Spl_ActType
    {
        public string ActType { get; set; }
        public string ActID { get; set; }
    }
}
