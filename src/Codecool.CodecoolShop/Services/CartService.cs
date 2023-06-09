using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {
        private readonly CartDaoMemory cartDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao supplierDao;

        public CartService(CartDaoMemory cartDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            this.cartDao = cartDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
        }

        public Cart GetCart()
        {
            return cartDao.cart;
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return productCategoryDao.GetAll();
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return supplierDao.GetAll();
        }
    }
}
