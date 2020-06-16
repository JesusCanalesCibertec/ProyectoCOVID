using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpProcesoconexioneventoDaoImpl : GenericoDaoImpl<BpProcesoconexionevento>, BpProcesoconexioneventoDao
    {
        private IServiceProvider servicioProveedor;

        public BpProcesoconexioneventoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpprocesoconexionevento")
        {
            servicioProveedor = _servicioProveedor;
        }

  

        public List<DtoBpProcesoconexionevento> listado(string pIdProceso, int pIdConexion)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpProcesoconexionevento> lst = new List<DtoBpProcesoconexionevento>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, pIdProceso));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdConexion", ConstanteUtil.TipoDato.Integer, pIdConexion));


            _reader = this.listarPorQuery("bpprocesoconexionevento.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpProcesoconexionevento bean = new DtoBpProcesoconexionevento();

                if (!_reader.IsDBNull(0))
                    bean.codProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codVersion = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.codConexion = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.codEvento = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.NomEvento = _reader.GetString(4);

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public string obtenercodigo(BpProcesoconexioneventoPk bean)
        {
            IQueryable<BpProcesoconexionevento> query = this.getCriteria();
            query = query.Where(
                p => 
                p.IdProceso == bean.IdProceso && 
                p.IdVersion == bean.IdVersion && 
                p.IdConexion == bean.IdConexion &&
                p.IdEvento == bean.IdEvento
                );

            List<BpProcesoconexionevento> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdEvento;
            return null;
        }
    }
}
