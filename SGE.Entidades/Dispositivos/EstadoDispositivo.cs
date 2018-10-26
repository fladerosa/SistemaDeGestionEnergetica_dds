using System.ComponentModel.DataAnnotations;

namespace SGE.Entidades.Dispositivos
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
