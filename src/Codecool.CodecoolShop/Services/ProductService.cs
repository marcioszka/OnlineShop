using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao supplierDao;
        private readonly CartDaoMemory cartDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao, CartDaoMemory cartDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
            this.cartDao = cartDao;
        }

        public Cart GetCart() => cartDao.cart;

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public Supplier GetProductForSupplier(int supplierId)
        {
            return this.supplierDao.Get(supplierId);
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return this.productCategoryDao.GetAll();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return this.supplierDao.GetAll();
        }
    }
}
