namespace SGE.Entidades
{
    public abstract class Dispositivo
    {
        public string Nombre { get; set; }

        public abstract decimal ObtenerConsumoEnergia();
        public string ObtenerNombre()
        {
            return this.Nombre;
        }
    }
    /*public class Dispositivo
    {
        public Dispositivo()
        {
            this.EstaEncendido = false;
        }

        public string Nombre { get; set; }
        public decimal ConsumoEnergia { get; set; }
        public bool EstaEncendido { get; set; }

        public void Accionar()
        {
            this.EstaEncendido = !this.EstaEncendido;
        }

        public bool EstaPrendido()
        {
            return this.EstaEncendido;
        }

        public decimal obtenerConsumoEnergia()
        {
            return this.ConsumoEnergia;
        }

        public string ObtenerNombre()
        {
            return this.Nombre;
        }
    }*/
}
