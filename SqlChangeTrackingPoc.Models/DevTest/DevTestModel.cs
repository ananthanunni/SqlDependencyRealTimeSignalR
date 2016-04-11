using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Models.DevTest
{
    public class DevTestModel
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
