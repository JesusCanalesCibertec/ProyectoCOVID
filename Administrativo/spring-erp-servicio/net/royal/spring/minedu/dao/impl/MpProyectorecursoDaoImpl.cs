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

    public class MpProyectorecursoDaoImpl: GenericoDaoImpl<MpProyectorecurso>, MpProyectorecursoDao
    {

        private IServiceProvider servicioProveedor;

        public MpProyectorecursoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "MpProyectorecurso")
        {
            servicioProveedor = _servicioProveedor;
        }

        public MpProyectorecurso registrar(UsuarioActual usuarioActual, MpProyectorecurso bean)
        {
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

  

        public MpProyectorecurso actualizar(UsuarioActual usuarioActual, MpProyectorecurso bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }


        public List<MpProyectorecurso> listarRecursos(int pIdProyecto)
        {
            List<MpProyectorecurso> lst = new List<MpProyectorecurso>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdProyecto", ConstanteUtil.TipoDato.Integer, pIdProyecto));


            _reader = this.listarPorQuery("MpProyectorecurso.listarRecursos", parametros);

            while (_reader.Read())
            {
                MpProyectorecurso bean = new MpProyectorecurso();

                if (!_reader.IsDBNull(0))
                    bean.IdRecurso = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Area = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdPersona = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.Nombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Cargo = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Rol = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.auxConocimientos = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.idContratacion = _reader.GetInt32(7);


                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }

        public void eliminarPeriodos(int pIdProyecto, int pIdPersona, int pIdContratacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdProyecto", ConstanteUtil.TipoDato.Integer, pIdProyecto));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPersona", ConstanteUtil.TipoDato.Integer, pIdPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdContratación", ConstanteUtil.TipoDato.Integer, pIdContratacion));

            this.ejecutarPorQuery("MpProyectorecurso.eliminarListaperiodos", parametros);

            this.dispose();
        }
    }
    
}
