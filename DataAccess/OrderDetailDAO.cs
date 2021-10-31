using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.OrderDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public OrderDetail GetOrderDetailByID(int id, int proId)
        {
            OrderDetail o = null;
            try
            {
                using var context = new FStoreContext();
                o = context.OrderDetails.SingleOrDefault(o => o.OrderId == id && o.ProductId == proId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return o;
        }

        public void AddNew(OrderDetail o)
        {
            try
            {
                OrderDetail tmp = GetOrderDetailByID(o.OrderId, o.ProductId);
                if (tmp == null)
                {
                    using var context = new FStoreContext();
                    context.OrderDetails.Add(o);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This orderid is already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(OrderDetail o)
        {
            try
            {
                OrderDetail tmp = GetOrderDetailByID(o.OrderId, o.ProductId);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.OrderDetails.Update(o);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This orderid does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int id, int proID)
        {
            try
            {
                OrderDetail tmp = GetOrderDetailByID(id, proID);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.OrderDetails.Remove(tmp);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This orderid does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
