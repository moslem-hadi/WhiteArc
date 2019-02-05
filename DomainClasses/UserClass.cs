using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClasses
{
  public  class UserClass
    {
        public int KishUserID
        {
            get;
            set;
        }


        public string UserName
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }

        public bool IsAdmin
        {
            get;
            set;
        }

        public UserClass()
        {
            KishUserID = 0;
            UserName = "";
            FullName = "";
            Type = "";
            IsAdmin = false;

        }

    }
}
