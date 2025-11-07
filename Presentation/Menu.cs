using ECommerce.Core.Entities.Payment;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Presentation.Helpers;
using ECommerce.Services;
using System;
using System.Text.RegularExpressions;


namespace ECommerce.Presentation
{
    public class Menu
    {
        private readonly UserService _userService;
        private readonly CartService _cartService;
        private readonly OrderService _orderService;
        private readonly PaymentService _paymentService;
        private readonly FakeDbContext _context;

        private int? _loggedInCustomerId;

        public Menu(FakeDbContext context)
        {
            _context = context; 

            var userRepo = new UserRepository(_context);
            var productRepo = new ProductRepository(_context);
            var orderRepo = new OrderRepository(_context);

            _userService = new UserService(userRepo);
            _cartService = new CartService(userRepo, productRepo);
            _orderService = new OrderService(userRepo, orderRepo, productRepo);
            _paymentService = new PaymentService();
        }



        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== E-Commerce System ===");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Register(); break;
                    case "2": Login(); break;
                    case "3": return;
                }
            }
        }

        private void Register()
        {
            string name;
            while (true)
            {
                name = InputHelper.ReadNonEmpty("Name: ");
                var check = _userService.ValidateUserData(name, "dummy@mail.com", "123456");
                if (!check.Success && check.Message.StartsWith("Invalid name"))
                {
                    Console.WriteLine(check.Message);
                    continue;
                }
                break;
            }

            string email;
            while (true)
            {
                email = InputHelper.ReadNonEmpty("Email: ");
                var check = _userService.ValidateUserData(name, email, "123456");
                if (!check.Success && (check.Message.StartsWith("Invalid email") || check.Message.Contains("already")))
                {
                    Console.WriteLine(check.Message);
                    continue;
                }
                break;
            }

            string pass;
            while (true)
            {
                pass = InputHelper.ReadNonEmpty("Password: ");
                var check = _userService.ValidateUserData(name, email, pass);
                if (!check.Success && check.Message.StartsWith("Password"))
                {
                    Console.WriteLine(check.Message);
                    continue;
                }
                break;
            }

            var result = _userService.RegisterCustomer(name, email, pass);
            Console.WriteLine(result.Message);
            Console.ReadKey();
        }





        private void Login()
        {
            var email = InputHelper.ReadNonEmpty("Email: ");
            var pass = InputHelper.ReadNonEmpty("Password: ");

            var result = _userService.Login(email, pass, out var user);

            if (!result.Success)
            {
                Console.WriteLine(result.Message);
                Console.ReadKey();
                return;
            }

            _loggedInCustomerId = user!.Id;
            Console.WriteLine($"Welcome, {user.Name}!");
            Console.ReadKey();
            MainMenu();
        }


        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. View Orders");
                Console.WriteLine("6. Logout");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewProducts(); break;
                    case "2": AddToCart(); break;
                    case "3": ViewCart(); break;
                    case "4": Checkout(); break;
                    case "5": ViewOrders(); break;
                    case "6": return;
                }
            }
        }

        private void ViewProducts()
        {
            Console.Clear();
            foreach (var p in _context.Products)
                Console.WriteLine($"{p.Id}. {p.Name} - {p.Price:C} ({p.StockQuantity} available)");
            Console.ReadKey();
        }

        private void AddToCart()
        {
            var productId = InputHelper.ReadInt("Enter product ID: ");
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine(" Product not found. Please enter a valid ID.");
                Console.ReadKey();
                return;
            }

            var quantity = InputHelper.ReadInt("Enter quantity: ");

            if (!_loggedInCustomerId.HasValue)
            {
                Console.WriteLine("You must be logged in first.");
                Console.ReadKey();
                return;
            }

            var result = _cartService.AddToCart(_loggedInCustomerId.Value, productId, quantity);
            Console.WriteLine(result.Message);
            Console.ReadKey();
        }



        private void ViewCart()
        {
            if (!_loggedInCustomerId.HasValue) return;
            var total = _cartService.GetCartTotal(_loggedInCustomerId.Value);
            Console.WriteLine($"Total: {total:C}");
            Console.ReadKey();
        }

        private void Checkout()
        {
            if (!_loggedInCustomerId.HasValue)
            {
                Console.WriteLine("You must be logged in first.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Checkout ===");
            Console.WriteLine("1. Credit Card");
            Console.WriteLine("2. Cash on Delivery");
            Console.Write("Choose payment method (1 or 2): ");

            var choice = Console.ReadLine();
            IPayment payment;

            if (choice == "1")
            {
                var card = InputHelper.ReadNonEmpty("Card Number (16 digits): ");
                var exp = InputHelper.ReadNonEmpty("Expiry Date (MM/YY): ");
                var cvv = InputHelper.ReadNonEmpty("CVV: ");
                payment = _paymentService.CreatePayment("creditcard", card, exp, cvv);
            }
            else if (choice == "2")
            {
                payment = _paymentService.CreatePayment("cash");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1 or 2.");
                Console.ReadKey();
                return;
            }

            var result = _orderService.PlaceOrder(_loggedInCustomerId.Value, payment);
            Console.WriteLine(result.Message);
            Console.ReadKey();
        }



        private void ViewOrders()
        {
            if (!_loggedInCustomerId.HasValue) return;
            var orders = _orderService.GetOrdersByCustomer(_loggedInCustomerId.Value);
            foreach (var o in orders)
                Console.WriteLine($"{o.Id} - {o.Status} - {o.TotalAmount:C}");
            Console.ReadKey();
        }
    }
}
