using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class User_Bll
    {

        FinalProjectDB db = new FinalProjectDB();
        public List<User> GetAll()
        {
            return db.Users.ToList();
        }
        public User GetById(int id)
        {
            return db.Users.Find(id);
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public User GetLastUserAdded()
        {
            var currentUser = (from s in db.Users
                               orderby s.Id descending
                               select s).FirstOrDefault();
            return currentUser;
        }
        public bool CheckEmail(string email)
        {
            var result = db.Users.Any(m => m.Email == email);
            return result;
        }
        public void Edit(User user)
        {
            User _user = db.Users.Find(user.Id);
            _user.Name = user.Name;
            _user.Image = user.Image;
            _user.Address = user.Address;
            _user.City = user.City;
            _user.Email = user.Email;
            _user.Gender = user.Gender;
            _user.Status = user.Status;

            _user.ConfirmPassword = _user.Password;
            db.SaveChanges();
        }
        public void EditPassword(User user)
        {
            User _user = db.Users.Find(user.Id);

            _user.Password = user.Password;
            _user.ConfirmPassword = user.Password;
            db.SaveChanges();
        }
        public void ChangePassword(User user)
        {
            User _user = db.Users.Find(user.Id);
            _user.Password = user.Password;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            User _user = db.Users.Find(id);
            db.Users.Remove(_user);
            db.SaveChanges();
        }
        public void Accept(int id)
        {
            User _user = db.Users.Find(id);
            _user.ConfirmPassword = _user.Password;
            _user.Status = true;
            db.SaveChanges();
        }
        public void Block(int id)
        {
            User _user = db.Users.Find(id);
            _user.ConfirmPassword = _user.Password;
            _user.Status = false;
            db.SaveChanges();
        }
        public User Login(string email, string password)
        {

            var result = db.Users.Any(s => s.Email == email && s.Password == password);
            if (result)
            {
                var current = db.Users.Where(s => s.Email == email && s.Password == password).FirstOrDefault();

                return current;
            }
            else
            {

                return null;
            }

        }
        public void AddFeedback(User_Rate user_Rate)
        {
            db.User_Rate.Add(user_Rate);
            db.SaveChanges();
        }
    }
}