//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompresJSON
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubCategory
    {
        public int SubCategoryID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string SubCategory1 { get; set; }
        public string SubCategoryDescription { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string Active { get; set; }
        public string MetaTags { get; set; }
    
        public virtual Category Category { get; set; }
    }
}