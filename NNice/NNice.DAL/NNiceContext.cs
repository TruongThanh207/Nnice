using Microsoft.EntityFrameworkCore;
using NNice.Common.Helpers;
using NNice.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNice.DAL
{
    public partial class NNiceContext : DbContext
    {

        public NNiceContext() { }
        public NNiceContext(DbContextOptions<NNiceContext> options) : base(options) { }

        public DbSet<AccountModel> Accounts { get; set; }
        //public DbSet<Import> Imports { get; set; }
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<PartyModel> Parties { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
        public DbSet<WorkShiftModel> WorkShifts { get; set; }
        public DbSet<InvoiceDetailModel> InvoiceDetails { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ComboModel> Combos { get; set; }
        public DbSet<MaterialModel> Materials { get; set; }
        public DbSet<ComboDetailModel> ComboDetails { get; set; }
        public DbSet<ProductDetailModel> ProductDetails { get; set; }
        public DbSet<InventoryModel> inventories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<InventoryDetailModel> inventoryDetailModels { get; set; }
        public DbSet<EmployeeShiftModel> EmployeeShifts { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\ThienNguyen;Database=NNiceDatabase;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=NNiceDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceDetailModel>()
                .HasKey(c => new { c.InvoiceID, c.ProductID });
            modelBuilder.Entity<ComboDetailModel>()
                .HasKey(c => new { c.CBID, c.ProductID });
            modelBuilder.Entity<InvoiceDetailModel>()
                .HasKey(c => new { c.InvoiceID, c.ProductID });
            modelBuilder.Entity<ProductDetailModel>()
                .HasKey(c => new { c.ProductID, c.MaterialID });
            modelBuilder.Entity<InventoryDetailModel>()
               .HasKey(c => new { c.InventoryID, c.MaterialID });
            modelBuilder.Entity<EmployeeShiftModel>()
                .HasKey(c => new { c.EmployeeID, c.WorkShiftID });

            /////seed data
            modelBuilder.Entity<AccountModel>().HasData(
                new AccountModel()
                {
                    ID = 1,
                    Username = "admin",
                    Password = "admin",
                    PasswordHash = HashPassword.GetHash("admin")
                ,
                    PasswordSalt = HashPassword.GetHash("admin"),
                    Role = Role.Admin
                },

                new AccountModel()
                {
                    ID = 2,
                    Username = "accountant",
                    Password = "accountant",
                    PasswordHash = HashPassword.GetHash("accountant")
                ,
                    PasswordSalt = HashPassword.GetHash("accountant"),
                    Role = Role.Accountant
                },
                new AccountModel()
                {
                    ID = 3,
                    Username = "Cashier",
                    Password = "Cashier",
                    PasswordHash = HashPassword.GetHash("Cashier")
                ,
                    PasswordSalt = HashPassword.GetHash("Cashier"),
                    Role = Role.Cashier
                }
                );
        }
    }
}
