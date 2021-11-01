using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int id);
        void InsertProduct(Product pro);
        void DeleteProduct(int id);
        void UpdateProduct(Product pro);
        public IEnumerable<Product> Sortasc();
        public IEnumerable<Product> Sortdesc();
        public List<Product> GetProductByName(string name);
        public List<Product> GetProductByPrice(int option);
    }
}
