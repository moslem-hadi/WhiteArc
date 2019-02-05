using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class ProductGroupDetailUiViewModel
    { 
        public int ID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string FullTitle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }   
        public string SeoKeywords { get; set; }
        public string Pic { get; set; } 


    }
}
