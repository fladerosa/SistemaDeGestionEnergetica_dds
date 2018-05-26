namespace SGE.Entidades.Dispositivos
{
    public class EstandarAdaptado : Inteligente
    {

        #region Propiedades
        private Estandar Dispositivo { get; set; }
        #endregion Propiedades

        #region Constructor
        public EstandarAdaptado(Estandar dispositivo) : base(dispositivo.Nombre, dispositivo.ConsumoEnergia)
        {
            this.Dispositivo = dispositivo;
        }
        #endregion Constructor

        #region Funcionamiento
        // Falta agregar los metodos de calculo de consumo que sobrecargan el comportamiento de Inteligente
        // y redefine su comportamiento con datos del Estandar al cual adapta.
        #endregion Funcionamiento

        #region Estadisticas
        public decimal ObtenerConsumoEnergiaNHoras()
        {
            return this.Dispositivo.PromedioUsoDiario; // por ejemplo...
        }
        #endregion Estadisticas
    }
}
