using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    class ProductCategoryDaoMemory : IProductCategoryDao
    {
        private List<ProductCategory> _data = new List<ProductCategory>();
        private static ProductCategoryDaoMemory _instance;

        private ProductCategoryDaoMemory()
        {
        }

        public static ProductCategoryDaoMemory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductCategoryDaoMemory();
            }

            return _instance;
        }

        public void Add(ProductCategory item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);
        }

        public void Remove(int id)
        {
            _data.Remove(this.Get(id));
        }

        public ProductCategory Get(int id)
        {
            return _data.Find(x => x.Id == id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _data;
        }
    }
}
