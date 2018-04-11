using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchRetreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if(includeItems)
            {
                return _ctx.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .ToList();
            }
            else
            {
                return _ctx.Orders
                       .ToList();
            }            
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == userName)
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .ToList();
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == userName)
                       .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products.OrderBy(p => p.Title).ToList();
        }

        public Order GetOrderById(string userName,int id)
        {
            return _ctx.Orders
                      .Include(o => o.Items)
                      .ThenInclude(i => i.Product)
                      .Where(o => o.Id == id && o.User.UserName == userName)
                      .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
