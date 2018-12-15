using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.MongoDB.Modelo
{
    public class ReporteTransformador
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Codigo")]
        public int Codigo { get; set; }
        [BsonElement("FechaDesde")]
        public string FechaDesde { get; set; }
        [BsonElement("FechaHasta")]
        public string FechaHasta { get; set; }
        [BsonElement("Consumo")]
        public Decimal Consumo { get; set; }
    }
}
