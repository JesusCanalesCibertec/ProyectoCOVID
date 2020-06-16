using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.constante;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;

namespace net.royal.spring.core.dao.impl
{
    public class EmpleadomastDaoImpl : GenericoDaoImpl<Empleadomast>, EmpleadomastDao
    {
        private IServiceProvider servicioProveedor;


        public EmpleadomastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "empleadomast")
        {
            servicioProveedor = _servicioProveedor;
        }
        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(int? idPersona)
        {
            DtoEmpleadoBasico bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, idPersona));
            _reader = this.listarPorQuery("empleadomast.obtenerInformacionPorIdPersona", parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.CompaniaId = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CompaniaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.PersonaId = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.PersonaNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Sexo = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Documento = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.FechaIngreso = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.FechaNacimiento = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.CodigoUsuario = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.CargoId = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.CargoNombre = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CentroCostosId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.CentroCostosNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.SucursalId = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.SucursalNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.DeptoOrganizacionId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.DeptoOrganizacionNombre = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.UnidadNegocioAsignadaId = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.UnidadNegocioAsignadaNombre = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.TipoPlanillaId = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.TipoPlanillaNombre = _reader.GetString(20);


                if (!_reader.IsDBNull(21))
                    bean.EstadoEmpleado = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.EstadoEmpleadoNombre = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.Personaant = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.PuestoEmpresaCodigo = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.PuestoEmpresaNombre = _reader.GetString(25);

                if (!_reader.IsDBNull(26))
                    bean.FechaInicioContrato = _reader.GetDateTime(26);
                if (!_reader.IsDBNull(27))
                    bean.NombrePersonaCts = _reader.GetString(27);

                if (!_reader.IsDBNull(28))
                    bean.nombreJefeDirecto = _reader.GetString(28);


                //bean.idInstitucion = _reader.GetString(29);

                //bean.idInstitucionNombre = _reader.GetString(30);
            }
            this.dispose();

            return bean;
        }

        public List<Empleadomast> obtenerVariosPorCorreoInterno(string CorreoInternoEnviado)
        {
            if (UString.estaVacio(CorreoInternoEnviado))
            {
                return new List<Empleadomast>();
            }
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Correointerno.Contains(CorreoInternoEnviado));
            List<Empleadomast> lst = query.ToList();
            return lst;
        }

        public Empleadomast obtenerPorCorreoInterno(string CorreoInternoEnviado)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Correointerno == CorreoInternoEnviado);
            List<Empleadomast> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public Empleadomast obtenerPorCodigoUsuario(string codigousuario)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Codigousuario == codigousuario);
            List<Empleadomast> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public Empleadomast obtenerPorIdEmpleado(int? idEmpleado)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Empleado == idEmpleado);
            List<Empleadomast> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public ParametroPaginacionGenerico listarAniversarios(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBandeja> lstRetorno = new List<DtoEmpleadoBandeja>();
            DtoEmpleadoBandeja bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (filtro.FechaInicio == null)
                filtro.FechaFin = null;
            if (filtro.FechaInicio != null)
            {
                filtro.FechaInicio = UFechaHora.obtenerFechaHoraInicioDia(filtro.FechaInicio);
                if (filtro.FechaFin == null)
                    filtro.FechaFin = filtro.FechaInicio;
                filtro.FechaFin = UFechaHora.obtenerFechaHoraFinDia(filtro.FechaFin);
            }
            if (UInteger.esCeroOrNulo(filtro.IdPersona))
            {
                filtro.IdPersona = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, filtro.IdPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_centro_costos", ConstanteUtil.TipoDato.String, filtro.IdCentroCostos));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_inicio", ConstanteUtil.TipoDato.DateTime, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_fin", ConstanteUtil.TipoDato.DateTime, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.compania));

            contador = this.contar("empleadomast.listarAniversariosContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarAniversariosPaginacion");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBandeja();
                if (!_reader.IsDBNull(0))
                    bean.PersonaId = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PersonaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CentroCostosId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CentroCostosNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaNacimiento = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.FechaIngreso = _reader.GetDateTime(5);
                if (!_reader.IsDBNull(6))
                    bean.CorreoInterno = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Anexo = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.PersonaNroDocumento = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.Dia = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.Mes = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.FechaIngresoDescripcion = _reader.GetString(11);
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }
        public Empleadomast obtenerPorIdEmpleadoCompania(int? idEmpleado, string compania)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Empleado == idEmpleado);
            query = query.Where(p => p.Companiasocio == compania);
            List<Empleadomast> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }


        public DtoEmpleadoBasico obtenerInformacionPorIdPersonaA(UsuarioActual usuario)
        {
            DtoEmpleadoBasico bean = new DtoEmpleadoBasico();

            bean.idInstitucion = usuario.idInstitucion;
            bean.PersonaNombre = usuario.PersonaNombres + " " + usuario.PersonaApellidos;
            bean.CodigoUsuario = usuario.UsuarioLogin;
            bean.periodoActual = usuario.PreferenciasPeriodoContableActual;
            bean.PersonaId = usuario.PersonaId;
            bean.CompaniaId = usuario.CompaniaSocioCodigo;
            return bean;
        }

        public DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario)
        {
            DtoEmpleadoBasico bean = new DtoEmpleadoBasico();

            bean.idInstitucion = usuario.idInstitucion;
            bean.PersonaNombre = usuario.PersonaApellidoPaterno + " " + usuario.PersonaApellidoMaterno + ", " + usuario.PersonaNombreCompleto;
            bean.CodigoUsuario = usuario.UsuarioLogin;
            bean.periodoActual = usuario.PreferenciasPeriodoContableActual;
            bean.PersonaId = usuario.PersonaId;
            bean.CompaniaId = usuario.CompaniaSocioCodigo;            

            var compania = usuario.CompaniaSocioCodigo;
            var idPersona = usuario.PersonaId;

            if (String.IsNullOrWhiteSpace(compania))
            {
                compania = null;
            }

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, idPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, compania));
            _reader = this.listarPorQuery("empleadomast.obtenerInformacionPorIdPersonaCompania", parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.CompaniaId = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CompaniaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.PersonaId = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.PersonaNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Sexo = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Documento = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.FechaIngreso = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.FechaNacimiento = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.CodigoUsuario = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.CargoId = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.CargoNombre = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CentroCostosId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.CentroCostosNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.SucursalId = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.SucursalNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.DeptoOrganizacionId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.DeptoOrganizacionNombre = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.UnidadNegocioAsignadaId = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.UnidadNegocioAsignadaNombre = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.TipoPlanillaId = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.TipoPlanillaNombre = _reader.GetString(20);


                if (!_reader.IsDBNull(21))
                    bean.EstadoEmpleado = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.EstadoEmpleadoNombre = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.Personaant = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.PuestoEmpresaCodigo = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.PuestoEmpresaNombre = _reader.GetString(25);


                if (!_reader.IsDBNull(27))
                    bean.TipoContratoNombre = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.FechaInicioContrato = _reader.GetDateTime(28);
                if (!_reader.IsDBNull(29))
                    bean.FechaFinContrato = _reader.GetDateTime(29);
                if (!_reader.IsDBNull(30))
                    bean.UnidadOperativaNombre = _reader.GetString(30);
                if (!_reader.IsDBNull(31))
                    bean.nombreJefeDirecto = _reader.GetString(31);

                bean.idInstitucion = usuario.idInstitucion;

                bean.idInstitucionNombre = usuario.idInstitucionNombre;
                bean.periodoActual = usuario.PreferenciasPeriodoContableActual;

            }
            this.dispose();

            return bean;
        }

        public DtoEmpleado obtenerCompaniaActiva(int empleado)
        {
            DtoEmpleado resultado = new DtoEmpleado();
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();

            _parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("empleadomast.obtenerCompaniaActiva", _parametros);


            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    resultado.Compania = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    resultado.CodigoUsuario = _reader.GetString(1);
            }
            this.dispose();

            return resultado;
        }

        public ParametroPaginacionGenerico listarAniversariosFiltroDividido(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBandeja> lstRetorno = new List<DtoEmpleadoBandeja>();
            DtoEmpleadoBandeja bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.IdPersona))
                filtro.IdPersona = null;
            if (UString.estaVacio(filtro.IdCentroCostos))
                filtro.IdCentroCostos = null;
            if (filtro.FechaInicio == null)
                filtro.FechaFin = null;
            if (filtro.FechaInicio != null)
            {
                filtro.FechaInicio = UFechaHora.obtenerFechaHoraInicioDia(filtro.FechaInicio);
                if (filtro.FechaFin == null)
                    filtro.FechaFin = filtro.FechaInicio;
                filtro.FechaFin = UFechaHora.obtenerFechaHoraFinDia(filtro.FechaFin);
            }
            if (UInteger.esCeroOrNulo(filtro.Mes))
                filtro.Mes = null;
            if (UInteger.esCeroOrNulo(filtro.Dia))
                filtro.Dia = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, filtro.IdPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_centro_costos", ConstanteUtil.TipoDato.String, filtro.IdCentroCostos));
            parametros.Add(new ParametroPersistenciaGenerico("p_anios", ConstanteUtil.TipoDato.Integer, filtro.Anios));
            parametros.Add(new ParametroPersistenciaGenerico("p_mes", ConstanteUtil.TipoDato.Integer, filtro.Mes));
            parametros.Add(new ParametroPersistenciaGenerico("p_dia", ConstanteUtil.TipoDato.Integer, filtro.Dia));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.compania));

            if (!String.IsNullOrEmpty(filtro.Signo))
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_s_menor_igual", ConstanteUtil.TipoDato.Integer, filtro.Signo.Equals("<=") ? 1 : 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_s_mayor_igual", ConstanteUtil.TipoDato.Integer, filtro.Signo.Equals(">=") ? 1 : 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_s_igual", ConstanteUtil.TipoDato.Integer, filtro.Signo.Equals("=") ? 1 : 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_s_menor", ConstanteUtil.TipoDato.Integer, filtro.Signo.Equals("<") ? 1 : 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_s_mayor", ConstanteUtil.TipoDato.Integer, filtro.Signo.Equals(">") ? 1 : 0));
            }


            contador = this.contar("empleadomast.listarAniversariosContarDividido", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarAniversariosPaginacionDividido");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBandeja();
                if (!_reader.IsDBNull(0))
                    bean.PersonaId = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PersonaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CentroCostosId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CentroCostosNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaNacimiento = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.FechaIngreso = _reader.GetDateTime(5);
                if (!_reader.IsDBNull(6))
                    bean.CorreoInterno = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Anexo = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.PersonaNroDocumento = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.Dia = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.Mes = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.FechaIngresoDescripcion = _reader.GetString(11);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBasico> lstRetorno = new List<DtoEmpleadoBasico>();
            DtoEmpleadoBasico bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.EmpleadoId))
                filtro.EmpleadoId = null;

            if (UString.estaVacio(filtro.EmpleadoEstado))
            {
                filtro.EmpleadoEstado = null;
            }

            if (UString.estaVacio(filtro.IdCompaniaSocio))
            {
                filtro.IdCompaniaSocio = null;
            }
            else if (filtro.IdCompaniaSocio.Equals("null"))
            {
                filtro.IdCompaniaSocio = null;
            }
            if (UString.estaVacio(filtro.IdSucursal))
                filtro.IdSucursal = null;

            if (UString.estaVacio(filtro.IdUnidadNegocioAsignada))
                filtro.IdUnidadNegocioAsignada = null;
            if (UString.estaVacio(filtro.EmpleadoDocumento))
                filtro.EmpleadoDocumento = null;
            if (filtro.EmpleaadoNombreCompleto == null)
                filtro.EmpleaadoNombreCompleto = "";

            //if (UString.estaVacio(paginacion.AtributoOrdenar))
            //{
            //    paginacion.AtributoOrdenar = "PER.NombreCompleto";
            //}

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.EmpleadoEstado));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_companiasocio", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_sucursal", ConstanteUtil.TipoDato.String, filtro.IdSucursal));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_unidadnegocio", ConstanteUtil.TipoDato.String, filtro.IdUnidadNegocioAsignada));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.EmpleadoDocumento));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreempleado", ConstanteUtil.TipoDato.String, filtro.EmpleaadoNombreCompleto));
            //parametros.Add(new ParametroPersistenciaGenerico("p_jefe", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoJefe));
            //parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            //parametros.Add(new ParametroPersistenciaGenerico("p_novalida", ConstanteUtil.TipoDato.String, filtro.EmpleadoNoValida));
            //parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            //parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));

            contador = this.contar("empleadomast.listarBusquedaContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarBusquedaPaginacion");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.PuestoEmpresaCodigo = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PuestoEmpresaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CompaniaId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CompaniaNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.PersonaId = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.PersonaNombre = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Sexo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Documento = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.FechaIngreso = _reader.GetDateTime(8);
                if (!_reader.IsDBNull(9))
                    bean.FechaNacimiento = _reader.GetDateTime(9);
                if (!_reader.IsDBNull(10))
                    bean.CodigoUsuario = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CargoId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.CargoNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.CentroCostosId = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.CentroCostosNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.SucursalId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.SucursalNombre = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.DeptoOrganizacionId = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.DeptoOrganizacionNombre = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.UnidadNegocioAsignadaId = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.UnidadNegocioAsignadaNombre = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.TipoPlanillaId = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.TipoPlanillaNombre = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.EstadoEmpleado = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.EstadoEmpleadoNombre = _reader.GetString(24);
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }

        public ParametroPaginacionGenerico listarCumpleanios(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBandeja> lstRetorno = new List<DtoEmpleadoBandeja>();
            DtoEmpleadoBandeja bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.IdPersona))
                filtro.IdPersona = null;
            if (UString.estaVacio(filtro.IdCentroCostos))
                filtro.IdCentroCostos = null;
            if (filtro.FechaInicio == null)
                filtro.FechaFin = null;
            if (filtro.FechaInicio != null)
            {
                filtro.FechaInicio = UFechaHora.obtenerFechaHoraInicioDia(filtro.FechaInicio);
                if (filtro.FechaFin == null)
                    filtro.FechaFin = filtro.FechaInicio;
                filtro.FechaFin = UFechaHora.obtenerFechaHoraFinDia(filtro.FechaFin);
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, filtro.IdPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_centro_costos", ConstanteUtil.TipoDato.String, filtro.IdCentroCostos));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_inicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_fin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_anio", ConstanteUtil.TipoDato.Integer, (new DateTime()).Year));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.compania));

            contador = this.contar("empleadomast.listarCumpleaniosContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarCumpleaniosPaginacion");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBandeja();
                if (!_reader.IsDBNull(0))
                    bean.PersonaId = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PersonaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CentroCostosId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CentroCostosNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaNacimiento = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.FechaIngreso = _reader.GetDateTime(5);
                if (!_reader.IsDBNull(6))
                    bean.CorreoInterno = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Anexo = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.PersonaNroDocumento = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.FechaNacimientoDescripcion = +_reader.GetInt32(9) + " Años";
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }

        public Empleadomast obtenerPorId(EmpleadomastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public List<Empleadomast> listarPorCorreoInterno(string correo)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Correointerno == correo);
            return query.ToList();
        }

        public string obtenerAfp(string codigoAfp)
        {
            String des = "";
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_afp", ConstanteUtil.TipoDato.String, codigoAfp));

            _reader = this.listarPorQuery("empleadomast.obtenerafp", parametros);

            if (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    des = _reader.GetString(0);
            }
            this.dispose();

            return des;
        }

        public ParametroPaginacionGenerico listarBusquedaUnidaOperativa(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBasico> lstRetorno = new List<DtoEmpleadoBasico>();
            DtoEmpleadoBasico bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.EmpleadoId))
                filtro.EmpleadoId = null;
            if (UString.estaVacio(filtro.EmpleadoEstado))
                filtro.EmpleadoEstado = null;
            if (UString.estaVacio(filtro.IdCompaniaSocio))
            {
                filtro.IdCompaniaSocio = null;
            }
            else if (filtro.IdCompaniaSocio.Equals("null"))
            {
                filtro.IdCompaniaSocio = null;
            }
            if (UString.estaVacio(filtro.IdSucursal))
                filtro.IdSucursal = null;
            if (UString.estaVacio(filtro.IdUnidadNegocioAsignada))
                filtro.IdUnidadNegocioAsignada = null;
            if (UString.estaVacio(filtro.EmpleadoDocumento))
                filtro.EmpleadoDocumento = null;
            if (UString.estaVacio(filtro.EmpleaadoNombreCompleto))
                filtro.EmpleaadoNombreCompleto = null;

            if (UString.estaVacio(paginacion.AtributoOrdenar))
            {
                paginacion.AtributoOrdenar = "PER.NombreCompleto";
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.EmpleadoEstado));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_companiasocio", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_sucursal", ConstanteUtil.TipoDato.String, filtro.IdSucursal));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_unidadnegocio", ConstanteUtil.TipoDato.String, filtro.IdUnidadNegocioAsignada));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.EmpleadoDocumento));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreempleado", ConstanteUtil.TipoDato.String, filtro.EmpleaadoNombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_novalida", ConstanteUtil.TipoDato.String, filtro.EmpleadoNoValida));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));

            parametros.Add(new ParametroPersistenciaGenerico("p_jefeunidad", ConstanteUtil.TipoDato.Integer, filtro.jefeUnidad));


            contador = this.contar("empleadomast.listarBusquedaContar2", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarBusquedaPaginacion2");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.PuestoEmpresaCodigo = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PuestoEmpresaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CompaniaId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CompaniaNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.PersonaId = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.PersonaNombre = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Sexo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Documento = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.FechaIngreso = _reader.GetDateTime(8);
                if (!_reader.IsDBNull(9))
                    bean.FechaNacimiento = _reader.GetDateTime(9);
                if (!_reader.IsDBNull(10))
                    bean.CodigoUsuario = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CargoId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.CargoNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.CentroCostosId = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.CentroCostosNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.SucursalId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.SucursalNombre = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.DeptoOrganizacionId = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.DeptoOrganizacionNombre = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.UnidadNegocioAsignadaId = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.UnidadNegocioAsignadaNombre = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.TipoPlanillaId = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.TipoPlanillaNombre = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.EstadoEmpleado = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.EstadoEmpleadoNombre = _reader.GetString(24);
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }

        public List<Empleadomast> listarPorUsuario(string usuarioRecuperar)
        {
            IQueryable<Empleadomast> query = this.getCriteria();
            query = query.Where(p => p.Codigousuario == usuarioRecuperar);
            List<Empleadomast> lst = query.ToList();
            return lst;
        }

        public ParametroPaginacionGenerico listarBusquedaPuesto(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            Int32 contador = 0;
            List<DtoEmpleadoBasico> lstRetorno = new List<DtoEmpleadoBasico>();
            DtoEmpleadoBasico bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.puesto) || !filtro.puesto.HasValue)
                filtro.puesto = null;

            if (UInteger.esCeroOrNulo(filtro.EmpleadoId))
                filtro.EmpleadoId = null;
            if (UString.estaVacio(filtro.EmpleadoEstado))
                filtro.EmpleadoEstado = null;
            if (UString.estaVacio(filtro.IdCompaniaSocio))
            {
                filtro.IdCompaniaSocio = null;
            }
            else if (filtro.IdCompaniaSocio.Equals("null"))
            {
                filtro.IdCompaniaSocio = null;
            }
            if (UString.estaVacio(filtro.IdSucursal))
                filtro.IdSucursal = null;
            if (UString.estaVacio(filtro.IdUnidadNegocioAsignada))
                filtro.IdUnidadNegocioAsignada = null;
            if (UString.estaVacio(filtro.EmpleadoDocumento))
                filtro.EmpleadoDocumento = null;
            if (UString.estaVacio(filtro.EmpleaadoNombreCompleto))
                filtro.EmpleaadoNombreCompleto = null;

            if (UString.estaVacio(paginacion.AtributoOrdenar))
            {
                paginacion.AtributoOrdenar = "PER.NombreCompleto";
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.EmpleadoEstado));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_companiasocio", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_sucursal", ConstanteUtil.TipoDato.String, filtro.IdSucursal));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_unidadnegocio", ConstanteUtil.TipoDato.String, filtro.IdUnidadNegocioAsignada));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.EmpleadoDocumento));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreempleado", ConstanteUtil.TipoDato.String, filtro.EmpleaadoNombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_novalida", ConstanteUtil.TipoDato.String, filtro.EmpleadoNoValida));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_puesto", ConstanteUtil.TipoDato.Integer, filtro.puesto));


            contador = this.contar("empleadomast.listarBusquedaContar3", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "empleadomast.listarBusquedaPaginacion3");
            while (_reader.Read())
            {
                bean = new DtoEmpleadoBasico();
                if (!_reader.IsDBNull(0))
                    bean.PuestoEmpresaCodigo = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.PuestoEmpresaNombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CompaniaId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CompaniaNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.PersonaId = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.PersonaNombre = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Sexo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Documento = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.FechaIngreso = _reader.GetDateTime(8);
                if (!_reader.IsDBNull(9))
                    bean.FechaNacimiento = _reader.GetDateTime(9);
                if (!_reader.IsDBNull(10))
                    bean.CodigoUsuario = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CargoId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.CargoNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.CentroCostosId = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.CentroCostosNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.SucursalId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.SucursalNombre = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.DeptoOrganizacionId = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.DeptoOrganizacionNombre = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.UnidadNegocioAsignadaId = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.UnidadNegocioAsignadaNombre = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.TipoPlanillaId = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.TipoPlanillaNombre = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.EstadoEmpleado = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.EstadoEmpleadoNombre = _reader.GetString(24);
                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }
        public string obtenerHorario(int persona, string compania, DateTime? inicio)
        {
            if (!inicio.HasValue)
            {
                return "";
            }

            String horario = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("persona", ConstanteUtil.TipoDato.Integer, persona));
            parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            parametros.Add(new ParametroPersistenciaGenerico("inicio", ConstanteUtil.TipoDato.Integer, (int)inicio.Value.DayOfWeek));

            _reader = this.listarPorQuery("empleadomast.obtenerHorario", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    horario = _reader.GetString(0);
            }

            if (UString.estaVacio(horario))
            {
                horario = inicio.Value.ToString("dd/MM/yyyy") + " De 00:00 a 23:59";
            }
            else
            {
                horario = inicio.Value.ToString("dd/MM/yyyy") + " " + horario;
            }

            this.dispose();

            return horario;
        }
        public Empleadomast obtenerPorId(String idCompania, Int32? idEmpleado)
        {
            return this.obtenerPorId(new EmpleadomastPk(idCompania, idEmpleado));
        }
        public List<MensajeUsuario> validarEmpleadoEnCompania(List<MensajeUsuario> lstMensajes, String idCompania, Int32? idEmpleado)
        {
            Empleadomast emp = this.obtenerPorId(idCompania, idEmpleado);
            if (emp == null)
            {
                lstMensajes.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Empleado no existe en la compañia : " + idCompania));
            }
            else
            {
                if (!emp.Estadoempleado.Equals("0"))
                    lstMensajes.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Empleado se encuentra inactivo en la compañia : " + idCompania));
            }
            return lstMensajes;
        }
        public List<DtoTabla> listarEmpresasActivasPorUsuario(string idUsuario)
        {
            String sentenciaSql = "";
            List<DtoTabla> lstDtoTabla = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));

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

        public DateTime? obtenerHoraFinHorarioPorDia(int persona, string compania, DateTime? inicio)
        {
            if (!inicio.HasValue)
            {
                return null;
            }

            DateTime? horario = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("persona", ConstanteUtil.TipoDato.Integer, persona));
            parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            parametros.Add(new ParametroPersistenciaGenerico("inicio", ConstanteUtil.TipoDato.Integer, (int)inicio.Value.DayOfWeek));

            _reader = this.listarPorQuery("empleadomast.obtenerHoraFinHorarioPorDia", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    horario = _reader.GetDateTime(0);
            }

            if (!horario.HasValue)
            {
                horario = new DateTime(2000, 12, 31, 0, 0, 0);
            }

            this.dispose();

            return horario;
        }

        public DateTime? obtenerHoraInicioHorarioPorDia(int persona, string compania, DateTime? inicio)
        {
            if (!inicio.HasValue)
            {
                return null;
            }

            DateTime? horario = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("persona", ConstanteUtil.TipoDato.Integer, persona));
            parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            parametros.Add(new ParametroPersistenciaGenerico("inicio", ConstanteUtil.TipoDato.Integer, (int)inicio.Value.DayOfWeek));

            _reader = this.listarPorQuery("empleadomast.obtenerHoraInicioHorarioPorDia", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    horario = _reader.GetDateTime(0);
            }

            if (!horario.HasValue)
            {
                horario = new DateTime(2000, 12, 31, 23, 59, 59);
            }

            this.dispose();

            return horario;
        }
        public string esFiscalizado(int persona)
        {
            String returno = "N";
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_persona", ConstanteUtil.TipoDato.Integer, persona));
            _reader = this.listarPorSentenciaSQL("select EsFiscalizado from AS_CARNETIDENTIFICACION where EMPLEADO = :p_persona ", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    returno = _reader.GetString(0);
            }
            this.dispose();
            return returno;
        }

        public bool esJefe(int? persona)
        {
            bool returno = false;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_persona", ConstanteUtil.TipoDato.Integer, persona));
            _reader = this.listarPorSentenciaSQL("select * from HR_UNIDADOPERATIVA where RESPONSABLE = :p_persona ", parametros);

            while (_reader.Read())
            {
                returno = true;
            }
            this.dispose();
            return returno;
        }

        public DateTime? obtenerHoraFinHorarioPorDia(int persona, string compania, DateTime inicio, bool conSobretiempo)
        {
            DateTime? horario = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("persona", ConstanteUtil.TipoDato.Integer, persona));
            parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            parametros.Add(new ParametroPersistenciaGenerico("inicio", ConstanteUtil.TipoDato.Integer, (int)inicio.DayOfWeek));

            parametros.Add(new ParametroPersistenciaGenerico("conSobretiempo", ConstanteUtil.TipoDato.String, conSobretiempo ? "S" : "N"));


            _reader = this.listarPorQuery("empleadomast.obtenerHoraFinHorarioPorDia", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    horario = _reader.GetDateTime(0);
            }

            if (!horario.HasValue)
            {
                horario = new DateTime(2000, 12, 31, 0, 0, 0);
            }

            this.dispose();

            return horario;
        }

        public DateTime? obtenerHoraInicioHorarioPorDia(int persona, string compania, DateTime inicio, bool conSobretiempo)
        {
            DateTime? horario = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("persona", ConstanteUtil.TipoDato.Integer, persona));
            parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            parametros.Add(new ParametroPersistenciaGenerico("inicio", ConstanteUtil.TipoDato.Integer, (int)inicio.DayOfWeek));

            parametros.Add(new ParametroPersistenciaGenerico("conSobretiempo", ConstanteUtil.TipoDato.String, conSobretiempo ? "S" : "N"));

            _reader = this.listarPorQuery("empleadomast.obtenerHoraInicioHorarioPorDia", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    horario = _reader.GetDateTime(0);
            }

            if (!horario.HasValue)
            {
                horario = new DateTime(2000, 12, 31, 23, 59, 59);
            }

            this.dispose();

            return horario;
        }

        public List<DtoHorario> obtenerHorarioEmpleado(DateTime par_desde, DateTime par_hasta, int par_empleado)
        {
            List<DtoHorario> lstDtoTabla = new List<DtoHorario>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("par_desde", ConstanteUtil.TipoDato.Date, par_desde));
            parametros.Add(new ParametroPersistenciaGenerico("par_hasta", ConstanteUtil.TipoDato.Date, par_hasta));
            parametros.Add(new ParametroPersistenciaGenerico("par_empleado", ConstanteUtil.TipoDato.Integer, par_empleado));


            _reader = this.listarPorQuery("empleadomast.obtenerHorarioEmpleado", parametros);

            while (_reader.Read())
            {
                DtoHorario bean = new DtoHorario();
                if (!_reader.IsDBNull(0))
                    bean.Empleado = _reader.GetDecimal(0);
                if (!_reader.IsDBNull(1))
                    bean.Fecha = _reader.GetDateTime(1);
                if (!_reader.IsDBNull(2))
                    bean.FechaHasta = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.Intervaloacceso = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Tipohorario = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.DerechoVacacional = _reader.GetDecimal(5);

                lstDtoTabla.Add(bean);
            }
            this.dispose();
            return lstDtoTabla;
        }
    }
}
