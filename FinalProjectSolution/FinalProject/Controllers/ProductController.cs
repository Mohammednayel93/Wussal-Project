using FinalProject.BLL;
using FinalProject.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductDetails(int id)
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetById(id);
            int category_id = result.Category_Id;
            var ctegoryList = bll.GetAllByCategory3(category_id);
            ViewBag.Category = ctegoryList;
            return View(result);


        }
        public PartialViewResult ReloadComments(int id)
        {
            Comment_Bll bll = new Comment_Bll();
            var result = bll.GetAllByProduct(id);
            return PartialView(result);
        }
        public void AddComment(string txtcomment, int product_Id)
        {
            Comment_Bll bll = new Comment_Bll();
            Comment comment = new Comment();
            comment.Date = DateTime.Now;
            comment.Product_Id = product_Id;
            int UserId = (Session["CurrentUser"] as User).Id;
            comment.User_Id = UserId;
            comment.Comment1 = txtcomment;
            bll.Add(comment);
            // Add Notification

            var Lastcomment = bll.GetLastCommentAdded();
            if (Lastcomment.User_Id != UserId)
            {
                Notification notification = new Notification();
                Notification_Bll notification_Bll = new Notification_Bll();

                Order_Bll bllOrder = new Order_Bll();
                notification.User_Id = bllOrder.GetUserID(Lastcomment.Product_Id);
                notification.CurrentUser = (Session["CurrentUser"] as User).Id;

                notification.Comment_Id = Lastcomment.Id;
                notification.IsRead = false;
                notification_Bll.Add(notification);

            }

        }

        public JsonResult UnLike(int product_id)
        {
            int user_id = (Session["CurrentUser"] as User).Id;
            Like_Bll like_bll = new Like_Bll();

            var getLike = like_bll.GetLikeByProduct(product_id, user_id);
            //Delete Related Notifications
            Notification notification = new Notification();
            Notification_Bll notification_Bll = new Notification_Bll();
            var notifications = notification_Bll.GetRelatedNotificationByLike(user_id, getLike.Id);

            notification_Bll.Delete(notifications.Id);

            // then Delete Like

            like_bll.Delete(getLike.Id);
            return Json(new { done = 1 }, JsonRequestBehavior.AllowGet);

        }
        public void DeleteComment(int id)
        {
            int user_id = (Session["CurrentUser"] as User).Id;
            //Delete Related Notifications
            Notification notification = new Notification();
            Notification_Bll notification_Bll = new Notification_Bll();
            var notifications = notification_Bll.GetAllRelatedNotificationByComment(user_id, id);
            foreach (var item in notifications)
            {
                notification_Bll.Delete(item.Id);
            }
            // then Delete Comment
            Comment_Bll bll = new Comment_Bll();
            bll.Delete(id);


        }
        public JsonResult ApplyOrder(int product_id)
        {
            Order_Bll bll = new Order_Bll();
            Order order = new Order();
            order.Date = DateTime.Now;
            order.Product_Id = product_id;
            order.User_Id = (Session["CurrentUser"] as User).Id;
            bll.Add(order);

            var lastOrder = bll.GetLastOrderAdded();
            Notification notification = new Notification();
            Notification_Bll noti_bll = new Notification_Bll();
            //notification.User_Id = bll.GetUserID(lastOrder.Product_Id);
            notification.User_Id = bll.GetUserID(lastOrder.Product_Id);
            notification.CurrentUser = (Session["CurrentUser"] as User).Id;

            notification.Fk_Order = lastOrder.Id;
            notification.IsRead = false;
            noti_bll.Add(notification);
            return Json(new { done = 1 }, JsonRequestBehavior.AllowGet);

        }
        public void Read(int id)
        {
            Notification_Bll notification_Bll = new Notification_Bll();
            notification_Bll.Read(id);

        }
        public JsonResult Like(int product_id)
        {
            Like_Bll bll = new Like_Bll();
            Like like = new Like();

            like.Product_Id = product_id;
            like.User_Id = (Session["CurrentUser"] as User).Id;
            bll.Add(like);

            var lastLike = bll.GetLastLikeAdded();
            Notification notification = new Notification();
            Notification_Bll noti_bll = new Notification_Bll();
            //notification.User_Id = bll.GetUserID(lastOrder.Product_Id);
            notification.User_Id = bll.GetUserID(lastLike.Product_Id);
            notification.CurrentUser = (Session["CurrentUser"] as User).Id;

            notification.Like_Id = lastLike.Id;
            notification.IsRead = false;
            noti_bll.Add(notification);
            return Json(new { done = 1 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult AddProduct()
        {
            ViewBag.Price = TempData["Price"];
            ViewBag.Type = TempData["type"];
            ViewBag.Added = TempData["Added"];
            Category_Bll category_Bll = new Category_Bll();
            var result = category_Bll.GetAll();
            SelectList listItems = new SelectList(result, "Id", "Name");
            ViewBag.CategoryList = listItems;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase ImageUploaded)
        {
            if (ImageUploaded != null)
            {
                product.Image = "~/imgs/" + ImageUploaded.FileName;
                ImageUploaded.SaveAs(Path.Combine(Server.MapPath("~/imgs/"), ImageUploaded.FileName));
            }
            else
            {
                product.Image = "~/imgs/deafultimage.png";
            }
            if (product.IsSell == true && product.Price == null || product.Price == 0)
            {
                TempData["Price"] = "Price Is Required while Choosing Type Sell";
                return RedirectToAction("AddProduct");
            }
            else if (product.IsRent == true && product.Price == null || product.Price == 0)
            {
                TempData["Price"] = "Price Is Required while Choosing Type Rent";
                return RedirectToAction("AddProduct");

            }
            if (product.IsSell == null && product.IsFree == null && product.IsRent == null && product.IsExchange == null)
            {
                TempData["type"] = "Deal Type Is Required";
                return RedirectToAction("AddProduct");

            }
            product.User_Id = (Session["CurrentUser"] as User).Id;
            Product_Bll bll = new Product_Bll();
            bll.Add(product);
            TempData["Added"] = "Product Added Successfully Wait Admin Response ";
            return RedirectToAction("AddProduct");


        }
        public ActionResult GetProductByCategory(int id)
        {
            Product_Bll bll = new Product_Bll();
            var result = bll.GetAllByCategory(id);
            return View(result);
        }
        public ActionResult Delete(int id)
        {
            //Delete Related Notification
            Order_Bll order_Bll = new Order_Bll();
            Notification_Bll notification_bll = new Notification_Bll();

            var Orders = order_Bll.GetAllByProduct(id);
            foreach (var item in Orders)
            {
                notification_bll.DeleteNotificationByOrder(item.Id);
            }
            //Delete Order 
            order_Bll.DeleteOrderByProduct(id);
            //Delete  Related Comment

            Comment_Bll comment_Bll = new Comment_Bll();
            var Comments = comment_Bll.GetAllByProduct(id);
            foreach (var item in Comments)
            {
                notification_bll.DeleteNotificationBycomment(item.Id);
            }
            // delete Comment
            comment_Bll.DeleteCommentByProduct(id);

            //Delete Likes
            Like_Bll like_Bll = new Like_Bll();
            var likes = like_Bll.GetLikeByProducts(id);
            foreach (var item in likes)
            {
                notification_bll.Delete(item.Id);
            }

            //Delete Like
            like_Bll.Delete(id);
            Product_Bll product_Bll = new Product_Bll();
            product_Bll.Delete(id);
            return RedirectToAction("Profile", "User");





        }
        public ActionResult DeleteOrderMyBag(int id)
        {
            Notification_Bll notification_bll = new Notification_Bll();
            notification_bll.DeleteNotificationByOrder(id);
            Order_Bll order_Bll = new Order_Bll();
            order_Bll.Delete(id);
            return RedirectToAction("Profile", "User");

        }
    }
}