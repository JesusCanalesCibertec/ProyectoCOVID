using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using net.royal.spring.minedu.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class MpPersonaController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpPersonaServicio mpPersonaServicio;
        private UsuarioServicio usuarioServicio;

        public MpPersonaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpPersonaServicio = servicioProveedor.GetService<MpPersonaServicio>();
            usuarioServicio = servicioProveedor.GetService<UsuarioServicio>();
        }


        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacion([FromBody] DtoTabla filtro)
        {
            return mpPersonaServicio.listarPaginacion(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico ListarPaginacionUsuario([FromBody] DtoTabla filtro)
        {
            return mpPersonaServicio.ListarPaginacionUsuario(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public MpPersona Registrar([FromBody]MpPersona bean)
        {
            return mpPersonaServicio.registrar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public Seguridadperfilusuario RegistrarUsuario([FromBody]Seguridadperfilusuario bean)
        {
            return mpPersonaServicio.RegistrarUsuario(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public Seguridadperfilusuario ActualizarUsuario([FromBody]Seguridadperfilusuario bean)
        {
            return mpPersonaServicio.ActualizarUsuario(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public Seguridadperfilusuario EliminarUsuario([FromBody]Seguridadperfilusuario bean)
        {
            return mpPersonaServicio.EliminarUsuario(bean);
        }

        


        [HttpGet("[action]")]
        public MpPersona ObtenerPorId(int pIdPersona, int pIdContratacion)
        {
            return mpPersonaServicio.obtenerPorId(pIdPersona, pIdContratacion);
        }


        [HttpPost("[action]")]
        public MpPersona Actualizar([FromBody]MpPersona bean)
        {
            return mpPersonaServicio.actualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MpContratacion Cambiarestado([FromBody]MpContratacion bean)
        {
            return mpPersonaServicio.cambiarestado(bean);
        }

        [HttpGet("[action]")]
        public List<MpPersona> ListarNombres()
        {
            return mpPersonaServicio.ListarNombres();
        }

        [HttpGet("[action]")]
        public List<DtoListafechas> ListarPersonal(Nullable<DateTime> parametro)
        {
            return mpPersonaServicio.ListarPersonal(parametro);
        }


        [HttpGet("[action]")]
        public async Task<Personareniec> ObtenerDatos(string pDni, string pToken)
        {

            System.Uri Url = new System.Uri("http://www.google.com/");
            System.Net.WebRequest WebRequest;
            WebRequest = System.Net.WebRequest.Create(Url);
            System.Net.WebResponse objResp;
            try
            {
                objResp = WebRequest.GetResponse();
                objResp.Close();
                WebRequest = null;

                string url = "http://dniruc.apisperu.com/api/v1/dni/";
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(url + pDni + "?token=" + pToken);
                return JsonConvert.DeserializeObject<Personareniec>(json);
            }
            catch (Exception)
            {
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, "El servidor no cuenta con conexión a Internet"));
                throw new UException(lista);
            }
        }

        [HttpGet("[action]")]
        public List<DtoEventos> ListarEventos(Nullable<Int32> pIdPersona)
        {
            return mpPersonaServicio.ListarEventos(pIdPersona);
        }


        [HttpGet("[action]")]
        public DtoTabla ValidarDirectorio(string pUsuario)
        {
            DtoDirectoriousuario bean = new DtoDirectoriousuario();
            bean = usuarioServicio.GetUserInformation(_usuarioActual, pUsuario);

            if (bean != null)
            {
                DtoTabla dto = new DtoTabla();
                dto.Descripcion = bean.Correo;
                return dto;
            }
            return null;
        }

    }
}
