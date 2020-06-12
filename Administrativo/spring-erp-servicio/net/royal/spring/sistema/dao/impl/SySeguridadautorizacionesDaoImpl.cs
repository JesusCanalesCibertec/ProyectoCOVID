using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;

namespace net.royal.spring.sistema.dao.impl
{
public class SySeguridadautorizacionesDaoImpl : GenericoDaoImpl<SySeguridadautorizaciones>, SySeguridadautorizacionesDao 
{
        private IServiceProvider servicioProveedor;
        
	    public SySeguridadautorizacionesDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "syseguridadautorizaciones") {
                servicioProveedor = _servicioProveedor;
	    }

        public List<DtoTabla> listarEmpresasPorUsuario(string idUsuario) {
            String sentenciaSql;
            List<DtoTabla> lstDtoTabla = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));

            sentenciaSql = "Select	 s.Concepto as 'codigo'";
            sentenciaSql = sentenciaSql + " ,c.Description as 'nombre'";
            sentenciaSql = sentenciaSql + " From SY_SeguridadAutorizaciones s";
            sentenciaSql = sentenciaSql + " Inner Join CompanyOwner c on c.CompanyOwner = s.Concepto";
            sentenciaSql = sentenciaSql + " Where S.AplicacionCodigo = 'SY' And S.Grupo = 'COMPANIASOCIO' And";
            sentenciaSql = sentenciaSql + " S.Usuario = :p_id_usuario";
            
            sentenciaSql = sentenciaSql + " Order By 2";

            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                lstDtoTabla.Add(bean);
            }
            this.dispose();
            return lstDtoTabla;
        }

        public List<DtoTabla> listarEmpresasPorUsuarioEnEmpleadoYSeguridadAutorizacion(string idUsuario)
        {
            String sentenciaSql;
            List<DtoTabla> lstDtoTabla = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));

            sentenciaSql = "Select	 s.Concepto as 'codigo'";
            sentenciaSql = sentenciaSql + " ,c.Description as 'nombre'";
            sentenciaSql = sentenciaSql + " From SY_SeguridadAutorizaciones s";
            sentenciaSql = sentenciaSql + " Inner Join CompanyOwner c on c.CompanyOwner = s.Concepto";
            sentenciaSql = sentenciaSql + " Where S.AplicacionCodigo = 'SY' And S.Grupo = 'COMPANIASOCIO' And";
            sentenciaSql = sentenciaSql + " S.Usuario = :p_id_usuario";

            sentenciaSql = sentenciaSql + " UNION";
            sentenciaSql = sentenciaSql + " Select C1.companyowner as 'codigo', c1.Description as 'nombre'";
            sentenciaSql = sentenciaSql + " From  CompanyOwner c1 INNER JOIN Empleadomast e";
            sentenciaSql = sentenciaSql + " on c1.companyowner = e.companiasocio";
            sentenciaSql = sentenciaSql + " where e.EstadoEmpleado='0' AND e.CodigoUsuario = :p_id_usuario";
            
            sentenciaSql = sentenciaSql + " UNION";
            sentenciaSql = sentenciaSql + " Select C2.companyowner as 'codigo', c2.Description as 'nombre' ";
            sentenciaSql = sentenciaSql + " From  CompanyOwner c2 INNER JOIN Empleadomast e2  on c2.companyowner = e2.companiasocio ";
            sentenciaSql = sentenciaSql + " where e2.EstadoEmpleado='0' AND e2.Empleado IN (sELECT E3.Empleado FROM Empleadomast E3 WHERE e3.CodigoUsuario = :p_id_usuario )";
            
            sentenciaSql = sentenciaSql + " Order By 2";

            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);                
                lstDtoTabla.Add(bean);
            }
            this.dispose();
            return lstDtoTabla;
        }

        public List<DtoTabla> listarPorGrupoYUsuario(string idUsuario)
        {
            String sentenciaSql;
            List<DtoTabla> lstDtoTabla = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));

            sentenciaSql = "select distinct au.concepto as 'codigo' ,mide.DescripcionLocal AS 'nombre'";
            sentenciaSql = sentenciaSql + " from SY_SeguridadAutorizaciones au inner join";
            sentenciaSql = sentenciaSql + " (SELECT u.Usuario FROM Usuario u WHERE u.Usuario = :p_id_usuario ";
            sentenciaSql = sentenciaSql + " union all";
            sentenciaSql = sentenciaSql + " SELECT pu.Perfil Usuario FROM SeguridadPerfilUsuario pu WHERE pu.Usuario = :p_id_usuario ) det";
            sentenciaSql = sentenciaSql + " on au.Usuario = det.Usuario inner join MA_MiscelaneosDetalle mide";

            sentenciaSql = sentenciaSql + " on mide.AplicacionCodigo='PS' AND mide.CodigoTabla='INSTITUC' AND mide.CodigoElemento = au.concepto";
            sentenciaSql = sentenciaSql + " where au.AplicacionCodigo = 'SY' AND au.grupo = 'INSTITUCION'";

            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                lstDtoTabla.Add(bean);
            }
            this.dispose();
            return lstDtoTabla;
        }
    }
}
