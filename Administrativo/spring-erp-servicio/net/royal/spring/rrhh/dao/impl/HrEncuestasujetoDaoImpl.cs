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

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrEncuestasujetoDaoImpl : GenericoDaoImpl<HrEncuestasujeto>, HrEncuestasujetoDao
    {
        private IServiceProvider servicioProveedor;


        public HrEncuestasujetoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuestasujeto")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestasujeto obtenerPorId(HrEncuestasujetoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }


        public HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto bean)
        {
            bean.Ultimafechamodif = DateTime.Today;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }
        public HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto bean, DateTime ahora)
        {
            bean.Ultimafechamodif = ahora;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEncuestasujeto coreActualizar(UsuarioActual usuarioActual, HrEncuestasujeto bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String pPeriodo)
        {
            coreEliminar(new HrEncuestasujetoPk(pSecuencia, pSujeto, pPregunta, pCompanyowner, pPeriodo));
        }

        public void coreEliminar(HrEncuestasujetoPk id)
        {
            HrEncuestasujeto bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuestasujeto coreAnular(UsuarioActual usuarioActual, HrEncuestasujetoPk id)
        {
            HrEncuestasujeto bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrEncuestasujeto coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String pPeriodo)
        {
            return coreAnular(usuarioActual, new HrEncuestasujetoPk(pSecuencia, pSujeto, pPregunta, pCompanyowner, pPeriodo));
        }

        public int autogenerarSujetoPorEncuesta(HrEncuestaPk hrEncuestaPk)
        {
            IQueryable<HrEncuestasujeto> query = this.getCriteria();

            query = query.Where(p => p.Companyowner == hrEncuestaPk.Companyowner);
            query = query.Where(p => p.Periodo == hrEncuestaPk.Periodo);
            query = query.Where(p => p.Secuencia == hrEncuestaPk.Secuencia);
            int? var = query.Select(p => p.Sujeto).DefaultIfEmpty(0).Max();
            return var == null ? 1 : var.Value + 1;
        }

        public DtoEncuesta anularSujeto(DtoEncuestaSujeto dto)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, dto.secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, dto.periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, dto.compania));
            parametros.Add(new ParametroPersistenciaGenerico("p_sujeto", ConstanteUtil.TipoDato.Integer, dto.sujeto));

            this.ejecutarPorQuery("hrencuestasujeto.anularSujeto", parametros);
            this.dispose();

            return new DtoEncuesta();
        }

        public DtoEncuesta copiar(HrEncuestaPk pk)
        {

            DtoEncuesta dto = new DtoEncuesta();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_secuencia", ConstanteUtil.TipoDato.Integer, pk.Secuencia));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, pk.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, pk.Companyowner));

            _reader = this.listarPorQuery("hrencuestasujeto.copiar", parametros);
            if (_reader.Read())
            {
                dto.compania = _reader.GetString(0);
                dto.periodo = _reader.GetString(1);
                dto.secuencia = _reader.GetInt32(2);
            }
            this.dispose();

            return dto;
        }
    }
}
