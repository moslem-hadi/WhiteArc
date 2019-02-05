using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class PostListViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public string PublishDate { get; set; }
        public string Url { get; set; }
        public string Brief { get; set; }
        public int LikeCount { get; set; }

        public int TotalCount { get; set; }

    }


}
