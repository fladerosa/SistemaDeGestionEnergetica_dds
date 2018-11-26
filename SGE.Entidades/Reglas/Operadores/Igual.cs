namespace SGE.Entidades.Reglas.Operadores {
    public class Igual : Operador {
        public override bool Verificar(string referencia, string valor) {
            return valor == referencia;
        }
    }
}
