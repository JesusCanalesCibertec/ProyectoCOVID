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
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.dao.impl
{
    public class SeguridadautorizacionesDaoImpl : GenericoDaoImpl<Seguridadautorizaciones>, SeguridadautorizacionesDao
    {
        private IServiceProvider servicioProveedor;
        private SeguridadperfilusuarioDao seguridadperfilusuarioDao;
        private SeguridadconceptoDao seguridadconceptoDao;
        private SeguridadgrupoDao seguridadgrupoDao;


        public SeguridadautorizacionesDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "seguridadautorizaciones")
        {
            servicioProveedor = _servicioProveedor;

            seguridadperfilusuarioDao = servicioProveedor.GetService<SeguridadperfilusuarioDao>();
            seguridadconceptoDao = servicioProveedor.GetService<SeguridadconceptoDao>();
            seguridadgrupoDao = servicioProveedor.GetService<SeguridadgrupoDao>();
        }

        public List<DtoTabla> listarAplicacionPorUsuario(string idUsuario)
        {
            List<DtoTabla> lstDtoTabla = new List<DtoTabla>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));
            _reader = this.listarPorQuery("seguridadautorizaciones.listarAplicacionPorUsuario", parametros);
            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Descripcion = _reader.GetString(2);
                lstDtoTabla.Add(bean);
            }
            this.dispose();
            return lstDtoTabla;
        }

        public List<Seguridadgrupo> listarPorAplicacionUsuario(string idAplicacion, string idUsuario)
        {
            IQueryable<Seguridadperfilusuario> query2 = seguridadperfilusuarioDao.getCriteria();
            query2 = query2.Where(p => p.Usuario == idUsuario);
            query2 = query2.Where(p => p.Estado == "A");
            List<Seguridadperfilusuario> lstSeguridadperfilusuario = query2.ToList();

            if (lstSeguridadperfilusuario.Count > 0)
            {
                Seguridadperfilusuario seg = new Seguridadperfilusuario();
                seg = lstSeguridadperfilusuario[0];
                seg.Ultimaconexion = DateTime.Now;
                //this.seguridadperfilusuarioDao.obtenerPorId(new SeguridadperfilusuarioPk() { Perfil = seg.Perfil, Usuario = seg.Usuario }.obtenerArreglo());
                this.seguridadperfilusuarioDao.actualizar(seg);
            }

            List<String> lstUSuario = new List<string>();
            foreach (Seguridadperfilusuario perfil in lstSeguridadperfilusuario)
            {
                lstUSuario.Add(perfil.Perfil.Trim());
            }

            IQueryable<Seguridadautorizaciones> query = this.getCriteria();
            query = query.Where(p => lstUSuario.Contains(p.Usuario));   /** CLAUSULA IN **/
            query = query.Where(p => p.Aplicacioncodigo == idAplicacion);
            query = query.Where(p => p.Estado == "A");
            List<Seguridadautorizaciones> lstSeguridadautorizaciones = query.ToList();

            if (lstSeguridadautorizaciones.Count == 0)
                return new List<Seguridadgrupo>();


            List<Seguridadautorizaciones> lstSeguridadautorizacionesNoRepetida =
                lstSeguridadautorizaciones.GroupBy(d => new { d.Grupo, d.Concepto })
                                  .Select(d => d.First())
                                  .ToList();


            List<Seguridadconcepto> lstSeguridadaconcepto = new List<Seguridadconcepto>();

            List<Seguridadgrupo> grupos = new List<Seguridadgrupo>();

            foreach (Seguridadautorizaciones autorizacion in lstSeguridadautorizacionesNoRepetida)
            {
                Seguridadconcepto bean = null;
                bean = seguridadconceptoDao.obtenerPorId(new SeguridadconceptoPk(autorizacion.Aplicacioncodigo, autorizacion.Grupo, autorizacion.Concepto));
                if (bean != null)
                {
                    if (bean.Estado.Trim() == "A")
                    {
                        if (!UBoolean.validarFlag(bean.WebFlag))
                        {
                            continue;
                        }
                        var esta = false;

                        foreach (var g in grupos)
                        {
                            if (g.Grupo == autorizacion.Grupo)
                            {
                                g.conceptos.Add(bean);
                                g.conceptos = g.conceptos.OrderBy(res => res.Orden).ToList();
                                esta = true;
                            }
                        }
                        if (!esta)
                        {
                            var grupoBean = seguridadgrupoDao.obtenerPorId(new SeguridadgrupoPk() { Aplicacioncodigo = autorizacion.Aplicacioncodigo, Grupo = autorizacion.Grupo }.obtenerArreglo());
                            if (grupoBean != null)
                            {
                                grupoBean.conceptos = new List<Seguridadconcepto>();
                                grupoBean.conceptos.Add(bean);
                                grupos.Add(grupoBean);
                            }
                        }
                        lstSeguridadaconcepto.Add(bean);
                    }
                }
            }
            return grupos;
        }
        public string esRRHH(string idUsuario)
        {
            string resultado = string.Empty;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_usuario", ConstanteUtil.TipoDato.String, idUsuario));
            _reader = this.listarPorQuery("seguridadautorizaciones.esRRHH", parametros);
            while (_reader.Read())
            {
                resultado = _reader.GetString(0);
            }
            this.dispose();
            return resultado;
        }

    }
}
