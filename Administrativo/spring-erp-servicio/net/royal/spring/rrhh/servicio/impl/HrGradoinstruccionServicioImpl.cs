using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dao;

namespace net.royal.spring.rrhh.servicio.impl
{
    public class HrGradoinstruccionServicioImpl : HrGradoinstruccionServicio
    {
        private HrGradoinstruccionDao hrGradoinstruccionDao;

        public HrGradoinstruccionServicioImpl(HrGradoinstruccionDao _HrDivisionDao) {
            hrGradoinstruccionDao = _HrDivisionDao;
        }

        public void registrar(HrGradoinstruccion enity)
        {
            hrGradoinstruccionDao.registrar(enity);
        }

        public void actualizar(HrGradoinstruccion enity)
        {
            hrGradoinstruccionDao.actualizar(enity);
        }

        public void eliminar(HrGradoinstruccion entity)
        {
            hrGradoinstruccionDao.eliminar(entity);
        }

        public HrGradoinstruccion obtenerPorId(HrGradoinstruccionPk pk)
        {
            return hrGradoinstruccionDao.obtenerPorId(pk.Gradoinstruccion);
        }

        public List<HrGradoinstruccion> listarActivos()
        {
            return hrGradoinstruccionDao.listarActivos();
        }

        public List<HrGradoinstruccion> listarTodos()
        {
            return hrGradoinstruccionDao.listarTodos();
        }

        public List<HrGradoinstruccion> listar(DtoFiltro filtro)
        {
            return hrGradoinstruccionDao.listar(filtro);
        }
    }
}
