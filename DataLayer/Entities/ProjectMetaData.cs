using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace DataLayer.Entities
{
public    class ProjectMetaData
    {
        //public ProjectMetaData()
        //{
        //    MinPrice = 0;MaxPrice = 0;DeadLine = 0;
            
        //}
        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ID { get; set; }

         


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string UserID { get; set; }


        [Required(ErrorMessage = "فیلد اجباری", AllowEmptyStrings = false)]
        [DisplayName("عنوان ")]
        [Display(Name = "عنوان ")]
        [StringLength(100, ErrorMessage = "حداکثر 100 کاراکتر")]
        public string Title { get; set; }



        [DisplayName("قیمت")]
        [Range(0, int.MaxValue, ErrorMessage = "دسته بندی را انتخاب کنید")]
        public int CategoryID { get; set; }


          
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Nullable<int> ExpertiseGuarantee { get; set; }
        public short DeadLine { get; set; }
        public bool AlloweOverDeadLine { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public byte ProjectStatus { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public byte ProgressStatus { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public short OffersCount { get; set; }



        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int OffersAvgAmount { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public Nullable<int> AcceptedOfferID { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public int ViewCount { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string RegDate { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string ApprovedDate { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public System.DateTime RenewDateEn { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public System.DateTime ExpireDateEn { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string RejectionMsg { get; set; }

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string Url { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public System.DateTime LastOfferDateEn { get; set; }


        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string SeoTitle { get; set; }

        [ScaffoldColumn(false)]
        [Bindable(false)]
        public string SeoDescription { get; set; }



        [AllowHtml]
        [DisplayName("متن ")]
        [Display(Name = "متن ")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool SeHiddenBadge { get; set; }
        [DefaultValue(false)]
        public bool PrivateBadge { get; set; }
        [DefaultValue(false)]
        public bool ContactAllowBadge { get; set; }
        [DefaultValue(false)]
        public bool UrgentBadge { get; set; }
        [DefaultValue(false)]
        public bool SpecialBadge { get; set; }
        [DefaultValue(false)]
        public bool LadderBadge { get; set; }
        [DefaultValue(false)]
        public bool RecruiteBadge { get; set; }

        [DefaultValue(false)]
        public bool HiddenOffersBadge { get; set; } 
    }
}
