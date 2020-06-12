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
    public class BpProcesorequerimientoDaoImpl : GenericoDaoImpl<BpProcesorequerimiento>, BpProcesorequerimientoDao
    {
        private IServiceProvider servicioProveedor;

        public BpProcesorequerimientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpprocesorequerimiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpProcesorequerimiento> listarPorProceso(BpProcesoPk bpProcesoPk)
        {
            IQueryable<BpProcesorequerimiento> cri = this.getCriteria();
            cri = cri.Where(x => x.IdProceso == bpProcesoPk.IdProceso);
            cri = cri.Where(x => x.IdVersion == bpProcesoPk.IdVersion);
            return cri.ToList();
        }

        public List<DtoBpProcesorequerimiento> listado(string codigo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpProcesorequerimiento> lst = new List<DtoBpProcesorequerimiento>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, codigo));


            _reader = this.listarPorQuery("bpprocesorequerimiento.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpProcesorequerimiento bean = new DtoBpProcesorequerimiento();

                if (!_reader.IsDBNull(0))
                    bean.idVersion = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.idProceso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nomProceso = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.idRequerimiento = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nomRequerimiento = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.obligatorio = _reader.GetString(5);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public string obtenercodigo(string pIdProceso, string pIdRequerimiento, Int32? pIdVersion)
        {
            IQueryable<BpProcesorequerimiento> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdRequerimiento == pIdRequerimiento && p.IdVersion == pIdVersion
                );

            List<BpProcesorequerimiento> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdRequerimiento;
            return null;
        }
    }
}
