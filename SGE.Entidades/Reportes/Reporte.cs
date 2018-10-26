using SGE.Entidades.Dispositivos;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;

namespace SGE.Entidades.Reportes
{
    public static class Reporte {
        //• Consumo por hogar/periodo.
        public static decimal consumoPorHogarYPeriodo(int idUsuario, DateTime fechaDesde, DateTime fechaHasta) {
            BaseRepositorio<Cliente> repoCliente = new BaseRepositorio<Cliente>();
            decimal consumo = 0;

            Cliente cliente = repoCliente.Single(u => u.Id == idUsuario);

            foreach (Inteligente inteligente in cliente.Inteligentes) {
                consumo += inteligente.ObtenerConsumoPeriodo(fechaDesde, fechaHasta);
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

            List<Inteligente> inteligentes = repoInteligente.GetAll();

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

            Transformador transformador = repoTransformador.Single(t => t.Id == idTransformador);

            if(transformador != null) {
                foreach (Cliente cliente in transformador.Clientes) {
                    consumo += consumoPorHogarYPeriodo(cliente.Id, fechaDesde, fechaHasta);
                }
            }

            return consumo;
        }
    }
}
