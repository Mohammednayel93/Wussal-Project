using FinalProject.Areas.Admin.Common;
using FinalProject.BLL;
using FinalProject.Models;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["CurrentUser"] != null)
            {
                if ((Session["CurrentUser"] as User).Role_Id == 2)
                {
                    User_Bll bll = new User_Bll();
                    var result = bll.GetAll();

                    return View(result);
                }
                else
                {
                    return Redirect("/Home/Index");
                }

            }
            else
            {
                return Redirect("/Home/Index");


            }

        }
        public ActionResult Message(int id)
        {
            User_Bll bll = new User_Bll();

            var result = bll.GetById(id);
            ViewBag.UserObject = result;
            return PartialView();
        }
        public ActionResult Accept(int id)
        {
            User_Bll bll = new User_Bll();
            bll.Accept(id);
            return RedirectToAction("Index");
        }
        public ActionResult Block(int id)
        {
            User_Bll bll = new User_Bll();
            bll.Block(id);
            return RedirectToAction("Index");
        }

        public void SendEmail(string email, string subject, string message, string username)
        {
            GMailer.GmailUsername = "Mohammednayel93@gmail.com";
            GMailer.GmailPassword = "888889741";

            GMailer mailer = new GMailer();
            mailer.ToEmail = email;
            mailer.Subject = subject;

            mailer.Body = " From : " + email + "\n Name : " + username + "\n Subject : " + subject + "\n Message : " + message;
            mailer.IsHtml = true;
            mailer.SendEmail();
        }
    }
}