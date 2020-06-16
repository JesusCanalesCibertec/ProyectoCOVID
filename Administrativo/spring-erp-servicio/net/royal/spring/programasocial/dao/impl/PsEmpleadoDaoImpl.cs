using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.core.dominio;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsEmpleadoDaoImpl : GenericoDaoImpl<PsEmpleado>, PsEmpleadoDao
    {
        private IServiceProvider servicioProveedor;


        public PsEmpleadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psempleado")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEmpleado obtenerPorId(PsEmpleadoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEmpleado obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            return obtenerPorId(new PsEmpleadoPk(pIdInstitucion, pIdEmpleado));
        }

        public PsEmpleado coreInsertar(UsuarioActual usuarioActual, PsEmpleado bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEmpleado coreActualizar(UsuarioActual usuarioActual, PsEmpleado bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            coreEliminar(new PsEmpleadoPk(pIdInstitucion, pIdEmpleado));
        }

        public void coreEliminar(PsEmpleadoPk id)
        {
            PsEmpleado bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEmpleado coreAnular(UsuarioActual usuarioActual, PsEmpleadoPk id)
        {
            PsEmpleado bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsEmpleado coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdEmpleado)
        {
            return coreAnular(usuarioActual, new PsEmpleadoPk(pIdInstitucion, pIdEmpleado));
        }

        public int generarCodigo()
        {
            IQueryable<PsEmpleado> query = this.getCriteria();

            List<PsEmpleado> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdEmpleado));
            }

            return var + 1;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPsEmpleado filtro)
        {
            Int32 contador = 0;

            List<DtoPsEmpleado> lstRetorno = new List<DtoPsEmpleado>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.codEmpleado))
                filtro.codEmpleado = null;

            if (UString.estaVacio(filtro.numDocumento))
                filtro.numDocumento = null;

            if (UString.estaVacio(filtro.area))
                filtro.area = null;

            if (UString.estaVacio(filtro.sexo))
                filtro.sexo = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_institucion", ConstanteUtil.TipoDato.String, filtro.codInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, filtro.codEmpleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nomEmpleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.numDocumento));
            parametros.Add(new ParametroPersistenciaGenerico("p_edad", ConstanteUtil.TipoDato.Integer, filtro.edad));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.area));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.sexo));

            contador = this.contar("psempleado.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psempleado.filtroPaginacion");

            while (_reader.Read())
            {
                DtoPsEmpleado bean = new DtoPsEmpleado();

                if (!_reader.IsDBNull(0))
                    bean.idInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.idEmpleado = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.nombre = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.documento = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.auxInstitucion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.sexo = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.edad = _reader.GetInt32(6);


                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public IList<DtoTabla> listarAreas()
        {

            List<DtoTabla> lstRetorno = new List<DtoTabla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            _reader = this.listarPorQuery("psempleado.listarAreas", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<Empleadomast> listarPorCorreoInterno(string correo)
        {
            List<Empleadomast> lstRetorno = new List<Empleadomast>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("correo", ConstanteUtil.TipoDato.String, correo));
            _reader = this.listarPorQuery("psempleado.listarPorCorreoInterno", parametros);

            while (_reader.Read())
            {
                Empleadomast bean = new Empleadomast();

                if (!_reader.IsDBNull(0))
                    bean.Empleado = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Codigousuario = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Companiasocio = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Correointerno = _reader.GetString(3);


                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }
    }
}
