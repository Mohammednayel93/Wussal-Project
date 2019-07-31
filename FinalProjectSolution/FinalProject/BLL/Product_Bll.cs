using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Product_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Product> GetAll()
        {
            return db.Products.Where(m => m.Status == true).ToList();
        }
        public List<Product> GetTop9()
        {
            return db.Products.Take(9).Where(m => m.Status == true).ToList();
        }
        public List<Product> GetPanding()
        {
            return db.Products.Where(m => m.Status == null).ToList();
        }
        public List<Product> GetAllProductByUserId(int id)
        {
            return db.Products.Where(m => m.User_Id == id).ToList();
        }
        public List<Product> GetAllByCategory9(int id)
        {
            return db.Products.Where(m => m.Category_Id == id).Take(9).Where(m => m.Status == true).ToList();
        }
        public List<Product> GetAllByCategory3(int id)
        {
            return db.Products.Where(m => m.Category_Id == id).Where(m => m.Status == true).Take(3).ToList();
        }
        public List<Product> GetAllByCategory(int id)
        {
            return db.Products.Where(m => m.Category_Id == id).Where(m => m.Status == true).ToList();
        }

        public List<Product> GetAllFree()
        {
            return db.Products.Where(m => m.IsFree == true).Where(m => m.Status == true).Take(9).ToList();
        }
        public List<Product> SearchProductByName(string name)
        {
            if (name != null || name != "")
            {
                return db.Products.Where(m => m.Name.Contains(name)).Where(m => m.Status == true).ToList();

            }
            else
            {
                return db.Products.Take(9).Where(m => m.Status == true).ToList();

            }
        }
        public List<Product> GetAllRent()
        {
            return db.Products.Where(m => m.IsRent == true).Where(m => m.Status == true).Take(9).ToList();
        }
        public List<Product> GetAllExchange()
        {
            return db.Products.Where(m => m.IsExchange == true).Where(m => m.Status == true).Take(9).ToList();

        }
        public List<Product> GetAllSell()
        {
            return db.Products.Where(m => m.IsSell == true).Where(m => m.Status == true).Take(9).ToList();
        }
        public Product GetById(int id)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            return db.Products.Find(id);
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void Edit(Product product)
        {
            Product _product = db.Products.Find(product.Id);
            _product.Name = product.Name;
            _product.Image = product.Image;
            _product.Category_Id = product.Category_Id;
            _product.Date = product.Date;
            _product.Description = product.Description;
            _product.IsExchange = product.IsExchange;
            _product.IsFree = product.IsFree;
            _product.IsRent = product.IsRent;
            _product.IsSell = product.IsSell;
            _product.Price = product.Price;
            _product.Status = product.Status;
            _product.User_Id = product.User_Id;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Product _product = db.Products.Find(id);
            db.Products.Remove(_product);
            db.SaveChanges();
        }
        public void Approve(int id)
        {
            Product _product = db.Products.Find(id);
            _product.Status = true;
            db.SaveChanges();
        }
        public void Block(int id)
        {
            Product _product = db.Products.Find(id);
            _product.Status = false;
            db.SaveChanges();
        }
    }
}