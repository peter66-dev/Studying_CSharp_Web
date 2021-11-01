using DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        //SqlConnection connection;
        //SqlCommand command;
        //static string connectionString = "Server=(local);uid=sa;pwd=sa;database=FStore";
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

        public List<OrderDetail> GetOrderDetailByID(int id)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                using var context = new FStoreContext();
                list = context.OrderDetails.Where(o => o.OrderId == id).ToList();
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

        //input: memberID 
        //output: orderID(s) -> order details
        
        public List<OrderDetail> GetOrderDetailByMemberID(int memID)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                using var context = new FStoreContext();
                List<Order> orderIDList = new List<Order>();
                orderIDList = context.Orders.Where(o => o.MemberId == memID).ToList();// các billid của member

                foreach (var or in orderIDList)
                {
                    List<OrderDetail> tmp = context.OrderDetails.Where(o => o.OrderId == or.OrderId).ToList();// các billdetailid của member
                    list.AddRange(tmp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        /*
        public List<OrderDetail> GetOrderDetailByMemberID(int memID)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("select orderid from [order] where memberid = @memID",connection);
            command.Parameters.AddWithValue("@memID", memID);
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                connection.Open();
                SqlDataReader rs = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        int orderID = rs.GetInt32("OrderID");
                        list.AddRange(GetOrderDetailByID(orderID));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        */
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
