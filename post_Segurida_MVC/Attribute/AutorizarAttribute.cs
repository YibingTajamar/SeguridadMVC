using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace post_Segurida_MVC.Attribute
{
    //estos es para comprobar si esta el usuario autentificado o no
    public class AutorizarAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //caputuramos el controlador y action que esta solicitado que usuario a entrar
            //string controladorOrigen = filterContext.RouteData.Values["controller"].ToString();
            //string actionOrigen = filterContext.RouteData.Values["action"].ToString();

            //preguntamos si esta validados o no
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                GenericPrincipal user = (GenericPrincipal)HttpContext.Current.User;
                //mediante el metodo isinroles(role)
                //podemos preguntar por el role de un user
                //if (actionOrigen == "ListaEmpleado")
                //{
                    if (user.IsInRole("ADMIN") == false)
                    {
                        //nos lo llevamos a sin acceso
                        RouteValueDictionary ruta = new RouteValueDictionary(new { Controller = "Manage", action = "ErrorAcceso" });
                        filterContext.Result = new RedirectToRouteResult(ruta);
                    }
                //}
               
            }
            else
            {               
                //guardamos el action y controlador en Temdata para que podemos recibirlo en la controlador de manage
                //filterContext.Controller.TempData["controlador"] = controladorOrigen;
                //filterContext.Controller.TempData["action"] = actionOrigen;
                //si no esta validaor 
                //debemos redirecionar al pagina del login
                //para poder enviar a otras direcciones nececitamos una ruta esta compuesta por un controlador y un action
                RouteValueDictionary rutalogin = new RouteValueDictionary(new { controller = "Manage", action = "Login" });
                //con filtercontext redirecionemos el ruta indicada
                filterContext.Result = new RedirectToRouteResult(rutalogin);
            }
        }
    }
}