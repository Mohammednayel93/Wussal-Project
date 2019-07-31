using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Notification_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Notification> GetAll()
        {
            return db.Notifications.ToList();
        }
        public List<Notification> GetAllRelatedNotificationByComment(int user_id, int comment_id)
        {
            return db.Notifications.Where(m => m.CurrentUser == user_id && m.Comment_Id == comment_id).ToList();
        }
        public Notification GetRelatedNotificationByLike(int user_id, int like_id)
        {
            return db.Notifications.Where(m => m.CurrentUser == user_id && m.Like_Id == like_id).FirstOrDefault();
        }

        public List<Notification> GetTop10(int userid)
        {
            return db.Notifications.Take(10).Where(m => m.User_Id == userid).ToList();
        }
        public int GetNotReadCount(int user_id)
        {
            return db.Notifications.Where(m => m.IsRead == false && m.User_Id == user_id).ToList().Count;
        }
        public void Read(int id)
        {
            Notification _noti = db.Notifications.Find(id);
            _noti.IsRead = true;
            db.SaveChanges();
        }
        public List<Notification> GetNotRead(int user_id)
        {
            return db.Notifications.Where(m => m.User_Id == user_id).ToList();
        }
        public Notification GetById(int id)
        {
            return db.Notifications.Find(id);
        }
        public void Add(Notification notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Notification _noti = db.Notifications.Find(id);
            db.Notifications.Remove(_noti);
            db.SaveChanges();
        }
        public void DeleteNotificationByOrder(int id)
        {
            List<Notification> _noti = db.Notifications.Where(m => m.Fk_Order == id).ToList();
            foreach (var item in _noti)
            {
                db.Notifications.Remove(item);
                db.SaveChanges();
            }

        }
        public void DeleteNotificationBycomment(int id)
        {
            List<Notification> _noti = db.Notifications.Where(m => m.Comment_Id == id).ToList();
            foreach (var item in _noti)
            {
                db.Notifications.Remove(item);
                db.SaveChanges();
            }

        }

    }
}