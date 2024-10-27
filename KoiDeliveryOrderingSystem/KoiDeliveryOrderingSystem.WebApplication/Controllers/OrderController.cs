using System.Collections.Generic;
using System.Linq;
using KoiDeliveryOrderingSystem.WebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Models;
using ModelsOrder = KoiDeliveryOrderingSystem.Models.Order;
using ModelsOrderDetail = KoiDeliveryOrderingSystem.Models.OrderDetail;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly HtqlkoiContext _context;

        public OrderController(HtqlkoiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            // Để người dùng chọn dịch vụ
            ViewBag.Services = _context.Services.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(ModelsOrder order, List<ModelsOrderDetail> orderDetails)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi ModelsOrder thành Data.Order trước khi thêm vào DbContext
                var dataOrder = new KoiDeliveryOrderingSystem.WebApplication.Data.Order
                {
                    CustomerId = order.CustomerId,
                    PickupLocation = order.PickupLocation,
                    DeliveryLocation = order.DeliveryLocation,
                    ShippingMethod = order.ShippingMethod,
                    TotalWeight = order.TotalWeight,
                    TotalQuantity = order.TotalQuantity,
                    AdditionalServices = order.AdditionalServices,
                    OrderDate = order.OrderDate,
                    Status = order.Status
                };

                // Lưu đơn hàng vào bảng Orders
                _context.Orders.Add(dataOrder);
                _context.SaveChanges();

                // Lưu từng chi tiết đơn hàng vào bảng OrderDetails
                foreach (var detail in orderDetails)
                {
                    var dataOrderDetail = new KoiDeliveryOrderingSystem.WebApplication.Data.OrderDetail
                    {
                        OrderId = dataOrder.OrderId,
                        KoiFishType = detail.KoiFishType,
                        Quantity = detail.Quantity,
                        Weight = detail.Weight
                    };
                    _context.OrderDetails.Add(dataOrderDetail);
                }
                _context.SaveChanges();

                return RedirectToAction("ConfirmOrder");
            }

            // Nếu ModelState không hợp lệ, trả về view để người dùng có thể sửa đổi
            return View(order);
        }

        public IActionResult ConfirmOrder()
        {
            return View();
        }
    }
}
