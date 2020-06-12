using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio.impl
{

    public class AsPeriodoServicioImpl : GenericoServicioImpl, AsPeriodoServicio
    {

        private IServiceProvider servicioProveedor;
        private AsPeriodoDao asPeriodoDao;

        public AsPeriodoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            asPeriodoDao = servicioProveedor.GetService<AsPeriodoDao>();
        }

        public bool esPeriodoAcitvo(Decimal empleado, DateTime fechadesde, DateTime fechahasta)
        {
            return asPeriodoDao.esPeriodoAcitvo(empleado, fechadesde, fechahasta);
        }

        public List<AsPeriodo> listarTodos()
        {
            return asPeriodoDao.listarTodos();
        }

        public string obtenerPeriodo(int empleado)
        {
            return asPeriodoDao.obtenerPeriodo(empleado);
        }

        public AsPeriodo obtenerPorId(AsPeriodoPk pk)
        {
            return asPeriodoDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
