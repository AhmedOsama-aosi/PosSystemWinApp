//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PosWInApp.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.SalesBills = new HashSet<SalesBill>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string email { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesBill> SalesBills { get; set; }
    }
}
