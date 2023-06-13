using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        //private readonly IProductDao productDao;
        //private readonly IProductCategoryDao productCategoryDao;
        private readonly IProductDao productDaoDB;
        private readonly IProductCategoryDao productCategoryDaoDB;
        private readonly ISupplierDao supplierDaoDB;

        public ProductService(IProductDao productDaoDB, IProductCategoryDao productCategoryDaoDB, ISupplierDao supplierDaoDB)
        {
            this.productDaoDB = productDaoDB;
            this.productCategoryDaoDB = productCategoryDaoDB;
            this.supplierDaoDB = supplierDaoDB;
        }
        
        public ProductCategory GetProductCategory(int categoryId) => this.productCategoryDaoDB.Get(categoryId);

        public IEnumerable<ProductCategory> GetProductCategories() => this.productCategoryDaoDB.GetAll();

        
        //public IEnumerable<Product> GetProductsForCategory(int categoryId)
        //{
        //    ProductCategory category = this.productCategoryDao.Get(categoryId);
        //    return this.productDao.GetBy(category);
        //}

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDaoDB.Get(categoryId);
            return this.productDaoDB.GetBy(category);
        }

        //public IEnumerable<Product> GetAllProducts() => this.productDaoDB.GetAll();

        public Supplier GetProductSupplier(int supplierId) => this.supplierDaoDB.Get(supplierId);

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDaoDB.Get(supplierId);
            return this.productDaoDB.GetBy(supplier);
        }

        public IEnumerable<Supplier> GetSuppliers() => this.supplierDaoDB.GetAll();

    }
}
