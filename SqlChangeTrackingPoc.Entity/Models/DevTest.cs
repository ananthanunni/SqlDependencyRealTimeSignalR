using System;
using System.Collections.Generic;

namespace SqlChangeTrackingPoc.Entity.Models
{
    public partial class DevTest
    {
        public int ID { get; set; }
        public string CampaignName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Clicks { get; set; }
        public Nullable<int> Conversions { get; set; }
        public Nullable<int> Impressions { get; set; }
        public string AffiliateName { get; set; }
    }
}
