using Demo.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace Demo.Infra.Mappings
{
    public static class ResearchMap
    {
        public static void ConfigureMap()
        {
            BsonClassMap.RegisterClassMap<Entity>(map => {

                map.AutoMap();
                map.MapIdMember(x => x.Id);

            });

            BsonClassMap.RegisterClassMap<Research>(map => {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Nome);
                map.MapMember(x => x.Sobrenome);
                map.MapMember(x => x.Cor);
                map.MapMember(x => x.Escolaridade);
                map.MapMember(x => x.Filiacao);
                map.MapMember(x => x.Filhos);

            });
        }
    }
}
