using Business.Contracts;
using Data;
using Data.Contracts;
using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public int Add(User user)
        {
            if (user.Id <= 0) return 0;
            if (string.IsNullOrEmpty(user.Name)) return 0;
            if (string.IsNullOrEmpty(user.Password)) return 0;
            return _userRepo.Add(user);
        }
        public bool AddAccount(Account account)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                try
                {
                    ctx.Accounts.Add(account);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool AddProduct(Product product)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                try
                {
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool Delete(int id)
        {
            if (id <= 0) return false;
            return (_userRepo.Delete(id));
        }
        public User Get(int id)
        {
            User u = _userRepo.Get(id);
            return u;
        }
        public List<Account> GetAccountsByDate(string date)
        {
            DateTime parsedDate;
            if (!DateTime.TryParse(date, out parsedDate))
            {
                throw new ArgumentException("Invalid date format");
            }

            using (var ctx = new InventoryStevDBContext())
            {
                var accounts = ctx.Accounts
                    .Where(a => a.CreatedDate.Year == parsedDate.Year && a.CreatedDate.Month == parsedDate.Month && a.CreatedDate.Day == parsedDate.Day)
                    .ToList();

                return accounts;
            }
        }
        public bool Login(string username, string password)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

                if (user == null)
                {
                    Console.WriteLine("Invalid username or password");
                    return false;
                }

                return true;
            }
        }
        public bool ModifyProduct(int productId, string productName, int quantity, decimal price, string status)
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var product = ctx.Products.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    Console.WriteLine("Product not found");
                    return false;
                }

                product.ProductName = productName;
                product.Quantity = quantity;
                product.Price = price;
                product.Status = status;

                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
        }
        public bool ModifyProduct(int productId, string productName, int quantity, decimal price)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            if (user.Id <= 0) return false;
            if (string.IsNullOrEmpty(user.Name)) return false;
            if (string.IsNullOrEmpty(user.Password)) return false;
            if (string.IsNullOrEmpty(user.UserName)) return false;
            return _userRepo.Update(user);
        }
        public List<Product> ViewInventory()
        {
            using (var ctx = new InventoryStevDBContext())
            {
                var products = ctx.Products.ToList();
                return products;
            }
        }
    }
}