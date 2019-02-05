using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class ProductGroupListUiViewModel
    {
        
        public int ID { get; set; } 
        public string Title { get; set; }
        public string Url { get; set; }
        public string Pic { get; set; }

        public ProductGroupListUiViewModel(int iD, string title, string url, string pic)
        {
            ID = iD; 
            Title = title;
            Url = url;
            Pic = pic;
        }
    }
}
