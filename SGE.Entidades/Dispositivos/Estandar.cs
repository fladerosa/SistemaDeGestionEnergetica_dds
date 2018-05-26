namespace SGE.Entidades.Dispositivos
{
    public class Estandar : Dispositivo
    {
        #region Propiedades
        public decimal PromedioUsoDiario { get; set; }
        #endregion


        #region Constructor
        public Estandar(string nombre, decimal consumo) : base(nombre, consumo)
        {
            
        }
        #endregion Constructor
    }
}
