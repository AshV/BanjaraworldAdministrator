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
    using System.ComponentModel.DataAnnotations;
    
    public partial class tbl_TermsAndCondition
    {
        public tbl_TermsAndCondition()
        {
            this.tbl_Package = new HashSet<tbl_Package>();
        }
    
        public int tnc_ID { get; set; }

        [Required(ErrorMessage="Name is maindatory.")]
        [StringLength(25, ErrorMessage = "Maximum Allowed length is {1}")]
        public string tnc_Name { get; set; }
        public string tnc_Description { get; set; }
    
        public virtual ICollection<tbl_Package> tbl_Package { get; set; }
    }
}
