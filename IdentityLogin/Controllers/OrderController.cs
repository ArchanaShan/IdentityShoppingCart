using IdentityLogin.Models;
using IdentityLogin.Repository;
using IdentityLogin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityLogin.Controllers
{
    public class OrderController : Controller
    {
        private const string SessionOrderId = "CurrentOrderId";
        private readonly ShoppingCartsContext _context;
        private readonly IOrderRepo _orderRepo;

        public OrderController(ShoppingCartsContext context, IOrderRepo orderRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
        }
        public IActionResult OrderSummary()
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);

            var summaryDetails = from product in _context.Products
                                 join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                                 where orderDetail.OrderId == orderId
                                 select new OrderSummaryViewModel
                                 {
                                     ProductId = product.ProductId,
                                     Name = product.Name,
                                     Image = product.Image,
                                     Price = product.Price,
                                     Quantity = orderDetail.Quantity
                                 };
            UpdatedTotalAmount();
            return View(summaryDetails);
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseItem(int productId, int redirect = 0)
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            int quantity = await _orderRepo.IncreaseQuantity(productId, orderId.Value);
            if (redirect == 0)
                return Ok(quantity);
            return RedirectToAction("Order", "OrderSummary");
        }
        public async Task<IActionResult> DecreaseItem(int productId, int redirect = 0)
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            int quantity = await _orderRepo.DecreaseQuantity(productId, orderId.Value);
            if (redirect == 0)
                return Ok(quantity);
            return RedirectToAction("Order", "OrderSummary");
        }
        [HttpGet]
        public int UpdatedTotalAmount()
        {
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
            decimal totalAmount = 0;
            IEnumerable<OrderSummaryViewModel> totalItem = from product in _context.Products
                                                           join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                                                           where orderDetail.OrderId == orderId
                                                           select new OrderSummaryViewModel
                                                           {
                                                               Price = product.Price,
                                                               Quantity = orderDetail.Quantity
                                                           };
            foreach (var item in totalItem)
            {
                totalAmount += item.Price * item.Quantity;
            }
            ViewBag.TotalAmount = totalAmount;
            return (int)totalAmount;
        }
        public IActionResult Checkout()
        {
            var userEmail = User.Identity.Name;
            var orderId = HttpContext.Session.GetInt32(SessionOrderId);
                var order = new Order
                {
                    UserId = userEmail,
                    OrderId = orderId.Value,
                    TotalAmount = UpdatedTotalAmount(),
                    CreatedOn = DateTime.Now,
                    CreatedBy = "Admin",
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = "Admin"
                };
                _context.Orders.Add(order);
            var userDetails = _context.AspNetUsers.Where(x => x.Email == userEmail).
                Select(u => new { u.FirstName, u.LastName, u.PhoneNumber, u.Address }).FirstOrDefault();

            var orderDetails = from product in _context.Products
                               join orderDetail in _context.OrderDetails on product.ProductId equals orderDetail.ProductId
                               where orderDetail.OrderId == orderId
                               select new OrderSummaryViewModel
                               {
                                   ProductId = product.ProductId,
                                   Name = product.Name,
                                   Image = product.Image,
                                   Price = product.Price,
                                   Quantity = orderDetail.Quantity
                               };

            var checkOutDetail = new CheckOutDetailsViewModel
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Phone = userDetails.PhoneNumber,
                Address = userDetails.Address,
                OrderId = orderId.Value,
                TotalAmount = UpdatedTotalAmount(),
                listOrderDetails = orderDetails.ToList()
            };

            return View(checkOutDetail);
        }
    }
}
