﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StoreManagement_MVCEntities : DbContext
    {
        public StoreManagement_MVCEntities()
            : base("name=StoreManagement_MVCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblCustomer> tblCustomers { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblPurchase> tblPurchases { get; set; }
        public virtual DbSet<tblSale> tblSales { get; set; }
    }
}