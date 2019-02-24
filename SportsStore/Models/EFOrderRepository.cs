using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        public IEnumerable<Order> Orders => _context.Orders.Include(x => x.Lines).ThenInclude(x => x.Product);

        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(x => x.Product));

            if (order.OrderId == 0)
                _context.Orders.Add(order);

            _context.SaveChanges();
        }
    }
}
