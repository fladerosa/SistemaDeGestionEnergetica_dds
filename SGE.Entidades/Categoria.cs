namespace SGE.Entidades
{
    public class Categoria
    {
        public string Codigo { get; set; }
        public decimal ConsumoMinimo { get; set; }
        public decimal ConsumoMaximo { get; set; }
        public decimal CostoFijo { get; set; }
        public decimal CostoVariable { get; set; }

        public bool ConsumoDentroDeLosLimites(decimal consumo)
        {
            return this.ConsumoMinimo < consumo && consumo < this.ConsumoMaximo;
        }

        public decimal CalcularFacturaMensual(decimal consumo)
        {
            return this.CostoFijo + (this.CostoVariable * consumo);
        }
    }
}
