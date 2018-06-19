namespace SGE.Entidades.Reglas
{
    public class Condicion
    {
        #region Propiedades

        public Sensor Sensor { get; set; }
        public decimal Valor { get; set; }
        public OperadorEnum Operador { get; set; }

        #endregion

        #region Constructores

        public Condicion(Sensor sensor, decimal valor, OperadorEnum operador)
        {
            this.Sensor = sensor;
            this.Valor = valor;
            this.Operador = operador;
        }

        #endregion

        #region Evaluación

        public bool Evaluar()
        {
            switch (Operador)
            {
                case OperadorEnum.MAYOR:
                    return Sensor.Medir() > Valor;
                case OperadorEnum.MENOR:
                    return Sensor.Medir() < Valor;
                case OperadorEnum.IGUAL:
                    return Sensor.Medir() == Valor;
                default:
                    return Sensor.Medir() != Valor;
            }
        }

        #endregion
    }
}
