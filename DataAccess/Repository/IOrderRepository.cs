using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderByID(int id);
        void InsertOrder(Order o);
        void DeleteOrder(int id);
        void UpdateOrder(Order o);
        public decimal? GetTotalMoney(List<Order> list);
        public IEnumerable<Order> GetStatistic(int start, int end);
        public IEnumerable<Order> SortAsc();
        public IEnumerable<Order> SortDesc();
        public decimal GetTotalByMemberId(int id);
    }
}
