//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tblPurchase
    {
        public int Purchase_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Purchase_Date { get; set; }
        public string Dealer_Name { get; set; }
        public string Dealer_Address { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
    }
}