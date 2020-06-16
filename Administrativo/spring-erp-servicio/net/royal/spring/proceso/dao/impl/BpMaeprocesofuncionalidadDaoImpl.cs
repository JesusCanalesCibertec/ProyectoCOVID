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
    public class BpMaeprocesofuncionalidadDaoImpl : GenericoDaoImpl<BpMaeprocesofuncionalidad>, BpMaeprocesofuncionalidadDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesofuncionalidadDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesofuncionalidad")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<DtoBpMaeprocesofuncionalidad> listado(string codigo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpMaeprocesofuncionalidad> lst = new List<DtoBpMaeprocesofuncionalidad>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, codigo));


            _reader = this.listarPorQuery("bpmaeprocesofuncionalidad.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpMaeprocesofuncionalidad bean = new DtoBpMaeprocesofuncionalidad();

                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomProceso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.idFuncionalidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomFuncionalidad = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.visibleDefecto = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.habilitadoDefecto = _reader.GetString(5);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

       

        public string obtenercadena(string pIdProceso, string pNombre)
        {
            IQueryable<BpMaeprocesofuncionalidad> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == pIdProceso && p.Nombre == pNombre);

            List<BpMaeprocesofuncionalidad> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdProceso, string pIdfuncionalidad)
        {
            IQueryable<BpMaeprocesofuncionalidad> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdFuncionalidad == pIdfuncionalidad
                );

            List<BpMaeprocesofuncionalidad> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdFuncionalidad;
            return null;
        }


    }
}
