using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace post_Segurida_MVC.Controllers
{
    public class ManageController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            string role = null;
            int idempelado=0;
            //comprobar el Exitencia de Usuario y obtener su Role,idempelado 
            //esta parte puede cambiarlo por un metodo que comprueba si el usuario y password que recipido coincide con tu BBDD o no
            if (usuario.ToUpper() =="ADMIN" && password.ToUpper()=="ADMIN")
            {
                role = "ADMIN";
                idempelado = 4;
            }
            else if (usuario.ToUpper() == "EMPLEADO" && password.ToUpper() == "EMPLEADO")
            {
                role = "EMPLEADO";
                idempelado = 1;
            }          
            //si role es diferente que null eso significa el usuairo ha pasado la comprobacion
            if (role != null)
            {  
                //ticket para la cookie de seguridad
                //6 parametro que necesitamao para crear un tick                       
                FormsAuthenticationTicket ticket = 
                    new FormsAuthenticationTicket
                    (
                        1,//vercion
                        idempelado.ToString(),//idempleado tiene alamacena de formato string,//este el valor que vamos a guarda en HttpContext.Current.User.Identity.Name
                        DateTime.Now, //fecha y hora de inicio
                        DateTime.Now.AddMinutes(10),//duracion de la session
                        true,//persistente
                        role //datos de usuario: se puede alamcenar cuaquiere dato que interesa recuperarlo despues
                        //aqui por ejemplo alamacena el role que tiene el usuario
                    );
                
                //Debemos cifrar el ticket
                string datos = FormsAuthentication.Encrypt(ticket);

                //crear una cookie con el nombre que deseemos,
                //el nombre es importante porque necesitamos recuperar el ticket para crear el usuraio en la sesion
                HttpCookie cookie = new HttpCookie("Usuario", datos);

                //añadir el cookie que hemos creamos en la coleccion de cookie del response
                Response.Cookies.Add(cookie);

                return RedirectToAction("ListaEmpleado", "Empleado");

                //string action = TempData["action"].ToString();
                //string controlador = TempData["controlador"].ToString();

                //if (action== "DetalleEmpelado")
                //{
                //    return RedirectToAction(action, controlador, new { idEmp = idempelado });
                //}
                //else
                //{
                //    return RedirectToAction(action, controlador);
                //}

            }
            else
            {
                //Falla de Comprobacion
                ViewBag.mensaje = "Usuario/Password incorrecto";
                return View();
            }

        }


        public ActionResult ErrorAcceso()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            //limpiamos el usuario actual de session
            HttpContext.User = null; // es quivalente que HttpContext.Current.User en el controlador
            //cerramos la sesion con el metodod signout
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies["Usuario"];
            cookie.Expires = DateTime.Now.AddMinutes(-30);
            //almacenar cookies con la nueva fecha
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }
    }
}