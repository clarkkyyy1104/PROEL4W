#  Blooms & Stitch — Crochet Flowers Management System

A web-based management system built with ASP.NET Core MVC for PROEL4W (Advanced Web Development).

##  Tech Stack
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- SQL Server LocalDB
- ASP.NET Core Identity
- HTML / CSS / JavaScript

##  Features
-  Login & Registration with email validation (@gmail.com only)
-  User Management (Admin only)
-  Order Management
-  Inventory Management
-  Stock Management with history logs
-  Notification System (auto + manual)
-  Reports Dashboard with PDF export
-  Role-based access control (Admin, Staff, Florist, Sales Associate, Farmer, Customer)

##  Roles
| Role | Access |
|------|--------|
| Admin | Full access to everything |
| Staff | Orders, Inventory, Notifications, Stocks |
| Florist / Sales Associate | Orders, Inventory, Notifications |
| Farmer | Dashboard only |
| Customer | Place orders, view own orders |

## 🚀 Getting Started
-everytime mag open mo ani e Update-Database usa aron mu run ug tarong

### Prerequisites
- Visual Studio 2022
- .NET 8 SDK
- SQL Server LocalDB

### Setup
1. Clone the repository:
```bash
   git clone https://github.com/yourusername/PROEL4W.git
```
2. Open `FINAL.slnx` in Visual Studio 2022
3. Open Package Manager Console and run:
```powershell
   Update-Database
```
4. Press **F5** to run

### Default Admin Accounts
| Name | Username | Email | Password |
|------|----------|-------|----------|
| Clark | admin | admin@gmail.com | admin123 |
| Geraldine | geraldine | geraldine@gmail.com | geraldine |

##  Project Structure
```
FINAL/
├── Controllers/     # Request handlers (business logic)
├── Models/          # Data models and view models
├── Views/           # Razor pages (UI)
├── Data/            # Database context
├── Migrations/      # EF Core migration history
└── wwwroot/         # Static files (CSS, JS)
```

##  Developer
- **Course:** PROEL4W — Advanced Web Development
- **Project:** Blooms & Stitch Crochet Flowers Management System
