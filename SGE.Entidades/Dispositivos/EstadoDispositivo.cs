using System.ComponentModel.DataAnnotations;

namespace SGE.Entidades.Dispositivos
{
    public enum EstadoDispositivo
    {
        [Display(Name = "Apagado")]
        Apagado = 0,
        [Display(Name = "Encendido")]
        Encendido = 1,
        [Display(Name = "AhorroEnergia")]
        AhorroEnergia = 2
     }
}
