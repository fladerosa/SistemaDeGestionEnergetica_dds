namespace SGE.Entidades
{
    public class Categoria
    {
        public string Codigo { get; set; }
        public double ConsumoMinimo { get; set; }
        public double ConsumoMaximo { get; set; }
        public double CostoFijo { get; set; }
        public double CostoVariable { get; set; }

        public bool ConsumoDentroDeLosLimites(double consumo)
        {
            return this.ConsumoMinimo < consumo && consumo < this.ConsumoMaximo;
        }

        public double CalcularFacturaMensual(double consumo)
        {
            return this.CostoFijo + (this.CostoVariable * consumo);
        }
    }
}
