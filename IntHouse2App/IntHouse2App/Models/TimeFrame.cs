using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace IntHouse2App.Models
{
    public enum TimeFrame{
        [Description("Latest day")]
        LatestHour,
        [Description("Latest day")]
        LatestDay,
        [Description("Latest week")]
        LatestWeek
    }
}
