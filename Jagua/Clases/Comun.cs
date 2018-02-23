using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace Jagua.Clases
{
    public class ReportesParametros
    {
        public object ReportSource { set; get; }

        public string ReportPath { set; get; }

        public string NombreArchivo { get; set; }

    }

    public class ReportParameters
    {

        public static ReportesParametros DatosReporte
        {
            get
            {
                return (ReportesParametros)HttpContext.Current.Session["ReportParams"];
            }

            set
            {
                HttpContext.Current.Session["ReportParams"] = value;
            }
        }
    }


    public class CacheManager
    {
        public ICacheProvider Cache { get; set; }
        public CacheManager()
            : this(new DefaultCacheProvider())
        {
        }
        public CacheManager(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }


    }
    public static class LoggerApp
    {

        /// <summary>
        /// realiza log en la base de datos
        /// </summary>
        /// <param name="dato_nuevo"></param>
        /// <param name="dato_anterior"></param>
        /// <param name="usuario"></param>
        /// <param name="tabla"></param>
        /// <param name="accion"></param>
        /// <param name="obs"></param>
        /// <param name="db"></param>
        public static void logApp(string dato_nuevo, string dato_anterior, int usuario, string tabla, string accion, string obs, gambling_dbEntities db)
        {
            try
            {

                logs dato = new logs();
                dato.dato_anterior = dato_anterior;
                dato.dato_nuevo = dato_nuevo;
                dato.usuario = usuario;
                dato.observacion = obs; //+ " " + GamblingHelper.datosPC();
                dato.tabla = tabla;
                dato.accion = accion;
                dato.fecha_accion = DateTime.Now;
                db.logs.Add(dato);
                db.SaveChanges();

            }
            catch (Exception ex)
            {


            }
        }

    }
    public static class Comun
    {
        public static string ConexionDb
        {
            get
            {

                var _ConexionDb = System.Configuration.ConfigurationManager.ConnectionStrings["gambling_dbContext"].ConnectionString;
                return _ConexionDb;
            }
        }


        public static string ObtenerFormatoNumeracion(int numero)
        {
            string strNum = "";
            if (numero < 10) { strNum = "000000" + numero; }
            if (numero > 9 && numero < 100) { strNum = "00000" + numero; }
            if (numero > 99 && numero < 1000) { strNum = "0000" + numero; }
            if (numero > 999 && numero < 10000) { strNum = "000" + numero; }
            if (numero > 9999 && numero < 100000) { strNum = "00" + numero; }
            if (numero > 99999 && numero < 1000000) { strNum = "0" + numero; }
            if (numero > 999999 && numero < 10000000) { strNum = "" + numero; }
            return strNum;
        }

        public static DataTable SqlSelecGenerico(string sql)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(Clases.Comun.ConexionDb);
            try
            {
                connection.Open();
                var command = new SqlCommand(sql, connection);
                command.CommandTimeout = 0;
                command.CommandType = System.Data.CommandType.Text;
                using (SqlDataAdapter a = new SqlDataAdapter(command))
                {
                    a.Fill(dt);
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                dt = new DataTable();
                connection.Close();
            }

            return dt;
        }

        public static IEnumerable<System.Web.Mvc.SelectListItem> CajaApertura(string s)
        {

            return new SelectList(cajaApertura, "Key", "Value", s);

        }

        public static Dictionary<string, string> cajaApertura = new Dictionary<string, string>
        {
                                      {"S", "SI"},
                                      {"N", "NO"}
        };


        public static IEnumerable<System.Web.Mvc.SelectListItem> CajaAbiertoList(string s)
        {
            return new SelectList(cajaAbierto, "Key", "Value");
        }

        public static Dictionary<string, string> cajaAbierto = new Dictionary<string, string>
        {
                                      {"", "Seleccionar"},
                                      {"S", "SI"},
                                      {"N", "NO"}
        };

        public static string cajaAbierto_Str(string abierto)
        {
            return cajaAbierto.Where(q => q.Key == abierto).FirstOrDefault().Value;
        }


        public static string MD5(string numeros)
        {
            string num = StringToMD5(numeros + "HAXORTANGOPYX").Trim().ToLower();
            num = num.Substring(0, 8) + " " + num.Substring(8, 8) + " " + num.Substring(16, 8) + " " + num.Substring(24, 8);
            return num;
        }
        /// <summary>
        /// resumen md5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToMD5(string s)
        {
            //Declarations
            string MD5String = null;
            byte[] EncStringBytes = null;
            UTF8Encoding Encoder = new UTF8Encoding();
            MD5CryptoServiceProvider MD5Hasher = new MD5CryptoServiceProvider();
            //Converts the String to bytes
            EncStringBytes = Encoder.GetBytes(s);
            //Generates the MD5 Byte Array
            EncStringBytes = MD5Hasher.ComputeHash(EncStringBytes);
            //Create MD5 hash
            MD5String = BitConverter.ToString(EncStringBytes);
            MD5String = MD5String.Replace("-", "");
            return MD5String;
        }
        /// <summary>
        /// Serializa un objeto a json , esto utilizamos en el log
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string Serialize(object objectToSerialize)
        {
            string ret = string.Empty;
            try
            {
                ret = Newtonsoft.Json.JsonConvert.SerializeObject(objectToSerialize, Newtonsoft.Json.Formatting.None,
                     new Newtonsoft.Json.JsonSerializerSettings()
                     {

                         ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                     });


            }
            catch (Exception ex)
            {

                ret = ex.Message;
            }



            return ret;

        }
        /// <summary>
        ///  envia correo en usuario alta y modificacion.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="correoPara"></param>
        /// <returns></returns>
        public static bool enviarcorreo(string mensaje, string usuario, string password, string correoPara)
        {
            bool retorno = false;
            Jagua_Entities db = new Jagua_Entities();
            try
            {
                var datos = db.parametros.Where(x => x.parametro.Contains("smtp")).ToList();
                string smtp1 = string.Empty;
                string smtpuser = string.Empty;
                string smtppass = string.Empty;
                string smtpport = string.Empty;

                foreach (parametros smtp in datos)
                {
                    if (smtp.parametro == "smtp")
                    {
                        smtp1 = smtp.valor;
                    }
                    if (smtp.parametro == "smtpuser")
                    {
                        smtpuser = smtp.valor;
                    }
                    if (smtp.parametro == "smtppass")
                    {
                        smtppass = smtp.valor;
                    }
                    if (smtp.parametro == "smtpport")
                    {
                        smtpport = smtp.valor;
                    }
                }
                string fromAddress = smtpuser;
                string mailPassword = smtppass;
                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(smtpport);
                client.Host = smtp1;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(fromAddress, mailPassword);
                var send_mail = new MailMessage();
                send_mail.IsBodyHtml = true;
                send_mail.From = new MailAddress(fromAddress);
                send_mail.To.Add(new MailAddress(correoPara));
                send_mail.Subject = "Datos de usuario";
                send_mail.Body = mensaje;
                client.Send(send_mail);
                retorno = true;
            }
            catch (Exception ex)
            {
                LoggerApp.logApp("N/A", "N/A", 1, "N/A", "N/A", "ERROR EN APLICACION ENVIAR CORREO " + ex.Message, db);
                retorno = false;
            }
            db.Dispose();
            return retorno;
        }
        public static bool Verifica_Acceso(string cont, string acc)
    {
        bool r = false;
        gambling_dbEntities db = new gambling_dbEntities();
        modulos mod = db.modulos.Where(q => q.modulo.ToLower() == cont.ToLower()).FirstOrDefault();
        if (mod != null)
        {
            permisos per = db.permisos.Where(q => q.id_modulo == mod.id && q.id_perfil == Clases.SessionManager.SessionData.id_perfil).FirstOrDefault();
            if (per != null)
            {
                switch (acc)
                {
                    case "a":
                        r = (bool)per.alta;
                        break;
                    case "b":
                        r = (bool)per.baja;
                        break;
                    case "e":
                        r = (bool)per.modificacion;
                        break;
                    case "c":
                        r = (bool)per.consulta;
                        break;
                    case "d":
                        //  r = (bool)per.desbloquear;
                        break;
                }
            }
        }
        return r;
    }
        public static string Acceso_List(string action)
    {
        string r = "";
        switch (action)
        {
            case "Create":
                r = "a";
                break;
            case "DeleteConfirmed":
                r = "b";
                break;
            case "Edit":
                r = "e";
                break;
            case "Index":
                r = "c";
                break;
            case "Unlock":
                r = "d";
                break;
        }
        return r;

    }

    }

    public static class Export
    {
        public static void ToExcel(HttpResponseBase Response, object dataList, string filename)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            grid.DataSource = dataList;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }


}