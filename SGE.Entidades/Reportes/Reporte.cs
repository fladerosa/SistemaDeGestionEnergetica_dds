using SGE.Entidades.Contexto;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SGE.Entidades.Reportes
{
    public static class Reporte {
        //• Consumo por hogar/periodo.
        public static decimal consumoPorHogarYPeriodo(int idUsuario, DateTime fechaDesde, DateTime fechaHasta) {
            SGEContext context = new SGEContext();
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>(context);
            decimal consumo = 0;

            var includesCliente = new List<Expression<Func<Cliente, object>>>() {
                c => c.Inteligentes
            };
            Cliente cliente = repoCliente.Single(u => u.Id == idUsuario, includesCliente);

            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                    i => i.RegistroDeActivaciones
                };

            foreach (Inteligente inteligente in cliente.Inteligentes) {
                BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>(context);
                Inteligente inte = repoInteligente.Single(i => i.Id == inteligente.Id, includesInteligente);

                consumo += inte.ObtenerConsumoPeriodo(fechaDesde, fechaHasta);
            }

            foreach (Estandar estandar in cliente.Estandars) {
                consumo += estandar.ConsumoAproximado((int)Math.Ceiling(fechaHasta.Subtract(fechaDesde).TotalHours));
            }

            return consumo;
        }

        //• Consumo promedio por tipo de dispositivo(inteligente o estándar) por periodo.
        //TODO: se deja como string para reconocer el tipo de dispositivo, sin embargo se podría sacar del nombre de una clase informada
        public static decimal consumoPorTipoDeDispositivoPorPeriodo(string tipoDispositivo, DateTime fechaDesde, DateTime fechaHasta) {
            switch (tipoDispositivo.ToUpper()) {
                case "INTELIGENTE":
                    return consumoDispositivosInteligentesPorPeriodo(fechaDesde, fechaHasta);

                case "ESTANDAR":
                    return consumoDispositivosEstandarsPorPeriodo(fechaDesde, fechaHasta);

                default:
                    //TODO: consultar si es correcto esto, o debería generar excepción
                    return 0;
            }
        }

        private static decimal consumoDispositivosInteligentesPorPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            decimal consumo = 0;
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();
            var includesInteligente = new List<Expression<Func<Inteligente, object>>>() {
                    i => i.RegistroDeActivaciones
                };
            List<Inteligente> inteligentes = repoInteligente.GetAll(includesInteligente);

            foreach (Inteligente inteligente in inteligentes) {
                consumo += inteligente.ObtenerConsumoPeriodo(fechaDesde, fechaHasta);
            }

            return consumo;
        }

        private static decimal consumoDispositivosEstandarsPorPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            decimal consumo = 0;
            BaseRepositorio<Estandar> repoEstandar = new BaseRepositorio<Estandar>();

            List<Estandar> estandars = repoEstandar.GetAll();

            foreach (Estandar estandar in estandars) {
                consumo += estandar.ConsumoAproximado((int)Math.Ceiling(fechaHasta.Subtract(fechaDesde).TotalHours));
            }

            return consumo;
        }

        //• Consumo por transformador por periodo.
        public static decimal consumoTransformadorPorPeriodo(int idTransformador, DateTime fechaDesde, DateTime fechaHasta) {
            decimal consumo = 0;
            BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
            var includesTransformador = new List<Expression<Func<Transformador, object>>>() {
                    t => t.Clientes
                };
            Transformador transformador = repoTransformador.Single(t => t.Id == idTransformador, includesTransformador);

            if(transformador != null) {
                foreach (Cliente cliente in transformador.Clientes) {
                    consumo += consumoPorHogarYPeriodo(cliente.Id, fechaDesde, fechaHasta);
                }
            }

            return consumo;
        }
    }
}
