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
    
    public partial class CUSTOMER
    {
        public int ID { get; set; }
        public string SURNAME { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public int ADDRESS_ID { get; set; }
        public int CUSTOMER_TYPE_ID { get; set; }
        public int ACCOUNT_ID { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual ADDRESS ADDRESS { get; set; }
        public virtual CUSTOMER_TYPE CUSTOMER_TYPE { get; set; }
    }
}
