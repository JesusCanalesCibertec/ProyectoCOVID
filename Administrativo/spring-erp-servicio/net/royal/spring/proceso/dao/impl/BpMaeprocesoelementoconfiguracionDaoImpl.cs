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
using net.royal.spring.framework.constante;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpMaeprocesoelementoconfiguracionDaoImpl : GenericoDaoImpl<BpMaeprocesoelementoconfiguracion>, BpMaeprocesoelementoconfiguracionDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesoelementoconfiguracionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesoelementoconfiguracion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpMaeprocesoelementoconfiguracion> listarPorElemento(BpMaeprocesoelementoPk bpMaeProcesoElementoPk)
        {
            IQueryable<BpMaeprocesoelementoconfiguracion> cri = this.getCriteria();
            cri = cri.Where(x => x.IdProceso == bpMaeProcesoElementoPk.IdProceso);
            cri = cri.Where(x => x.IdElemento == bpMaeProcesoElementoPk.IdElemento);
            return cri.ToList();
        }
    }
}
