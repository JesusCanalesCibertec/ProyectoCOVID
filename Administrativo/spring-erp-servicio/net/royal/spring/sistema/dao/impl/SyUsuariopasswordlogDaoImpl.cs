using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.constante;

namespace net.royal.spring.sistema.dao.impl
{
public class SyUsuariopasswordlogDaoImpl : GenericoDaoImpl<SyUsuariopasswordlog>, SyUsuariopasswordlogDao 
{
        private IServiceProvider servicioProveedor;


	public SyUsuariopasswordlogDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "syusuariopasswordlog") {
            servicioProveedor = _servicioProveedor;
	}

        public DtoUsuarioPasswordLog obtenerUltimoLogin(string idUsuario)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoUsuarioPasswordLog> lstDtoTabla = new List<DtoUsuarioPasswordLog>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));

            _reader = this.listarPorQuery("syusuariopasswordlog.obtenerFechaUltimoLogin", parametros);
            while (_reader.Read())
            {
                DtoUsuarioPasswordLog bean = new DtoUsuarioPasswordLog();

                if (!_reader.IsDBNull(0))
                    bean.FechaUltimoLogin = _reader.GetDateTime(0);
                if (!_reader.IsDBNull(1))
                    bean.NumeroLogins = _reader.GetInt32(1);
               
                lstDtoTabla.Add(bean);
            }

            this.dispose();
            if (lstDtoTabla.Count == 1)
            {
                return lstDtoTabla[0];
            }

		return null;
        }
    }
}
