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

    public partial class ClassifierValue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassifierValue()
        {
            this.ClassifierTimeWeathers = new HashSet<ClassifierTimeWeather>();
        }
    
        public int id { get; set; }

        [Display(Name = "Категория")]
        public string Name { get; set; }

        [Display(Name = "Наименование")]
        public string Label { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassifierTimeWeather> ClassifierTimeWeathers { get; set; }
    }
}
