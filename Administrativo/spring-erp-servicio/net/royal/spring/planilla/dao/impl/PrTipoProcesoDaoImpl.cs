using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using System.Data.Common;
using net.royal.spring.planilla.dominio;
using net.royal.spring.framework.core;

namespace net.royal.spring.planilla.dao.impl
{
    public class PrTipoProcesoDaoImpl : GenericoDaoImpl<PrTipoProceso>, PrTipoProcesoDao
    {
        public PrTipoProcesoDaoImpl(GenericoDbContext context) : base(context, "prtipoproceso")
        {
        }

        public List<PrTipoProceso> listarActivos()
        {
            IQueryable<PrTipoProceso> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            List<PrTipoProceso> lstlista = query.ToList();

            return lstlista;
        }

        public List<DtoTabla> listarPorPeriodoEmpleado(string periodo, int? personaId)
        {
            DtoTabla bean = null;
            List<DtoTabla> lista = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, personaId.Value));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, periodo));

            _reader = this.listarPorQuery("prtipoproceso.listarPorPeriodoEmpleado", parametros);
            while (_reader.Read())
            {
                bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);
                lista.Add(bean);

            }
            this.dispose();



            return lista;
        }

        public List<PrTipoProceso> listarProcesosPrestamo()
        {
            var permitidos = new string[] { "SM1", "SM2", "SM3", "SM4", "SM5" };

            IQueryable<PrTipoProceso> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            query = query.Where(p => permitidos.Contains(p.TipoProceso));
            List<PrTipoProceso> lstlista = query.ToList();

            return lstlista;
        }
    }
}
