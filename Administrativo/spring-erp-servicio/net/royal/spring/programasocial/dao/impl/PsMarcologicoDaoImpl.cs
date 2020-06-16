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
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl
{

    public class PsMarcologicoDaoImpl : GenericoDaoImpl<PsMarcologico>, PsMarcologicoDao
    {
        private IServiceProvider servicioProveedor;


        public PsMarcologicoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psmarcologico")
        {
            servicioProveedor = _servicioProveedor;
        }



        public PsMarcologico coreInsertar(UsuarioActual usuarioActual, PsMarcologico bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsMarcologico coreActualizar(UsuarioActual usuarioActual, PsMarcologico bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMarcologico filtro)
        {
            Int32 contador = 0;

            List<DtoMarcologico> lstRetorno = new List<DtoMarcologico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("psmarcologico.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psmarcologico.filtroPaginacion");

            while (_reader.Read())
            {
                DtoMarcologico bean = new DtoMarcologico();

                if (!_reader.IsDBNull(0))
                    bean.codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.estado = _reader.GetString(2);


                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public List<DtoMarcologico> listarTree(FiltroMarcologico filtro)
        {
            List<DtoMarcologico> lstRetorno = new List<DtoMarcologico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));



            _reader = this.listarPorQuery("psmarcologico.listarTree", parametros);

            while (_reader.Read())
            {
                DtoMarcologico bean = new DtoMarcologico();

                if (!_reader.IsDBNull(0))
                    bean.marco = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nombreMarco = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.resultado = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nombreResultado = _reader.GetString(3);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> obtenerListaGraficos(FiltroGraficos filtro)
        {

            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.IdSexo))
            {
                filtro.IdSexo = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }


            if (filtro.Codigo == "RES03")
            {
                _reader = this.listarPorQuery("psmarcologico.habitossaludables", parametros);
            }
            else
            {
                _reader = this.listarPorQuery("psmarcologico.nivelsatisfaccion", parametros);
            }

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.NnaCantidad = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.Nnapor = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.NcdCantidad = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.Ncdpor = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.AamCantidad = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.Aampor = _reader.GetDecimal(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> obtenerListaGraficosEstandares(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            _reader = this.listarPorQuery("psmarcologico.nutricionestandares", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.GruposNutricionales = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.ReqInstitucionCant = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.ReqInstitucionPorc = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.AporFundacionCant = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.AporFundacionPorc = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.OtrosAportesCant = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.OtrosAportesPorc = _reader.GetDecimal(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> listarBPManufacturada(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            _reader = this.listarPorQuery("psmarcologico.listarBPManufacturada", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.maximo = _reader.GetInt32(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoGraficoMultiple> MostarGraficosBPManufacturada(String idPeriodo)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, idPeriodo));
            _reader = this.listarPorQuery("psmarcologico.listarBPManufacturadaGraficos", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Cantidad = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.Nombre = _reader.GetString(2);

                lstRetorno.Add(bean);
            }
            this.dispose();


            List<DtoGraficoMultiple> grafico = new List<DtoGraficoMultiple>();

            for (int g = 0; g < lstRetorno.Count; g++)
            {
                DtoGraficoMultiple cabecera = new DtoGraficoMultiple();
                Series serie = new Series();

                if (lstRetorno[g].Institucion != null)
                {
                    cabecera = new DtoGraficoMultiple();
                    cabecera.name = lstRetorno[g].Institucion;

                    cabecera.series = new List<Series>();
                    serie = new Series();
                    serie.name = lstRetorno[g].Nombre;
                    serie.value = lstRetorno[g].Cantidad.Value;
                    cabecera.series.Add(serie);
                    grafico.Add(cabecera);
                }

            }

            return grafico;

        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccion(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }


            _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisfaccion", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.NnaCantidad = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.Nnapor = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.NcdCantidad = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.Ncdpor = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.AamCantidad = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.Aampor = _reader.GetDecimal(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;


        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisfaccionInstitucion(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisfaccionInstitucion", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NNABuenaCant = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.NNABuena = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.NNARegularCant = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.NNARegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.NNAMalaCant = _reader.GetDecimal(5);
                if (!_reader.IsDBNull(6))
                    bean.NNAMala = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.NCDBuenaCant = _reader.GetDecimal(7);
                if (!_reader.IsDBNull(8))
                    bean.NCDBuena = _reader.GetDecimal(8);
                if (!_reader.IsDBNull(9))
                    bean.NCDRegularCant = _reader.GetDecimal(9);
                if (!_reader.IsDBNull(10))
                    bean.NCDRegular = _reader.GetDecimal(10);
                if (!_reader.IsDBNull(11))
                    bean.NCDMalaCant = _reader.GetDecimal(11);
                if (!_reader.IsDBNull(12))
                    bean.NCDMala = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.AAMBuenaCant = _reader.GetDecimal(13);
                if (!_reader.IsDBNull(14))
                    bean.AAMBuena = _reader.GetDecimal(14);
                if (!_reader.IsDBNull(15))
                    bean.AAMRegularCant = _reader.GetDecimal(15);
                if (!_reader.IsDBNull(16))
                    bean.AAMRegular = _reader.GetDecimal(16);
                if (!_reader.IsDBNull(17))
                    bean.AAMMalaCant = _reader.GetDecimal(17);
                if (!_reader.IsDBNull(18))
                    bean.AAMMala = _reader.GetDecimal(18);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;

        }

        public List<Series> MostrarGraficoPie(string idPeriodo, String NombreGrafico, Int32 secuencia, String idPrograma, String sexo, Int32? edad)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(idPrograma))
            {
                idPrograma = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, idPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, idPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, sexo));

            if (edad == 0)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (edad == 1 || edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (edad * 5)));
            }
            if (edad == 4 || edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, edad * 5));
            }

            if (NombreGrafico == "RES12" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisfaccionInstitucionPie1", parametros);
            }

            if (NombreGrafico == "RES12" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisfaccionInstitucionPie2", parametros);
            }

            else if (NombreGrafico == "RES13" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReportePorcHabitosSaludablesSaludPie1", parametros);
            }
            else if (NombreGrafico == "RES13" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReportePorcHabitosSaludablesSaludPie2", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie1", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie2", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 3)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie3", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 4)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie4", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 5)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie5", parametros);
            }
            else if (NombreGrafico == "RES17" && secuencia == 6)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNAPie6", parametros);
            }
            else if (NombreGrafico == "RES19" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoNroNNAParticipanPie1", parametros);
            }
            else if (NombreGrafico == "RES19" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoNroNNAParticipanPie2", parametros);
            }
            else if (NombreGrafico == "RES19" && secuencia == 3)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoNroNNAParticipanPie3", parametros);
            }
            else if (NombreGrafico == "RES19" && secuencia == 4)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoNroNNAParticipanPie4", parametros);
            }
            else if (NombreGrafico == "RES20" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoPorcAutonomiaLogradaPie1", parametros);
            }
            else if (NombreGrafico == "RES20" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoPorcAutonomiaLogradaPie2", parametros);
            }
            else if (NombreGrafico == "RES20" && secuencia == 3)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoPorcAutonomiaLogradaPie3", parametros);
            }
            else if (NombreGrafico == "RES20" && secuencia == 4)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoPorcAutonomiaLogradaPie4", parametros);
            }
            else if (NombreGrafico == "RES21" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReportePorcGraficoAAMHabOcupacPie1", parametros);
            }
            else if (NombreGrafico == "RES24" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisMejorasFisicasPie1", parametros);
            }
            else if (NombreGrafico == "RES24" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisMejorasFisicasPie2", parametros);
            }
            else if (NombreGrafico == "RES24" && secuencia == 3)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisMejorasFisicasPie3", parametros);
            }
            else if (NombreGrafico == "RES24" && secuencia == 4)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisMejorasFisicasPie4", parametros);
            }

            else if (NombreGrafico == "RES25" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoPorcAAMFortHabSocialesPie1", parametros);
            }

            else if (NombreGrafico == "RES22" && secuencia == 1)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie1", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 2)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie2", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 3)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie3", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 4)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie4", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 5)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie5", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 6)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie6", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 7)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie7", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 8)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie8", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 9)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie9", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 10)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie10", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 11)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie11", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 12)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie12", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 13)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie13", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 14)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie14", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 15)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie15", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 16)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie16", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 17)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie17", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 18)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie18", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 19)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie19", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 20)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie20", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 21)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie21", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 22)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie22", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 23)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie23", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 24)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie24", parametros);
            }
            else if (NombreGrafico == "RES22" && secuencia == 25)
            {
                _reader = this.listarPorQuery("psmarcologico.listarReporteGraficoMejoraHabilidadesSocialesNNAPie25", parametros);
            }


            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();
                if (!_reader.IsDBNull(0))
                    bean.Nombre = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Cantidad = _reader.GetDecimal(1);
                lstRetorno.Add(bean);
            }
            this.dispose();

            List<Series> grafico = new List<Series>();

            for (int g = 0; g < lstRetorno.Count; g++)
            {
                Series serie = new Series();
                serie = new Series();
                serie.name = lstRetorno[g].Nombre;
                serie.value = lstRetorno[g].Cantidad.Value;
                grafico.Add(serie);

            }




            return grafico;
        }


        public List<DtoValoresNutricionales> ListarValoresNutricionales(FiltroGraficos filtro)
        {
            List<DtoValoresNutricionales> lstRetorno = new List<DtoValoresNutricionales>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));


            _reader = this.listarPorQuery("psmarcologico.valoresnutricionales", parametros);


            while (_reader.Read())
            {
                DtoValoresNutricionales bean = new DtoValoresNutricionales();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.poblacion = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.normalCant = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.normalPorc = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.adelgaSeveroCant = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.adelgaSeveroPorc = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.adelgaModeradoCant = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.adelgaModeradoPorc = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.sobrepesoCant = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.sobrepesoPorc = _reader.GetDecimal(9);

                if (!_reader.IsDBNull(10))
                    bean.obesidadCant = _reader.GetInt32(10);

                if (!_reader.IsDBNull(11))
                    bean.obesidadPorc = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.noEvaluadosCant = _reader.GetInt32(12);

                if (!_reader.IsDBNull(13))
                    bean.noEvaluadosPorc = _reader.GetDecimal(13);

                if (!_reader.IsDBNull(14))
                    bean.codInstitucion = _reader.GetString(14);



                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoActividadesPreventivas> ListarActividadesPreventivas(FiltroGraficos filtro)
        {
            List<DtoActividadesPreventivas> lstRetorno = new List<DtoActividadesPreventivas>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.actividadespreventivas", parametros);

            while (_reader.Read())
            {
                DtoActividadesPreventivas bean = new DtoActividadesPreventivas();

                if (!_reader.IsDBNull(0))
                    bean.nombre = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.tipo = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.numActividades = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.asistencia = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.numActividades_porc = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.asistencia_porc = _reader.GetDecimal(5);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoEstadoSalud> ListarEstadoSalud(FiltroGraficos filtro)
        {
            List<DtoEstadoSalud> lstRetorno = new List<DtoEstadoSalud>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.estadosalud", parametros);

            while (_reader.Read())
            {
                DtoEstadoSalud bean = new DtoEstadoSalud();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codPrograma = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.sano = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.enfAguda = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.enfCronica = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.discapacidad = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.sinEnf = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.discapacidadEnfAguda = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.discapacidadEnfCronica = _reader.GetDecimal(8);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarServiciosDeSaludBasicos(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.listarReportePorcHabitosSaludablesSalud", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();


                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NNABuenaCant = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.NNABuena = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.NNARegularCant = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.NNARegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.NNAMalaCant = _reader.GetDecimal(5);
                if (!_reader.IsDBNull(6))
                    bean.NNAMala = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.NCDBuenaCant = _reader.GetDecimal(7);
                if (!_reader.IsDBNull(8))
                    bean.NCDBuena = _reader.GetDecimal(8);
                if (!_reader.IsDBNull(9))
                    bean.NCDRegularCant = _reader.GetDecimal(9);
                if (!_reader.IsDBNull(10))
                    bean.NCDRegular = _reader.GetDecimal(10);
                if (!_reader.IsDBNull(11))
                    bean.NCDMalaCant = _reader.GetDecimal(11);
                if (!_reader.IsDBNull(12))
                    bean.NCDMala = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.AAMBuenaCant = _reader.GetDecimal(13);
                if (!_reader.IsDBNull(14))
                    bean.AAMBuena = _reader.GetDecimal(14);
                if (!_reader.IsDBNull(15))
                    bean.AAMRegularCant = _reader.GetDecimal(15);
                if (!_reader.IsDBNull(16))
                    bean.AAMRegular = _reader.GetDecimal(16);
                if (!_reader.IsDBNull(17))
                    bean.AAMMalaCant = _reader.GetDecimal(17);
                if (!_reader.IsDBNull(18))
                    bean.AAMMala = _reader.GetDecimal(18);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReporteSatisfaccionServicioNNA(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.listarReporteSatisfaccionServicioNNA", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NnaCantidad = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.Nnapor = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.NcdCantidad = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.Ncdpor = _reader.GetDecimal(4);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;

        }

        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan1(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechaInicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechaFin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));

            _reader = this.listarPorQuery("psmarcologico.listarReporteNroNNAParticipan1", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.NroTallerFor = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.TalleForRegular = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.TalleForBueno = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.TalleForExcelente = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(1))
                    bean.NroTallerDep = _reader.GetInt32(1);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepRegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepBueno = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepExcelente = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(1))
                    bean.NroTallerArt = _reader.GetInt32(1);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtRegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtBueno = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtExcelente = _reader.GetDecimal(4);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;

        }

        public List<DtoIndicadoresGraficos> listarReporteNroNNAParticipan2(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechaInicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechaFin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));

            _reader = this.listarPorQuery("psmarcologico.listarReporteNroNNAParticipan2", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();
                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NroTallerFor = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.TalleForRegular = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.TalleForBueno = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.TalleForExcelente = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(1))
                    bean.NroTallerDep = _reader.GetInt32(1);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepRegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepBueno = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleDepExcelente = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(1))
                    bean.NroTallerArt = _reader.GetInt32(1);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtRegular = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtBueno = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(4))
                    bean.TalleArtExcelente = _reader.GetDecimal(4);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReporteNivelSatisMejorasFisicas(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.IdInstitucion))
            {
                filtro.IdInstitucion = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == 0)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }


            _reader = this.listarPorQuery("psmarcologico.listarReporteNivelSatisMejorasFisicas", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NnaCantidad = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.Nnapor = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.NcdCantidad = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.Ncdpor = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.AamCantidad = _reader.GetDecimal(5);
                if (!_reader.IsDBNull(6))
                    bean.Aampor = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoAnemiaPrevalente> ListarAnemiaPrevalente(FiltroGraficos filtro)
        {
            List<DtoAnemiaPrevalente> lstRetorno = new List<DtoAnemiaPrevalente>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_Edad", ConstanteUtil.TipoDato.Integer, filtro.Edad));

            _reader = this.listarPorQuery("psmarcologico.anemiaprevalente", parametros);

            while (_reader.Read())
            {
                DtoAnemiaPrevalente bean = new DtoAnemiaPrevalente();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.mascPTotal = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.femePTotal = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.mascSinAnemia = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.femeSinAnemia = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.mascAnemiaLeve = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.femeAnemiaLeve = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.mascAnemiaMod = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.femeAnemiaMod = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.mascAnemiaSev = _reader.GetDecimal(9);

                if (!_reader.IsDBNull(10))
                    bean.femeAnemiaSev = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.mascNoEvaluado = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.femeNoEvaluado = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.codInstitucion = _reader.GetString(13);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }


        public List<DtoIndicadoresGraficos> listarReporteEducacionCalidad(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechaInicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechaFin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            _reader = this.listarPorQuery("psmarcologico.listarReporteNroCapEspPersonal", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.NutriCantidad = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.NutriRegular = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.NutriBueno = _reader.GetDecimal(3);
                if (!_reader.IsDBNull(4))
                    bean.NutriExcelente = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.SaludCantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.SaludRegular = _reader.GetDecimal(6);
                if (!_reader.IsDBNull(7))
                    bean.SaludBueno = _reader.GetDecimal(7);
                if (!_reader.IsDBNull(8))
                    bean.SaludExcelente = _reader.GetDecimal(8);
                if (!_reader.IsDBNull(9))
                    bean.CapaciCantidad = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.CapaciRegular = _reader.GetDecimal(10);
                if (!_reader.IsDBNull(11))
                    bean.CapaciBueno = _reader.GetDecimal(11);
                if (!_reader.IsDBNull(12))
                    bean.CapaciExcelente = _reader.GetDecimal(12);
                if (!_reader.IsDBNull(13))
                    bean.DesarrCantidad = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.DesarrRegular = _reader.GetDecimal(14);
                if (!_reader.IsDBNull(15))
                    bean.DesarrBueno = _reader.GetDecimal(15);
                if (!_reader.IsDBNull(16))
                    bean.DesarrExcelente = _reader.GetDecimal(16);


                if (!_reader.IsDBNull(17))
                    bean.NutriRegularCan = _reader.GetInt32(17);
                if (!_reader.IsDBNull(18))
                    bean.NutriBuenoCan = _reader.GetInt32(18);
                if (!_reader.IsDBNull(19))
                    bean.NutriExcelenteCan = _reader.GetInt32(19);

                if (!_reader.IsDBNull(20))
                    bean.SaludRegularCan = _reader.GetInt32(20);
                if (!_reader.IsDBNull(21))
                    bean.SaludBuenoCan = _reader.GetInt32(21);
                if (!_reader.IsDBNull(22))
                    bean.SaludExcelenteCan = _reader.GetInt32(22);

                if (!_reader.IsDBNull(23))
                    bean.CapaciRegularCan = _reader.GetInt32(23);
                if (!_reader.IsDBNull(24))
                    bean.CapaciBuenoCan = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.CapaciExcelenteCan = _reader.GetInt32(25);

                if (!_reader.IsDBNull(26))
                    bean.DesarrRegularCan = _reader.GetInt32(26);
                if (!_reader.IsDBNull(27))
                    bean.DesarrBuenoCan = _reader.GetInt32(27);
                if (!_reader.IsDBNull(28))
                    bean.DesarrExcelenteCan = _reader.GetInt32(28);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;

        }

        public List<DtoIndicadoresGraficos> listarReportePorcAutonomiaLograda(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));


            _reader = this.listarPorQuery("psmarcologico.listarReportePorcAutonomiaLograda", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.TotalBeneficiarios = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.NroEvaluados = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.BarIndeCantidad = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.BarIndePorcen = _reader.GetDecimal(4);
                if (!_reader.IsDBNull(5))
                    bean.BarDepenLeveCantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.BarDepenLevePorcent = _reader.GetDecimal(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReportePorcAAMHabOcupac(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));


            _reader = this.listarPorQuery("psmarcologico.listarReportePorcAAMHabOcupac", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.PoblacionTotal = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.DesarrollanHO = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.NoDesarrollanHO = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.EnProcesoDeDesarrollarHO = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.NoEvaluados = _reader.GetInt32(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReportePorcAAMFortHabSociales(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }


            _reader = this.listarPorQuery("psmarcologico.listarReportePorcAAMFortHabSociales", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.HabIntegracionCant = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.HabIntegracionPorc = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.HabAsertividadCant = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.HabAsertividadPorc = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.HabComuCant = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.HabComuPorc = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.HabPartiCant = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.HabPartiPorc = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.HabCoopCant = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.HabCoopPorc = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.HabEmpaCant = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.HabEmpaPorc = _reader.GetDecimal(12);


                if (!_reader.IsDBNull(13))
                    bean.Resultado = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.NoEvaluados = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.PoblacionTotal = _reader.GetInt32(15);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }




        public List<DtoAccesoEducacion> ListarAccesoEducacion(FiltroGraficos filtro)
        {
            List<DtoAccesoEducacion> lstRetorno = new List<DtoAccesoEducacion>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));


            _reader = this.listarPorQuery("psmarcologico.accesoeducacion", parametros);

            while (_reader.Read())
            {
                DtoAccesoEducacion bean = new DtoAccesoEducacion();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.cantFemeRegular = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.cantMascRegular = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.porcFemeRegular = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.porcMascRegular = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.cantFemeEspecial = _reader.GetInt32(5);

                if (!_reader.IsDBNull(6))
                    bean.cantMascEspecial = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.porcFemeEspecial = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.porcMascEspecial = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.cantFemeRegularInc = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.cantMascRegularInc = _reader.GetInt32(10);

                if (!_reader.IsDBNull(11))
                    bean.porcFemeRegularInc = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.porcMascRegularInc = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.cantFemeAlternativa = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.cantMascAlternativa = _reader.GetInt32(14);

                if (!_reader.IsDBNull(15))
                    bean.porcFemeAlternativa = _reader.GetDecimal(15);

                if (!_reader.IsDBNull(16))
                    bean.porcMascAlternativa = _reader.GetDecimal(16);

                if (!_reader.IsDBNull(17))
                    bean.cantFemeCEPTRO = _reader.GetInt32(17);

                if (!_reader.IsDBNull(18))
                    bean.cantMascCEPTRO = _reader.GetInt32(18);

                if (!_reader.IsDBNull(19))
                    bean.porcFemeCEPTRO = _reader.GetDecimal(19);

                if (!_reader.IsDBNull(20))
                    bean.porcMascCEPTRO = _reader.GetDecimal(20);

                if (!_reader.IsDBNull(21))
                    bean.cantFemeNinguno = _reader.GetInt32(21);

                if (!_reader.IsDBNull(22))
                    bean.cantMascNinguno = _reader.GetInt32(22);

                if (!_reader.IsDBNull(23))
                    bean.porcFemeNinguno = _reader.GetDecimal(23);

                if (!_reader.IsDBNull(24))
                    bean.porcMascNinguno = _reader.GetDecimal(24);

                if (!_reader.IsDBNull(21))
                    bean.cantFemeTotal = _reader.GetInt32(21);

                if (!_reader.IsDBNull(22))
                    bean.cantMascTotal = _reader.GetInt32(22);

                if (!_reader.IsDBNull(23))
                    bean.porcFemeTotal = _reader.GetDecimal(23);

                if (!_reader.IsDBNull(24))
                    bean.porcMascTotal = _reader.GetDecimal(24);

                if (!_reader.IsDBNull(25))
                    bean.codInstitucion = _reader.GetString(25);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNA(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_residencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));

            _reader = this.listarPorQuery("psmarcologico.listarReporteMejoraHabilidadesSocialesNNA", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.HabSociConflictosCant = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.HabSociConflictosPorc = _reader.GetDecimal(2);


                if (!_reader.IsDBNull(3))
                    bean.HabEmoCant = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.HabEmoPorc = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.HabComuCant = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.HabComuPorc = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.HabCoopCant = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.HabCoopPorc = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.HabAsertividadCant = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.HabAsertividadPorc = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.HabEmpaCant = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.HabEmpaPorc = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.Resultado = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.NoEvaluados = _reader.GetInt32(14);



                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNAEdad(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_residencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));


            _reader = this.listarPorQuery("psmarcologico.listarReporteMejoraHabilidadesSocialesNNAEdad", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Institucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.HabSociConflictosCant = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.HabSociConflictosPorc = _reader.GetDecimal(2);


                if (!_reader.IsDBNull(3))
                    bean.HabEmoCant = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.HabEmoPorc = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.HabComuCant = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.HabComuPorc = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.HabCoopCant = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.HabCoopPorc = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.HabAsertividadCant = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.HabAsertividadPorc = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.HabEmpaCant = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.HabEmpaPorc = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.Resultado = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.NoEvaluados = _reader.GetInt32(14);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoIndicadoresGraficos> listarReporteMejoraHabilidadesSocialesNNASatisf(FiltroGraficos filtro)
        {
            List<DtoIndicadoresGraficos> lstRetorno = new List<DtoIndicadoresGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));


            _reader = this.listarPorQuery("psmarcologico.listarReporteMejoraHabilidadesSocialesNNASatisf", parametros);

            while (_reader.Read())
            {
                DtoIndicadoresGraficos bean = new DtoIndicadoresGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nnapor = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.Ncdpor = _reader.GetDecimal(2);
                if (!_reader.IsDBNull(3))
                    bean.NnaCantidad = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.NcdCantidad = _reader.GetInt32(4);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        /*ERNESTO*/
        public List<DtoRendimientoEducativo> ListarRendimientoEducativo(FiltroGraficos filtro)
        {
            List<DtoRendimientoEducativo> lstRetorno = new List<DtoRendimientoEducativo>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            parametros.Add(new ParametroPersistenciaGenerico("p_Nivel", ConstanteUtil.TipoDato.String, filtro.Nivel));
            parametros.Add(new ParametroPersistenciaGenerico("p_Grado", ConstanteUtil.TipoDato.String, filtro.Grado));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.rendimientoeducativo", parametros);


            while (_reader.Read())
            {
                DtoRendimientoEducativo bean = new DtoRendimientoEducativo();

                if (!_reader.IsDBNull(0))
                    bean.rendimiento = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.cantLogMat = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.porcLogMat = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.cantComuni = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.porcComuni = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.cantComLec = _reader.GetInt32(5);

                if (!_reader.IsDBNull(6))
                    bean.porctComLec = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.cantPerSoc = _reader.GetInt32(7);

                if (!_reader.IsDBNull(8))
                    bean.porcPerSoc = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.cantCieAmb = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.porcCieAmb = _reader.GetDecimal(10);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoRetrasoEducativo> ListarRetrasoEducativo(FiltroGraficos filtro)
        {
            List<DtoRetrasoEducativo> lstRetorno = new List<DtoRetrasoEducativo>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }


            _reader = this.listarPorQuery("psmarcologico.retrasoeducativo", parametros);

            while (_reader.Read())
            {
                DtoRetrasoEducativo bean = new DtoRetrasoEducativo();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.tipoRetraso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.regular = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.regularP = _reader.GetDecimal(3);



                if (!_reader.IsDBNull(4))
                    bean.especial = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.especialP = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.regularInc = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.regularIncP = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.alternativa = _reader.GetDecimal(8);

                if (!_reader.IsDBNull(9))
                    bean.alternativaP = _reader.GetDecimal(9);

                if (!_reader.IsDBNull(10))
                    bean.cetpro = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.cetproP = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.ninguna = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.ningunaP = _reader.GetDecimal(13);

                if (!_reader.IsDBNull(14))
                    bean.total = _reader.GetDecimal(14);

                if (!_reader.IsDBNull(15))
                    bean.totalP = _reader.GetDecimal(15);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoRetrasoEducativo> ListarTiempoRetardo(FiltroGraficos filtro)
        {
            List<DtoRetrasoEducativo> lstRetorno = new List<DtoRetrasoEducativo>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.retrasoporinstitucion", parametros);

            while (_reader.Read())
            {
                DtoRetrasoEducativo bean = new DtoRetrasoEducativo();

                if (!_reader.IsDBNull(0))
                    bean.tiempo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.cantidad = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.porcentaje = _reader.GetDecimal(2);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoActividadesPreventivas> ListarPrevalenciaGrupo(FiltroGraficos filtro)
        {
            List<DtoActividadesPreventivas> lstRetorno = new List<DtoActividadesPreventivas>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_FechaInicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_FechaFin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdDiagnostico", ConstanteUtil.TipoDato.Integer, filtro.idDiagnostico));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipoRAS", ConstanteUtil.TipoDato.String, filtro.tipoRAS));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.prevalenciagrupo", parametros);

            while (_reader.Read())
            {
                DtoActividadesPreventivas bean = new DtoActividadesPreventivas();

                if (!_reader.IsDBNull(0))
                    bean.diagnostico = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.atencion = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.prevalencia = _reader.GetDecimal(2);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoActividadesPreventivas> ListarTratamientoFuera(FiltroGraficos filtro)
        {
            List<DtoActividadesPreventivas> lstRetorno = new List<DtoActividadesPreventivas>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_FechaInicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_FechaFin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            _reader = this.listarPorQuery("psmarcologico.tratamientofuera", parametros);

            while (_reader.Read())
            {
                DtoActividadesPreventivas bean = new DtoActividadesPreventivas();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.programa = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.atencion = _reader.GetInt32(2);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoEstadoFuncional> ListarEstadoFuncional(FiltroGraficos filtro)
        {
            List<DtoEstadoFuncional> lstRetorno = new List<DtoEstadoFuncional>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.estadofuncional", parametros);


            while (_reader.Read())
            {
                DtoEstadoFuncional bean = new DtoEstadoFuncional();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.poblacion = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.cantInde = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.porcInde = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.cantDepeLeve = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.porcDepeLeve = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.cantDepeMode = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.porcDepeMode = _reader.GetDecimal(7);

                if (!_reader.IsDBNull(8))
                    bean.cantDepeGrave = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.porcDepeGrave = _reader.GetDecimal(9);

                if (!_reader.IsDBNull(10))
                    bean.cantEnfMental = _reader.GetInt32(10);

                if (!_reader.IsDBNull(11))
                    bean.porcEnfMental = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.cantTotal = _reader.GetInt32(12);

                if (!_reader.IsDBNull(13))
                    bean.porcTotal = _reader.GetDecimal(13);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoActividadesPreventivas> ListarEstadoFuncionalApoyos(FiltroGraficos filtro)
        {
            List<DtoActividadesPreventivas> lstRetorno = new List<DtoActividadesPreventivas>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            _reader = this.listarPorQuery("psmarcologico.apoyobiomecanico", parametros);

            while (_reader.Read())
            {
                DtoActividadesPreventivas bean = new DtoActividadesPreventivas();

                if (!_reader.IsDBNull(0))
                    bean.tipo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.equipo = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.cantidad = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.porcentaje = _reader.GetDecimal(3);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoMejoraInfraestructura> ListarMejorasInfraestructura(FiltroGraficos filtro)
        {
            List<DtoMejoraInfraestructura> lstRetorno = new List<DtoMejoraInfraestructura>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdComponente", ConstanteUtil.TipoDato.String, filtro.IdComponente));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            _reader = this.listarPorQuery("psmarcologico.mejorainfraestructura", parametros);


            while (_reader.Read())
            {
                DtoMejoraInfraestructura bean = new DtoMejoraInfraestructura();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomComponente = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.aspecto = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.funcBueno = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.funcRegu = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.funcMalo = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.estrBueno = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.estrRegu = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.estrMalo = _reader.GetString(8);


                if (!_reader.IsDBNull(9))
                    bean.estrBueno2 = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.estrRegu2 = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.estrMalo2 = _reader.GetString(11);

                if (!_reader.IsDBNull(13))
                    bean.cantidad = _reader.GetInt32(13);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoMejoraProyecto> ListarMejorasProyecto(FiltroGraficos filtro)
        {
            List<DtoMejoraProyecto> lstRetorno = new List<DtoMejoraProyecto>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.mejoraproyecto", parametros);


            while (_reader.Read())
            {
                DtoMejoraProyecto bean = new DtoMejoraProyecto();

                if (!_reader.IsDBNull(0))
                    bean.nomInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.proyecto = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.inicFunc = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.inicEstr = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.finFunc = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.finEstr = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.revFunc = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.revEstr = _reader.GetString(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res07Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res07Cabecera", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res10Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res10", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }
        public List<ResultadoResGenerico> res16Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }




            _reader = this.listarPorQuery("psmarcologico.res16", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTerciario = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }
        public List<ResultadoResGenerico> res11Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, null));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res11", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res01Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {

                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res01Cabecera", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res01Detalle(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.IdPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                if (filtro.Edad == 19)
                {
                    parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                }
                else
                {
                    parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                }
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res01Detalle", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }



        public List<ResultadoResGenerico> res09Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            if (filtro.Edad == 1)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 5));
            }
            else if (filtro.Edad == 2)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 100));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 100));
            }
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            _reader = this.listarPorQuery("psmarcologico.res09Cabecera", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                {
                    bean.idTerciario = _reader.GetString(3);
                    bean.nombreTerciario = bean.idTerciario == "F" ? "Femenino" : "Masculino";
                }
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res09Detalle(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            if (filtro.Edad == 1)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 5));
            }
            else if (filtro.Edad == 2)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 100));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_menor", ConstanteUtil.TipoDato.Integer, 0));
                parametros.Add(new ParametroPersistenciaGenerico("p_mayor", ConstanteUtil.TipoDato.Integer, 100));
            }

            _reader = this.listarPorQuery("psmarcologico.res09Detalle", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                {
                    bean.idTerciario = _reader.GetString(3);
                    bean.nombreTerciario = bean.idTerciario == "F" ? "Femenino" : "Masculino";
                }
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res16Detalle(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdResidencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res16Detalle", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTerciario = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res21Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_residencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res21", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res19Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.FechaFin));

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res19Cabecera", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTerciario = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res19Detalle(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.FechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res19Detalle", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTerciario = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.cantidad = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.porcentaje = _reader.GetDecimal(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<ResultadoResGenerico> res20Cabecera(FiltroGraficos filtro)
        {
            List<ResultadoResGenerico> lstRetorno = new List<ResultadoResGenerico>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));


            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_residencia", ConstanteUtil.TipoDato.String, filtro.IdResidencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdSexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));

            if (filtro.Edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.Edad == 1 || filtro.Edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5)));
            }
            if (filtro.Edad == 4 || filtro.Edad == 19)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.Edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.Edad * 5));
            }

            _reader = this.listarPorQuery("psmarcologico.res20", parametros);

            while (_reader.Read())
            {
                ResultadoResGenerico bean = new ResultadoResGenerico();

                if (!_reader.IsDBNull(0))
                    bean.idPrimario = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.nombrePrimario = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idSecundario = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreSecundario = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.cantidad = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.porcentaje = _reader.GetDecimal(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoReportesGraficos> ListarReportePoblacionPorInstitucionyAnio(FiltroGraficos filtro)
        {
            List<DtoReportesGraficos> lstRetorno = new List<DtoReportesGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (filtro.AnioInicio == null)
            {
                filtro.AnioInicio = DateTime.Now.Year - 1;
            }
            if (filtro.AnioFin == null)
            {
                filtro.AnioFin = DateTime.Now.Year;
            }

            parametros.Add(new ParametroPersistenciaGenerico("pAnioInicio", ConstanteUtil.TipoDato.Integer, filtro.AnioInicio));
            parametros.Add(new ParametroPersistenciaGenerico("pAnioFin", ConstanteUtil.TipoDato.Integer, filtro.AnioFin));

            _reader = this.listarPorQuery("psmarcologico.ListarReportePoblacionPorInstitucionyAnio", parametros);



            while (_reader.Read())
            {
                DtoReportesGraficos bean = new DtoReportesGraficos();
                Int32 Canevaro = 0;
                Int32 Sanvicente = 0;
                Int32 Inmaculada = 0;
                Int32 Sanfrancisco = 0;
                Int32 Desamparados = 0;
                Int32 Misioneras = 0;
                Int32 Puricultorio = 0;

                if (!_reader.IsDBNull(0))
                {
                    bean.Anio = _reader.GetInt32(0);
                }


                if (!_reader.IsDBNull(1))
                {
                    bean.Canevaro = _reader.GetInt32(1);
                    Canevaro = _reader.GetInt32(1);
                }

                if (!_reader.IsDBNull(2))
                {
                    bean.Sanvicente = _reader.GetInt32(2);
                    Sanvicente = _reader.GetInt32(2);
                }

                if (!_reader.IsDBNull(3))
                {
                    bean.Inmaculada = _reader.GetInt32(3);
                    Inmaculada = _reader.GetInt32(3);
                }

                if (!_reader.IsDBNull(4))
                {
                    bean.Sanfrancisco = _reader.GetInt32(4);
                    Sanfrancisco = _reader.GetInt32(4);
                }

                if (!_reader.IsDBNull(5))
                {
                    bean.Desamparados = _reader.GetInt32(5);
                    Desamparados = _reader.GetInt32(5);
                }

                if (!_reader.IsDBNull(6))
                {
                    bean.Misioneras = _reader.GetInt32(6);
                    Misioneras = _reader.GetInt32(6);
                }

                if (!_reader.IsDBNull(7))
                {
                    bean.Puricultorio = _reader.GetInt32(7);
                    Puricultorio = _reader.GetInt32(7);
                }

                bean.Total = Canevaro + Sanvicente + Inmaculada + Sanfrancisco + Desamparados + Misioneras + Puricultorio;

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoReportesGraficos> ListarReporteSubvencionesPorInstitucion(FiltroGraficos filtro)
        {
            List<DtoReportesGraficos> lstRetorno = new List<DtoReportesGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.ListarReporteSubVencionesEjecutadasInstitucion", parametros);

            while (_reader.Read())
            {
                DtoReportesGraficos bean = new DtoReportesGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Tipo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.PuricultorioD = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.CanevaroD = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.SanvicenteD = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.InmaculadaD = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.SanfranciscoD = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.AsiloHermanitas = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.MisionerasD = _reader.GetDecimal(7);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoReportesGraficos> ListarReporteSubvencionesPorPrograma(FiltroGraficos filtro)
        {
            List<DtoReportesGraficos> lstRetorno = new List<DtoReportesGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.ListarReporteSubVencionesEjecutadas", parametros);

            while (_reader.Read())
            {
                DtoReportesGraficos bean = new DtoReportesGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Tipo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.PuricultorioD = _reader.GetDecimal(1);

                if (!_reader.IsDBNull(2))
                    bean.CanevaroD = _reader.GetDecimal(2);

                if (!_reader.IsDBNull(3))
                    bean.SanvicenteD = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.InmaculadaD = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.SanfranciscoD = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.AsiloHermanitas = _reader.GetDecimal(6);

                if (!_reader.IsDBNull(7))
                    bean.MisionerasD = _reader.GetDecimal(7);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoReportesGraficos> ListarReportePoblacionBeneficiaria(FiltroGraficos filtro)
        {
            List<DtoReportesGraficos> lstRetorno = new List<DtoReportesGraficos>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));

            _reader = this.listarPorQuery("psmarcologico.ListarReportePoblacionBeneficiaria", parametros);

            while (_reader.Read())
            {
                DtoReportesGraficos bean = new DtoReportesGraficos();

                if (!_reader.IsDBNull(0))
                    bean.Tipo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Anio2013 = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.Anio2014 = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.Anio2015 = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.Anio2016 = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.Anio2017 = _reader.GetInt32(5);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoTabla> obtenerAnalisisNutricionDetalle(FiltroEncuestaAnalisis filtro)
        {
            List<DtoTabla> lstRetorno = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.companyonwer));
            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, filtro.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));
            
            _reader = this.listarPorQuery("psmarcologico.obtenerAnalisisNutricionDetalle", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.CodigoNumerico = _reader.GetInt32(1);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }
    }
}
