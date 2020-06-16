using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.minedu.dominio.dto;
using net.royal.spring.minedu.dominio;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;

namespace net.royal.spring.sistema.dao.impl
{
    public class SeguridadperfilusuarioDaoImpl : GenericoDaoImpl<Seguridadperfilusuario>, SeguridadperfilusuarioDao
    {
        private IServiceProvider servicioProveedor;

	    public SeguridadperfilusuarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "seguridadperfilusuario") {
                servicioProveedor = _servicioProveedor;
	    }

        public Seguridadperfilusuario coreInsertar(UsuarioActual usuarioActual, Seguridadperfilusuario bean, string estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionFecha = DateTime.Now;
            this.registrar(bean);
            return bean;
        }

        public ParametroPaginacionGenerico ListarPaginacionUsuario(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<Seguridadperfilusuario> lstResultado = new List<Seguridadperfilusuario>();

            Int32 contador = 0;

            contador = this.contar("seguridadperfilusuario.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "seguridadperfilusuario.filtroPaginacion");

            while (_reader.Read())
            {
                Seguridadperfilusuario bean = new Seguridadperfilusuario();

                if (!_reader.IsDBNull(0))
                    bean.Perfil = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Usuario = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Nombres = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Estado = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.CreacionFecha = _reader.GetDateTime(4);

                if (!_reader.IsDBNull(5))
                    bean.Ultimaconexion = _reader.GetDateTime(5);

                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public List<Seguridadperfilusuario> listarPerfilesActivos(String idUsuario) {
            IQueryable<Seguridadperfilusuario> query = this.getCriteria();
            query = query.Where(p => p.Usuario == idUsuario);
            query = query.Where(p => p.Estado == "A");
            return query.ToList(); ;
        }


    }
}
