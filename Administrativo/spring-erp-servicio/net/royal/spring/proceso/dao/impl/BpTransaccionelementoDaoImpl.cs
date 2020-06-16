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
using net.royal.spring.proceso.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpTransaccionelementoDaoImpl : GenericoDaoImpl<BpTransaccionelemento>, BpTransaccionelementoDao
    {
        private IServiceProvider servicioProveedor;

        public BpTransaccionelementoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bptransaccionelemento")
        {
            servicioProveedor = _servicioProveedor;
        }
    }
}
