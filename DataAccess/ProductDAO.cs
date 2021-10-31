using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            try
            {
                using (var context = new FStoreContext())
                {
                    list = context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public Product GetProductByID(int id)
        {
            Product pro = null;
            try
            {
                using var context = new FStoreContext();
                pro = context.Products.SingleOrDefault(pro => pro.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pro;
        }

        public void AddNew(Product pro)
        {
            try
            {
                Product tmp = GetProductByID(pro.ProductId);
                if (tmp == null)
                {
                    using var context = new FStoreContext();
                    context.Products.Add(pro);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product is already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product pro)
        {
            try
            {
                Product tmp = GetProductByID(pro.ProductId);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Products.Update(pro);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                Product tmp = GetProductByID(id);
                if (tmp != null)
                {
                    using var context = new FStoreContext();
                    context.Products.Remove(tmp);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product does not already exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
