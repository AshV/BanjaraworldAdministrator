//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BW_Admin
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Price
    {
        public int prc_ID { get; set; }
        public string prc_Coordinate { get; set; }
        public Nullable<int> prc_PackageId { get; set; }
        public Nullable<int> prc_Price { get; set; }
    
        public virtual tbl_Package tbl_Package { get; set; }
    }
}