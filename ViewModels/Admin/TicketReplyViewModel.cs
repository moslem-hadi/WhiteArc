using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TicketReplyViewModel
    {
        public TicketReplyViewModel()
        {
            ReplyList = new List<Replies>();
        }
        public List<Replies> ReplyList { get; set; }
        public string TicketTitle { get; set; }
        public string TicketStatus { get; set; }
            public string TicketPart { get; set; }
        public string TicketLastUpdate { get; set; }
    }
    public class Replies
    {
        public int ID { get; set; }
        public string SendDate { get; set; }
        public string Text { get; set; }
        public string FileName { get; set; }
        public bool IsManageReply { get; set; }
    }
}
