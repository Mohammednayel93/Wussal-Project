using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Areas.Admin.Common;
using FinalProject.BLL;

namespace FinalProject.Areas.Admin.Controllers
{
    public class ContactUsController : Controller
    {
       
        public ActionResult Index()
        {
           
            return View();
        }
        public JsonResult LoadData()
        {
            Contact_Bll bll = new Contact_Bll();
            var result = bll.GetAll().Select(n => new
            {
                n.Id,
                n.User.Email,
                n.Subject,
               //Message= n.Message.Substring(0, 15)+"..."
               Message=n.Message

            });
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            Contact_Bll bll = new Contact_Bll();
            bll.Delete(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Details(int id)
        {
            Contact_Bll bll = new Contact_Bll();
            var result = bll.GetById(id);
            return PartialView(result);
        }
        public PartialViewResult Reply(int id)
        {
            Contact_Bll bll = new Contact_Bll();
            var result = bll.GetById(id);
            ViewBag.UserObject = result.User;
            return PartialView( );
        }
        
    }
}