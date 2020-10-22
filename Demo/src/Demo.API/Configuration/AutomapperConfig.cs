using AutoMapper;
using Demo.API.ViewModels;
using Demo.Domain.Entities;

namespace Demo.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<EntityBase, EntityBaseViewModel>().ReverseMap();
            CreateMap<Research, ResearchViewModel>().ReverseMap();
            CreateMap<Person, PersonViewModel>().ReverseMap();
            CreateMap<RegionalReport, RegionalReportViewModel>().ReverseMap();
            CreateMap<AncestorsReport, AncestorsReportViewModel>().ReverseMap();
            CreateMap<ChildrenReport, ChildrenReportViewModel>().ReverseMap();
            CreateMap<ParentsReport, ParentsReportViewModel>().ReverseMap();
        }
    }
}
