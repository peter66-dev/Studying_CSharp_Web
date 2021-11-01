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
        public decimal? GetTotalMoney(List<Order> list) => OrderDAO.Instance.GetTotalMoney(list);
        public IEnumerable<Order> GetStatistic(int start, int end) => OrderDAO.Instance.GetStatistic(start, end);
        public IEnumerable<Order> SortAsc() => OrderDAO.Instance.SortAsc();
        public IEnumerable<Order> SortDesc() => OrderDAO.Instance.SortDesc();
        public decimal GetTotalByMemberId(int id) => OrderDAO.Instance.GetTotalByMemberId(id);


    }
}
