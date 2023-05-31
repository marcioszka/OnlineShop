using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoMemory : ISupplierDao
    {
        private List<Supplier> _data = new List<Supplier>();
        private static SupplierDaoMemory _instance;

        private SupplierDaoMemory()
        {
        }

        public static SupplierDaoMemory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SupplierDaoMemory();
            }

            return _instance;
        }

        public void Add(Supplier item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);
        }

        public void Remove(int id)
        {
            _data.Remove(this.Get(id));
        }

        public Supplier Get(int id)
        {
            return _data.Find(x => x.Id == id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _data;
        }
    }
}
