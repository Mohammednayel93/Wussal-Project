using FinalProject.BLL;
using System.Linq;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadData()
        {

            User_Bll emp_bll = new User_Bll();

            var data = emp_bll.GetAll()
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Gender = s.Gender,
                    City = s.City



                })
                .OrderByDescending(x => x.Id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}