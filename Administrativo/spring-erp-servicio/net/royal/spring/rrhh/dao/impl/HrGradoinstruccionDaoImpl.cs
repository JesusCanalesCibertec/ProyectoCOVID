using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using System.Data.Common;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrGradoinstruccionDaoImpl : GenericoDaoImpl<HrGradoinstruccion>, HrGradoinstruccionDao
    {
        public HrGradoinstruccionDaoImpl(GenericoDbContext context):base(context, "hrgradoinstruccion") { 
        }

        public List<HrGradoinstruccion> listar(DtoFiltro filtro)
        {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<HrGradoinstruccion> lst = new List<HrGradoinstruccion>();

            _parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.Codigo));
            _parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
            _parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));

            _reader = this.listarPorSentenciaSQL("select gr.Descripcion,gr.Estado,gr.FlagCentroEstudios,gr.FlagTieneCarrera, " + 
                                                   " gr.GradoInstruccion, gr.gradoinstruccionsuperior, gr.NivelEducativoRTPS, " +
                                                   " gr.UltimaFechaModif,gr.UltimoUsuario " +
                                                   " from HR_GradoInstruccion gr WHERE gr.gradoinstruccion = ISNULL(:p_codigo, gr.gradoinstruccion) " +
                                                   " AND gr.Estado = ISNULL(:p_estado, gr.Estado) " +
                                                   " AND gr.Descripcion LIKE '%' + COALESCE(:p_nombre, gr.Descripcion) + '%'" +
                                                   " ORDER BY gr.Descripcion", _parametros);

            while (_reader.Read())
            {
                HrGradoinstruccion bean = new HrGradoinstruccion();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Estado = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Flagcentroestudios = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Flagtienecarrera = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Gradoinstruccion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Gradoinstruccionsuperior = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Niveleducativortps = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.Ultimafechamodif = _reader.GetDateTime(7);

                if (!_reader.IsDBNull(8))
                    bean.Ultimousuario = _reader.GetString(8);

                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }

        public List<HrGradoinstruccion> listarActivos()
        {
            IQueryable<HrGradoinstruccion> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            List<HrGradoinstruccion> lstlista = query.ToList();

            return lstlista;
        }

        public string obtenerNombreGrado(string nivelacademico)
        {
            IQueryable<HrGradoinstruccion> query = this.getCriteria();

            query = query.Where(p => p.Gradoinstruccion == nivelacademico);
            List< HrGradoinstruccion >datos= query.ToList();

            if (datos.Count > 0)
                return datos[0].Descripcion;
            return null;
        }
    }
}
