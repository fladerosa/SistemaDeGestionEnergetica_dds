namespace SGE.Entidades.Reglas.Operadores {
    public class MenorOIgual : Operador {
        public override bool Verificar(string referencia, string valor) {
            return decimal.Parse(valor) <= decimal.Parse(referencia);
        }
    }
}
