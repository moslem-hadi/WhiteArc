using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ProductListUiViewModel
    { 
        public int ID { get; set; }
        public string Title { get; set; } 
        public string Url { get; set; }
        public string Pic { get; set; } 
        public string GroupTitle { get; set; } 
        public string GroupUrl { get; set; } 
         

        public ProductListUiViewModel(int iD, string title, string url, string pic,string groupTitle, string groupUrl)
        {
            ID = iD;
            Title = title; 
            Url = url;
            Pic = pic;
            GroupTitle = groupTitle;
            GroupUrl = groupUrl;
        }
    }
}
