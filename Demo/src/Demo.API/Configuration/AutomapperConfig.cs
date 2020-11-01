using AutoMapper;
using Demo.API.ViewModels;
using Demo.Domain.Entities;
using Demo.Infra.DTO;

namespace Demo.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Research, ResearchViewModel>().ReverseMap();
            CreateMap<Person, PersonViewModel>().ReverseMap();
            CreateMap<AncestorsTree, AncestorsReportViewModel>().ReverseMap();
            CreateMap<ChildrenTree, ChildrenReportViewModel>().ReverseMap();
            CreateMap<ParentsTree, ParentsReportViewModel>().ReverseMap();
            CreateMap<FilteredResearchGrouped, FilteredResearchGroupedViewModel>().ReverseMap();
        }
    }
}
