using AutoMapper;
using Demo.API.DTO;
using Demo.Domain.Entities;

namespace Demo.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Research, PesquisaDto>().ReverseMap();

            // exemplo de mapeamenteo de propriedades complexas (via relacionamento entre entidades)
            /*
             
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));             

             */
        }
    }
}
