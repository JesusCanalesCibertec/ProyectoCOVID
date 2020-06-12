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

    public class MpPersonaDaoImpl : GenericoDaoImpl<MpPersona>, MpPersonaDao
    {

        private IServiceProvider servicioProveedor;
        public MpPersonaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "mpPersona")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoPersona> lstResultado = new List<DtoPersona>();

            Int32 contador = 0;


            parametros.Add(new ParametroPersistenciaGenerico("p_dni", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_apellido", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_idmodalidad", ConstanteUtil.TipoDato.String, filtro.valor1));

            contador = this.contar("mpPersona.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "mpPersona.filtroPaginacion");

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
                    bean.IdModalidad = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.NomModalidad = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Desde = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.Hasta = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.Correo = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.Estado = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.IdContratacion = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.Ultimafecha = _reader.GetDateTime(10);

                if (!_reader.IsDBNull(11))
                    bean.Fechacese = _reader.GetDateTime(11);

                if (!_reader.IsDBNull(12))
                    bean.Canttitulos = _reader.GetInt32(12);

                if (!_reader.IsDBNull(13))
                    bean.Cantconocimientos = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.Cantadendasentregable = _reader.GetInt32(14);



                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public MpPersona registrar(UsuarioActual usuarioActual, MpPersona bean)
        {
            //bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<MpPersona> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdPersona).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public MpPersona actualizar(UsuarioActual usuarioActual, MpPersona bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<MpPersona> ListarNombres()
        {
            IQueryable<MpPersona> query = this.getCriteria();

            List<MpPersona> lst = query.ToList();

            if (lst.Count > 0)
                return lst;
            return null;
        }

        public MpPersona ObtenerPersonaxUsuario(string usuario)
        {
            IQueryable<MpPersona> query = this.getCriteria();
            query = query.Where(res => res.Usuario == usuario);

            List<MpPersona> lst = query.ToList();

            if (lst.Count > 0)
                return lst.FirstOrDefault();
            return null;
        }

        public MpPersona ObtenerPersonaxDNI(string pDNI)
        {
            IQueryable<MpPersona> query = this.getCriteria();
            query = query.Where(res => res.Dni == pDNI);

            List<MpPersona> lst = query.ToList();

            if (lst.Count > 0)
                return lst.FirstOrDefault();
            return null;
        }

        public List<DtoListafechas> ListarPersonal(DateTime? parametro)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoListafechas> lstResultado = new List<DtoListafechas>();

            parametros.Add(new ParametroPersistenciaGenerico("p_parametro", ConstanteUtil.TipoDato.DateTime, parametro));

            _reader = this.listarPorQuery("mpPersona.listarpersonal", parametros);

            while (_reader.Read())
            {
                DtoListafechas bean = new DtoListafechas();

                if (!_reader.IsDBNull(0))
                    bean.IdPersona = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Dia01 = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.Dia02 = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.Dia03 = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.Dia04 = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.Dia05 = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.Dia06 = _reader.GetInt32(6);
                if (!_reader.IsDBNull(7))
                    bean.Dia07 = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.Dia08 = _reader.GetInt32(8);
                if (!_reader.IsDBNull(9))
                    bean.Dia09 = _reader.GetInt32(9);
                if (!_reader.IsDBNull(10))
                    bean.Dia10 = _reader.GetInt32(10);
                if (!_reader.IsDBNull(11))
                    bean.Dia11 = _reader.GetInt32(11);
                if (!_reader.IsDBNull(12))
                    bean.Dia12 = _reader.GetInt32(12);
                if (!_reader.IsDBNull(13))
                    bean.Dia13 = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.Dia14 = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.Dia15 = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.Dia16 = _reader.GetInt32(16);
                if (!_reader.IsDBNull(17))
                    bean.Dia17 = _reader.GetInt32(17);
                if (!_reader.IsDBNull(18))
                    bean.Dia18 = _reader.GetInt32(18);
                if (!_reader.IsDBNull(19))
                    bean.Dia19 = _reader.GetInt32(19);
                if (!_reader.IsDBNull(20))
                    bean.Dia20 = _reader.GetInt32(20);
                if (!_reader.IsDBNull(21))
                    bean.Dia21 = _reader.GetInt32(21);
                if (!_reader.IsDBNull(22))
                    bean.Dia22 = _reader.GetInt32(22);
                if (!_reader.IsDBNull(23))
                    bean.Dia23 = _reader.GetInt32(23);
                if (!_reader.IsDBNull(24))
                    bean.Dia24 = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.Dia25 = _reader.GetInt32(25);
                if (!_reader.IsDBNull(26))
                    bean.Dia26 = _reader.GetInt32(26);
                if (!_reader.IsDBNull(27))
                    bean.Dia27 = _reader.GetInt32(27);
                if (!_reader.IsDBNull(28))
                    bean.Dia28 = _reader.GetInt32(28);
                if (!_reader.IsDBNull(29))
                    bean.Dia29 = _reader.GetInt32(29);
                if (!_reader.IsDBNull(30))
                    bean.Dia30 = _reader.GetInt32(30);
                if (!_reader.IsDBNull(31))
                    bean.Dia31 = _reader.GetInt32(31);
                if (!_reader.IsDBNull(32))
                    bean.Codigo = _reader.GetInt32(32);
                if (!_reader.IsDBNull(33))
                    bean.IdModalidad = _reader.GetString(33);
                if (!_reader.IsDBNull(34))
                    bean.Nombre = _reader.GetString(34);

                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;
        }

        public List<DtoEventos> ListarEventos(int? pIdPersona)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPersona", ConstanteUtil.TipoDato.Integer, pIdPersona));

            List<DtoEventos> lst = new List<DtoEventos>();

            _reader = base.listarPorQuery("mpPersona.listareventos", parametros);

            while (_reader.Read())
            {
                DtoEventos bean = new DtoEventos();
                if (!_reader.IsDBNull(0))
                    bean.title = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.start = _reader.GetDateTime(1);

                if (!_reader.IsDBNull(2))
                    bean.end = _reader.GetDateTime(2);

                if (!_reader.IsDBNull(3))
                    bean.id = _reader.GetInt32(3);

                lst.Add(bean);
            }

            this.dispose();

            return lst;

        }
    }

}
