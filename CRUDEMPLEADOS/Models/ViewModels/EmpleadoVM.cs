using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDEMPLEADOS.Models.ViewModels
{
    public class EmpleadoVM
    {
        public Empleado empleado { get; set; }

        public List<SelectListItem> listaCargo { get; set; }
    }
}
