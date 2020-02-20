using post_Segurida_MVC.Attribute;
using post_Segurida_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace post_Segurida_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        public List<Empleado> GetListaEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>          
            { 
              new Empleado{ IdEmpleado=1,NombreEmpleado="Juan",PuestoTrabajo="Desarrollador"},
              new Empleado{ IdEmpleado=2,NombreEmpleado="Maria",PuestoTrabajo="Operador de sistema"},
              new Empleado{ IdEmpleado=3,NombreEmpleado="Jose",PuestoTrabajo="Desarrollador"},
              new Empleado{ IdEmpleado=4,NombreEmpleado="Jefe",PuestoTrabajo="jefe"},
            };

            return empleados;                                       
        }
        // GET: ListaEmpleado
        [Autorizar]
        public ActionResult ListaEmpleado()
        {
            List<Empleado> empleados = GetListaEmpleados();
            return View(empleados);
        }

        //GET: DetalleEmpelado
        [Autorizar]
        public ActionResult DetalleEmpelado(int idEmp)
        {           
            Empleado empleado = GetListaEmpleados().SingleOrDefault(c=>c.IdEmpleado==idEmp);
            return View(empleado);
        }
    }
}
