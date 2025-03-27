using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment
{
    internal class EntityF
    {
    }
}
public class Stock
{

    public string ProductModel { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string sex { get; set; }
    public byte[] image { get; set; }
    public float price { get; set; }

}

public class AllSale1
{
    public int Saleid { get; set; }
    public int ProductModel { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string sex { get; set; }
    public float price { get; set; }
    public string SaleDate { get; set; }
    public string SaleTime { get; set; }
    public string Cashier { get; set; }


}

public class Staff
{

    public string Sid { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Sex { get; set; }
    public string Roll { get; set; }
    public string image { get; set; }
    public string Password { get; set; }

}

public class recycle
{
    public int Saleid { get; set; }
    public int ProductModel { get; set; }
    public string ProductName { get; set; }
    public string ProductType { get; set; }
    public string sex { get; set; }
    public float price { get; set; }
    public string SaleDate { get; set; }
    public string SaleTime { get; set; }
    public string Cashier { get; set; }


}

public class SampleContext : DbContext
{
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<AllSale1> AllSale1s { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<recycle> recycles { get; set; }
}