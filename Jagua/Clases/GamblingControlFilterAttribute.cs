using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Jagua.Clases
{
    /// <summary>
    ///
    /// </summary>
    public class JaguaControlFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            CacheManager c = new CacheManager();
            base.OnActionExecuting(filterContext);
            var nombre = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            if (!c.Cache.IsSet("Usuario_" + nombre))
            {
                HttpContext.Current.Response.Redirect("~/Login/Index");
            }
            else
            {
                // seccion apra validacion de controladores/metodos por perfil
                var descriptor = filterContext.ActionDescriptor;
                var actionName = descriptor.ActionName;
                var controllerName = descriptor.ControllerDescriptor.ControllerName;

            }


            if (filterContext.HttpContext.Session != null)
            {
                if (filterContext.HttpContext.Session.IsNewSession)
                {
                    var sessionCookie = filterContext.HttpContext.Request.Headers["Cookie"];
                    if ((sessionCookie == null) && (sessionCookie.IndexOf("ASP.NET_SessionId") == 0))
                    {
                        // redirect to login
                        HttpContext.Current.Response.Redirect("~/Login/Index");
                    }
                }
            }

        }

    }
}