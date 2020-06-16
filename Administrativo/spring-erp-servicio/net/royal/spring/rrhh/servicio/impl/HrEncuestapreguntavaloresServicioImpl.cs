using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrEncuestapreguntavaloresServicioImpl : GenericoServicioImpl, HrEncuestapreguntavaloresServicio {

        private IServiceProvider servicioProveedor;
        private HrEncuestapreguntavaloresDao hrEncuestapreguntavaloresDao;

        public HrEncuestapreguntavaloresServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrEncuestapreguntavaloresDao = servicioProveedor.GetService<HrEncuestapreguntavaloresDao>();
        }

        public HrEncuestapreguntavalores coreInsertar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean)
        {
            return hrEncuestapreguntavaloresDao.coreInsertar(usuarioActual,bean);
        }

        public HrEncuestapreguntavalores coreActualizar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean)
        {
            return hrEncuestapreguntavaloresDao.coreActualizar(usuarioActual,bean);
        }


        public void coreEliminar(HrEncuestapreguntavaloresPk id)
        {
            hrEncuestapreguntavaloresDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pPregunta,Nullable<Int32> pValor)
        {
            hrEncuestapreguntavaloresDao.coreEliminar( pPregunta, pValor);
        }


        public HrEncuestapreguntavalores obtenerPorId(HrEncuestapreguntavaloresPk id)
        {
            return hrEncuestapreguntavaloresDao.obtenerPorId(id);
        }

        public HrEncuestapreguntavalores obtenerPorId(Nullable<Int32> pPregunta,Nullable<Int32> pValor)
        {
            return hrEncuestapreguntavaloresDao.obtenerPorId( pPregunta, pValor);
        }

        public List<HrEncuestapreguntavalores> listarValores(int? numero)
        {
            return hrEncuestapreguntavaloresDao.listarValores(numero);
        }
    }
}
