using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Category_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return db.Categories.Find(id);
        }
        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }
        public void Edit(Category category)
        {
            Category _category = db.Categories.Find(category.Id);
            _category.Name = category.Name;
            _category.Image = category.Image;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Category _category = db.Categories.Find(id);
            db.Categories.Remove(_category);
            db.SaveChanges();
        }
    }
}