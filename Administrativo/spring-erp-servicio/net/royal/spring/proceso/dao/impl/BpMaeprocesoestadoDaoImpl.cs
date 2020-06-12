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
    public class BpMaeprocesoestadoDaoImpl : GenericoDaoImpl<BpMaeprocesoestado>, BpMaeprocesoestadoDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesoestadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesoestado")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpMaeprocesoestado> listarEstadoPorProceso(string idProceso)
        {
            IQueryable<BpMaeprocesoestado> query = this.getCriteria();
            query = query.Where(x => x.IdProceso == idProceso);
            return query.ToList();
        }

        public List<DtoBpMaeprocesoestado> listado(string codigo)
        {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpMaeprocesoestado> lst = new List<DtoBpMaeprocesoestado>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, codigo));


            _reader = this.listarPorQuery("bpmaeprocesoestado.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpMaeprocesoestado bean = new DtoBpMaeprocesoestado();

                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomProceso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.idEstado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomEstado = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.porDefecto = _reader.GetString(4);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }
       


        public string obtenercadena(string pIdProceso, string pNombre)
        {
            IQueryable<BpMaeprocesoestado> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == pIdProceso && p.Nombre == pNombre);

            List<BpMaeprocesoestado> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdProceso, string IdEstado)
        {
            IQueryable<BpMaeprocesoestado> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdEstado == IdEstado
                );

            List<BpMaeprocesoestado> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdEstado;
            return null;
        }
    }
}
