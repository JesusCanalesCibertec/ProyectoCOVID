using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.constante;
using System.Collections.Generic;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.sistema.util;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.dao.impl
{
    public class UsuarioDaoImpl : GenericoDaoImpl<Usuario>, UsuarioDao
    {
        private IServiceProvider servicioProveedor;


        public UsuarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "usuario")
        {
            servicioProveedor = _servicioProveedor;
        }

        public void actualizarCodigoUsuarioDeEmpleado(string idCompaniaSocio, int? idEmpleado, string codigoUsuario)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_compania_socio", ConstanteUtil.TipoDato.String, idCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, idEmpleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_usuario", ConstanteUtil.TipoDato.String, codigoUsuario));
            this.ejecutarPorQuery("usuario.actualizarCodigoUsuarioDeEmpleado", parametros);
        }

        /*public void actualizarConformidad(string usuarioLogin)
        {
            Usuario u = obtenerPorId(new UsuarioPk() { Usuario = usuarioLogin }.obtenerArreglo());
            if (u != null)
            {
                u.flagConformidadDatos = "S";
                u.aprobacionFechaConformidadDatos = DateTime.Now;
                actualizar(u);
            }
        }*/

        /*public void actualizarConformidadBoleta(string usuarioLogin)
        {
            Usuario u = obtenerPorId(new UsuarioPk() { Usuario = usuarioLogin }.obtenerArreglo());
            if (u != null)
            {
                u.flagConformidadBoleta = "S";
                u.aprobacionFechaConformidadBoleta = DateTime.Now;
                actualizar(u);
            }
        }*/

        /*public void ActualizarConformidadCambioHorario(string usuarioLogin)
        {
            Usuario u = obtenerPorId(new UsuarioPk() { Usuario = usuarioLogin }.obtenerArreglo());
            if (u != null)
            {
                u.flagConvenioCambioHorario = "S";
                u.aprobacionFechaConvenioCambioHorario = DateTime.Now;
                actualizar(u);
            }
        }*/

        public bool existeEmpleado(string idCompaniaSocio, int? idEmpleado)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_compania_socio", ConstanteUtil.TipoDato.String, idCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, idEmpleado));
            _reader = this.listarPorQuery("usuario.existeEmpleado", parametros);
            while (_reader.Read())
            {
                contador++;
            }
            this.dispose();
            if (contador == 0)
                return false;
            else
                return true;
        }

        public void guardarHistorial(string usuario, string clave)
        {
            clave = clave.Replace("'", "''");
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("usuario", ConstanteUtil.TipoDato.String, usuario));
            parametros.Add(new ParametroPersistenciaGenerico("clave", ConstanteUtil.TipoDato.String, clave));
            ejecutarPorQuery("usuario.ingresarHistorial", parametros);
        }

        public ParametroPaginacionGenerico listarReporteSeguridad(ParametroPaginacionGenerico paginacion, string compania, int? empleado, string rEPORTE_COMPROMISO_DATOS)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoEmpleadoBasico> lstRegistros = new List<DtoEmpleadoBasico>();

            if (UInteger.esCeroOrNulo(empleado) || UString.estaVacio(compania))
            {
                empleado = null;
                compania = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_compania", ConstanteUtil.TipoDato.String, compania));

            if (rEPORTE_COMPROMISO_DATOS.Equals(Usuario.REPORTE_COMPROMISO_DATOS))
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_flag_datos", ConstanteUtil.TipoDato.String, "S"));
                parametros.Add(new ParametroPersistenciaGenerico("p_flag_boletas", ConstanteUtil.TipoDato.String, null));
            }
            else if (rEPORTE_COMPROMISO_DATOS.Equals(Usuario.REPORTE_CONFORMIDAD_BOLETAS))
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_flag_datos", ConstanteUtil.TipoDato.String, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_flag_boletas", ConstanteUtil.TipoDato.String, "S"));
            }

            Int32 contador = this.contar("usuario.contarReporteSeguridad", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "usuario.listarReporteSeguridad");

            while (_reader.Read())
            {
                DtoEmpleadoBasico bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.PersonaId = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PersonaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.correo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CodigoUsuario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Fecha = _reader.GetDateTime(4);

                lstRegistros.Add(bean);
            }

            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        public UsuarioActual obtenerDatosEmpleadoPorUsuario(string idCompaniaSocio, string codigoUsuario, String origen)
        {
            UsuarioActual bean = null;

            
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_compania_socio", ConstanteUtil.TipoDato.String, idCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_usuario", ConstanteUtil.TipoDato.String, codigoUsuario));

            _reader = this.listarPorQuery("usuario.obtenerSuperAdmin", parametros);



            while (_reader.Read())
            {
                bean = new UsuarioActual();
                if (!_reader.IsDBNull(0))
                    bean.CompaniaSocioCodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CompaniaSocioNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.UsuarioCodigo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.UsuarioCorreoInterno = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.UsuarioLogin = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.PersonaNombres = _reader.GetString(5);

                //_reader = this.listarPorQuery("usuario.obtenerDatosEmpleadoMinedu", parametros);



                //    while (_reader.Read())
                //    {
                //        bean = new UsuarioActual();
                //        if (!_reader.IsDBNull(0))
                //            bean.CompaniaSocioCodigo = _reader.GetString(0);
                //        if (!_reader.IsDBNull(1))
                //            bean.CompaniaSocioNombre = _reader.GetString(1);
                //        if (!_reader.IsDBNull(2))
                //            bean.UsuarioCodigo = _reader.GetString(2);
                //        if (!_reader.IsDBNull(3))
                //            bean.UsuarioCorreoInterno = _reader.GetString(3);
                //        if (!_reader.IsDBNull(4))
                //            bean.UsuarioLogin = _reader.GetString(4);
                //        if (!_reader.IsDBNull(5))
                //            bean.PersonaId = _reader.GetInt32(5);
                //        if (!_reader.IsDBNull(6))
                //            bean.PersonaTipoDocumento = _reader.GetString(6);
                //        if (!_reader.IsDBNull(7))
                //            bean.PersonaNroDocumento = _reader.GetString(7);
                //        if (!_reader.IsDBNull(8))
                //            bean.PersonaNombreCompleto = _reader.GetString(8);
                //        if (!_reader.IsDBNull(9))
                //            bean.PersonaNombres = _reader.GetString(9);
                //        if (!_reader.IsDBNull(10))
                //            bean.PersonaApellidos = _reader.GetString(10);
                //        if (!_reader.IsDBNull(11))
                //            bean.PersonaApellidoPaterno = _reader.GetString(11);
                //        if (!_reader.IsDBNull(12))
                //            bean.PersonaApellidoMaterno = _reader.GetString(12);
                //        if (!_reader.IsDBNull(13))
                //            bean.UnidadNegocioAsignadaCodigo = _reader.GetString(13);
                //        if (!_reader.IsDBNull(14))
                //            bean.UnidadNegocioAsignadaNombre = _reader.GetString(14);
                //        if (!_reader.IsDBNull(15))
                //            bean.SucursalCodigo = _reader.GetString(15);
                //        if (!_reader.IsDBNull(16))
                //            bean.SucursalNombre = _reader.GetString(16);
                //        if (!_reader.IsDBNull(17))
                //            bean.DepartamentoCodigo = _reader.GetString(17);
                //        if (!_reader.IsDBNull(18))
                //            bean.DepartamentoNombre = _reader.GetString(18);
                //        if (!_reader.IsDBNull(19))
                //            bean.PuestoEmpresaCodigo = _reader.GetInt32(19);
                //        if (!_reader.IsDBNull(20))
                //            bean.PuestoEmpresaNombre = _reader.GetString(20);

                //        if (!_reader.IsDBNull(21))
                //            bean.CentroCostosCodigo = _reader.GetString(21);
                //        if (!_reader.IsDBNull(22))
                //            bean.CentroCostosNombre = _reader.GetString(22);
                //        if (!_reader.IsDBNull(23))
                //            bean.tipoPlanilla = _reader.GetString(23);
                //        if (!_reader.IsDBNull(24))
                //            bean.CodigoPosicion = _reader.GetInt32(24);
                //        if (!_reader.IsDBNull(25))
                //            bean.estadoEmpleado = _reader.GetString(25);
                //        if (!_reader.IsDBNull(26))
                //            bean.estado = _reader.GetString(26);
                //        if (!_reader.IsDBNull(27))
                //            bean.carnetId = _reader.GetString(27);
                //        if (!_reader.IsDBNull(28))
                //            bean.flagConformidadDatos = _reader.GetString(28);
                //        if (!_reader.IsDBNull(29))
                //            bean.ExpirarPasswordFlag = _reader.GetString(29);
                //        if (!_reader.IsDBNull(30))
                //            bean.FechaExpiracion = _reader.GetDateTime(30);
                //        if (!_reader.IsDBNull(31))
                //            bean.Unidadoperativa = _reader.GetString(31);
            }

            this.dispose();

            return bean;
        }
        public String obtenerCompaniaPorDefecto(string codigoUsuario)
        {
            String retorno = "";
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_usuario", ConstanteUtil.TipoDato.String, codigoUsuario));
            _reader = this.listarPorQuery("usuario.obtenerCompaniaPorDefecto", parametros);
            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    retorno = _reader.GetString(0);
            }
            this.dispose();

            return retorno;
        }

        public DtoEmpleadoSeguridad obtenerEmpleadoEstados(string idCompaniaSocio, string codigoUsuario)
        {
            DtoEmpleadoSeguridad bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_compania_socio", ConstanteUtil.TipoDato.String, idCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_usuario", ConstanteUtil.TipoDato.String, codigoUsuario));
            _reader = this.listarPorQuery("usuario.obtenerEmpleadoEstados", parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleadoSeguridad();
                if (!_reader.IsDBNull(0))
                    bean.IdEmpleado = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.IdEstado = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.IdEstadoEmpleado = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CodigoUsuario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.IdCompaniaSocio = _reader.GetString(4);
            }
            this.dispose();

            return bean;
        }

        public Usuario obtenerPorId(UsuarioPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public bool validarClaveHistorico(string usuario, string claveEncriptada)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            String claveValidar = claveEncriptada.Replace("'", "''");

            parametros.Add(new ParametroPersistenciaGenerico("usuario", ConstanteUtil.TipoDato.String, usuario));
            parametros.Add(new ParametroPersistenciaGenerico("clave", ConstanteUtil.TipoDato.String, claveEncriptada));

            _reader = this.listarPorQuery("usuario.validarClaveHistorico", parametros);
            var valida = true;

            while (_reader.Read())
            {
                valida = false;
            }

            this.dispose();

            return valida;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroUsuario filtro)
        {
            Int32 contador = 0;

            List<Usuario> lstRetorno = new List<Usuario>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.codUsuario))
                filtro.codUsuario = null;



            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));


            contador = this.contar("usuario.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "usuario.filtroPaginacion");

            while (_reader.Read())
            {
                Usuario bean = new Usuario();

                if (!_reader.IsDBNull(0))
                    bean.Usuario = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);



                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public UsuarioActual Logeo(string idUsuario)
        {
            UsuarioActual bean = null;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_usuario", ConstanteUtil.TipoDato.String, idUsuario));

            _reader = this.listarPorQuery("usuario.obtenerUsuarioconpermiso", parametros);

            while (_reader.Read())
            {
                bean = new UsuarioActual();
                if (!_reader.IsDBNull(0))
                    bean.CompaniaSocioCodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CompaniaSocioNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.UsuarioCodigo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.UsuarioCorreoInterno = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.UsuarioLogin = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.PersonaNombres = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.PersonaApellidos = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.estado = _reader.GetString(7);
            }

            this.dispose();

            return bean;
        }
    }
}
