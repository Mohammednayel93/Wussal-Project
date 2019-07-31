using FinalProject.BLL;
using System.Linq;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        Product_Bll bll = new Product_Bll();
        // GET: Admin/Product
        public ActionResult Index()
        {

            return View();
        }
        public JsonResult LoadDataGrid()
        {

            var result = bll.GetPanding().OrderByDescending(m => m.Id).Select(z => new
            {
                z.Id,
                z.Name,
                Date = z.Date.ToShortDateString(),
                Category = z.Category.Name,
                z.Price,
                UserName = z.User.Name

            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Approve(int id)
        {

            bll.Approve(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {

            bll.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id)
        {

            var result = bll.GetById(id);
            return PartialView(result);
        }

    }
}