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
using net.royal.spring.proceso.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpAuditoriaDaoImpl : GenericoDaoImpl<BpAuditoria>, BpAuditoriaDao
    {
        private IServiceProvider servicioProveedor;

        public BpAuditoriaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpauditoria")
        {
            servicioProveedor = _servicioProveedor;
        }

        public int? generarIdSecuencia(string idProceso, string idFuncionalidad, int? idPersona)
        {
            IQueryable<BpAuditoria> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == idProceso);
            query = query.Where(p => p.IdFuncionalidad == idFuncionalidad);
            query = query.Where(p => p.IdPersona == idPersona);
            Int32? contador = query.Select(p => p.IdSecuencia).DefaultIfEmpty(0).Max();
            contador++;
            return contador;
        }

        public ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroPaginacionAuditoria filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoAuditoria> lstRegistros = new List<DtoAuditoria>();

            if (UInteger.esCeroOrNulo(filtro.IdEmpleado))
                filtro.IdEmpleado = null;
            if (UString.estaVacio(filtro.Periodo))
                filtro.Periodo = null;
            if (UString.estaVacio(filtro.IdCompaniaSocio))
                filtro.IdCompaniaSocio = null;
            if (UString.estaVacio(filtro.IdCentroCosto))
                filtro.IdCentroCosto = null;
            if (UString.estaVacio(filtro.IdSucursal))
                filtro.IdSucursal = null;
            if (UString.estaVacio(filtro.IdProceso))
                filtro.IdProceso = null;
            if (UString.estaVacio(filtro.IdFuncionalidad))
                filtro.IdFuncionalidad = null;
            if (UString.estaVacio(filtro.IdTipoPlanilla))
                filtro.IdTipoPlanilla = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, filtro.IdEmpleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_companiasocio", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_centrocosto", ConstanteUtil.TipoDato.String, filtro.IdCentroCosto));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_sucursal", ConstanteUtil.TipoDato.String, filtro.IdSucursal));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_proceso", ConstanteUtil.TipoDato.String, filtro.IdProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_funcionalidad", ConstanteUtil.TipoDato.String, filtro.IdFuncionalidad));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_tipoplanilla", ConstanteUtil.TipoDato.String, filtro.IdTipoPlanilla));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.FechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.FechaHasta));

            Int32 contador = this.contar("bpauditoria.listarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "bpauditoria.listarPaginacion");
            while (_reader.Read())
            {
                DtoAuditoria bean = new DtoAuditoria();
                if (!_reader.IsDBNull(0))
                    bean.Periodo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CompaniaSocioId = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CompaniaSocioNombre = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.CentroCostoId = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.CentroCostoNombre = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.SucursalId = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.SucursalNombre = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.ProcesoId = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.FuncionalidadId = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.FuncionalidadNombre = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.EmpleadoId = _reader.GetInt32(10);
                if (!_reader.IsDBNull(11))
                    bean.EmpleadoNombre = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.EmpleadoDocumento = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.fecha = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.ProcesoNombre = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.planillaNombre = _reader.GetString(15);
                lstRegistros.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        public List<DtoTabla> listarPeriodoBoleta()
        {

            List<DtoTabla> lstRegistros = new List<DtoTabla>();

            _reader = this.listarPorSentenciaSQL("select distinct ID_PERIODO from sgseguridadsys.BP_AUDITORIA order by ID_PERIODO desc", new List<ParametroPersistenciaGenerico>());

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                {
                    bean.Codigo = _reader.GetString(0);
                }
                bean.Descripcion = bean.Codigo.Substring(0, 6);
                lstRegistros.Add(bean);
            }
            this.dispose();

            return lstRegistros;
        }
    }
}
