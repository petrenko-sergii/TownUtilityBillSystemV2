//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TownUtilityBillSystemV2
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEMPERATURE
    {
        public int ID { get; set; }
        public System.DateTime DATE { get; set; }
        public int TOWN_ID { get; set; }
        public int MINVALUE { get; set; }
        public int MAXVALUE { get; set; }
    
        public virtual TOWN TOWN { get; set; }
    }
}
