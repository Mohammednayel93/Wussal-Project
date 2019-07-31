using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.BLL;
using FinalProject.Models;


namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult contactUs()
        {
            return View();
        }
		public JsonResult AddMessage(Contact contact) {
			try {
				contact.User_Id = (Session["CurrentUser"] as User).Id;
				Contact_Bll cnt = new Contact_Bll();
				cnt.Add(contact);
				
				return Json(new { add = true }, JsonRequestBehavior.AllowGet);
			} catch (Exception) {
				return Json(new { add = false }, JsonRequestBehavior.AllowGet);
			}

		}
    }
}