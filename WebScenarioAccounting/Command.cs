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

    public partial class Command
    {
        public int id { get; set; }

        [Display(Name = "��������")]
        public int ScenarioID { get; set; }

        [Display(Name = "������� ����� Else")]
        public bool isElse { get; set; }

        [Display(Name = "��������")]
        public int ThingID { get; set; }

        [Display(Name = "�������")]
        public int CommandID { get; set; }
    
        public virtual Actuator Actuator { get; set; }
        public virtual Scenario Scenario { get; set; }
    }
}
