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

    public class MpContratacionDaoImpl: GenericoDaoImpl<MpContratacion>, MpContratacionDao
    {

        private IServiceProvider servicioProveedor;

        public MpContratacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "mpContratacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoContratacion> lstResultado = new List<DtoContratacion>();

            Int32 contador = 0;

            parametros.Add(new ParametroPersistenciaGenerico("p_modalidad", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_cargo", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.String, filtro.fechainicio1));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.String, filtro.fechafin1));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            contador = this.contar("mpContratacion.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "mpContratacion.filtroPaginacion");

            while (_reader.Read())
            {
                DtoContratacion bean = new DtoContratacion();

                if (!_reader.IsDBNull(0))
                    bean.IdContratacion = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.IdModalidad = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Modalidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Persona = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Cargo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Fechainicio = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.Fechafin = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.Estado = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.Numeroorden = _reader.GetString(8);

                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public MpContratacion registrar(UsuarioActual usuarioActual, MpContratacion bean)
        {
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int? generarCodigo()
        {
            IQueryable<MpContratacion> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdContratacion).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public MpContratacion actualizar(UsuarioActual usuarioActual, MpContratacion bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<DtoContratacion> Contratosactivos()
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoContratacion> lst = new List<DtoContratacion>();

            _reader = base.listarPorQuery("mpContratacion.contratosactivos", parametros);

            while (_reader.Read())
            {
                DtoContratacion bean = new DtoContratacion();
                if (!_reader.IsDBNull(0))
                    bean.IdContratacion = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.IdModalidad = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Modalidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Persona = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Cargo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Fechainicio = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.Fechafin = _reader.GetDateTime(6);

                if (!_reader.IsDBNull(7))
                    bean.Estado = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.Numeroorden = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.IdPersona = _reader.GetInt32(9);
                lst.Add(bean);
            }

            this.dispose();

            return lst;

        }

        public MpContratacion obtenerUltimoXPersona(int pIdPersona)
        {
            IQueryable<MpContratacion> query = this.getCriteria();
            query = query.Where(p => p.IdPersona == pIdPersona);
            Int32? contador = query.Select(p => p.IdContratacion).DefaultIfEmpty(0).Max();
            
            IQueryable<MpContratacion> query2 = this.getCriteria();
            query2 = query2.Where(p => p.IdContratacion == contador);

            List<MpContratacion> lst = query2.ToList();
            if (lst.Count > 0)
                return lst.FirstOrDefault();
            return null;

        }

        public List<DtoTabla> ListarPie()
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = base.listarPorQuery("mpContratacion.listarpie", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Nombre = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.valorNumerico = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.Porcentaje = _reader.GetDecimal(2);

                lst.Add(bean);
            }

            this.dispose();

            return lst;
        }

        public List<DtoContratacion> ListarContratoPorPersona(int pIdPersona)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoContratacion> lst = new List<DtoContratacion>();

            parametros.Add(new ParametroPersistenciaGenerico("id_persona", ConstanteUtil.TipoDato.Integer, pIdPersona));

            _reader = base.listarPorQuery("mpContratacion.listarcontratosporpersona", parametros);

            while (_reader.Read())
            {
                DtoContratacion bean = new DtoContratacion();
                if (!_reader.IsDBNull(0))
                    bean.Secuencia = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.IdModalidad = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Modalidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.IdCargo = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Cargo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Numeroorden = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Plazo = _reader.GetInt32(6);

                if (!_reader.IsDBNull(7))
                    bean.Fechainicio = _reader.GetDateTime(7);

                if (!_reader.IsDBNull(8))
                    bean.Fechafin = _reader.GetDateTime(8); 

                lst.Add(bean);
            }

            this.dispose();

            if (lst.Count < 1) return null;
            return lst;
        }

        public MpContratacion ObtenerContratoxNOrden(string pOrden)
        {
            IQueryable<MpContratacion> query = this.getCriteria();
            query = query.Where(res => res.Numeroorden == pOrden);

            List<MpContratacion> lst = query.ToList();

            if (lst.Count > 0)
                return lst.FirstOrDefault();
            return null;
        }

        public List<DtoFechasdisponibles> obtenerFechasnodisponibles(DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoFechasdisponibles> lst = new List<DtoFechasdisponibles>();

            parametros.Add(new ParametroPersistenciaGenerico("p_idproyecto", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_idcontratacion", ConstanteUtil.TipoDato.Integer, filtro.Id));
            parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_desde", ConstanteUtil.TipoDato.DateTime, filtro.fechainicio1));
            parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_hasta", ConstanteUtil.TipoDato.DateTime, filtro.fechafin1));
            parametros.Add(new ParametroPersistenciaGenerico("p_horas", ConstanteUtil.TipoDato.Integer, filtro.Secuencia));

            _reader = base.listarPorQuery("mpContratacion.listarfechasnodisponibles", parametros);

            while (_reader.Read())
            {
                DtoFechasdisponibles bean = new DtoFechasdisponibles();
                if (!_reader.IsDBNull(0))
                    bean.secuencia = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.fecha = _reader.GetDateTime(1);

                if (!_reader.IsDBNull(2))
                    bean.estado = _reader.GetInt32(2);

           
                lst.Add(bean);
            }
            this.dispose();

            if (lst.Count < 1) return null;
            return lst;
        }

        public ParametroPaginacionGenerico ListarPersonaldisponible1(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoContratacion> lstResultado = new List<DtoContratacion>();

            Int32 contador = 0;

            if (UString.estaVacio(filtro.Nombre)) filtro.Nombre = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_cargo", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_nivel", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_conocimiento", ConstanteUtil.TipoDato.Decimal, filtro.valorNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_desde", ConstanteUtil.TipoDato.DateTime, filtro.fechainicio1));
            parametros.Add(new ParametroPersistenciaGenerico("p_proyecto_hasta", ConstanteUtil.TipoDato.DateTime, filtro.fechafin1));
            parametros.Add(new ParametroPersistenciaGenerico("p_inicio", ConstanteUtil.TipoDato.DateTime, filtro.fechainicio2));
            parametros.Add(new ParametroPersistenciaGenerico("p_fin", ConstanteUtil.TipoDato.DateTime, filtro.fechafin2));
            parametros.Add(new ParametroPersistenciaGenerico("p_horas", ConstanteUtil.TipoDato.Integer, filtro.Secuencia));

            contador = this.contar("mpContratacion.listarpersonaldisponiblecontar1", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "mpContratacion.listarpersonaldisponible1");

            while (_reader.Read())
            {
                DtoContratacion bean = new DtoContratacion();
                if (!_reader.IsDBNull(0))
                    bean.IdPersona = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Persona = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.IdCargo = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Cargo = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Conocimientos = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.IdContratacion = _reader.GetInt32(5);

                lstResultado.Add(bean);
            }
            this.dispose();

            foreach (var item in lstResultado)
            {
                DtoTabla auxDto = new DtoTabla();
                auxDto.fechainicio1 = filtro.fechainicio1;
                auxDto.fechafin1 = filtro.fechafin1;
                auxDto.CodigoNumerico = filtro.CodigoNumerico;
                auxDto.Secuencia = filtro.Secuencia;
                auxDto.Id = item.IdContratacion;

                List<DtoFechasdisponibles> auxlista = new List<DtoFechasdisponibles>();
                auxlista = this.obtenerFechasnodisponibles(auxDto);

                if (auxlista != null)
                {
                    foreach (var aux in auxlista)
                    {
                        item.diasocupados.Add(aux.fecha);
                    }
                }
            }
            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;
            return paginacion;
        }
    }
    
}
