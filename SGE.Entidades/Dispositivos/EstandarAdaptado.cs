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
    }
}
