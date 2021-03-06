//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebScenarioAccounting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Actuator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Actuator()
        {
            this.Commands = new HashSet<Command>();
        }

        public int id { get; set; }

        [Display(Name = "?????????????")]
        public string Manufacturer { get; set; }

        [Display(Name = "????????")]
        public string Name { get; set; }

        [Display(Name = "?????????")]
        public int Room { get; set; }

        [Display(Name = "?????? ???????")]
        public int SubType { get; set; }

        [Display(Name = "???? ????????? ????????????")]
        public Nullable<System.DateTime> TerminationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Command> Commands { get; set; }
        public virtual ClassifierSubTypeThing ClassifierSubTypeThing { get; set; }
        public virtual Room Room1 { get; set; }
    }
}
