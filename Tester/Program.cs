using System;
using DataAccess;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderDetailRepository repo = new OrderDetailRepository();
            foreach (var mem in repo.GetOrderDetails())
            {
                Console.WriteLine($"orderid: {mem.OrderId}");
            }
        }
    }
}
