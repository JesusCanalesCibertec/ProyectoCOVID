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

    public class MpPersonatituloDaoImpl: GenericoDaoImpl<MpPersonatitulo>, MpPersonatituloDao
    {

        private IServiceProvider servicioProveedor;

        public MpPersonatituloDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "mpPersonatitulo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoPersona> lstResultado = new List<DtoPersona>();

            Int32 contador = 0;


            parametros.Add(new ParametroPersistenciaGenerico("p_dni", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_apellido", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

           contador = this.contar("mpPersonatitulo.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "mpPersonatitulo.filtroPaginacion");

            while (_reader.Read())
            {
                DtoPersona bean = new DtoPersona();

                if (!_reader.IsDBNull(0))
                    bean.Id = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.DNI = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Nombre = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Apellido = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Instruccion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Anexo = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Celular = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.Correo = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.Estado = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.Centro = _reader.GetString(9);



                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public MpPersonatitulo registrar(UsuarioActual usuarioActual, MpPersonatitulo bean)
        {
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<MpPersonatitulo> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdPersona).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public MpPersonatitulo actualizar(UsuarioActual usuarioActual, MpPersonatitulo bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<DtoPersonatitulo> listado(int? pIdPersona)
        {
            List<DtoPersonatitulo> lst = new List<DtoPersonatitulo>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_idpersona", ConstanteUtil.TipoDato.Integer, pIdPersona));
         

            _reader = this.listarPorQuery("mpPersonatitulo.listartitulos", parametros);

            while (_reader.Read())
            {
                DtoPersonatitulo bean = new DtoPersonatitulo();

                if (!_reader.IsDBNull(0))
                    bean.IdPersona = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.IdCarrera = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.NomCarrera = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.IdNivel = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.NomNivel = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.IdGrado = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.NomGrado = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdCentro = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.NomCentro = _reader.GetString(8);


                lst.Add(bean);
            }
            this.dispose();


            return lst;
        }
    }
    
}
