using System.ComponentModel.DataAnnotations;

namespace SGE.WebconAutenticacion.Dispositivos
{
    public enum EstadoDispositivo
    {
        [Display(Name = "Encendido")]
        Encendido = 0,
        [Display(Name = "Apagado")]
        Apagado = 1,
        [Display(Name = "AhorroEnergia")]
        AhorroEnergia = 2
     }
}
