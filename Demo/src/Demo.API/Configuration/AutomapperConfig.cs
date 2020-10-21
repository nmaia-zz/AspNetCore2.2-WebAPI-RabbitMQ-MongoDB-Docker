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

            // exemplo de mapeamenteo de propriedades complexas (via relacionamento entre entidades)
            /*
             
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));             

             */
        }
    }
}
