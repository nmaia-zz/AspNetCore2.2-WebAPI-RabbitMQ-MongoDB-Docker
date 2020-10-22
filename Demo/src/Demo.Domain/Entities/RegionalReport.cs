﻿using System.Collections.Generic;

namespace Demo.Domain.Entities
{
    public class RegionalReport : Report
    {
        public Dictionary<string, decimal> PercentagePerName { get; set; }
    }
}