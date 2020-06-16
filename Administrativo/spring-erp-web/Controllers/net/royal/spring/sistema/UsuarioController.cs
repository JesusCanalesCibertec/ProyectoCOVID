using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.servicio;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.constante;
using net.royal.spring.core.servicio;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using Microsoft.AspNetCore.Routing;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.servicio;
using System.Security.Permissions;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using net.royal.spring.minedu.dominio.dto;

namespace Angular.Controllers.net.royal.spring
{


    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        private readonly IConfiguration _config;

        private UsuarioServicio _usuarioServicio;
        private PsInstitucionServicio psInstitucionServicio;
        private SySeguridadautorizacionesServicio sySeguridadautorizacionesServicio;
        public UsuarioController(
            IServiceProvider _servicioProveedor,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, _servicioProveedor)
        {
            _config = configuration;
            _usuarioServicio = _servicioProveedor.GetService<UsuarioServicio>();
            psInstitucionServicio = _servicioProveedor.GetService<PsInstitucionServicio>();
            sySeguridadautorizacionesServicio = _servicioProveedor.GetService<SySeguridadautorizacionesServicio>();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]/{usuario}/{clave}/{clave1}/{clave2}")]
        public void cambiarClave(String usuario, String clave, String clave1, String clave2)
        {
            _usuarioServicio.cambiarClave(usuario, clave, clave1, clave2);
        }

        [HttpGet("[action]")]
        public List<PsInstitucion> listarTodosActivas()
        {
            return psInstitucionServicio.listarTodosActivas();
        }

        [HttpPost("[action]")]
        public JsonResult Login([FromBody]LoginUser loginUser)
        {
            loginUser.Usuario = loginUser.Usuario == null ? null : loginUser.Usuario.ToUpper();
            UsuarioActual usuarioActual = null;

            if (loginUser.Usuario == "EACH1")
            {
                usuarioActual = _usuarioServicio.login(ConstanteSpringNetRRHH.APLICACION_CODIGO, loginUser.Usuario, loginUser.Clave, loginUser.Compania, loginUser.conformidad, loginUser.origen);
            }
            else
            {
                usuarioActual = _usuarioServicio.Logeo(loginUser.Usuario, loginUser.Clave);
            }
                

            var claims = new[]
        {
                        new Claim(JwtRegisteredClaimNames.Jti, usuarioActual.UsuarioLogin),
                        new Claim(JwtRegisteredClaimNames.Sid, usuarioActual.CompaniaSocioCodigo),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(600),
            signingCredentials: creds);

            if (!UString.estaVacio(loginUser.Compania))
            {
                PsInstitucion psInstitucion = psInstitucionServicio.obtenerPorId(loginUser.Compania);
                usuarioActual.idInstitucion = loginUser.Compania;
                usuarioActual.idInstitucionNombre = psInstitucion.Nombre;
            }

          
            setSessionData(usuarioActual.UsuarioLogin + "|" + usuarioActual.CompaniaSocioCodigo, usuarioActual);

            this.inicializarMenu(loginUser.Usuario);

            return Json(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        //[HttpPost("[action]")]
        //public JsonResult Login([FromBody]LoginUser loginUser)
        //{
        //    loginUser.Usuario = loginUser.Usuario == null ? null : loginUser.Usuario.ToUpper();
        //    UsuarioActual usuarioActual = null;

        //    usuarioActual = _usuarioServicio.login(ConstanteSpringNetRRHH.APLICACION_CODIGO, loginUser.Usuario, loginUser.Clave, loginUser.Compania, loginUser.conformidad, loginUser.origen);


        //    usuarioActual.paginacion = 20;

        //    var claims = new[]
        //{
        //                new Claim(JwtRegisteredClaimNames.Jti, usuarioActual.UsuarioLogin),
        //                new Claim(JwtRegisteredClaimNames.Sid, usuarioActual.CompaniaSocioCodigo),
        //            };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
        //    _config["Tokens:Issuer"],
        //    claims,
        //    expires: DateTime.Now.AddMinutes(600),
        //    signingCredentials: creds);

        //    if (!UString.estaVacio(loginUser.Compania))
        //    {
        //        PsInstitucion psInstitucion = psInstitucionServicio.obtenerPorId(loginUser.Compania);
        //        usuarioActual.idInstitucion = loginUser.Compania;
        //        usuarioActual.idInstitucionNombre = psInstitucion.Nombre;
        //    }

        //    setSessionData(usuarioActual.UsuarioLogin + "|" + usuarioActual.CompaniaSocioCodigo, usuarioActual);

        //    this.inicializarMenu(loginUser.Usuario);

        //    return Json(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        //}

        [HttpPost("[action]")]
        public JsonResult enviarRecuperarClave([FromBody]DtoCorreo correo)
        {
            List<MensajeUsuario> lista = _usuarioServicio.recuperarClave(correo.correo, correo.compania);

            if (lista.Count != 0)
            {
                throw new UException(lista);
            }

            return Json(new { mensaje = "Se ha enviado la información a correo: " + correo.correo });
        }

        [HttpPost("[action]")]
        public List<DtoTabla> listarCompanias([FromBody]LoginUser loginUser)
        {
            List<DtoTabla> lista = sySeguridadautorizacionesServicio.listarPorGrupoYUsuario(loginUser.Usuario);
            return lista;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        public JsonResult tienePermiso(String opcion)
        {
            Nullable<bool> tieneOpcion = getSessionData<bool>("tiene" + opcion);
            tieneOpcion = tieneOpcion.HasValue ? tieneOpcion : false;
            return Json(tieneOpcion);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("[action]")]
        public JsonResult obtenerMenu()
        {
            var menu = getSessionData<Menu>("menu");
            menu = menu == null ? new Menu(new List<Item>()) : menu;
            return Json(menu);
        }

        /*ernesto*/
        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroUsuario filtro)
        {
            return _usuarioServicio.listarPaginacion(filtro.paginacion, filtro);

        }


        [HttpPost("[action]")]
        public Usuario obtenerPorId([FromBody]UsuarioPk llave)
        {
            return _usuarioServicio.obtenerPorId(llave);
        }

        /*ernesto*/
    }



    public class Menu
    {
        public List<Item> items { get; set; }

        public Menu()
        {
        }

        public Menu(List<Item> items)
        {
            this.items = items;
        }
    }
    public class Item
    {
        public string id { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }
        public string action { get; set; }
        public List<Item> items { get; set; }
    }

    public class LoginUser
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Compania { get; set; }
        public string captcha { get; set; }
        public string conformidad { get; set; }
        public string origen { get; set; }
    }

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = 500;

            string result = null;


            if (exception is UException)
            {
                var e = exception as UException;
                var bean = new ErrorResumen() { flagTabla = e.flagTabla, errores = e.obtenerErrores() };
                result = JsonConvert.SerializeObject(bean);
                code = 606;
            }
            else
            {
                result = JsonConvert.SerializeObject(new List<MensajeUsuario>() { new MensajeUsuario() { Mensaje = exception.Message, TipoMensaje = MensajeUsuario.tipo_mensaje.ERROR } });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }

}

class ErrorResumen
{
    public bool flagTabla { get; set; }
    public IList<MensajeUsuario> errores { get; set; }
}