using System;
using System.Text;
using System.Linq;


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
using Microsoft.Extensions.Logging;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsNutricionDaoImpl : GenericoDaoImpl<PsNutricion>, PsNutricionDao
    {

        private IServiceProvider servicioProveedor;
        private ILogger _logger;
        private PsBeneficiarioDao psBeneficiarioDao;


        public PsNutricionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psnutricion")
        {
            servicioProveedor = _servicioProveedor;
            _logger = servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
        }

        public PsNutricion obtenerPorId(PsNutricionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsNutricion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion)
        {
            return obtenerPorId(new PsNutricionPk(pIdInstitucion, pIdBeneficiario, pIdNutricion));
        }

        private List<MensajeUsuario> validar(PsNutricion bean)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            if (bean.FechaInforme == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Fecha del Informe es obligatoria"));
            }

            if (bean.EvaluadoBoolean)
            {
                if (String.IsNullOrEmpty(bean.Comentarios))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el Comentario"));
                }
                return lstRetorno;
            }

            if (bean.Peso == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Peso es requerido"));
            }

            if (bean.Talla == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Talla es requerido"));
            }

            if (bean.ValidarNinos)
            {
                if (bean.IdValoracion == null || bean.IdValoracion == "")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El valoración es requerido"));
                }


            }
            else
            {
                if (bean.IdPresionMensual == null || bean.IdPresionMensual == "")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Presión Manual es requerido"));
                }

                if (bean.IdPerimetroPierna == null || bean.IdPerimetroPierna == "")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Circunferencia Pierna es requerido"));
                }

                if (bean.IdValoracion == null || bean.IdValoracion == "")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Valoración es requerido"));
                }

            }

            if (bean.IdTipoDieta != null)
            {
                if (bean.TipoDietaPorDia == null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe de Ingresar tipo de Dieta por Día "));

                }
            }

            if (bean.IdSuplementoNutricional != null)
            {
                if (bean.SuplementoNutricionalPorDia == null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Cantidad de Suplemento Nutricional por día es requerido"));
                }
            }

            return lstRetorno;
        }

        public PsNutricion coreInsertar(UsuarioActual usuarioActual, PsNutricion bean, String estado)
        {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;

            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            bean.IdNutricion = this.generarCodigo();
            if (bean.EvaluadoBoolean)
            {
                bean.Evaluado = "S";
            }
            else
            {
                bean.Evaluado = "";
            }

            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion, bean.IdBeneficiario).obtenerArreglo());
            if (bean.IdOrigen != "EVA")
            {

                psBeneficiario.IdComponenteNutricion = bean.IdNutricion;
                psBeneficiarioDao.actualizar(psBeneficiario);
            }
            else
            {
                if (psBeneficiario.IdComponenteNutricion == null)
                {
                    psBeneficiario.IdComponenteNutricion = bean.IdNutricion;
                    psBeneficiarioDao.actualizar(psBeneficiario);
                }
            }

            return this.registrar(bean); ;
        }

        public PsNutricion coreActualizar(UsuarioActual usuarioActual, PsNutricion bean, String estado)
        {
            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;

            if (bean.EvaluadoBoolean)
            {
                bean.Evaluado = "S";
            }
            else
            {
                bean.Evaluado = "";
            }

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion)
        {
            coreEliminar(new PsNutricionPk(pIdInstitucion, pIdBeneficiario, pIdNutricion));
        }

        public void coreEliminar(PsNutricionPk id)
        {
            PsNutricion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsNutricion coreAnular(UsuarioActual usuarioActual, PsNutricionPk id)
        {
            PsNutricion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsNutricion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdNutricion)
        {
            return coreAnular(usuarioActual, new PsNutricionPk(pIdInstitucion, pIdBeneficiario, pIdNutricion));
        }

        public int generarCodigo()
        {
            IQueryable<PsNutricion> query = this.getCriteria();

            List<PsNutricion> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdNutricion));
            }

            return var + 1;
        }

        public ParametroPaginacionGenerico listarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PsNutricion> lstResultado = new List<PsNutricion>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea))
            {
                filtro.IdArea = null;
            }
            if (String.IsNullOrEmpty(filtro.IdSexo))
            {
                filtro.IdSexo = null;
            }
            if (String.IsNullOrEmpty(filtro.TipoEdad))
            {
                filtro.TipoEdad = null;
            }
            if (String.IsNullOrEmpty(filtro.IdValoracion))
            {
                filtro.IdValoracion = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipoEdad", ConstanteUtil.TipoDato.String, filtro.TipoEdad));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_idValoracion", ConstanteUtil.TipoDato.String, filtro.IdValoracion));

            _reader = this.listarPorQuery("psnutricion.listarNutricionPaginacion", parametros);

            while (_reader.Read())
            {
                PsNutricion bean = new PsNutricion();

                if (!_reader.IsDBNull(0))
                    bean.IdBeneficiario = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.NombreCompleto = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.FechaInforme = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.Peso = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.Talla = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.PesoEdad = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.TallaEdad = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.PesoTalla = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Imc = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdValoracion = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdTipoDieta = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.TipoDietaPorDia = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.IdSuplementoNutricional = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.SuplementoNutricionalPorDia = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.Comentarios = _reader.GetString(14);

                if (!_reader.IsDBNull(15))
                    bean.IdNutricion = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.IdInstitucion = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.Estado = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdPeriodo = _reader.GetString(18);

                if (!_reader.IsDBNull(19))
                    bean.IdPerimetroPierna = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.IdPresionMensual = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.VariacionPeso = _reader.GetDecimal(21);
                if (!_reader.IsDBNull(22))
                    bean.Evaluado = _reader.GetString(22);

                if (!_reader.IsDBNull(23))
                    contador = _reader.GetInt32(23);
                if (!_reader.IsDBNull(24))
                    bean.IdOrigen = _reader.GetString(24);

                if (!_reader.IsDBNull(25))
                    bean.Edad = _reader.GetInt32(25);

                if (!_reader.IsDBNull(26))
                    bean.sexo = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.secuencia = Convert.ToInt32(_reader.GetInt64(27));

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }

        public List<PsNutricion> consultar(FiltroNutricion filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PsNutricion> lstResultado = new List<PsNutricion>();


            if (String.IsNullOrEmpty(filtro.IdSexo))
            {
                filtro.IdSexo = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea))
            {
                filtro.IdArea = null;
            }

            if (String.IsNullOrEmpty(filtro.Periodo))
            {
                filtro.Periodo = null;
            }

            if (String.IsNullOrEmpty(filtro.TipoEdad))
            {
                filtro.TipoEdad = null;
            }
            if (String.IsNullOrEmpty(filtro.IdValoracion))
            {
                filtro.IdValoracion = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_cantidad", ConstanteUtil.TipoDato.Integer, filtro.CantidadRegistros));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));


            _reader = this.listarPorQuery("psnutricion.listarNutricion", parametros);

            while (_reader.Read())
            {
                PsNutricion bean = new PsNutricion();

                if (!_reader.IsDBNull(0))
                    bean.IdBeneficiario = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.NombreCompleto = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.FechaInforme = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.Peso = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.Talla = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.PesoEdad = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.TallaEdad = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.PesoTalla = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Imc = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdValoracion = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdTipoDieta = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.TipoDietaPorDia = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.IdSuplementoNutricional = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.SuplementoNutricionalPorDia = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.Comentarios = _reader.GetString(14);

                if (!_reader.IsDBNull(15))
                    bean.IdNutricion = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.IdInstitucion = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.Estado = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdPeriodo = _reader.GetString(18);

                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;
        }

        public DtoNutricion obtenerCalculos(DtoNutricion bean)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoNutricion calculos = new DtoNutricion();

            parametros.Add(new ParametroPersistenciaGenerico("pIdPersona", ConstanteUtil.TipoDato.Integer, bean.IdBeneficiario));
            parametros.Add(new ParametroPersistenciaGenerico("pPeso", ConstanteUtil.TipoDato.Decimal, bean.Peso));
            parametros.Add(new ParametroPersistenciaGenerico("pTalla", ConstanteUtil.TipoDato.Decimal, bean.Talla));
            parametros.Add(new ParametroPersistenciaGenerico("pHemoglobina", ConstanteUtil.TipoDato.Decimal, bean.Hemoglobina));

            parametros.Add(new ParametroPersistenciaGenerico("pTipoProceso", ConstanteUtil.TipoDato.String, bean.IdTipoProceso));
            parametros.Add(new ParametroPersistenciaGenerico("pIdProceso", ConstanteUtil.TipoDato.Integer, bean.IdProceso));

            _reader = this.listarPorQuery("psnutricion.obtenerCalculos", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    calculos.HemoglobinaNombre = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    calculos.Imc = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    calculos.TallaEdad = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    calculos.PesoEdad = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    calculos.PesoTalla = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    calculos.VariacionPeso = _reader.GetDecimal(5);
            }

            this.dispose();


            return calculos;
        }

        public ParametroPaginacionGenerico consultarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PsNutricion> lstResultado = new List<PsNutricion>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipoEdad", ConstanteUtil.TipoDato.String, filtro.TipoEdad));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));

            _reader = this.listarPorQuery("psnutricion.consultarNutricion", parametros);

            while (_reader.Read())
            {
                PsNutricion bean = new PsNutricion();

                if (!_reader.IsDBNull(0))
                    bean.IdBeneficiario = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.NombreCompleto = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.FechaInforme = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.Peso = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.Talla = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.PesoEdad = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.TallaEdad = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.PesoTalla = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Imc = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdValoracion = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdTipoDieta = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.TipoDietaPorDia = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.IdSuplementoNutricional = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.SuplementoNutricionalPorDia = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.Comentarios = _reader.GetString(14);

                if (!_reader.IsDBNull(15))
                    bean.IdNutricion = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.IdInstitucion = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.Estado = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdPeriodo = _reader.GetString(18);

                if (!_reader.IsDBNull(19))
                    bean.IdPerimetroPierna = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.IdPresionMensual = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.VariacionPeso = _reader.GetDecimal(21);
                if (!_reader.IsDBNull(22))
                    contador = _reader.GetInt32(22);

                if (!_reader.IsDBNull(23))
                    bean.NombreValoracionAdultos = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.NombreTipoDietaAdultos = _reader.GetString(24);
                if (!_reader.IsDBNull(25))
                    bean.NombreValoracionNinos = _reader.GetString(25);
                if (!_reader.IsDBNull(26))
                    bean.NombreTipoDietaNinos = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.NombreSuplemento = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.NombrePerimetro = _reader.GetString(28);
                if (!_reader.IsDBNull(29))
                    bean.NombrePresion = _reader.GetString(29);
                if (!_reader.IsDBNull(30))
                    bean.Evaluado = _reader.GetString(30);

                if (!_reader.IsDBNull(31))
                    bean.sexo = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.secuencia = _reader.GetInt32(32);
                if (!_reader.IsDBNull(33))
                    bean.Edad = _reader.GetInt32(33);


                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }
    }
}
