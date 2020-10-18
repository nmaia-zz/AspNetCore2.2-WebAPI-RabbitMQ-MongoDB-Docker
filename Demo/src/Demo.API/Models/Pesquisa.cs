using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.API.Models
{
    public class Pesquisa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PesquisaId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cor { get; set; }
        public string Filiacao { get; set; }
        public string Filhos { get; set; }
        public string Escolaridade { get; set; }
    }
}
