using AutoMapper;
using Vila.WebApi.Dtos;
using Vila.WebApi.Utility;

namespace Vila.WebApi.Mappings
{
    public class ModelsMapper :Profile
    {
        public ModelsMapper()
        {
            CreateMap<Models.Vila, VilaDto>().
               ForMember(x => x.BuildDate, s => s.MapFrom(src => src.BuildDate.ToPersainDate()))
               .ReverseMap().
                ForMember(x => x.BuildDate, s => s.MapFrom(src => src.BuildDate.ToEnglishDateTime()));

            CreateMap<Models.Detail, DetailDto>().ReverseMap();

            CreateMap<Models.Vila, VilaSearchDto>().
                ForMember(x => x.BuildDate, s => s.MapFrom(src => src.BuildDate.ToPersainDate()));


            CreateMap<Models.Customer, LoginResultDto>();
        }
    }
}
