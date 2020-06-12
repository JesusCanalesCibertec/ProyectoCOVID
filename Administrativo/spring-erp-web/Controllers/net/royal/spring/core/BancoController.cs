using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.core.servicio;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class BancoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BancoServicio bancoServicio;
        public BancoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bancoServicio = servicioProveedor.GetService<BancoServicio>();
        }

        [HttpGet("[action]")]
        public List<Banco> listarTodos()
        {
            return bancoServicio.listarTodos();
        }

        [HttpPost("[action]")]
        public List<Banco> listar([FromBody]DtoFiltro filtro)
        {
            return bancoServicio.listar(filtro);
        }

        [HttpGet("[action]")]
        public List<Banco> listarActivos()
        {
            return bancoServicio.listarActivos();
        }

        [HttpGet("[action]")]
        public Banco obtenerPorId(String banco)
        {
            BancoPk pk = new BancoPk();
            pk.Banco = banco;
            return bancoServicio.obtenerPorId(pk);
        }


    }
}
