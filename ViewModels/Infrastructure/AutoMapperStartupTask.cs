using AutoMapper;
using DataLayer; 

namespace ViewModels.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {

        public int Order
        {
            get { return 0; }
        }

        public void Execute()
        {


            //var config = new MapperConfiguration(cfg =>
            //            cfg.CreateMap<PageContent, PageViewModel>().ForMember(dest => dest.Locales, mo => mo.Ignore())
            //            );
            //    var config = new MapperConfiguration(cfg => {
            //        cfg.CreateMap<PageViewModel, PageContent>();
            //    });

            //    IMapper mapper = config.CreateMapper();
            //    var source = new PageViewModel();
            //    var dest = mapper.Map<PageViewModel, PageContent>(source);


            //Mapper.CreateMap<PageContent, PageViewModel>()
            //    .ForMember(dest => dest.WidgetWrapContent, mo => mo.MapFrom(x => x.WidgetWrapContent.HasValue ? x.WidgetWrapContent.Value : true))
            //    .ForMember(dest => dest.Url, mo => mo.Ignore())
            //    .ForMember(dest => dest.Locales, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
            //    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableWidgetZones, mo => mo.Ignore())
            //    .ForMember(dest => dest.AvailableTitleTags, mo => mo.Ignore());
            //Mapper.CreateMap<PageViewModel, PageContent>();
        }
    }
}
