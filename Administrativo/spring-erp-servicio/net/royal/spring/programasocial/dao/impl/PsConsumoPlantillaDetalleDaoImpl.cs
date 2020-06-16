using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.rrhh.dao.impl
{
public class PsConsumoPlantillaDetalleDaoImpl : GenericoDaoImpl<PsConsumoPlantillaDetalle>, PsConsumoPlantillaDetalleDao 
{
        private IServiceProvider servicioProveedor;


        public PsConsumoPlantillaDetalleDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrencuestaplantilladetalle")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsConsumoPlantillaDetalle obtenerPorId(PsConsumoPlantillaDetallePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsConsumoPlantillaDetalle obtenerPorId(String pIdInstitucion, Nullable<Int32> pPlantilla, String pIdItem)
        {
            return obtenerPorId(new PsConsumoPlantillaDetallePk(pIdInstitucion, pPlantilla, pIdItem));
        }

        public PsConsumoPlantillaDetalle coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantillaDetalle bean)
        {
            this.registrar(bean);
            return bean;
        }

        public PsConsumoPlantillaDetalle coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantillaDetalle bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pPlantilla, String pIdItem)
        {
            coreEliminar(new PsConsumoPlantillaDetallePk(pIdInstitucion, pPlantilla, pIdItem));
        }

        public void coreEliminar(PsConsumoPlantillaDetallePk id)
        {
            PsConsumoPlantillaDetalle bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsConsumoPlantillaDetalle coreAnular(UsuarioActual usuarioActual,PsConsumoPlantillaDetallePk id)
        {
            PsConsumoPlantillaDetalle bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        

        public List<PsConsumoPlantillaDetalle> listarDetalle(String pIdInstitucion,int plantilla)
        {
            IQueryable<PsConsumoPlantillaDetalle> query = getCriteria();
            query = query.Where(x => x.IdInstitucion == pIdInstitucion);
            query = query.Where(x => x.Plantilla == plantilla);
            return query.ToList();
        }

        
    }
}
