using CRUDEMPLEADOS.Models;
using CRUDEMPLEADOS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDEMPLEADOS.Controllers
{
    public class HomeController : Controller
    {
        private readonly EMPLEADOSContext contexto;

        public HomeController(EMPLEADOSContext context)
        {
            contexto = context;
        }

        public IActionResult Index()
        {
            List<Empleado> lista = contexto.Empleados.Include(c=>c.Cargo).ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Detalles( int IdEmpleado)
        {
            EmpleadoVM empleadoVM = new EmpleadoVM()
            {
                empleado = new Empleado(),
                listaCargo = contexto.Cargos.Select(c => new SelectListItem()
                {
                    Text = c.Descripcion,
                    Value = c.IdCargo.ToString()
                }).ToList()
            };
            if (IdEmpleado != 0)
            {
                empleadoVM.empleado=contexto.Empleados.Find(IdEmpleado);
            }




            return View(empleadoVM);

        }
        [HttpPost]
        public IActionResult Detalles(EmpleadoVM empleadoVM)
        {
            if (empleadoVM.empleado.IdEmpleado == 0)
            {
                contexto.Empleados.Add(empleadoVM.empleado);

            }
            else
            {
                contexto.Empleados.Update(empleadoVM.empleado);
            }

            contexto.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Eliminar(int IdEmpleado)
        {
            Empleado em = contexto.Empleados.Include(c => c.Cargo).Where(e => e.IdEmpleado == IdEmpleado).FirstOrDefault();
            return View(em);

        }

        [HttpPost]
        public IActionResult Eliminar(Empleado empleado)
        {
            contexto.Empleados.Remove(empleado);
            contexto.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}