using System.Linq;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Services
{
    public class CartService
    {
        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _productRepo;

        public CartService(IUserRepository userRepo, IProductRepository productRepo)
        {
            _userRepo = userRepo;
            _productRepo = productRepo;
        }

        public (bool Success, string Message) AddToCart(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
                return (false, "Quantity must be greater than zero.");

            var user = _userRepo.GetById(customerId) as Customer;
            if (user == null)
                return (false, "Customer not found.");

            var product = _productRepo.GetById(productId);
            if (product == null)
                return (false, "Product not found.");

            if (quantity > product.StockQuantity)
                return (false, $"Only {product.StockQuantity} units available.");

            user.Cart.AddItem(product, quantity);
            return (true, $"Added {quantity} × {product.Name} to cart.");
        }

        public (bool Success, string Message) RemoveFromCart(int customerId, int productId)
        {
            var user = _userRepo.GetById(customerId) as Customer;
            if (user == null)
                return (false, "Customer not found.");

            var existing = user.Cart.Items.FirstOrDefault(i => i.Product.Id == productId);
            if (existing == null)
                return (false, "Product not found in cart.");

            user.Cart.RemoveItem(productId);
            return (true, "Item removed from cart.");
        }

        public decimal GetCartTotal(int customerId)
        {
            var user = _userRepo.GetById(customerId) as Customer;
            return user?.Cart.GetTotalPrice() ?? 0;
        }

        public void ClearCart(int customerId)
        {
            var user = _userRepo.GetById(customerId) as Customer;
            user?.Cart.ClearCart();
        }
    }
}
