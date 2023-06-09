using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using System;

namespace Codecool.CodecoolShop.Services
{
    public class DataManager
    {
        private readonly IConfiguration configuration;
        private string ConnectionString => configuration.GetConnectionString("ShopConnection");
        public DataManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];
                    
                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }

        public List<Product> GetProductsBySupplier(int supplierId)
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                WHERE s.id = @supplierId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@supplierId", supplierId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];

                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                WHERE c.id = @categoryId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@categoryId", categoryId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];

                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }

        public List<Product> GetProductsInCart(string userId)
        {
            RegisterUserCart(userId);
            int cartId = GetCartId(userId);

            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description, cp.quantity
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                INNER JOIN cart_product cp on cp.product_id = p.id
                                WHERE cp.cart_id = @cartId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();
                    var newCartProduct = new CartProduct();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];

                    newCartProduct.Quantity = (int)sqlDataReader["quantity"];

                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;
                    newProduct.CartProduct = newCartProduct;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }

        public void AddProductToCart(int productId, string userId)
        {
            RegisterUserCart(userId);
            int cartId = GetCartId(userId);

            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryCheck = @"SELECT cart_id, product_id, quantity FROM cart_product WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command1 = new SqlCommand(sqlQueryCheck, connection);
            command1.Parameters.AddWithValue("@cartId", cartId);
            command1.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            SqlDataReader sqlDataReader = command1.ExecuteReader();
            bool productInCart = sqlDataReader.HasRows;
            connection.Close();

            if(productInCart)
            {
                string sqlQueryUpdate= @"UPDATE cart_product SET quantity = quantity + 1 WHERE cart_id = @cartId AND product_id = @productId";
                SqlCommand command2 = new SqlCommand(sqlQueryUpdate, connection);
                command2.Parameters.AddWithValue("@cartId", cartId);
                command2.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                command2.ExecuteNonQuery();
                connection.Close();
            } 
            else
            {
                string sqlQueryInsert = @"INSERT INTO cart_product(cart_id, product_id, quantity) VALUES (@cartId, @productId, 1)";
                SqlCommand command3 = new SqlCommand(sqlQueryInsert, connection);
                command3.Parameters.AddWithValue("@cartId", cartId);
                command3.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                command3.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void IncreaseProductQuantity(int productId, string userId)
        {
            int cartId = GetCartId(userId);

            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"UPDATE cart_product SET quantity = quantity + 1 WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveProductFromCart(int productId, string userId)
        {
            int cartId = GetCartId(userId);

            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"UPDATE cart_product SET quantity = quantity - 1 WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command1 = new SqlCommand(sqlQuery, connection);
            command1.Parameters.AddWithValue("@cartId", cartId);
            command1.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command1.ExecuteNonQuery();
            connection.Close();

            int currentQuantity = 1;
            string sqlQueryCheck = @"SELECT quantity FROM cart_product WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command2 = new SqlCommand(sqlQueryCheck, connection);
            command2.Parameters.AddWithValue("@cartId", cartId);
            command2.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            SqlDataReader sqlDataReader = command2.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    currentQuantity = (int)sqlDataReader["quantity"];
                }
            }
            connection.Close();

            if(currentQuantity <= 0)
            {
                DeleteProductFromCart(productId, userId);
            }
        }

        public void DeleteProductFromCart(int productId, string userId)
        {
            int cartId = GetCartId(userId);

            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"DELETE FROM cart_product WHERE @productId = product_id AND @cartId = cart_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ClearCart(string userId)
        {
            int cartId = GetCartId(userId);

            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"DELETE FROM cart_product WHERE @cartId = cart_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT * FROM category";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newCategory = new ProductCategory();

                    newCategory.Id = (int)sqlDataReader["id"];
                    newCategory.Name = (string)sqlDataReader["name"];
                    newCategory.Description = (string)sqlDataReader["description"];
                    newCategory.Department = (string)sqlDataReader["department"];

                    categories.Add(newCategory);
                }
            }

            connection.Close();
            return categories;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT * FROM supplier";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newSupplier = new Supplier();

                    newSupplier.Id = (int)sqlDataReader["id"];
                    newSupplier.Name = (string)sqlDataReader["name"];
                    newSupplier.Description = (string)sqlDataReader["description"];

                    suppliers.Add(newSupplier);
                }
            }

            connection.Close();
            return suppliers;
        }

        public int GetCartId(string userId)
        {
            List<int> usersIdList = new List<int>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryUserIdCheck = @"SELECT cart.id FROM cart WHERE cart.account_id = @userId;";
            SqlCommand commandUserId = new SqlCommand(sqlQueryUserIdCheck, connection);
            commandUserId.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            SqlDataReader sqlDataReaderUserId = commandUserId.ExecuteReader();
            if (sqlDataReaderUserId.HasRows)
            {
                while (sqlDataReaderUserId.Read())
                {
                    usersIdList.Add((int)sqlDataReaderUserId["id"]);
                }
            }
            connection.Close();
            int cartId = usersIdList[0];
            return cartId;
        }

        public int GetOrderId(string userId)
        {
            List<int> orderIdList = new List<int>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryOrderIdCheck = @"SELECT id FROM client_order WHERE account_id = @userId ORDER BY id DESC;";
            SqlCommand commandUserId = new SqlCommand(sqlQueryOrderIdCheck, connection);
            commandUserId.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            SqlDataReader sqlDataReaderUserId = commandUserId.ExecuteReader();
            if (sqlDataReaderUserId.HasRows)
            {
                while (sqlDataReaderUserId.Read())
                {
                    orderIdList.Add((int)sqlDataReaderUserId["id"]);
                }
            }
            connection.Close();
            int orderId = orderIdList[0];
            return orderId;
        }

        public void CreateOrder(string userId, string firstName, string lastName, 
            string email, string address, string phoneNumber, string country, string city, string zipCode)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryInsertClienOrder = @"INSERT INTO client_order(account_id, order_status, first_name, last_name, client_email, client_address, phone_number, country, city, zip_code, order_date) 
                                                VALUES (@account_id, @order_status, @first_name, @last_name, @client_email, @client_address, @phone_number, @country, @city, @zip_code, @order_date)";
            SqlCommand command1 = new SqlCommand(sqlQueryInsertClienOrder, connection);
            string orderStatus = "checked";
            command1.Parameters.AddWithValue("@account_id", userId);
            command1.Parameters.AddWithValue("@order_status", orderStatus);
            command1.Parameters.AddWithValue("@first_name", firstName);
            command1.Parameters.AddWithValue("@last_name", lastName);
            command1.Parameters.AddWithValue("@client_email", email);
            command1.Parameters.AddWithValue("@client_address", address);
            command1.Parameters.AddWithValue("@phone_number", phoneNumber);
            command1.Parameters.AddWithValue("@country", country);
            command1.Parameters.AddWithValue("@city", city);
            command1.Parameters.AddWithValue("@zip_code", zipCode);
            command1.Parameters.AddWithValue("@order_date", DateTime.Now);
            connection.Open();
            command1.ExecuteNonQuery();
            connection.Close();

            List<Dictionary<string, dynamic>> productsList = new List<Dictionary<string, dynamic>>();
            int cartId = GetCartId(userId);
            string sqlQuerySelect = @"SELECT product_id, quantity FROM cart_product WHERE cart_id = @cartId";
            SqlCommand command2 = new SqlCommand(sqlQuerySelect, connection);
            command2.Parameters.AddWithValue("@cartId", cartId);
            connection.Open();
            SqlDataReader sqlDataReader = command2.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    int currentProductId = (int)sqlDataReader["product_id"];
                    int currentQuantity = (int)sqlDataReader["quantity"];
                    Dictionary<string, dynamic> productDict = new Dictionary<string, dynamic>();
                    productDict.Add("ProductId", currentProductId);
                    productDict.Add("Quantity", currentQuantity);
                    productsList.Add(productDict);
                }
            }
            connection.Close();

            int clientOrderId = GetOrderId(userId);
            foreach (var dict in productsList)
            {
                int productId = dict["ProductId"];
                int quantity = dict["Quantity"];
                string sqlQueryInsertOrderProduct = @"INSERT INTO order_product(client_order_id, product_id, quantity) 
                                                    VALUES (@client_order_id, @product_id, @quantity)";
                SqlCommand command3 = new SqlCommand(sqlQueryInsertOrderProduct, connection);
                command3.Parameters.AddWithValue("@client_order_id", clientOrderId);
                command3.Parameters.AddWithValue("@product_id", productId);
                command3.Parameters.AddWithValue("@quantity", quantity);
                connection.Open();
                command3.ExecuteNonQuery();
                connection.Close();
            }
            
            ClearCart(userId);
        }

        public List<Order> GetOrders(string userId)
        {
            List<Order> orders = GetOrdersDetails(userId);
            foreach (var order in orders)
            {
                order.OrderProducts = GetOrderProducts(order.Id);
                foreach (var orderProduct in order.OrderProducts)
                {
                    orderProduct.Product = GetProductsById(orderProduct.ProductId);
                }
            }
            return orders;
        }

        public List<OrderProduct> GetPaidOrderProducts(Order currentOrder)
        {
            currentOrder.OrderProducts = GetOrderProducts(currentOrder.Id);
            foreach (var orderProduct in currentOrder.OrderProducts)
            {
                orderProduct.Product = GetProductsById(orderProduct.ProductId);              
            }
            return currentOrder.OrderProducts;
        }

        public List<Order> GetOrdersDetails(string userId)
        {
            List<Order> orders = new List<Order>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT c_o.id, c_o.order_date, c_o.order_status
                                FROM client_order c_o
                                WHERE account_id = @userId
                                ORDER BY c_o.order_date DESC";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newOrder = new Order();

                    newOrder.Id = (int)sqlDataReader["id"];
                    newOrder.OrderDate = (DateTime)sqlDataReader["order_date"];
                    newOrder.OrderStatus = (string)sqlDataReader["order_status"];

                    orders.Add(newOrder);
                }
            }

            connection.Close();
            return orders;
        }

        public List<OrderProduct> GetOrderProducts(int clientOrderId)
        {
            List<OrderProduct> orderProducts = new List<OrderProduct>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT product_id, quantity 
                                FROM  order_product
                                WHERE client_order_id = @clientOrderId;";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@clientOrderId", clientOrderId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newOrderProduct = new OrderProduct();

                    newOrderProduct.ProductId = (int)sqlDataReader["product_id"];
                    newOrderProduct.Quantity = (int)sqlDataReader["quantity"];

                    orderProducts.Add(newOrderProduct);
                }
            }

            connection.Close();
            return orderProducts;
        }

        public Product GetProductsById(int productId)
        {
            Product product = new Product() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT name, defaultprice
                                FROM product
                                WHERE id = @productId;";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();

                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];

                    product = newProduct;
                }
            }

            connection.Close();
            return product;
        }
        public Order GetClientOrderDetails(int orderId)
        {
            List<Order> ordersList = new List<Order>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT *
                                FROM client_order
                                WHERE id = @orderId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@orderId", orderId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newOrder = new Order();

                    newOrder.Id = (int)sqlDataReader["id"];
                    newOrder.AccountId = (string)sqlDataReader["account_id"];
                    newOrder.FirstName = (string)sqlDataReader["first_name"];
                    newOrder.LastName = (string)sqlDataReader["last_name"];
                    newOrder.ClientEmail = (string)sqlDataReader["client_email"];
                    newOrder.ClientAddress = (string)sqlDataReader["client_address"];
                    newOrder.PhoneNumber = (string)sqlDataReader["phone_number"];
                    newOrder.Country = (string)sqlDataReader["country"];
                    newOrder.City = (string)sqlDataReader["city"];
                    newOrder.ZipCode = (string)sqlDataReader["zip_code"];

                    ordersList.Add(newOrder);
                }
            }

            connection.Close();
            var order = ordersList[0];

            return order;
        }

        public Order GetClientLastOrderDetails(string userId)
        {
            List<Order> ordersList = new List<Order>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT TOP 1 *
                                FROM client_order
                                WHERE account_id = @userId
                                ORDER BY order_date DESC";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newOrder = new Order();

                    newOrder.Id = (int)sqlDataReader["id"];
                    newOrder.AccountId = (string)sqlDataReader["account_id"];
                    newOrder.FirstName = (string)sqlDataReader["first_name"];
                    newOrder.LastName = (string)sqlDataReader["last_name"];
                    newOrder.ClientEmail = (string)sqlDataReader["client_email"];
                    newOrder.ClientAddress = (string)sqlDataReader["client_address"];
                    newOrder.PhoneNumber = (string)sqlDataReader["phone_number"];
                    newOrder.Country = (string)sqlDataReader["country"];
                    newOrder.City = (string)sqlDataReader["city"];
                    newOrder.ZipCode = (string)sqlDataReader["zip_code"];

                    ordersList.Add(newOrder);
                }
            }

            connection.Close();
            var order = ordersList[0];

            return order;
        }

        public void SetOrderStatus(int orderId, string status)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryUpdate = @"UPDATE client_order SET order_status = @status WHERE id = @orderId";
            SqlCommand command2 = new SqlCommand(sqlQueryUpdate, connection);
            command2.Parameters.AddWithValue("@status", status);
            command2.Parameters.AddWithValue("@orderId", orderId);
            connection.Open();
            command2.ExecuteNonQuery();
            connection.Close();
        }

        public decimal GetOrderTotalByOrderId(int orderId)
        {
            decimal total = 0;
            Order newOrder = new Order();
            newOrder.OrderProducts = GetOrderProducts(orderId);
            foreach (var orderProduct in newOrder.OrderProducts)
            {
                orderProduct.Product = GetProductsById(orderProduct.ProductId);
            }
            foreach (var orderProduct in newOrder.OrderProducts)
            {
                total += orderProduct.Quantity * orderProduct.Product.DefaultPrice;
            }
            return total;
        }

        public void RegisterUserCart(string userId)
        {
            if(!UserHasCart(userId))
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                string sqlQueryInsert = @"INSERT INTO cart(account_id) VALUES (@userId)";
                SqlCommand command = new SqlCommand(sqlQueryInsert, connection);
                command.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool UserHasCart(string userId)
        {
            List<string> cartAccountsIdsList = new List<string>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT account_id
                                FROM cart;";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    string accountId = (string)sqlDataReader["account_id"];

                    cartAccountsIdsList.Add(accountId);
                }
            }

            connection.Close();

            return cartAccountsIdsList.Contains(userId);
        }

    }
}
