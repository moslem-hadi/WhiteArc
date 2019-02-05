using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class UpgradePageViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int OfferCount { get; set; }
        public int Price { get; set; }
        public int FreelancerCommission { get; set; }
        public int EmployerCommission { get; set; }
        public int Skills { get; set; }
        public int PortfolioCount { get; set; }
        public int LevelCode { get; set; }
    }
}
