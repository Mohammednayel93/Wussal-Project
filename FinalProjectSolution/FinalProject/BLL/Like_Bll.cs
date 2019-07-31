using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Like_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Like> GetAll()
        {
            return db.Likes.ToList();
        }
        public Like GetById(int id)
        {
            return db.Likes.Find(id);
        }
        public void Add(Like like)
        {
            db.Likes.Add(like);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Like like = db.Likes.Find(id);
            if (like != null)
            {
                db.Likes.Remove(like);
                db.SaveChanges();
            }

        }
        public Like GetLikeByProduct(int product, int user)
        {
            var result = db.Likes.Where(m => m.Product_Id == product && m.User_Id == user).FirstOrDefault();
            return result;
        }
        public List<Like> GetLikeByProducts(int product)
        {
            var result = db.Likes.Where(m => m.Product_Id == product).ToList();
            return result;
        }
        public Like GetLastLikeAdded()
        {
            var currentUser = (from s in db.Likes
                               orderby s.Id descending
                               select s).FirstOrDefault();
            return currentUser;
        }
        public int GetUserID(int id)
        {
            Product product = db.Products.Where(m => m.Id == id).FirstOrDefault();

            return product.User_Id;
        }
    }
}