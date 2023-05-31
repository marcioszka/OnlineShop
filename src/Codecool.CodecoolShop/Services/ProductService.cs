using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        //private readonly IProductDao productDao;
        //private readonly IProductCategoryDao productCategoryDao;
        private readonly IProductDao _productDaoDb;
        private readonly IProductCategoryDao _productCategoryDaoDb;
        private readonly ISupplierDao _supplierDaoDb;

        public ProductService(IProductDao productDaoDb, IProductCategoryDao productCategoryDaoDb, ISupplierDao supplierDaoDb)
        {
            this._productDaoDb = productDaoDb;
            this._productCategoryDaoDb = productCategoryDaoDb;
            this._supplierDaoDb = supplierDaoDb;
        }
        //public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao)
        //{
        //    this.productDao = productDao;
        //    this.productCategoryDao = productCategoryDao;
        //}

        public ProductCategory GetProductCategory(int categoryId) => this._productCategoryDaoDb.Get(categoryId);

        public IEnumerable<ProductCategory> GetProductCategories() => this._productCategoryDaoDb.GetAll();

        //public ProductCategory GetProductCategory(int categoryId)
        //{
        //    return this.productCategoryDao.Get(categoryId);
        //}

        //public IEnumerable<Product> GetProductsForCategory(int categoryId)
        //{
        //    ProductCategory category = this.productCategoryDao.Get(categoryId);
        //    return this.productDao.GetBy(category);
        //}

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            var category = this._productCategoryDaoDb.Get(categoryId);
            return this._productDaoDb.GetBy(category);
        }

        public IEnumerable<Product> GetAllProducts() => this._productDaoDb.GetAll();

        public Supplier GetProductSupplier(int supplierId) => this._supplierDaoDb.Get(supplierId);

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            var supplier = this._supplierDaoDb.Get(supplierId);
            return this._productDaoDb.GetBy(supplier);
        }

        public IEnumerable<Supplier> GetSuppliers() => this._supplierDaoDb.GetAll();

    }
}
