using Apps.Common;
using Apps.Spl.IBLL;
using LShop.Models;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace LShop.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public ISpl_WareBLL m_BLL { get; set; }
        [Dependency]
        public ISpl_BrandBLL mb_BLL { get; set; }
        [Dependency]
        public ISpl_ProductCategorySBLL mpc_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        public ActionResult Index()
        {            
            return View();
        }

        /// <summary>
        /// 商品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult Catalog()
        {
            var model = new CatalogViewModel();

            model.BrandList = mb_BLL.GetListValue("", 0, 200);

            model.ProductTypeList = mpc_BLL.GetListValue("", 0, 200);
            return PartialView(model);
        }

        /// <summary>
        /// 导航
        /// </summary>
        /// <returns></returns>
        public ActionResult Navigation()
        {
            var model = new NavViewModel();

            model.BrandTypeList = mb_BLL.GetListByProduct();
            model.BrandList = mb_BLL.GetListValue("", 0, 200);

            model.ProductTypeList = mpc_BLL.GetListValue("", 0, 200);
            return PartialView(model);
        }

        public ActionResult Detail(string id)
        {
            var pro = m_BLL.GetById(id);//.Products.FirstOrDefault(r => r.Id == id);
            return View(pro);
        }

        public ActionResult GetBrandList(int id)
        {
            return View();
        }

        public ActionResult GetProduct(string bid, string tsid)
        {
            var list = m_BLL.GetProducts(bid, tsid, 0, 200);//.Products.Where(r => (r.BrandID == bid && bid > 0) || (r.TypeID == tid && tid > 0));
            return View(list);
        }
    }
}