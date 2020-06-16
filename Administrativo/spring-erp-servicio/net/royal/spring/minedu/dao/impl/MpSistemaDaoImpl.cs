using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net.royal.spring.minedu.dao.impl
{

    public class MpSistemaDaoImpl: MySqlDaoImpl<MpSistema>, MpSistemaDao
    {

        private IServiceProvider servicioProveedor;

        public MpSistemaDaoImpl(MySqlDbContext context, IServiceProvider _servicioProveedor):base(context, "MpSistema")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<MpSistema> ListarSistemas()
        {
            IQueryable<MpSistema> query = this.getCriteria();
            query = query.OrderBy(x => x.Nombre);

            return query.ToList();
        }

        //public List<DtoFechasdisponibles> obtenerFechasnodisponibles(DtoTabla filtro)
        //{
        //    List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
        //    List<DtoFechasdisponibles> lst = new List<DtoFechasdisponibles>();

        //    parametros.Add(new ParametroPersistenciaGenerico("p_idproyecto", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_idcontratacion", ConstanteUtil.TipoDato.Integer, filtro.Id));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_desde", ConstanteUtil.TipoDato.DateTime, filtro.fechainicio1));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_hasta", ConstanteUtil.TipoDato.DateTime, filtro.fechafin1));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_horas", ConstanteUtil.TipoDato.Integer, filtro.Secuencia));


        //    _reader = base.listarPorQuery("MpSistema.listarfechasnodisponibles", parametros);

        //    while (_reader.Read())
        //    {
        //        DtoFechasdisponibles bean = new DtoFechasdisponibles();
        //        if (!_reader.IsDBNull(0))
        //            bean.secuencia = _reader.GetInt32(0);

        //        if (!_reader.IsDBNull(1))
        //            bean.fecha = _reader.GetDateTime(1);

        //        if (!_reader.IsDBNull(2))
        //            bean.estado = _reader.GetInt32(2);


        //        lst.Add(bean);
        //    }

        //    this.dispose();

        //    if (lst.Count < 1) return null;
        //    return lst;
        //}
    }
    
}
