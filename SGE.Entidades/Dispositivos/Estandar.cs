namespace SGE.Entidades.Dispositivos
{
    public class Estandar : Dispositivo
    {
        #region Constructor
        public Estandar(string nombre, decimal consumo) : base(nombre, consumo)
        {
            
        }
        #endregion Constructor

        public decimal ConsumoAproximado(int horas)
        {
            return this.ConsumoEnergia * horas;
        }
    }
}
