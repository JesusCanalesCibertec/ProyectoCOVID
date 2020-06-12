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
    public class BpMaeprocesoelementofuncionalidadDaoImpl : GenericoDaoImpl<BpMaeprocesoelementofuncionalidad>, BpMaeprocesoelementofuncionalidadDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesoelementofuncionalidadDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesoelementofuncionalidad")
        {
            servicioProveedor = _servicioProveedor;
        }


        public List<DtoBpMaeprocesoelementofuncionalidad> listado(string pIdProceso, string pIdElemento)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpMaeprocesoelementofuncionalidad> lst = new List<DtoBpMaeprocesoelementofuncionalidad>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, pIdProceso));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdElemento", ConstanteUtil.TipoDato.String, pIdElemento));


            
            _reader = this.listarPorQuery("bpmaeprocesoelementofuncionalidad.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpMaeprocesoelementofuncionalidad bean = new DtoBpMaeprocesoelementofuncionalidad();

                if (!_reader.IsDBNull(0))
                    bean.codProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codElemento = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.codFuncionalidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomFuncionalidad = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.flagVisible = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.flagHabilitado = _reader.GetString(5);

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public string obtenercodigo(BpMaeprocesoelementofuncionalidadPk bean)
        {
            IQueryable<BpMaeprocesoelementofuncionalidad> query = this.getCriteria();
            query = query.Where(
                p =>
                p.IdProceso == bean.IdProceso &&
                p.IdElemento == bean.IdElemento &&
                p.IdFuncionalidad == bean.IdFuncionalidad
                );

            List<BpMaeprocesoelementofuncionalidad> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdFuncionalidad;
            return null;
        }


    }
}
