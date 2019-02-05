using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ViewModels
{
    [MetadataType(typeof(DataLayer.Entities.DownloadFileMetaData))] //خوندن متادیتا ها
    public class DownloadFileListViewModel
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Size { get; set; }
        public string PicUrl { get; set; }
        public string FileUrl { get; set; }
        public int DownloadCount { get; set; }
        public bool IsPrivate { get; set; }
        public int Priority { get; set; }
        public string GroupName { get; set; }

         
    }
}
