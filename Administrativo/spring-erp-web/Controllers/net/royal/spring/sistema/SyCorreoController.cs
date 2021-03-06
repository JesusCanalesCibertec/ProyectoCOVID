﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.correo.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyCorreoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyCorreoServicio syCorreoServicio;
        public SyCorreoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
        }

        /* PENDIENTE-DARIO
        EmailConfiguracion obtenerConfiguracion();

        void enviarCorreInmediato(EmailConfiguracion config, List<Email> listaEmail);

        void enviarCorreInmediatoAsincrono(EmailConfiguracion config, List<Email> listaEmail);*/
    }
}
