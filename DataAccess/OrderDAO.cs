using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            List<Order> list = new List<Order>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Order GetOrderByID(int id)
        {
            Order o = null;
            try
            {
                using var context = new FStoreContext();
                o = context.Orders.SingleOrDefault(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return o;
        }

        public void AddNew(Order o)
        {
            try
            {
                Order tmp = GetOrderByID(o.OrderId);
                if (tmp == null)
                {
                    using var context = new FStoreContext();
                    context.Orders.Add(o);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order is already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order o)
        {
            try
            {
                Order tmp = GetOrderByID(o.OrderId);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Orders.Update(o);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                Order tmp = GetOrderByID(id);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Orders.Remove(tmp);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
