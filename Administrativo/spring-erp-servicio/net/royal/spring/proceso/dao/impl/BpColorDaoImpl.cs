using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpColorDaoImpl : GenericoDaoImpl<BpColor>, BpColorDao
    {
        private IServiceProvider servicioProveedor;

        public BpColorDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpcolor")
        {
            servicioProveedor = _servicioProveedor;
        }

       
    }
}
