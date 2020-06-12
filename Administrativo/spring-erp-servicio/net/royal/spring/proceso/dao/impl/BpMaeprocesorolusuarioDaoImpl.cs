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
    public class BpMaeprocesorolusuarioDaoImpl : GenericoDaoImpl<BpMaeprocesorolusuario>, BpMaeprocesorolusuarioDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesorolusuarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesorolusuario")
        {
            servicioProveedor = _servicioProveedor;
        }


        public List<DtoBpMaeprocesorolusuario> listado(string pIdProceso, string pIdRol, string pIdTipoSeguridad)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpMaeprocesorolusuario> lst = new List<DtoBpMaeprocesorolusuario>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, pIdProceso));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdRol", ConstanteUtil.TipoDato.String, pIdRol));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdTipoSeguridad", ConstanteUtil.TipoDato.String, pIdTipoSeguridad));

            
            _reader = this.listarPorQuery("bpmaeprocesorolusuario.filtro"+ pIdTipoSeguridad, _parametros);

            while (_reader.Read())
            {
                DtoBpMaeprocesorolusuario bean = new DtoBpMaeprocesorolusuario();

                if (!_reader.IsDBNull(0))
                    bean.codProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codRol = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.codTipoSeguridad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.codConcepto = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.NomConcepto = _reader.GetString(4);

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public string obtenercodigo(BpMaeprocesorolusuarioPk bean)
        {
            IQueryable<BpMaeprocesorolusuario> query = this.getCriteria();
            query = query.Where(
                p =>
                p.IdProceso == bean.IdProceso &&
                p.IdRol == bean.IdRol &&
                p.IdTipoSeguridad == bean.IdTipoSeguridad &&
                p.IdConcepto == bean.IdConcepto
                );

            List<BpMaeprocesorolusuario> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdConcepto;
            return null;
        }


    }
}
