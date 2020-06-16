using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;

namespace net.royal.spring.programasocial.dao.impl {
    public class PsSocioAmbientalDaoImpl : GenericoDaoImpl<PsSocioAmbiental>, PsSocioAmbientalDao {
        private IServiceProvider servicioProveedor;
        private PsBeneficiarioDao psBeneficiarioDao;


        public PsSocioAmbientalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pssocioambiental") {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
        }

        public PsSocioAmbiental obtenerPorId(PsSocioAmbientalPk id) {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSocioAmbiental obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            return obtenerPorId(new PsSocioAmbientalPk(pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental));
        }

        private List<MensajeUsuario> validar(PsSocioAmbiental bean) {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();


            if (bean.FechaInforme == null) {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Fecha del Informe es obligatoria"));
            }

            if (bean.EvaluadoBoolean) {
                if (String.IsNullOrEmpty(bean.Comentarios)) {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el Comentario"));
                }
                return lstRetorno;
            }

            if (bean.IdCooperacion == null || bean.IdCooperacion == "") {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Cooperación es requerido"));
            }

            if (bean.IdAsertavidad == null || bean.IdAsertavidad == "") {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Asertividad es requerido"));
            }

            if (bean.IdEmpatia == null || bean.IdEmpatia == "") {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Empatia es requerido"));
            }

            if (bean.IdComunicacion == null || bean.IdComunicacion == "") {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Comunicación es requerido"));
            }

            if (bean.ValidarNinos) {
                if (bean.IdConflictos == null || bean.IdConflictos == "") {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Resolución de Conflictos es requerido"));
                }

                if (bean.IdEmocional == null || bean.IdEmocional == "") {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Autocontrol Emocional es requerido"));
                }

            }
            else {
                if (bean.IdIntegracion == null || bean.IdIntegracion == "") {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Integración es requerido"));
                }

                if (bean.IdParticipacion == null || bean.IdParticipacion == "") {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Participación es requerido"));
                }
            }

            return lstRetorno;
        }

        public PsSocioAmbiental coreInsertar(UsuarioActual usuarioActual, PsSocioAmbiental bean, String estado) {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;

            if (bean.EvaluadoBoolean) {
                bean.Evaluado = "S";
            }
            else {
                bean.Evaluado = "";
            }

            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            bean.IdSocioAmbiental = this.generarCodigo();

            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion, bean.IdBeneficiario).obtenerArreglo());
            if (bean.IdOrigen != "EVA") {
                
                psBeneficiario.IdComponenteSocioAmbiental = bean.IdSocioAmbiental;
                psBeneficiarioDao.actualizar(psBeneficiario);
            }
            else {
                if (psBeneficiario.IdComponenteSocioAmbiental == null) {
                    psBeneficiario.IdComponenteSocioAmbiental = bean.IdSocioAmbiental;
                    psBeneficiarioDao.actualizar(psBeneficiario);
                }
            }

            this.registrar(bean);
            return bean;
        }

        public int generarCodigo() {
            IQueryable<PsSocioAmbiental> query = this.getCriteria();

            List<PsSocioAmbiental> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0) {
                var = (int)(lst.Max(bean => bean.IdSocioAmbiental));
            }

            return var + 1;
        }

        public PsSocioAmbiental coreActualizar(UsuarioActual usuarioActual, PsSocioAmbiental bean, String estado) {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;

            if (bean.EvaluadoBoolean) {
                bean.Evaluado = "S";
            }
            else {
                bean.Evaluado = "";
            }

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            coreEliminar(new PsSocioAmbientalPk(pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental));
        }

        public void coreEliminar(PsSocioAmbientalPk id) {
            PsSocioAmbiental bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, PsSocioAmbientalPk id) {
            PsSocioAmbiental bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            return coreAnular(usuarioActual, new PsSocioAmbientalPk(pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental));
        }

        public ParametroPaginacionGenerico listarSocioAmbiental(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PsSocioAmbiental> lstResultado = new List<PsSocioAmbiental>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto)) {
                filtro.NombreCompleto = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea)) {
                filtro.IdArea = null;
            }
            if (String.IsNullOrEmpty(filtro.IdSexo)) {
                filtro.IdSexo = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            _reader = this.listarPorQuery("pssocioambiental.listarPaginacion", parametros);

            while (_reader.Read()) {
                PsSocioAmbiental bean = new PsSocioAmbiental();


                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdSocioAmbiental = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.IdPeriodo = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.IdConflictos = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.IdComunicacion = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdEmocional = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdCooperacion = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdAsertavidad = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdEmpatia = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdIntegracion = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdParticipacion = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.Comentarios = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.Estado = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.NombreCompleto = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    contador = _reader.GetInt32(16);
                if (!_reader.IsDBNull(17))
                    bean.Evaluado = _reader.GetString(17);

                if (!_reader.IsDBNull(18))
                    bean.Edad = _reader.GetInt32(18);

                if (!_reader.IsDBNull(19))
                    bean.sexo = _reader.GetString(19);

                if (!_reader.IsDBNull(20))
                    bean.secuencia = _reader.GetInt32(20);


                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro) {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PsSocioAmbiental> lstResultado = new List<PsSocioAmbiental>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto)) {
                filtro.NombreCompleto = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea)) {
                filtro.IdArea = null;
            }
            if (String.IsNullOrEmpty(filtro.IdSexo)) {
                filtro.IdSexo = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));


            _reader = this.listarPorQuery("pssocioambiental.listarPaginacionConsulta", parametros);

            while (_reader.Read()) {
                PsSocioAmbiental bean = new PsSocioAmbiental();


                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdSocioAmbiental = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.IdPeriodo = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.IdConflictos = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.IdComunicacion = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdEmocional = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdCooperacion = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdAsertavidad = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdEmpatia = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdIntegracion = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdParticipacion = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.Comentarios = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.Estado = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.NombreCompleto = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    contador = _reader.GetInt32(16);

                if (!_reader.IsDBNull(17))
                    bean.NombreConflictos = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.NombreComunicacion = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.NombreEmocional = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.NombreCooperacion = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.NombreAsertividad = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.NombreEmpatia = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.NombreIntegracion = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.NombreParticipacion = _reader.GetString(24);
                if (!_reader.IsDBNull(25))
                    bean.Evaluado = _reader.GetString(25);

                if (!_reader.IsDBNull(26))
                    bean.sexo = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.secuencia = _reader.GetInt32(27);
                if (!_reader.IsDBNull(28))
                    bean.Edad = _reader.GetInt32(28);


                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }
    }
}
