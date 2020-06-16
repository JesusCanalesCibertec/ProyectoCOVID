using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using System.Collections.Generic;

namespace net.royal.spring.asistencia.dao.impl
{
    public class AsPeriodoDaoImpl : GenericoDaoImpl<AsPeriodo>, AsPeriodoDao
    {
        private IServiceProvider servicioProveedor;


        public AsPeriodoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asperiodo")
        {
            servicioProveedor = _servicioProveedor;
        }
        public bool esPeriodoAcitvo(Decimal empleado, DateTime fechadesde, DateTime fechahasta)
        {
            bool resultado = false;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, fechahasta));

            Int32 contador = this.contar("asperiodo.contarPeriodoActivo", parametros);

            if (contador > 0)
            {
                resultado = true;
            }

            return resultado;
        }

        public string obtenerPeriodo(int empleado)
        {
            AsPeriodo resultado = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("asperiodo.obtenerPeriodo", parametros);

            while (_reader.Read())
            {
                resultado = new AsPeriodo();
                if (!_reader.IsDBNull(0))
                    resultado.Periodoplanilla = _reader.GetString(0);
               
            }
            this.dispose();
            return resultado.Periodoplanilla;
        }

        public AsPeriodo obtenerPorEmpleado(int? solicitanteId)
        {
            AsPeriodo resultado = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, solicitanteId));

            _reader = this.listarPorQuery("asperiodo.obtenerPorEmpleado", parametros);

            while (_reader.Read())
            {
                resultado = new AsPeriodo();
                if (!_reader.IsDBNull(0))
                    resultado.Fechadesde = _reader.GetDateTime(0);
                if (!_reader.IsDBNull(1))
                    resultado.Fechahasta = _reader.GetDateTime(1);
                if (!_reader.IsDBNull(2))
                    resultado.Estado = _reader.GetString(2);
            }


            this.dispose();
            return resultado;
        }

        public List<AsPeriodo> obtenerPorEmpleadoRangoFecha(DateTime? fecha, DateTime? fechafin, int? solicitanteId)
        {
            if (!fechafin.HasValue)
            {
                fechafin = fecha;
            }

            List<AsPeriodo> resultados = new List<AsPeriodo>();
            AsPeriodo res;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, solicitanteId));
            parametros.Add(new ParametroPersistenciaGenerico("desde", ConstanteUtil.TipoDato.Date, fecha));
            parametros.Add(new ParametroPersistenciaGenerico("hasta", ConstanteUtil.TipoDato.Date, fechafin));

            _reader = this.listarPorQuery("asperiodo.obtenerPorEmpleadoRango", parametros);

            while (_reader.Read())
            {
                res = new AsPeriodo();
                if (!_reader.IsDBNull(0))
                    res.Fechadesde = _reader.GetDateTime(0);
                if (!_reader.IsDBNull(1))
                    res.Fechahasta = _reader.GetDateTime(1);
                if (!_reader.IsDBNull(2))
                    res.Estado = _reader.GetString(2);

                resultados.Add(res);
            }


            this.dispose();
            return resultados;
        }

        public AsPeriodo obtenerPorId(AsPeriodoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
