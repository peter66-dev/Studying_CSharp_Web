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

        public decimal? GetTotalMoney(List<Order> list)
        {
            decimal? total = 0;
            try
            {
                var context = new FStoreContext();
                List<OrderDetail> tmp = new List<OrderDetail>();
                foreach (var order in list)
                {
                    total += order.Freight;
                    tmp = context.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();
                }
                if (tmp.Count != 0)
                {
                    foreach (var o in tmp)
                    {
                        total += ((decimal)(1 - o.Discount) * o.UnitPrice * o.Quantity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return total;
        }

        public IEnumerable<Order> GetStatistic(int start, int end)
        {
            List<Order> list = new List<Order>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Orders.Where(o => start <= o.OrderDate.Day && o.OrderDate.Day <= end).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<Order> SortAsc()
        {
            List<Order> list = new List<Order>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Orders.OrderBy(o => o.OrderDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public IEnumerable<Order> SortDesc()
        {
            List<Order> list = new List<Order>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Orders.OrderByDescending(o => o.OrderDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public decimal GetTotalByMemberId(int id)
        {
            decimal total = 0;
            try
            {
                var context = new FStoreContext();
                List<Order> oList = context.Orders.Where(o => o.MemberId == id).ToList();
                foreach (var o in oList)
                {
                    foreach (var od in context.OrderDetails)
                    {
                        if (o.OrderId == od.OrderId)
                        {
                            total += od.Quantity * od.UnitPrice * (decimal)(1 - od.Discount);
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return total;
        }

    }
}
