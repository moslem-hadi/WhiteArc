using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{ 
     public class PageUiDetails
    {
        public int ID { get; set; } 
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public string RobotAccess { get; set; }
        public bool OnlyUsersCanSee { get; set; }


    }
}
