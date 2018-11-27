using SGE.Core.Helpers;
using SGE.Entidades.Contexto;
using SGE.Entidades.Managers;
using SGE.Entidades.Reglas;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Dispositivos {
    [Table(name: "Inteligente")]
    public class Inteligente : Dispositivo {
        #region Propiedades

        DispositivosManager dispositivosManager;
        [NotMapped]
        public SGEContext Context { get; set; }

        /// <summary>
        /// Indica el estado del dispositivo
        /// </summary>
        public EstadoDispositivo Estado { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; } //many to many con Clientes
        public virtual ICollection<Activacion> RegistroDeActivaciones { get; set; }

        public virtual ICollection<Regla> Reglas { get; set; }

        public int CatalogoId { get; set; } // fk con tabla Catalogo
        [ForeignKey("CatalogoId")]
        public virtual Catalogo Catalogo { get; set; } // one to many con Catalogo
        /// <summary>
        /// Devuelve un valor que indica si el equipo esta encendido
        /// </summary>
        public bool EstaPrendido {
            get {
                return this.Estado == EstadoDispositivo.Encendido;
            }
        }

        /// <summary>
        /// Devuelve un valor que indica si el equipo esta encendido
        /// </summary>
        public bool EstaApagado {
            get {
                return this.Estado == EstadoDispositivo.Apagado;
            }
        }

        /// <summary>
        /// Devuelve un valor que indicasiel equipo esta en modo ahorro de energia
        /// </summary>
        public bool EstaEnModoAhorroEnergia {
            get {
                return this.Estado == EstadoDispositivo.AhorroEnergia;
            }
        }

        #endregion

        #region Constructor

        public Inteligente() {
            this.Clientes = new List<Cliente>();
            Context = new SGEContext();
            this.RegistroDeActivaciones = new List<Activacion>();
        }

        public Inteligente(string nombre, decimal consumo) : base(nombre, consumo) {
            this.Clientes = new List<Cliente>();
            this.RegistroDeActivaciones = new List<Activacion>();
            Context = new SGEContext();
        }

        public Inteligente(string nombre, string id) : base(nombre) {
            this.Clientes = new List<Cliente>();
            this.RegistroDeActivaciones = new List<Activacion>();
            this.ConsumoEnergia = Convert.ToDecimal(DispositivosHelper.GetInstace().Dispositivos.Where(x => x.Id == id).Single().Consumo);
            this.IdentificadorFabrica = id;
            Context = new SGEContext();
        }

        #endregion

        #region Funcionamiento

        /// <summary>
        /// Enciendo el equipo
        /// </summary>
        public void Encender() {
            if (this.Estado != EstadoDispositivo.Encendido) {
                this.Estado = EstadoDispositivo.Encendido;

                Activacion activacion = new Activacion() {
                    Estado = this.Estado,
                    Inteligente = this,
                    FechaDeRegistro = DateTime.Now,
                    InteligenteId = this.Id
                };

                //BaseRepositorio<Activacion> repoActivacion = new BaseRepositorio<Activacion>(Context);
                //repoActivacion.Create(activacion);

                this.RegistroDeActivaciones.Add(activacion);
            }
        }

        public List<string> ObtenerIntervalosEncendidoPorPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            List<string> intervalosEncendido = new List<string>();

            List<Activacion> activacionesDentroPeriodo = this.RegistroDeActivaciones.Where(a => a.FechaDeRegistro >= fechaDesde && a.FechaDeRegistro <= fechaHasta).ToList();
            Activacion activacion = null;

            for (var i = 0; i < activacionesDentroPeriodo.Count; i++) {
                activacion = activacionesDentroPeriodo[i];

                if (activacion.Estado == EstadoDispositivo.Encendido) {
                    //Busco la posicion siguiente para saber cuando se apagó, si no la encuentro asumo que nunca se apagó, por lo que asumo que sigue encendido hasta la fecha máxima indicada
                    if (activacionesDentroPeriodo.ElementAtOrDefault(i + 1) != null) {
                        intervalosEncendido.Add("Encendido desde '" + activacion.FechaDeRegistro + "' hasta '" + activacionesDentroPeriodo[i + 1].FechaDeRegistro + "' Consumo en hs: '" + Math.Truncate((activacionesDentroPeriodo[i + 1].FechaDeRegistro - activacion.FechaDeRegistro).TotalHours));
                    } else {
                        intervalosEncendido.Add("Encendido desde '" + activacion.FechaDeRegistro + "' hasta (al menos) '" + fechaHasta + "' Consumo en hs: '" + Math.Truncate((fechaHasta - activacion.FechaDeRegistro).TotalHours));
                    }
                }
            }

            return intervalosEncendido;
        }

        /// <summary>
        /// Apaga el equipo
        /// </summary>
        public void Apagar() {
            if (this.Estado != EstadoDispositivo.Apagado && this.Estado != EstadoDispositivo.AhorroEnergia) {
                this.Estado = EstadoDispositivo.Apagado;
                //this.Driver.Apagar();

                Activacion activacion = new Activacion(this.Estado) {
                    Inteligente = this,
                    FechaDeRegistro = DateTime.Now,
                    InteligenteId = this.Id
                };

                //BaseRepositorio<Activacion> repoActivacion = new BaseRepositorio<Activacion>(Context);
                //repoActivacion.Create(activacion);

                this.RegistroDeActivaciones.Add(activacion);
            }
        }

        /// <summary>
        /// Coloca el dispositivo en modo ahorro energía
        /// </summary>
        public void ColocarEnAhorroEnergia() {
            this.Estado = EstadoDispositivo.AhorroEnergia;
            //this.Driver.PonerEnModoAhorroEnergia();

            Activacion activacion = new Activacion(this.Estado) {
                Inteligente = this,
                FechaDeRegistro = DateTime.Now,
                InteligenteId = this.Id
            };

            //BaseRepositorio<Activacion> repoActivacion = new BaseRepositorio<Activacion>(Context);
            //repoActivacion.Create(activacion);

            this.RegistroDeActivaciones.Add(activacion);
        }

        public void LevantarDispositivosArchivo() {
            BaseRepositorio<Inteligente> repoInteligente = new BaseRepositorio<Inteligente>();

            foreach (Core.Entidades.Dispositivo dispositivo in DispositivosHelper.GetInstace().Dispositivos) {
                if (repoInteligente.Single(d => d.Nombre == dispositivo.Tipo) == null)
                    repoInteligente.Create(new Inteligente() {
                        Nombre = dispositivo.Tipo,
                        ConsumoEnergia = (decimal)dispositivo.Consumo,
                        IdentificadorFabrica = dispositivo.Id.Substring(0, 14)
                    });
            }
        }

        #endregion

        #region Estadisticas

        public decimal ObtenerConsumoDeUltimasNHoras(int cantidadHoras) {
            DateTime fechaBusqueda = (DateTime.Now).AddHours((-1) * cantidadHoras);
            if (this.RegistroDeActivaciones != null && this.RegistroDeActivaciones.Count > 0) {
                List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaBusqueda).ToList<Activacion>();
                return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
            }
            return 0;
        }

        public decimal ObtenerConsumoPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            if (this.RegistroDeActivaciones != null && this.RegistroDeActivaciones.Count > 0) {
                List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaDesde && x.FechaDeRegistro <= fechaHasta).ToList<Activacion>();
                if (lista == null || lista.Count == 0) {
                    Activacion ultimaActivacion = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro == (this.RegistroDeActivaciones.Max(y => y.FechaDeRegistro))).Single();
                    if (ultimaActivacion.Estado == EstadoDispositivo.Encendido) {
                        lista.Add(new Activacion() {
                            Estado = EstadoDispositivo.Encendido,
                            Inteligente = this,
                            FechaDeRegistro = fechaDesde,
                            InteligenteId = this.Id
                        });
                    }

                }
                return this.ConsumoEnergia * this.CalcularHorasDeUso(lista);
            }
            return 0;
        }

        public decimal ObtenerHorasPeriodo(DateTime fechaDesde, DateTime fechaHasta) {
            if (this.RegistroDeActivaciones != null && this.RegistroDeActivaciones.Count > 0) {
                List<Activacion> lista = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro >= fechaDesde && x.FechaDeRegistro <= fechaHasta).ToList<Activacion>();
                if (lista == null || lista.Count == 0) {
                    Activacion ultimaActivacion = this.RegistroDeActivaciones.Where(x => x.FechaDeRegistro == (this.RegistroDeActivaciones.Max(y => y.FechaDeRegistro))).Single();
                    if (ultimaActivacion.Estado == EstadoDispositivo.Encendido) {
                        lista.Add(new Activacion() {
                            Estado = EstadoDispositivo.Encendido,
                            Inteligente = this,
                            FechaDeRegistro = fechaDesde,
                            InteligenteId = this.Id
                        });
                    }

                }
                return this.CalcularHorasDeUso(lista);
            }
            return 0;
        }

        private decimal CalcularHorasDeUso(List<Activacion> lista) {
            int horas = 0;
            bool flag = false;
            Activacion activacionAnterior = null;

            foreach (Activacion registro in lista) {
                if (registro.Estado != EstadoDispositivo.Apagado) {
                    activacionAnterior = registro;
                    flag = true;
                } else if (registro.Estado == EstadoDispositivo.Apagado && flag) {
                    horas += Math.Abs(registro.FechaDeRegistro.Subtract(activacionAnterior.FechaDeRegistro).Hours);
                    flag = false;
                }
            }

            if (activacionAnterior != null && activacionAnterior.Estado != EstadoDispositivo.Apagado && flag)
                horas += Math.Abs(Convert.ToInt32(activacionAnterior.FechaDeRegistro.Subtract(DateTime.Now).TotalHours));

            return horas;
        }


        public double ObtenerCantidadDeHoraDeUsoMensual() {
            DateTime now = DateTime.Now;
            DateTime ini = new DateTime(now.Year, now.Month, 1);
            DateTime fin = ini.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            return decimal.ToDouble(this.ObtenerHorasPeriodo(ini, fin));
        }
        #endregion

        #region Nuevas Mediciones

        public void NotificarNuevaMedicion(string valor) {

        }

        #endregion

        #region Observer

        public void Agregar(DispositivosManager manager) {
            this.dispositivosManager = manager;
        }

        public void Quitar(Inteligente dispositivo) {
            this.dispositivosManager = null;
        }

        public void NotificarMedicion(decimal valor) {
            DispositivosManager.Instance.NotificarNuevaMedicion(this);
        }

        #endregion
    }
}
