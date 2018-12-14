using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.MongoDB.Modelo
{
    public class ReporteCliente
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("IdUsuario")]
        public String IdUsuario { get; set; }
        [BsonElement("FechaDesde")]
        public String FechaDesde { get; set; }
        [BsonElement("FechaHasta")]
        public String FechaHasta { get; set; }
        [BsonElement("Consumo")]
        public Decimal Consumo { get; set; }
    }
}
