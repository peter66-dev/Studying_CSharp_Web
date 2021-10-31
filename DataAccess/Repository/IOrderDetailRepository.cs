using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailByID(int id, int proId);
        void InsertOrderDetail(OrderDetail o);
        void DeleteOrderDetail(int id, int proId);
        void UpdateOrderDetail(OrderDetail o);
    }
}
