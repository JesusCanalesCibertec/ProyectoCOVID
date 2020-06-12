using net.royal.spring.framework.constante;
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

    public class MpProyectorecursoperiodoDaoImpl : GenericoDaoImpl<MpProyectorecursoperiodo>, MpProyectorecursoperiodoDao
    {

        private IServiceProvider servicioProveedor;

        public MpProyectorecursoperiodoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "MpProyectorecursoperiodo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public MpProyectorecursoperiodo registrar(UsuarioActual usuarioActual, MpProyectorecursoperiodo bean)
        {
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public MpProyectorecursoperiodo actualizar(UsuarioActual usuarioActual, MpProyectorecursoperiodo bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<MpProyectorecursoperiodo> listarPeriodos(int pIdProyecto, int pIdPersona)
        {
            IQueryable<MpProyectorecursoperiodo> query = this.getCriteria();
            query = query.Where(x => x.IdProyecto == pIdProyecto);
            query = query.Where(x => x.IdPersona == pIdPersona);
            return query.ToList();
        }

        //public List<MpProyectorecursoperiodo> listarInteresados(int pIdProyecto)
        //{
        //    IQueryable<MpProyectorecursoperiodo> query = this.getCriteria();
        //    query = query.Where(x => x.IdProyecto == pIdProyecto);
        //    query = query.Where(x => Int32.Parse(x.Rol == null?"100":x.Rol) < 4);
        //    return query.ToList();
        //}

        //public List<MpProyectorecursoperiodo> listarEquipo(int pIdProyecto)
        //{
        //    List<MpProyectorecursoperiodo> lst = new List<MpProyectorecursoperiodo>();

        //    List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

        //    parametros.Add(new ParametroPersistenciaGenerico("p_IdProyecto", ConstanteUtil.TipoDato.Integer, pIdProyecto));


        //    _reader = this.listarPorQuery("MpProyectorecursoperiodo.listarEquipo", parametros);

        //    while (_reader.Read())
        //    {
        //        MpProyectorecursoperiodo bean = new MpProyectorecursoperiodo();

        //        if (!_reader.IsDBNull(0))
        //            bean.IdRecurso = _reader.GetInt32(0);
        //        if (!_reader.IsDBNull(1))
        //            bean.Area = _reader.GetInt32(1);
        //        if (!_reader.IsDBNull(2))
        //            bean.IdPersona = _reader.GetInt32(2);
        //        if (!_reader.IsDBNull(3))
        //            bean.Nombre = _reader.GetString(3);
        //        if (!_reader.IsDBNull(4))
        //            bean.Cargo = _reader.GetString(4);
        //        if (!_reader.IsDBNull(5))
        //            bean.Rol = _reader.GetString(5);
        //        if (!_reader.IsDBNull(6))
        //            bean.auxConocimientos = _reader.GetString(6);


        //        lst.Add(bean);
        //    }
        //    this.dispose();


        //    return lst;
        //    return null;
        //}
    }

}
