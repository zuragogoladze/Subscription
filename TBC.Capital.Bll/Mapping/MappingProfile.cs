using AutoMapper;
using TBC.Capital.Core.Utils;
using TBC.Capital.Core.ViewModels.API;
using TBC.Capital.Dal.Models.Content;

namespace TBC.Capital.Bll.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<News, NewsVM>()
                 .ForMember(sli => sli.Image, opt => opt.MapFrom(c => string.IsNullOrWhiteSpace(c.Image) ? "" : Settings.siteUrl + c.Image))
                 .ForMember(sli => sli.CoverImage, opt => opt.MapFrom(c => string.IsNullOrWhiteSpace(c.CoverImage) ? "" : Settings.siteUrl + c.CoverImage))
                 .ForMember(sli => sli.ShareImage, opt => opt.MapFrom(c => string.IsNullOrWhiteSpace(c.ShareImage) ? "" : Settings.siteUrl + c.ShareImage));
            CreateMap<News, SearchVM>()
                .ForMember(sli => sli.Image, opt => opt.MapFrom(c => string.IsNullOrWhiteSpace(c.Image) ? "" : Settings.siteUrl + c.Image))
                .ForMember(sli => sli.SearchCategory, opt => opt.MapFrom(c => SearchCategory.News))
                .ForMember(sli => sli.Date, opt => opt.MapFrom(c => c.PublishDate));
        }
    }
}
