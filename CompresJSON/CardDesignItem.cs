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
    
    public partial class CardDesignItem
    {
        public int CardDesignItemID { get; set; }
        public Nullable<int> CardDesignID { get; set; }
        public string CardDesignItem1 { get; set; }
        public Nullable<int> FontID { get; set; }
        public Nullable<int> ColourID { get; set; }
        public Nullable<int> FontSizeID { get; set; }
        public string ItemText { get; set; }
        public string ItemAttributes { get; set; }
    
        public virtual CardDesign CardDesign { get; set; }
    }
}