using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Comment_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Comment> GetAll()
        {
            return db.Comments.ToList();
        }
        public List<Comment> GetAllByProduct(int id)
        {
            return db.Comments.Where(m => m.Product_Id == id).ToList();
        }

        public Comment GetById(int id)
        {
            return db.Comments.Find(id);
        }
        public Comment GetLastCommentAdded()
        {
            var currentUser = (from s in db.Comments
                               orderby s.Id descending
                               select s).FirstOrDefault();
            return currentUser;
        }
        public void Add(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }
        public void Edit(Comment comment)
        {
            Comment _comment = db.Comments.Find(comment.Id);
            _comment.Comment1 = comment.Comment1;
            _comment.Product_Id = comment.Product_Id;
            _comment.User_Id = comment.User_Id;
            _comment.Date = comment.Date;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Comment _comment = db.Comments.Find(id);
            db.Comments.Remove(_comment);
            db.SaveChanges();
        }
        public void DeleteCommentByProduct(int id)
        {
            List<Comment> Commment = db.Comments.Where(m => m.Product_Id == id).ToList();
            foreach (var item in Commment)
            {
                db.Comments.Remove(item);
                db.SaveChanges();
            }

        }
    }
}