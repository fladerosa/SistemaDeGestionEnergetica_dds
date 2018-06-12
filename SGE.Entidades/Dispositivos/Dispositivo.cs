namespace SGE.Entidades.Dispositivos
{
    public abstract class Dispositivo
    {
        #region Propiedades
        public string Nombre { get; set; }
        public decimal ConsumoEnergia { get; set; }
        #endregion Propiedades

        #region Constructor
        protected Dispositivo(string nombre, decimal consumo)
        {
            this.Nombre = nombre;
            this.ConsumoEnergia = consumo;
        }
        #endregion Constructor
    }
}
