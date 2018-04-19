namespace SGE.Entidades
{
    public class Dispositivo
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
    }
}
