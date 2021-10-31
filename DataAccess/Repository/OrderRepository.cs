using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int id) => OrderDAO.Instance.Remove(id);

        public Order GetOrderByID(int id) => OrderDAO.Instance.GetOrderByID(id);

        public IEnumerable<Order> GetOrders() => OrderDAO.Instance.GetOrders();

        public void InsertOrder(Order o) => OrderDAO.Instance.AddNew(o);

        public void UpdateOrder(Order o) => OrderDAO.Instance.Update(o);
    }
}
