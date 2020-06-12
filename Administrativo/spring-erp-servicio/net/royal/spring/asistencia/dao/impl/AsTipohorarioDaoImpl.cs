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
    public class AsTipohorarioDaoImpl : GenericoDaoImpl<AsTipohorario>, AsTipohorarioDao
    {
        private IServiceProvider servicioProveedor;


        public AsTipohorarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "astipohorario")
        {
            servicioProveedor = _servicioProveedor;
        }

        public string[] listarHorasSemana(int? tipohorario)
        {

            var resultado = new String[] { "", "", "", "", "", "", "" };

            if (!tipohorario.HasValue)
            {
                return resultado;
            }

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo_dia", ConstanteUtil.TipoDato.Integer, tipohorario));

            _reader = this.listarPorQuery("astipohorario.listarHorasSemana", parametros);
            var c = 0;
            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    resultado[c] = _reader.GetString(0);

                c++;
            }
            this.dispose();

            return resultado;
        }

        public AsTipohorario obtenerPorId(AsTipohorarioPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
