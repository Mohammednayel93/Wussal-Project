

using FinalProject.BLL;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.LoginError = TempData["LoginError"];
            ViewBag.blocked = TempData["Blocked"];
            ViewBag.register = TempData["Register"];
            Category_Bll categorybll = new Category_Bll();
            var result = categorybll.GetAll();
            ViewBag.CategoryList = result;
            Product_Bll productbll = new Product_Bll();
            var productList = productbll.GetTop9();
            return View(productList);
        }
        public PartialViewResult GetProductByCategory(int id)
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllByCategory9(id);
            return PartialView(result);
        }
        public PartialViewResult GetProduct9()
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetTop9();
            return PartialView(result);
        }
        public PartialViewResult Search(string name)
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.SearchProductByName(name);
            return PartialView(result);
        }
        public PartialViewResult Details(int id)
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetById(id);
            return PartialView(result);
        }

        public ActionResult NotificationDetails(int id)
        {
            Notification_Bll bll = new Notification_Bll();
            var result = bll.GetById(id);

            return View(result);
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public PartialViewResult GetForSell()
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllSell();
            return PartialView(result);
        }
        public PartialViewResult GetForFree()
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllFree();
            return PartialView(result);
        }
        public PartialViewResult GetForRent()
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllRent();
            return PartialView(result);
        }
        public PartialViewResult GetForExchange()
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllExchange();
            return PartialView(result);
        }
    }
}