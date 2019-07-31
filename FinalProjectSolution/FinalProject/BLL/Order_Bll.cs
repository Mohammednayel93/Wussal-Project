using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.BLL
{
    public class Order_Bll
    {
        FinalProjectDB db = new FinalProjectDB();
        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }
        public List<Order> GetAllMyBag(int user_id)
        {
            return db.Orders.Where(m => m.User_Id == user_id).Where(m => m.Status == null).ToList();
        }
        public List<Order> GetAllByProduct(int product)
        {
            return db.Orders.Where(m => m.Product_Id == product).ToList();
        }
        public List<Order> GetAllMyOrders(int user_id)
        {
            return db.Orders.Where(m => m.User_Id != user_id && m.Product.User_Id == user_id && m.Status == null).ToList();
        }
        public Order GetById(int id)
        {
            return db.Orders.Find(id);
        }
        public void Add(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public Order GetLastOrderAdded()
        {
            var currentUser = (from s in db.Orders
                               orderby s.Id descending
                               select s).FirstOrDefault();
            return currentUser;
        }
        public int GetUserID(int id)
        {
            Product product = db.Products.Where(m => m.Id == id).FirstOrDefault();

            return product.User_Id;
        }
        public void Delete(int id)
        {
            Order _order = db.Orders.Find(id);
            db.Orders.Remove(_order);
            db.SaveChanges();
        }

        public void Accept(int id)
        {
            Order _order = db.Orders.Find(id);
            _order.Status = true;
            db.SaveChanges();
        }
        public void Reject(int id)
        {
            Order _order = db.Orders.Find(id);
            _order.Status = false;
            db.SaveChanges();
        }
        public void DeleteOrderByProduct(int id)
        {
            List<Order> _order = db.Orders.Where(m => m.Product_Id == id).ToList();
            foreach (var item in _order)
            {
                db.Orders.Remove(item);
                db.SaveChanges();
            }

        }
    }
}