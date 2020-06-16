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

    public class MpProyectoDaoImpl: GenericoDaoImpl<MpProyecto>, MpProyectoDao
    {

        private IServiceProvider servicioProveedor;

        public MpProyectoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "MpProyecto")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            //filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoProyecto> lstResultado = new List<DtoProyecto>();

            Int32 contador = 0;


            //parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.valor1));
            //parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            //parametros.Add(new ParametroPersistenciaGenerico("p_apellido", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            //parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

           contador = this.contar("MpProyecto.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "MpProyecto.filtroPaginacion");

            while (_reader.Read())
            {
                DtoProyecto bean = new DtoProyecto();

                if (!_reader.IsDBNull(0))
                    bean.Tipoproyecto = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nomtipoproyecto = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Idproyecto = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.Nombre = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Area = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.Niveles = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Fechainicio = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.Fechafin = _reader.GetDateTime(7);

                if (!_reader.IsDBNull(8))
                    bean.Estado = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.Nomestado = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.Desviacion = _reader.GetDecimal(10);


                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public MpProyecto registrar(UsuarioActual usuarioActual, MpProyecto bean)
        {
            bean.IdProyecto = this.generarCodigo();
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<MpProyecto> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdProyecto).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public MpProyecto actualizar(UsuarioActual usuarioActual, MpProyecto bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<MpProyecto> ListarNombres()
        {
            IQueryable<MpProyecto> query = this.getCriteria();

            List<MpProyecto> lst = query.ToList();

            if (lst.Count > 0)
                return lst;
            return null;
        }

        public List<DtoProyecto> Listardetalle(string pTipo, int? pAnio, string pEstado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoProyecto> lst = new List<DtoProyecto>();

            if (pAnio == 0) pAnio = null;
            if (UString.estaVacio(pTipo)) pTipo = null;
            if (UString.estaVacio(pEstado)) pEstado = null;


            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, pTipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_anio", ConstanteUtil.TipoDato.Integer, pAnio));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, pEstado));


            _reader = base.listarPorQuery("MpProyecto.listadoestados", parametros);

            while (_reader.Read())
            {
                DtoProyecto bean = new DtoProyecto();

                if (!_reader.IsDBNull(0))
                    bean.Tipoproyecto = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nomtipoproyecto = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Idproyecto = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.Nombre = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Area = _reader.GetInt32(4);

                if (!_reader.IsDBNull(5))
                    bean.Niveles = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Fechainicio = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.Fechafin = _reader.GetDateTime(7);

                if (!_reader.IsDBNull(8))
                    bean.Estado = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.Nomestado = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.Desviacion = _reader.GetDecimal(10);

                if (!_reader.IsDBNull(11))
                    bean.planificado = _reader.GetDecimal(11);

                if (!_reader.IsDBNull(12))
                    bean.real = _reader.GetDecimal(12);

                if (!_reader.IsDBNull(13))
                    bean.idSIS = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.NomcortoSIS = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.NombreSIS = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.Gestor = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.Asunto = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.Proceso = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.NomArea = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.Observacion = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.Expediente = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.Fechaexpediente = _reader.GetDateTime(22);
                if (!_reader.IsDBNull(23))
                    bean.Secuencia = _reader.GetInt32(23);



                lst.Add(bean);
            }

            this.dispose();

           

            return lst;
        }

        public void eliminarListarecursos(int? pIdProyecto)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdProyecto", ConstanteUtil.TipoDato.Integer, pIdProyecto));
      
            this.ejecutarPorQuery("MpProyecto.eliminarListarecursos", parametros);

            this.dispose();
        }

        public List<DtoTabla> ListarBarraEstados()
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = base.listarPorQuery("MpProyecto.listarestados", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Secuencia = _reader.GetInt32(2);


                lst.Add(bean);
            }

            this.dispose();

            return lst;
        }

        public List<DtoTabla> ListarBarraTipos()
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = base.listarPorQuery("MpProyecto.listartipos", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.num1 = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.num2 = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.num3 = _reader.GetInt32(4);

     
                lst.Add(bean);
            }

            this.dispose();

            return lst;
        }
    }
    
}
