namespace SGE.Entidades.Dispositivos
{
    public class EstandarAdaptado : Inteligente
    {
        #region Propiedades

        private Estandar Dispositivo { get; set; }

        #endregion

        #region Constructores

        public EstandarAdaptado(Estandar dispositivo) : base(dispositivo.Nombre, dispositivo.ConsumoEnergia)
        {
            this.Dispositivo = dispositivo;
        }

        #endregion
    }
}
