using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Principal;

namespace post_Segurida_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //(ES METODO QUE SE EJECUTA EN EL SERVIDOR CON CADA INTENTO DE VALIDACION DE USUARIO)
        public void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {           
            HttpCookie cookie = Request.Cookies["Usuario"];
            if (cookie != null)
            {              
                string datos = cookie.Value;           
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(datos);              
                string role = ticket.UserData;
                GenericPrincipal principal = new GenericPrincipal(new GenericIdentity(ticket.Name), new string[] { role });
                HttpContext.Current.User = principal;
            }
        }
    }
}
