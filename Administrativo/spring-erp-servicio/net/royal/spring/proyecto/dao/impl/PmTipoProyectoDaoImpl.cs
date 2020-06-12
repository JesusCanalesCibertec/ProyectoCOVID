using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmTipoproyectoDaoImpl : GenericoDaoImpl<PmTipoproyecto>, PmTipoproyectoDao
    {
        private IServiceProvider servicioProveedor;


        public PmTipoproyectoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmtipoproyecto")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PmTipoproyecto obtenerPorId(String IdTipoProyecto)
        {
            object[] myObjArray = { IdTipoProyecto };
            return base.obtenerPorId(myObjArray);
        }
    }
}
