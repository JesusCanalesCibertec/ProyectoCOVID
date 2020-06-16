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

namespace net.royal.spring.rrhh.dao.impl
{
public class HrEncuestaplantilladetalleDaoImpl : GenericoDaoImpl<HrEncuestaplantilladetalle>, HrEncuestaplantilladetalleDao 
{
        private IServiceProvider servicioProveedor;


        public HrEncuestaplantilladetalleDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrencuestaplantilladetalle")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestaplantilladetalle obtenerPorId(HrEncuestaplantilladetallePk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuestaplantilladetalle obtenerPorId(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta)
        {
            return obtenerPorId(new HrEncuestaplantilladetallePk( pPlantilla, pPregunta));
        }

        public HrEncuestaplantilladetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean)
        {
            this.registrar(bean);
            return bean;
        }

        public HrEncuestaplantilladetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta)
        {
            coreEliminar(new HrEncuestaplantilladetallePk( pPlantilla, pPregunta));
        }

        public void coreEliminar(HrEncuestaplantilladetallePk id)
        {
            HrEncuestaplantilladetalle bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrEncuestaplantilladetalle coreAnular(UsuarioActual usuarioActual,HrEncuestaplantilladetallePk id)
        {
            HrEncuestaplantilladetalle bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public HrEncuestaplantilladetalle coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta)
        {
            return coreAnular(usuarioActual,new HrEncuestaplantilladetallePk( pPlantilla, pPregunta));
        }

        public List<HrEncuestaplantilladetalle> listarPreguntas(int plantilla)
        {
            IQueryable<HrEncuestaplantilladetalle> query = getCriteria();
            query = query.Where(x => x.Plantilla == plantilla);
            return query.ToList();
        }

        
    }
}
