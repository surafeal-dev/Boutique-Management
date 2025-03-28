# 🏪 StoreB-Management System

**StoreB-Management** is a **.NET Core & MySQL-powered** web application designed to efficiently manage boutique inventory, sales, and customer relationships.

---

## 🛠 Technology Stack

🔹 **Backend:** .NET Core 6+ (C#)  
🔹 **Database:** MySQL  
🔹 **Frontend:** Windows Forms  

---

## ✅ Prerequisites

- 📌 **.NET 6+ SDK**
- 📌 **MySQL**
- 📌 **Visual Studio 2022** or **VS Code**

---

## ⚙️ Installation & Setup

### 📥 1. Clone the Repository
```sh
git clone https://github.com/TallOrder99/StoreB-Management.git
cd StoreB-Management
```

### 🛠 2. Configure the Database
Modify `appsettings.json` with your MySQL connection details:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=StoreBDB;User Id=root;Password=yourpassword;"
}
```

### 🔄 3. Apply Migrations
```sh
dotnet ef database update
```

### 🚀 4. Launch the Application
```sh
dotnet run
```
📌 The application will be accessible at: **http://localhost:5000**

---

## 📌 Features

- **🛒 Inventory Management** – Organize products, categories, and stock levels.
- **💰 Sales & Transactions** – Track purchases, returns, and customer order history.
- **👥 Customer Database** – Store client details and purchase records.
- **💾 MySQL Integration** – Secure and efficient data storage.


---

### 💡 Built with ❤️ using .NET & MySQL 🚀
