using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
  public   class PostUiViewModel
    {

        public int ID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string Text { get; set; }
        public string KeyWords { get; set; }
        public string SeoDescription { get; set; }
        public string Pic { get; set; }
        public string Cover { get; set; } 
        public string RobotAccess { get; set; }
        public bool OnlyUserCanComment { get; set; }
        public bool IsCommentActive { get; set; }
        public int ViewCount { get; set; }
        public string Brief { get; set; } 
        public bool IsPublished { get; set; }
        public System.DateTime PublishDate { get; set; }
        public int LikeCount { get; set; }



    }
}
