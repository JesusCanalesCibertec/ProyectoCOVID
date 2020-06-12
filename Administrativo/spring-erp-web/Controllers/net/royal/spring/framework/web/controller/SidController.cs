using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using net.royal.spring.framework.web.controller;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.core.servicio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.constante;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio;
using net.royal.spring.sistema.dominio.dto;
using System.Web;

namespace net.royal.spring.framework.web.controller
{
    [Route("api/[controller]")]
    public class SidController : BaseController
    {
        private readonly IConfiguration _config;
        private UsuarioServicio _usuarioServicio;
        private EmpleadomastServicio _empleadomastServicio;
        private SeguridadautorizacionesServicio _seguridadautorizacionesServicio;
        private BpEnlaceServicio _bpEnlaceServicio;
        private SyAprobacionprocesoServicio _syAprobacionprocesoServicio;

        public SidController(
            IServiceProvider _servicioProveedor,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, _servicioProveedor)
        {
            _config = configuration;
            _usuarioServicio = _servicioProveedor.GetService<UsuarioServicio>();
            _empleadomastServicio = _servicioProveedor.GetService<EmpleadomastServicio>();
            _seguridadautorizacionesServicio = _servicioProveedor.GetService<SeguridadautorizacionesServicio>();
            _bpEnlaceServicio = _servicioProveedor.GetService<BpEnlaceServicio>();
            _syAprobacionprocesoServicio = _servicioProveedor.GetService<SyAprobacionprocesoServicio>();
        }

        [HttpGet("[action]")]
        public JsonResult enviar(string sid, string correo)
        {
            DtoEnlace enlace = new DtoEnlace()
            {
                Enlace = sid,
                Correo = correo
            };

            DtoEnlace enlaceRespuesta = _bpEnlaceServicio.validarEnlace(enlace);

            if (!enlaceRespuesta.flgValida)
            {
                if (enlaceRespuesta.listaMensaje.Count > 0)
                {
                    throw new UException(enlaceRespuesta.listaMensaje[0].Mensaje);
                }
            }

            ////////////////////////////////////////////

            UsuarioActual usuarioActual = enlaceRespuesta.usuarioActual;

            usuarioActual.paginacion = 20;
            usuarioActual.esJefe = _empleadomastServicio.esJefe(usuarioActual.PersonaId);
            usuarioActual.esAdministradorWeb = _usuarioServicio.esAdministradorWeb(usuarioActual.UsuarioLogin);

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
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

            setSessionData(usuarioActual.UsuarioLogin + "|" + usuarioActual.CompaniaSocioCodigo, usuarioActual);

            this.inicializarMenu(usuarioActual.UsuarioLogin);

            DtoSolicitud dtoBean = obtenerSolicitudPorUrl(enlaceRespuesta, usuarioActual);

            return Json(new { token = new JwtSecurityTokenHandler().WriteToken(token), url = enlaceRespuesta.Url, dto = dtoBean });
        }

        private DtoSolicitud obtenerSolicitudPorUrl(DtoEnlace enlace, UsuarioActual usuarioActual)
        {
            //TO-DO
            //en cada return null se puder lanzar un UException con el mensaje...

            if (UString.estaVacio(enlace.Url) || UString.estaVacio(enlace.CodigoProceso))
            {
                return null;
            }

            string[] urlParts = enlace.Url.Split(ConstanteCorreo.SEPARADOR_PARAMETROS);

            if (urlParts.Length != 2)
            {
                return null;
            }

            string llave = urlParts[1];

            if (UString.estaVacio(llave))
            {
                return null;
            }

            enlace.Url = urlParts[0];

            DtoSolicitud dtoSolicitud = null;

            dtoSolicitud = _syAprobacionprocesoServicio.obtenerSolicitud(_syAprobacionprocesoServicio.obtenerCodigoProcesoAprobacion(enlace.CodigoProceso), enlace.NumeroProceso, usuarioActual);

            //retornar la llave aun el dto este en nulo, (vista de invitado)
            dtoSolicitud = dtoSolicitud == null ? new DtoSolicitud() : dtoSolicitud;
            dtoSolicitud.Llave = llave;

            return dtoSolicitud;


        }
    }
}