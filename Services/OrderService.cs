using System.Linq;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Payment;
using ECommerce.Core.Interfaces;

namespace ECommerce.Services
{
    public class OrderService
    {
        private readonly IUserRepository _userRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(IUserRepository userRepo, IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _userRepo = userRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public (bool Success, string Message) PlaceOrder(int customerId, IPayment paymentMethod)
        {
            var customer = _userRepo.GetById(customerId) as Customer;
            if (customer == null)
                return (false, "Customer not found.");

            var cart = customer.Cart;
            if (!cart.Items.Any())
                return (false, "Cart is empty.");

            foreach (var item in cart.Items)
            {
                if (item.Quantity > item.Product.StockQuantity)
                    return (false, $"Insufficient stock for {item.Product.Name}.");
            }

            foreach (var item in cart.Items)
            {
                item.Product.ReduceStock(item.Quantity);
                _productRepo.Update(item.Product);
            }

            var order = new Order(customer, cart.Items, paymentMethod);
            var success = order.ProcessPayment();
            if (!success)
                return (false, "Payment failed.");

            _orderRepo.Add(order);
            customer.PlaceOrder(order);
            cart.ClearCart();

            return (true, "Order placed successfully.");
        }

        public IEnumerable<Order> GetOrdersByCustomer(int customerId)
        {
            return _orderRepo.GetByCustomerId(customerId);
        }

        public (bool Success, string Message) CancelOrder(int orderId)
        {
            var order = _orderRepo.GetById(orderId);
            if (order == null)
                return (false, "Order not found.");

            order.CancelOrder();
            _orderRepo.Update(order);
            return (true, "Order canceled successfully.");
        }
    }
}
