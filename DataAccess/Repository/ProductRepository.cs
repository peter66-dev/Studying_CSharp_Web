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

        public List<Product> GetProductByName(string name) => ProductDAO.Instance.GetProductByName(name);

        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public void InsertProduct(Product pro) => ProductDAO.Instance.AddNew(pro);

        public IEnumerable<Product> Sortasc() => ProductDAO.Instance.Sortasc();

        public IEnumerable<Product> Sortdesc() => ProductDAO.Instance.Sortdesc();

        public void UpdateProduct(Product pro) => ProductDAO.Instance.Update(pro);
        public List<Product> GetProductByPrice(int option) => ProductDAO.Instance.GetProductByPrice(option);
    }
}
