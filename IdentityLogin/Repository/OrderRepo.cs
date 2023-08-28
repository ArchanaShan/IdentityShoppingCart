using IdentityLogin.Common;
using IdentityLogin.Models;
using IdentityLogin.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IdentityLogin.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ShoppingCartsContext _context;
        private readonly StoredProcedureCall _sp;
        public OrderRepo(ShoppingCartsContext context, StoredProcedureCall sp)
        {
            _context = context;
            _sp = sp;
        }
        public async Task<int> DecreaseQuantity(int productId, int orderId)
        {
            using var transaction = _context.Database.BeginTransaction();
            var currentQuantity = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (currentQuantity is null)
            {
                throw new Exception("No item in cart");

            }
            else
            {
                _sp.DecreaseOrderDetail(currentQuantity.OrderId, currentQuantity.ProductId, currentQuantity.Quantity);
            }
            int quan = await UpdatedQuantity(productId,orderId);
            return quan;
        }

        public async Task<int> IncreaseQuantity(int productId, int orderId)
        {
            var currentQuantity = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (currentQuantity != null)
            {
                currentQuantity.Quantity = currentQuantity.Quantity + 1;
                _sp.UpdateOrderDetail(currentQuantity.OrderId, currentQuantity.ProductId, currentQuantity.Quantity);
            }
            int quan = await UpdatedQuantity(productId,orderId);
            return quan;
        }

        public async Task<int> UpdatedQuantity(int productId,int orderId)
        {
            var updateQuantity = await _context.OrderDetails.FirstOrDefaultAsync(y => y.OrderId == orderId && y.ProductId == productId);
            if (updateQuantity == null)
            {
                return 0;
            }
            return updateQuantity.Quantity;
        }

        
    }
}
