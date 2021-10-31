using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int id) => ProductDAO.Instance.Remove(id);
        public Product GetProductByID(int id) => ProductDAO.Instance.GetProductByID(id);

        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public void InsertProduct(Product pro) => ProductDAO.Instance.AddNew(pro);

        public void UpdateProduct(Product pro) => ProductDAO.Instance.Update(pro);
    }
}
