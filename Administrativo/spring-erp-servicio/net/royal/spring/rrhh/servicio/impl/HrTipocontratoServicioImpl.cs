using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.servicio.impl
{

public class HrTipocontratoServicioImpl : GenericoServicioImpl, HrTipocontratoServicio {

        private IServiceProvider servicioProveedor;
        private HrTipocontratoDao hrTipocontratoDao;

        public HrTipocontratoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            hrTipocontratoDao = servicioProveedor.GetService<HrTipocontratoDao>();
}

        public void actualizar(HrTipocontrato hrTipocontrato)
        {
            hrTipocontratoDao.actualizar(hrTipocontrato);
        }

        public void eliminar(HrTipocontrato hrTipocontrato)
        {
            hrTipocontratoDao.eliminar(hrTipocontrato);
        }

        public List<HrTipocontrato> listar(DtoFiltro filtro)
        {
           return hrTipocontratoDao.listar(filtro);
        }

        public List<HrTipocontrato> listarActivos()
        {
            return hrTipocontratoDao.listarActivos();
        }

        public List<HrTipocontrato> listarTodos()
        {
            return hrTipocontratoDao.listarTodos();
        }

        public HrTipocontrato obtenerPorId(HrTipocontratoPk hrTipocontratoPk)
        {
            return hrTipocontratoDao.obtenerPorId(hrTipocontratoPk);
        }

        public void registrar(HrTipocontrato hrTipocontrato)
        {
            hrTipocontratoDao.registrar(hrTipocontrato);
        }
    }
}
