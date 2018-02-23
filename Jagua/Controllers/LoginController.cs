
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Jagua.Controllers
{
    public class LoginController : Controller
    {

        //private JaguaEntities db = new JaguaEntities();
        // GET: Login

        //[AllowAnonymous]
        //public ActionResult Index()
        //{

        //    return View(new usuario());
        //}


        //// GET: Login/Create

        //// POST: Login/Create
        //[HttpPost]
        //[AllowAnonymous]
        ////public ActionResult Autenticar([Bind(Include = "id,id_funcionario,id_perfil,usuario,contrasena")] usuarios usuarios, string returnUrl)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var usuario = login(usuarios.usuario, usuarios.contrasena);
        //            if (usuario != null)
        //            {
        //                if (usuario.estado != "I")
        //                {
        //                    if (usuario.perfiles.estado != "I")
        //                    {
        //                        string req = Request.Url.Authority;
        //                        FormsAuthentication.SetAuthCookie(usuario.usuario, false);
        //                        Clases.CacheManager c = new Clases.CacheManager();
        //                        c.Cache.Invalidate("Usuario_" + usuario.usuario);
        //                        c.Cache.Set("Usuario_" + usuario.usuario, usuario, 500);
        //                        if (usuario.modificar.Value)
        //                        {
        //                            return RedirectToAction("ModificarPassword", "Login");
        //                        }
        //                        else
        //                        {
        //                            return RedirectToAction("Index", "Home");
        //                        }
        //                        // }
        //                    }
        //                    else
        //                    {
        //                        ModelState.AddModelError("", "No se pudo acceder, perfil inactivo, comuníquese con el administrador.");
        //                    }

        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("", "No se pudo acceder, usuario inactivo, comuníquese con el administrador.");
        //                }

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "No se pudo acceder, verifique sus datos.");
        //            }
        //        }

        //        return View("Index", usuarios);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Error en el servicio de autenticación.");
        //        Clases.Logger.Log.Debug(ex.Message);
        //        return View("Index", usuarios);
        //    }
        //}

        //private usuarios login(string user, string pass)
        //{

        //    string elpass = Clases.Comun.MD5(pass);
        //    usuarios datos = db.usuarios.Where(x => x.usuario == user && x.contrasena == elpass).FirstOrDefault();
        //    return datos;

        //}

        //public ActionResult Logout()
        //{
        //    SalirSistema();
        //    return RedirectToAction("Index", "Login");
        //}
        //private void SalirSistema()
        //{
        //    try
        //    {
        //        var nombre = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
        //        FormsAuthentication.SignOut();
        //        Clases.CacheManager c = new Clases.CacheManager();
        //        c.Cache.Invalidate("Usuario_" + nombre);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}


        //private bool enviarCorreo(string usuario, string password, string correoPara)
        //{
        //    bool retorno = false;

        //    string mensaje = "<p>Se a solicitado recuperación de contraseña, sus credenciales temporales son : </p><br />";

        //    mensaje += " USUARIO : " + usuario + "<br />";
        //    mensaje += " PASSWORD : " + password + "<br />";


        //    retorno = Clases.Comun.enviarcorreo(mensaje, usuario, password, correoPara);

        //    return retorno;

        //}


        //public string CreatePassword(int length)
        //{
        //    const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    System.Text.StringBuilder res = new System.Text.StringBuilder();
        //    Random rnd = new Random();
        //    while (0 < length--)
        //    {
        //        res.Append(valid[rnd.Next(valid.Length)]);
        //    }
        //    return res.ToString();
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult RecuperarPassword([Bind(Include = "usuario")] usuarios usuarios)
        //{
        //    string passnuevo = GenerateToken(8);
        //    Clases.Logger.Log.Debug(passnuevo);

        //    try
        //    {
        //        usuarios recuperar = db.usuarios.Where(x => x.usuario == usuarios.usuario).FirstOrDefault();
        //        if (recuperar != null)
        //        {
        //            var estado = recuperar.estado;
        //            if (estado == "A")
        //            {
        //                recuperar.contrasena = Web.Gambling.Clases.Comun.MD5(passnuevo);
        //                recuperar.modificar = true;
        //                recuperar.primera_vez = true;
        //                db.Entry(recuperar).State = EntityState.Modified;
        //                db.SaveChanges();
        //                funcionarios funcionarioGambling = db.funcionarios.Find(recuperar.id_funcionario);
        //                var tipo = funcionarioGambling.tipo;
        //                if (tipo == "Co")
        //                {
        //                    var datofuncionario = funcionarioGambling.funcionarios_contactos.Where(x => x.id_contacto == 2).FirstOrDefault();
        //                    if (datofuncionario != null)
        //                    {
        //                        string email = datofuncionario.valor.ToString();
        //                        if (email != string.Empty)
        //                        {
        //                            if (enviarCorreo(recuperar.usuario, passnuevo, email))
        //                            {
        //                                Web.Gambling.Clases.LoggerApp.logApp("N/A", "N/A", 1, "N/A", "N/A", "en proceso de actualizacion - recuperar contraseña -  se envia correo a :  " + usuarios.usuario, db);
        //                            }
        //                        }
        //                    }
        //                    ViewBag.recuperar = "recuperado";
        //                    return View("RecuperarPassword", usuarios);
        //                }
        //                else
        //                {
        //                    ViewBag.recuperar = "recuperar";
        //                    ModelState.AddModelError("", "Para cambiar o recuperar su contraseña por favor contacte con un administrador");
        //                    return View("RecuperarPassword", usuarios);
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.recuperar = "recuperar";
        //                ModelState.AddModelError("", "Su usuario esta inactivo o eliminado, por favor contacte con un administrador");
        //                return View("RecuperarPassword", usuarios);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.recuperar = "recuperar";
        //            ModelState.AddModelError("", "El usuario ingresado no existe en el sistema");
        //            return View("RecuperarPassword", usuarios);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.recuperar = "recuperar";
        //        ModelState.AddModelError("", "Error en el sistema de recuperación, consulte con el adminsitrador");
        //        return View("RecuperarPassword", usuarios);

        //    }
        //}

        //public string GenerateToken(int length)
        //{
        //    System.Security.Cryptography.RNGCryptoServiceProvider cryptRNG = new System.Security.Cryptography.RNGCryptoServiceProvider();
        //    byte[] tokenBuffer = new byte[length];
        //    cryptRNG.GetBytes(tokenBuffer);
        //    return Convert.ToBase64String(tokenBuffer);
        //}

        //[AllowAnonymous]
        //public ActionResult RecuperarPassword()
        //{
        //    ViewBag.recuperar = "recuperar";
        //    return View(new usuarios());
        //}


        //[Authorize]
        //[Clases.GamblingControlFilter]
        //public ActionResult ModificarPassword()
        //{
        //    var dato = new viewModelModificarPassword();

        //    if (Request.IsAuthenticated)
        //    {
        //        var nombre = FormsAuthentication.Decrypt(HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
        //        Clases.CacheManager c = new Clases.CacheManager();
        //        c.Cache.Invalidate("Usuario_" + nombre);
        //        dato.usuario = nombre;
        //        dato.contrasena = string.Empty;
        //        dato.contrasena_nueva = string.Empty;
        //    }
        //    else {

        //        return RedirectToAction("Index", "Login");
        //    }


        //    ViewBag.recuperar = "recuperar";
        //    return View(dato);
        //}


        //[HttpPost]
        ////[Authorize]
        ////[Clases.GamblingControlFilter]
        //public ActionResult ModificarPassword([Bind(Include = "usuario,contrasena,contrasena_nueva")] viewModelModificarPassword usuarios)
        //{
        //    try
        //    {
        //        if (usuarios.contrasena_nueva == "" || usuarios.contrasena_nueva == "" || usuarios.contrasena_nueva == null || usuarios.contrasena_nueva == null)
        //        {
        //            ViewBag.recuperar = "recuperar";
        //            ModelState.AddModelError("", "Las contraseñas no pueden estar vacías");
        //            return View("ModificarPassword", usuarios);
        //        }
        //        usuarios recuperar = db.usuarios.Where(x => x.usuario == usuarios.usuario).FirstOrDefault();
        //        if (recuperar != null)
        //        {

        //            if (usuarios.contrasena_nueva != usuarios.contrasena)
        //            {
        //                ViewBag.recuperar = "recuperar";
        //                ModelState.AddModelError("", "Las contraseñas ingresadas no coinciden");
        //                return View("ModificarPassword", usuarios);
        //            }
        //            else
        //            {
        //                string newpas = Web.Gambling.Clases.Comun.MD5(usuarios.contrasena_nueva);
        //                pass_aud pass = db.pass_aud.Where(y => y.id_usuario == recuperar.id && y.contrasena == newpas).FirstOrDefault();

        //                if (pass != null)
        //                {
        //                    ViewBag.recuperar = "recuperar";
        //                    ModelState.AddModelError("", "La contraseña ingresada ya fue utilizada anteriormente ");
        //                    return View("ModificarPassword", usuarios);
        //                }
        //                else
        //                {
        //                    pass_aud passnuevo = new pass_aud();
        //                    passnuevo.id_usuario = recuperar.id;
        //                    passnuevo.usuario = recuperar.usuario;
        //                    passnuevo.contrasena = Web.Gambling.Clases.Comun.MD5(usuarios.contrasena_nueva);
        //                    passnuevo.fecha_alta = DateTime.Now;

        //                    db.pass_aud.Add(passnuevo);
        //                    db.SaveChanges();

        //                }
        //                recuperar.contrasena = Web.Gambling.Clases.Comun.MD5(usuarios.contrasena_nueva);
        //                recuperar.modificar = false;
        //                recuperar.primera_vez = false;
        //                recuperar.fecha_ult_modf = DateTime.Now;
        //                db.Entry(recuperar).State = EntityState.Modified;
        //                db.SaveChanges();


        //                Web.Gambling.Clases.LoggerApp.logApp("N/A", "N/A", 1, "N/A", "N/A", "en proceso de actualizacion - modificar contraseña  contraseña    :  " + usuarios.usuario, db);
        //                AutoMapper.Mapper.CreateMap<viewModelModificarPassword, ModificarPasswordDTO>();
        //                var datoactual = AutoMapper.Mapper.Map<ModificarPasswordDTO>(usuarios);
        //                datoactual.contrasena = Web.Gambling.Clases.Comun.MD5(usuarios.contrasena);
        //                datoactual.contrasena_nueva = Web.Gambling.Clases.Comun.MD5(usuarios.contrasena_nueva);
        //                string nuevo = Clases.Comun.Serialize(datoactual);
        //                Web.Gambling.Clases.LoggerApp.logApp(nuevo, "N/A", Clases.SessionManager.SessionData.id, "usuario", "UPDATE", "cambio de contraseña", db);
        //                ViewBag.recuperar = "recuperado";
        //                SalirSistema();
        //                return View("ModificarPassword", usuarios);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.recuperar = "recuperar";
        //            ModelState.AddModelError("", "El usuario ingresado no existe en el sistema");
        //            return View("RecuperarPassword", usuarios);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.recuperar = "recuperar";
        //        ModelState.AddModelError("", "Error en el sistema de actualizacion, consulte con el adminsitrador");
        //        return View("RecuperarPassword", usuarios);

        //    }
        //}




    }
}
