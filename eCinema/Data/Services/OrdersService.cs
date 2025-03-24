using eCinema.Models;
using Microsoft.EntityFrameworkCore;

namespace eCinema.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ تنفيذ الدالة المطلوبة
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Movie)
                .Include(o => o.User) // لضمان إحضار بيانات المستخدم المرتبط بالطلب
                .ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        // ✅ تخزين الطلب في قاعدة البيانات
        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmail)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmail,
                OrderItems = new List<OrderItem>()
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // إضافة العناصر إلى الطلب
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    OrderId = order.Id,
                    MovieId = item.Movie.Id,
                    Amount = item.Amount,
                    Price = item.Movie.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
