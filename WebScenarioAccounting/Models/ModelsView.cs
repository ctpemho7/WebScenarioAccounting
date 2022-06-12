using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScenarioAccounting.Models
{
    public class TimeWeatherView
    {
        public int ConditionID;
        public string Name;
        public string Value;
    }

    public class ActionView
    {
        public int ActuatorID;
        public string Manufacturer;
        public string ActuatorName;
        public string RoomName;
        public string CommandText;

        public bool isElse;
    }
}