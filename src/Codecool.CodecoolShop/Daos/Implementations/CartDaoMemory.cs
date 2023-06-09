using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class CartDaoMemory
    {
        public Cart cart { get; } = new Cart();
        private static CartDaoMemory instance = null;

        private CartDaoMemory()
        {
        }

        public static CartDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new CartDaoMemory();
            }

            return instance;
        }

    }
}
