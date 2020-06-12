using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.constante;
using Microsoft.Extensions.Logging;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class HrPuestoempresaServicioImpl : GenericoServicioImpl, HrPuestoempresaServicio
    {

        private IServiceProvider servicioProveedor;
        private HrPuestoempresaDao hrPuestoempresaDao;


        public HrPuestoempresaServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            hrPuestoempresaDao = servicioProveedor.GetService<HrPuestoempresaDao>();
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPuestoempresa filtro)
        {
            return hrPuestoempresaDao.listarPaginacion(paginacion,filtro);
        }

       
        public HrPuestoempresa obtenerPorId(HrPuestoempresaPk llave)
        {
            return hrPuestoempresaDao.obtenerPorId(llave);
        }
    }
}
