using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.minedu.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;

namespace net.royal.spring.minedu.servicio.impl
{
    public class MpSistemaServicioImpl : GenericoServicioImpl, MpSistemaServicio
    {
        private IServiceProvider servicioProveedor;
        private MpSistemaDao mpSistemaDao;

        public MpSistemaServicioImpl(IServiceProvider _serivicioProveedor)
        {
            servicioProveedor = _serivicioProveedor;
            mpSistemaDao = servicioProveedor.GetService<MpSistemaDao>();
     
        }
        
        public List<MpSistema> ListarSistemas()
        {
            return mpSistemaDao.ListarSistemas();
        }

       
    }
}
