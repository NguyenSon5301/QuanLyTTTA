//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BigProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class InClass
    {
        public InClass()
        {
            this.InStudents = new HashSet<InStudent>();
        }
    
        public string MaLop { get; set; }
        public Nullable<int> SLHVDK { get; set; }
        public Nullable<int> SLHVTD { get; set; }
        public string KGH { get; set; }
        public Nullable<System.DateTime> NBD { get; set; }
        public Nullable<int> SBH { get; set; }
        public Nullable<int> HP { get; set; }
        public Nullable<int> IDGV { get; set; }
    
        public virtual GiaoVien GiaoVien { get; set; }
        public virtual ICollection<InStudent> InStudents { get; set; }
    }
}
