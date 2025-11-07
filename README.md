## 📦 E-Commerce System

### 🧩 Overview  
The **E-Commerce System** is a fully object-oriented console-based shopping platform built in **C# (.NET)**.  
It demonstrates professional software architecture principles — including **Encapsulation, Inheritance, Polymorphism, Abstraction**, and full **SOLID compliance** — while simulating a real-world online shop environment.

The project includes modules for **user management**, **product catalog**, **cart handling**, **checkout process**, and **payment simulation** using both credit card and cash methods.

---

### 🎗️ System Architecture  

**Layered Design Pattern (3-Tier + Repository):**
| Layer | Responsibility |
|-------|----------------|
| **Core (Entities + Interfaces)** | Domain models and business contracts (Product, Category, Customer, Order, etc.) |
| **Infrastructure** | Data persistence and repository implementation using a `FakeDbContext` |
| **Services (Business Layer)** | Business logic, validation, and rule enforcement |
| **Presentation (Console UI)** | User interface handling and navigation menus |

**Applied Principles:**
- ✅ **OOP Concepts:** Inheritance, Encapsulation, Polymorphism, Abstraction  
- ✅ **SOLID Principles:**  
  - Single Responsibility (each class handles one job)  
  - Open/Closed (services extendable for new features)  
  - Liskov Substitution (base–derived class structure)  
  - Interface Segregation (specific interfaces per repository type)  
  - Dependency Inversion (services depend on abstractions, not implementations)

---

### ⚙️ Core Features  

#### 🧑‍💻 User Management
- Register new customers with full data validation  
- Login system with email/password authentication  
- Prevents duplicate email registration  
- Password policy (min. 6 chars, must include letters & numbers)  

#### 🛍️ Product Catalog
- Displays categorized products (Electronics, Clothing, etc.)  
- Dynamic product listing with stock quantity tracking  

#### 🛍️ Cart Management
- Add / remove products with quantity validation  
- Prevent adding unavailable stock  
- Auto-updates total price and stock after checkout  

#### 💳 Checkout & Payment
- Two payment methods:
  1. Credit Card  
  2. Cash on Delivery  
- Validates card number (16 digits), expiry (MM/YY), CVV (3 digits)  
- Simulates order confirmation and payment success/failure  

#### 📦 Orders
- View all past orders per customer  
- Cancel order functionality  
- Automatic cart clearing after successful checkout  

---

### 🧠 Highlights  

- 🔹 **Repository Pattern:** clean separation between data access and business logic  
- 🔹 **Dependency Injection-ready** architecture  
- 🔹 **Console-based UI** designed for clarity and step-by-step navigation  
- 🔹 **Safe input handling** via centralized helper class (`InputHelper`)  
- 🔹 **FakeDbContext** acts as in-memory database for testing  

---

### 🧉 Tech Stack  

| Category | Tools |
|-----------|--------|
| Language | C# |
| Framework | .NET 6 / .NET 8 Console |
| Architecture | 3-Tier + Repository Pattern |
| Storage | In-Memory (`FakeDbContext`) |
| Version Control | Git + GitHub |

---

### 🚀 How to Run  

```bash
git clone https://github.com/yourusername/ECommerceSystem.git
cd ECommerceSystem
dotnet build
dotnet run --project ECommerce.Presentation
```

---

### 📚 Example Flow  

```
=== E-Commerce System ===
1. Register
2. Login
3. Exit
Choose: 1
Name: Ahmed
Email: ahmed@gmail.com
Password: 123abc
Registration successful!

=== Main Menu ===
1. View Products
2. Add to Cart
3. View Cart
4. Checkout
5. View Orders
6. Logout
Choose: 2
Enter product ID: 1
Enter quantity: 2
✅ Added 2 × Laptop to cart.
```

---

### 🌟 Future Enhancements  
- Replace `FakeDbContext` with a real SQL database using Entity Framework  
- Add Admin Panel for product & order management  
- Implement REST API version for frontend integration  
- Include JWT authentication and role-based authorization  
- Introduce Unit Tests and logging (Serilog)

---

### 👨‍💻 Author  
**[Your Name]**  
📧 your.email@example.com  
💼 [LinkedIn Profile](https://www.linkedin.com/)  
💻 Passionate about clean code, software architecture, and OOP mastery.

