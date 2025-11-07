using System.Linq;
using System.Text.RegularExpressions;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public (bool Success, string Message) ValidateUserData(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || !Regex.IsMatch(name, @"^[A-Za-z\s]+$"))
                return (false, "Invalid name. Use letters only and at least 3 characters.");

            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email.Trim().ToLower(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return (false, "Invalid email format.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return (false, "Password must be at least 6 characters.");

            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
                return (false, "Password must contain both letters and numbers.");

            if (_userRepo.GetByEmail(email.Trim().ToLower()) != null)
                return (false, "This email is already registered.");

            return (true, "Valid");
        }


        public (bool Success, string Message) RegisterCustomer(string name, string email, string password)
        {
            var newCustomer = new Customer(_userRepo.GetAll().Count() + 1, name.Trim(), email.Trim().ToLower(), password);
            _userRepo.Add(newCustomer);
            return (true, "Registration successful.");
        }

        public (bool Success, string Message) Login(string email, string password, out User? user)
        {
            user = _userRepo.GetByEmail(email.Trim().ToLower());
            if (user == null)
                return (false, "Email not found.");

            if (!user.VerifyPassword(password))
                return (false, "Incorrect password.");

            return (true, "Login successful.");
        }

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();
    }
}
