//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Group_Mapping
    {
        public int ID { get; set; }
        public int ProductGroupID { get; set; }
        public int ProductID { get; set; }
        public Nullable<bool> MainGroup { get; set; }
    
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual Product Product { get; set; }
    }
}
