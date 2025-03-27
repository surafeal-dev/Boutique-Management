# **Boutique Management System**  

A **.NET Core** and **SQL**-based web application for managing boutique inventory, sales, and customer data.  

---

## **📌 Features**  
✔ **Inventory Management** – Track products, categories, and stock levels.  
✔ **Sales & Order Processing** – Record purchases, returns, and customer history.  s.  
✔ **Customer Management** – Store client details and purchase history.  
✔ **ySQL Database** – Uses **MySQL** for data persistence.  

---

## **🛠 Technologies Used**  
- **Backend**: .NET Core 6+ (C#)  
- **Database**: MySQL  
- **Frontend**: Windows Forms

---

## **🚀 Getting Started**  

### **Prerequisites**  
- .NET 6+ SDK  
- MySQL  
- Visual Studio 2022 / VS Code
- 
Visual Studio IDE for development and debugging

### **Setup & Run**  
1. **Clone the repository**:  
   ```sh
   git clone https://github.com/TallOrder99/Boutique-Management-System.git
   cd Boutique-Management-System
   ```  

2. **Configure the database**:  
   - Update `appsettings.json`:  
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=localhost;Database=BoutiqueDb;User Id=sa;Password=yourpassword;"
     }
     ```  

3. **Apply database migrations**:  
   ```sh
   dotnet ef database update
   ```  

4. **Run the application**:  
   ```sh
   dotnet run
   ```  
   - The app will start at: `http://localhost:5000`  

