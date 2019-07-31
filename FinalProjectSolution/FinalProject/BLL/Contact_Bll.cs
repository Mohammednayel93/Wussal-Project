using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Contact_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Contact> GetAll()
        {
            return db.Contacts.ToList();
        }
        public Contact GetById(int id)
        {
            return db.Contacts.Find(id);
        }
        public void Add(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }
        public void Edit(Contact contact)
        {
            Contact _contact = db.Contacts.Find(contact.Id);
            _contact.Subject = contact.Subject;
            _contact.Message = contact.Message;
            _contact.User_Id = contact.User_Id;


            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Contact _contact = db.Contacts.Find(id);
            db.Contacts.Remove(_contact);
            db.SaveChanges();
        }
    }
}