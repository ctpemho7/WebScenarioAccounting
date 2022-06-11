﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebScenarioAccountingEntities : DbContext
    {
        public WebScenarioAccountingEntities()
            : base("name=WebScenarioAccountingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actuator> Actuators { get; set; }
        public virtual DbSet<ClassifierSensor> ClassifierSensors { get; set; }
        public virtual DbSet<ClassifierSex> ClassifierSexes { get; set; }
        public virtual DbSet<ClassifierSign> ClassifierSigns { get; set; }
        public virtual DbSet<ClassifierSubTypeThing> ClassifierSubTypeThings { get; set; }
        public virtual DbSet<ClassifierThingCommand> ClassifierThingCommands { get; set; }
        public virtual DbSet<ClassifierTimeWeather> ClassifierTimeWeathers { get; set; }
        public virtual DbSet<ClassifierTypeThing> ClassifierTypeThings { get; set; }
        public virtual DbSet<ClassifierUserType> ClassifierUserTypes { get; set; }
        public virtual DbSet<ClassifierValue> ClassifierValues { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Scenario> Scenarios { get; set; }
        public virtual DbSet<SensorCondition> SensorConditions { get; set; }
        public virtual DbSet<SensorMN> SensorMNs { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<TimeWeatherCondition> TimeWeatherConditions { get; set; }
    }
}
