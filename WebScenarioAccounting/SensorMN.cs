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

    public partial class SensorMN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SensorMN()
        {
            this.SensorConditions = new HashSet<SensorCondition>();
        }
    
        public int id { get; set; }

        [Display(Name = "???")]
        public int TypeID { get; set; }

        [Display(Name = "?????????")]
        public int RoomID { get; set; }

        [Display(Name = "????????")]
        public string Description { get; set; }
    
        public virtual ClassifierSensor ClassifierSensor { get; set; }
        public virtual Room Room { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SensorCondition> SensorConditions { get; set; }
    }
}
