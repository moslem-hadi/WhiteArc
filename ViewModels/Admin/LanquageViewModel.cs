using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LanquageViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Rtl { get; set; } 
    }
}
