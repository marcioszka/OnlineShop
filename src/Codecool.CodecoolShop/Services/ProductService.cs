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

        public ProductService(IProductDao productDaoDB, IProductCategoryDao productCategoryDaoDB)
        {
            this.productDaoDB = productDaoDB;
            this.productCategoryDaoDB = productCategoryDaoDB;
        }
        //public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao)
        //{
        //    this.productDao = productDao;
        //    this.productCategoryDao = productCategoryDao;
        //}

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDaoDB.Get(categoryId);
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return this.productCategoryDaoDB.GetAll();
        }

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
            ProductCategory category = this.productCategoryDaoDB.Get(categoryId);
            return this.productDaoDB.GetBy(category);
        }

        public IEnumerable<Product> GetAllProducts() => this.productDaoDB.GetAll();
    }
}
