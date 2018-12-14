using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.MongoDB.Modelo
{
    public class ReporteDispositivo
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Tipo")]
        public String Tipo { get; set; }
        [BsonElement("FechaDesde")]
        public String FechaDesde { get; set; }
        [BsonElement("FechaHasta")]
        public String FechaHasta { get; set; }
        [BsonElement("Consumo")]
        public Decimal Consumo { get; set; }
    }
}
