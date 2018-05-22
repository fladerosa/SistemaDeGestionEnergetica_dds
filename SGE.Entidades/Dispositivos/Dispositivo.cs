namespace SGE.Entidades.Dispositivos
{
    public abstract class Dispositivo
    {
        #region Propiedades

        public string Nombre { get; set; }
        public abstract decimal ConsumoEnergia { get; set; }

        #endregion
    }
}
