using FinalProject.BLL;
using FinalProject.Models;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.email = TempData["Email"];
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase ImageUploaded)
        {
            if (ImageUploaded != null)
            {

                user.Image = "/imgs/" + ImageUploaded.FileName;
                ImageUploaded.SaveAs(Path.Combine(Server.MapPath("/imgs/"), ImageUploaded.FileName));
            }
            else
            {
                user.Image = "~/imgs/facebook.jpg";


            }
            user.Role_Id = 1;
            user.Status = true;
            user.ConfirmPassword = user.Password;
            User_Bll bll = new User_Bll();
            if (bll.CheckEmail(user.Email))
            {
                TempData["Email"] = "Email Id Already  Exsists ";
                return RedirectToAction("Register", user);
            }
            else
            {
                bll.Add(user);
                TempData["Register"] = "Registeration Successfully";
                var currentuser = bll.GetLastUserAdded();
                Session["CurrentUser"] = currentuser;
                return RedirectToAction("Index", "Home");
            }


        }
        [HttpGet]
        public PartialViewResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Login(User user_Info)
        {


            User_Bll securityUser_BLL = new User_Bll();

            var current = securityUser_BLL.Login(user_Info.Email, user_Info.Password);
            if (current != null)
            {

                if (current.Role_Id == 1)
                {
                    if (current.Status == false)
                    {
                        TempData["Blocked"] = "You are Blocked";
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        //Success
                        Session["CurrentUser"] = current;
                        TempData["Blocked"] = string.Empty;
                        return Redirect("/");
                    }

                }
                else
                {
                    Session["CurrentUser"] = current;
                    return Redirect("/Admin/Admin/Index");
                }

            }
            else
            {
                TempData["LoginError"] = "Wrong Password or Email";
                return Redirect("~/Home/Index");
            }

        }
        public JsonResult CheckEmail(string email)
        {
            User_Bll bll = new User_Bll();
            var resut = bll.CheckEmail(email);
            return Json(resut, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            TempData["Register"] = null;
            return Redirect("/");
        }
        public new ActionResult Profile()
        {
            if (Session["CurrentUser"] != null)
            {
                var result = Session["CurrentUser"] as User;
                ViewBag.Updated = TempData["Updated"];
                //Make  List Of My Bag لستة بالاللي انا هطلبة 
                Order_Bll bll = new Order_Bll();
                var MyBag = bll.GetAllMyBag(result.Id);
                ViewBag.MyBag = MyBag;
                var MyOrders = bll.GetAllMyOrders(result.Id);
                ViewBag.MyOrders = MyOrders;
                Product_Bll product_Bll = new Product_Bll();
                var MyProducts = product_Bll.GetAllProductByUserId(result.Id);
                ViewBag.MyProducts = MyProducts;
                return View(result);

            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult EditProfile()
        {
            var result = Session["CurrentUser"] as User;
            return PartialView(result);
        }
        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        public ActionResult Edit(User user, HttpPostedFileBase ImageUploaded)
        {
            User_Bll bll = new User_Bll();

            if (ImageUploaded != null)
            {
                string fileName = ImageUploaded.FileName;
                ImageUploaded.SaveAs(Server.MapPath("~/imgs/" + fileName));
                user.Image = "~/imgs/" + fileName;
            }
            else
            {
                var result = Session["CurrentUser"] as User;
                //var userold = bll.GetById(user.Id);
                user.Image = result.Image;
            }
            user.ConfirmPassword = user.Password;
            bll.Edit(user);
            Session["CurrentUser"] = user;
            TempData["Updated"] = "Updated Successfully";
            return RedirectToAction("Profile");
        }



        public JsonResult Change(string newPassword, string OldPassword)
        {
            User_Bll bll = new User_Bll();

            var currentUser = Session["CurrentUser"] as User;
            if (OldPassword == currentUser.Password)
            {
                currentUser.Password = newPassword;
                currentUser.ConfirmPassword = newPassword;
                bll.EditPassword(currentUser);
                return Json(new { done = 0 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { done = 1 }, JsonRequestBehavior.AllowGet);

            }

        }
        public ActionResult Accepted(int id)
        {
            Order_Bll bll = new Order_Bll();
            bll.Accept(id);
            return RedirectToAction("Profile");
        }
        public ActionResult Rejected(int id)
        {
            Order_Bll bll = new Order_Bll();
            bll.Reject(id);
            return RedirectToAction("Profile");

        }
        public PartialViewResult OpenModalDone(int id)
        {
            User_Bll user_Bll = new User_Bll();
            var user = user_Bll.GetById(id);

            return PartialView(user);
        }
        public JsonResult AddFeedback(string feedback, int id)
        {
            User_Bll user_Bll = new User_Bll();

            User_Rate user_Rate = new User_Rate();
            user_Rate.User_Id = (Session["CurrentUser"] as User).Id;
            user_Rate.ProductUser_Id = id;
            user_Rate.Rate = feedback;
            user_Bll.AddFeedback(user_Rate);
            return Json(new { done = 1 }, JsonRequestBehavior.AllowGet);
        }
    }
}