using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Gambling.Domain.Entities;
namespace Web.Gambling.Clases
{

    public static class SessionManager
    {

        /// <summary>
        /// Propiedad el cual es accesible sin instancia y determina  hay ssession valida
        /// </summary>
        public static bool isSessionValid
        {
            get
            {
                return ControlSession();
            }
        }

        /// <summary>
        /// Esta propiedad optiene el Ip Addres del caller.
        /// </summary>
        public static string getRemoteAddresApp
        {
            get
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }
        /// <summary>
        /// Esta propiedad Obtiene los datos (GetFileName(FilePath)) de con el que obtenemos el nombre de la pagina que ingresa al sitio
        /// </summary>
        public static string getGetFileName
        {
            get
            {
                return System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.FilePath);
            }
        }
        /// <summary>
        /// Esta propiedad retorna datoss del usuario logueado
        /// </summary>
        public static usuarios SessionData
        {

            get
            {
                usuarios retorno = new usuarios();               
                CacheManager c = new CacheManager();
                var nombre = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                if (c.Cache.IsSet("Usuario_" + nombre))
                {
                    retorno = (usuarios)c.Cache.Get("Usuario_" + nombre);
                }
                return retorno;
            }

        }

        /// <summary>
        /// Este metodo indica si los valores de cache session estan activos corresponde a session activa , caso contrario la session expiro
        /// </summary>
        /// <returns></returns>
        private static bool ControlSession()
        {
            bool retorno = false;
            try
            {
                HttpContext ctx = HttpContext.Current;
                CacheManager c = new CacheManager();
                var nombre = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                if (!c.Cache.IsSet("Usuario_" + nombre))
                {
                    retorno = false;
                }
                else
                {
                    retorno = true;
                }


            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }
    }
}