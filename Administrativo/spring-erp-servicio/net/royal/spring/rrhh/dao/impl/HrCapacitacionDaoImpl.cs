using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrCapacitacionDaoImpl : GenericoDaoImpl<HrCapacitacion>, HrCapacitacionDao
    {
        private IServiceProvider servicioProveedor;


        public HrCapacitacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrcapacitacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrCapacitacion obtenerPorId(HrCapacitacionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCapacitacion obtenerPorId(String pCompanyowner, String pCapacitacion)
        {
            return obtenerPorId(new HrCapacitacionPk(pCompanyowner, pCapacitacion));
        }

        public HrCapacitacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacion bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCapacitacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacion bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }

            bean.Estado = estado;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;



        }

        public void coreEliminar(String pCompanyowner, String pCapacitacion)
        {
            coreEliminar(new HrCapacitacionPk(pCompanyowner, pCapacitacion));
        }

        public void coreEliminar(HrCapacitacionPk id)
        {
            HrCapacitacion bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrCapacitacion coreAnular(UsuarioActual usuarioActual, HrCapacitacionPk id)
        {
            HrCapacitacion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCapacitacion coreAnular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion)
        {
            return coreAnular(usuarioActual, new HrCapacitacionPk(pCompanyowner, pCapacitacion));
        }

        public string generarCodigo(string unidadNegocioAsignadaCodigo)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("unidad", ConstanteUtil.TipoDato.String, unidadNegocioAsignadaCodigo));

            int? num = this.contar("hrcapacitacion.generarCodigo", parametros);
            String numString = Convert.ToString(num);

            return unidadNegocioAsignadaCodigo + numString.PadLeft(6, '0');
        }

        public ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, FiltroPaginacionCapacitacion filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoSolicitudCapacitacion> lst = new List<DtoSolicitudCapacitacion>();

            if (UInteger.esCeroOrNulo(filtro.idSolicitante))
                filtro.idSolicitante = null;
            if (UString.estaVacio(filtro.estado))
                filtro.estado = null;
            if (UString.estaVacio(filtro.institucion))
                filtro.institucion = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_solicitante", ConstanteUtil.TipoDato.Integer, filtro.idSolicitante));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.DateTime, filtro.fechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.DateTime, filtro.fechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_idCurso", ConstanteUtil.TipoDato.Integer, filtro.idcurso));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));



            Int32 contador = this.contar("hrcapacitacion.solicitudListarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrcapacitacion.solicitudListarPaginacion");

            while (_reader.Read())
            {
                DtoSolicitudCapacitacion bean = new DtoSolicitudCapacitacion();
                if (!_reader.IsDBNull(0))
                    bean.comapnyowner = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.capacitacionId = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.cursoId = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.cursoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.tipocursoId = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.tipocursoNombre = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.solicitanteId = _reader.GetInt32(6);
                if (!_reader.IsDBNull(7))
                    bean.solicitanteNombre = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.fechaSolicitud = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.fechaDesde = _reader.GetDateTime(9);
                if (!_reader.IsDBNull(10))
                    bean.fechaHasta = _reader.GetDateTime(10);
                if (!_reader.IsDBNull(11))
                    bean.monedaId = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.monedaSigla = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.costoTotalEstimadoLocal = _reader.GetDecimal(13);
                if (!_reader.IsDBNull(14))
                    bean.costoTotalEstimadoExtranjero = _reader.GetDecimal(14);
                if (!_reader.IsDBNull(15))
                    bean.estadoId = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.estadoNombre = _reader.GetString(16);

                lst.Add(bean);
            }
            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            return paginacion;
        }

        public List<string> listarCorreos(HrCapacitacion hrCapacitacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<string> lst = new List<string>();

            parametros.Add(new ParametroPersistenciaGenerico("p_capacitacion", ConstanteUtil.TipoDato.String, hrCapacitacion.Capacitacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrCapacitacion.Companyowner));


            _reader = this.listarPorQuery("hrcapacitacion.listarCorreos", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    lst.Add(_reader.GetString(0));
            }
            this.dispose();

            return lst;
        }

        public List<DtoTabla> listarParametros(HrCapacitacion hrCapacitacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("p_capacitacion", ConstanteUtil.TipoDato.String, hrCapacitacion.Capacitacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrCapacitacion.Companyowner));


            _reader = this.listarPorQuery("hrcapacitacion.listarParametros", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);
                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }

        public List<DtoPrevencionSalud> listarPrevencionSaludCabecera(FiltroGraficos filtro)
        {
            List<DtoPrevencionSalud> lstRetorno = new List<DtoPrevencionSalud>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));

            _reader = this.listarPorQuery("hrcapacitacion.prevencionsaludcabecera", parametros);


            while (_reader.Read())
            {
                DtoPrevencionSalud bean = new DtoPrevencionSalud();

                if (!_reader.IsDBNull(0))
                    bean.medicina = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.odontologia = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.psicologia = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.terapia = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.nutricion = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.otrosTaller = _reader.GetInt32(5);

                if (!_reader.IsDBNull(6))
                    bean.vacunas = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.profilaxis = _reader.GetInt32(7);

                if (!_reader.IsDBNull(8))
                    bean.diagnostico = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.tratamiento = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.otrosCampanias = _reader.GetInt32(10);



                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoPrevencionSalud> listarPrevencionSaludDetalle(FiltroGraficos filtro)
        {
            List<DtoPrevencionSalud> lstRetorno = new List<DtoPrevencionSalud>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.Date, filtro.FechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, filtro.FechaFin));

            _reader = this.listarPorQuery("hrcapacitacion.prevencionsaluddetalle", parametros);


            while (_reader.Read())
            {
                DtoPrevencionSalud bean = new DtoPrevencionSalud();

                if (!_reader.IsDBNull(0))
                    bean.idBeneficiario = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.idInstitucion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.apePaterno = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.apeMaterno = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nombres = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.fecha = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.medicina = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.odontologia = _reader.GetInt32(7);

                if (!_reader.IsDBNull(8))
                    bean.psicologia = _reader.GetInt32(8);

                if (!_reader.IsDBNull(9))
                    bean.terapia = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.nutricion = _reader.GetInt32(10);

                if (!_reader.IsDBNull(11))
                    bean.otrosTaller = _reader.GetInt32(11);

                if (!_reader.IsDBNull(12))
                    bean.vacunas = _reader.GetInt32(12);

                if (!_reader.IsDBNull(13))
                    bean.profilaxis = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.diagnostico = _reader.GetInt32(14);

                if (!_reader.IsDBNull(15))
                    bean.tratamiento = _reader.GetInt32(15);

                if (!_reader.IsDBNull(16))
                    bean.otrosCampanias = _reader.GetInt32(16);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoCapacitacionParticipante> listarParticipantes(HrCapacitacionPk hrCapacitacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCapacitacionParticipante> lst = new List<DtoCapacitacionParticipante>();

            parametros.Add(new ParametroPersistenciaGenerico("p_capacitacion", ConstanteUtil.TipoDato.String, hrCapacitacion.Capacitacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, hrCapacitacion.Companyowner));


            _reader = this.listarPorQuery("hrcapacitacion.listarParticipantes", parametros);

            while (_reader.Read())
            {
                DtoCapacitacionParticipante bean = new DtoCapacitacionParticipante();
                if (!_reader.IsDBNull(0))
                    bean.participante = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.institucion = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.asistio = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.rendimiento = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.participacion = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.comentario = _reader.GetString(5);

                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }
    }
}
