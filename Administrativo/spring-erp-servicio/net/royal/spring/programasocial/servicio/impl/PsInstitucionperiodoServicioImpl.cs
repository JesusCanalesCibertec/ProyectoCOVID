using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Text.RegularExpressions;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsInstitucionperiodoServicioImpl : GenericoServicioImpl, PsInstitucionperiodoServicio {

        private IServiceProvider servicioProveedor;
        private PsInstitucionperiodoDao psInstitucionperiodoDao;

        public PsInstitucionperiodoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psInstitucionperiodoDao = servicioProveedor.GetService<PsInstitucionperiodoDao>();
        }

        public void copiarPeriodo(DtoInstitucionperiodo dtoInstitucionperiodo) {
            psInstitucionperiodoDao.copiarPeriodo(dtoInstitucionperiodo);
        }

        public List<DtoInstitucionperiodo> coreActualizar(List<DtoInstitucionperiodo> list) {



            for (int i = 0; i < list.Count; i++) {
                string cadena = null;
                PsInstitucionperiodo bean = new PsInstitucionperiodo();

                bean = psInstitucionperiodoDao.obtenerPorId(new PsInstitucionperiodoPk(
                    list[i].codInstitucion,
                    list[i].codAplicacion,
                    list[i].codGrupo,
                    list[i].codConcepto,
                    list[i].codPeriodo
                    ).obtenerArreglo());

                /*
                String[] parts = list[i].codPeriodo.Split("-");
                String part1 = parts[0];
                String part2 = parts[1];

                cadena = part1 + part2;

                bean.IdPeriodo = cadena;
                */
                bean.IdPeriodo = list[i].codPeriodo;
                bean.Fechainicio = list[i].fechainicio;
                bean.Fechafin = list[i].fechafin;
                bean.Fechainicioregistro = list[i].fechainicioregistro;
                bean.Fechafinregistro = list[i].fechafinregistro;

                psInstitucionperiodoDao.actualizar(bean);


            }

            return list;
        }

        public List<DtoInstitucionperiodo> listado(string pIdInstitucion, string pIdPeriodo) {
            return psInstitucionperiodoDao.listado(pIdInstitucion, pIdPeriodo);
        }

        public List<DtoTabla> listarPeriodoPorComponente(string pIdInstitucion, string pIdPrograma, string pIdComponente) {
            return psInstitucionperiodoDao.listarPeriodoPorComponente(pIdInstitucion, pIdPrograma, pIdComponente);
        }

        public List<DtoTabla> periodoListarSimple(string idPeriodo) {
            return psInstitucionperiodoDao.periodoListarSimple(idPeriodo);
        }
    }
}

