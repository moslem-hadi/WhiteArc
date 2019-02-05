using AutoMapper;
using DataLayer; 

namespace ViewModels
{
    public static class Mapping
    {

        //این بررسی شود که کانفیگ قرارداده شود
        //https://cpratt.co/using-automapper-getting-started/


        #region PageContent
        public static PageContent ToEntity(this PageViewModel model)
        {
            Mapper.CreateMap<PageContent, PageViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap(); ;
            return Mapper.Map<PageViewModel, PageContent>(model);  
        }
        

        public static PageViewModel ToModel(this PageContent entity)
        {
            Mapper.CreateMap<PageViewModel, PageContent>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<PageContent, PageViewModel>(entity);
        }
        public static PageUiDetails ToUiModel(this getPageByUrl_Result model)
        {
            Mapper.CreateMap<PageUiDetails, getPageByUrl_Result>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<getPageByUrl_Result, PageUiDetails>(model);  
        }

        #endregion


        #region Post
        public static Post ToEntity(this PostViewModel model)
        {
            Mapper.CreateMap<Post, PostViewModel>()
                    .ForMember(dest => dest.SelectedGroupIDs, mo => mo.Ignore()).ReverseMap();
            return Mapper.Map<PostViewModel, Post>(model);
        }


        public static PostViewModel ToModel(this Post entity)
        {
            Mapper.CreateMap<PostViewModel, Post>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<Post, PostViewModel>(entity);
        }



        public static PostUiViewModel ToUiModel(this Post entity)
        {
            Mapper.CreateMap<PostUiViewModel, Post>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<Post, PostUiViewModel>(entity);
        }
        #endregion



         




        

          


        #region PostGroup
        public static PostGroupViewModel ToModel(this PostGroup entity)
        {
            Mapper.CreateMap<PostGroupViewModel, PostGroup>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<PostGroup, PostGroupViewModel>(entity);
        }

        public static PostGroup ToEntity(this PostGroupViewModel model)
        {
            Mapper.CreateMap<PostGroup, PostGroupViewModel>()
                   .IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<PostGroupViewModel, PostGroup>(model);
        }

        #endregion



        #region Product
        public static Product ToEntity(this ProductViewModel model)
        {
            Mapper.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Locales, mo => mo.Ignore()).ReverseMap();
            return Mapper.Map<ProductViewModel, Product>(model);
        }


        public static ProductViewModel ToModel(this Product entity)
        {
            Mapper.CreateMap<ProductViewModel, Product>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<Product, ProductViewModel>(entity);
        }
        #endregion




        public static ContactPM ToEntity(this ContactFormViewModel model)
        {
            Mapper.CreateMap<ContactPM, ContactFormViewModel>()
                    .ReverseMap();
            return Mapper.Map<ContactFormViewModel, ContactPM>(model);
        }


        #region ProductGroup
        public static ProductGroup ToEntity(this ProductGroupViewModel model)
        {
            Mapper.CreateMap<ProductGroup, ProductGroupViewModel>() 
                    .ForMember(dest => dest.Locales, mo => mo.Ignore()).ReverseMap();
            return Mapper.Map<ProductGroupViewModel, ProductGroup>(model);
        }


        public static ProductGroupViewModel ToModel(this ProductGroup entity)
        {
            Mapper.CreateMap<ProductGroupViewModel, ProductGroup>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<ProductGroup, ProductGroupViewModel>(entity);
        }


        public static ProductGroupDetailUiViewModel ToUiModel(this getGroupDetailsByUrl_Result entity)
        {
            Mapper.CreateMap<ProductGroupDetailUiViewModel, getGroupDetailsByUrl_Result>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<getGroupDetailsByUrl_Result, ProductGroupDetailUiViewModel>(entity);
        }
        #endregion



        #region DownloadFile
        public static DownloadFile ToEntity(this DownloadFileListViewModel model)
        {
            Mapper.CreateMap<DownloadFile, DownloadFileListViewModel>() .ReverseMap();
            return Mapper.Map<DownloadFileListViewModel, DownloadFile>(model);
        }


        public static DownloadFileListViewModel ToModel(this DownloadFile entity)
        {
            Mapper.CreateMap<DownloadFileListViewModel, DownloadFile>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<DownloadFile, DownloadFileListViewModel>(entity);
        }



        #endregion

        #region UsersData
        public static UsersData ToEntity(this AddUserViewModel model)
        {
            Mapper.CreateMap<UsersData, AddUserViewModel>() .ReverseMap();
            return Mapper.Map<AddUserViewModel, UsersData>(model);
        }


        public static AddUserViewModel ToModel(this UsersData entity)
        {
            Mapper.CreateMap<AddUserViewModel, UsersData>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
            return Mapper.Map<UsersData, AddUserViewModel>(entity);
        }



        #endregion

        //public static LanquageViewModel ToModel(this Language entity)
        //{
        //    Mapper.CreateMap<LanquageViewModel, Language>().IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
        //    return Mapper.Map<Language, LanquageViewModel>(entity);
        //}


    }
}
