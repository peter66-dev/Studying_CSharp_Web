using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int id, int proId) => OrderDetailDAO.Instance.Remove(id, proId);

        public OrderDetail GetOrderDetailByID(int id, int proID) => OrderDetailDAO.Instance.GetOrderDetailByID(id, proID);

        public List<OrderDetail> GetOrderDetailByMemberID(int memID) => OrderDetailDAO.Instance.GetOrderDetailByMemberID(memID);

        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetails();

        public void InsertOrderDetail(OrderDetail o) => OrderDetailDAO.Instance.AddNew(o);

        public void UpdateOrderDetail(OrderDetail o) => OrderDetailDAO.Instance.Update(o);
    }
}
