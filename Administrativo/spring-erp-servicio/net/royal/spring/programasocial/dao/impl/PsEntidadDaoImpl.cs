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
using net.royal.spring.rrhh.dominio.dtoscomponente;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsEntidadDaoImpl : GenericoDaoImpl<PsEntidad>, PsEntidadDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidad")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidad obtenerPorId(PsEntidadPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidad obtenerPorId(Nullable<Int32> pIdEntidad)
        {
            return obtenerPorId(new PsEntidadPk(pIdEntidad));
        }

        public PsEntidad coreInsertar(UsuarioActual usuarioActual, PsEntidad bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<PsEntidad> query = this.getCriteria();

            List<PsEntidad> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdEntidad));
            }

            return var + 1;
        }

        public PsEntidad coreActualizar(UsuarioActual usuarioActual, PsEntidad bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad)
        {
            coreEliminar(new PsEntidadPk(pIdEntidad));
        }

        public void coreEliminar(PsEntidadPk id)
        {
            PsEntidad bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidad coreAnular(UsuarioActual usuarioActual, PsEntidadPk id)
        {
            PsEntidad bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsEntidad coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad)
        {
            return coreAnular(usuarioActual, new PsEntidadPk(pIdEntidad));
        }

        public ParametroPaginacionGenerico listarFurh(ParametroPaginacionGenerico paginacion, DtoComponenteFurh filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoComponenteFurh> lstResultado = new List<DtoComponenteFurh>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.Estado))
            {
                filtro.Estado = null;
            }
            if (String.IsNullOrEmpty(filtro.IdEspecialidadAcademica))
            {
                filtro.IdEspecialidadAcademica = null;
            }
            if (String.IsNullOrEmpty(filtro.IdNivelAcademico))
            {
                filtro.IdNivelAcademico = null;
            }
            if (String.IsNullOrEmpty(filtro.Nombrecompleto))
            {
                filtro.Nombrecompleto = null;
            }
            if (String.IsNullOrEmpty(filtro.IdAreaTrabajo))
            {
                filtro.IdAreaTrabajo = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_nivel", ConstanteUtil.TipoDato.String, filtro.IdNivelAcademico));
            parametros.Add(new ParametroPersistenciaGenerico("p_especialidad", ConstanteUtil.TipoDato.String, filtro.IdEspecialidadAcademica));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.String, filtro.Nombrecompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdAreaTrabajo));
            parametros.Add(new ParametroPersistenciaGenerico("p_conusuario", ConstanteUtil.TipoDato.Integer, filtro.conusuario ? 1 : 0));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));

            contador = this.contar("psentidad.filtroContar", parametros);

            _reader = this.listarPorQuery("psentidad.filtroPaginacion", parametros);

            while (_reader.Read())
            {
                DtoComponenteFurh bean = new DtoComponenteFurh();

                if (!_reader.IsDBNull(0))
                    bean.IdEntidad = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.ApellidoPaterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.ApellidoMaterno = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Nombres = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Nombrecompleto = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.FechaIngreso = _reader.GetDateTime(5);
                if (!_reader.IsDBNull(6))
                    bean.IdNivelAcademico = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdEspecialidadAcademica = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdNivelAcademicoNombre = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdEspecialidadAcademicaNombre = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.EstadoNombre = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.TiempoPermanencia = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdInstitucion = _reader.GetString(12);

                if (!_reader.IsDBNull(13))
                    bean.Correo1 = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.IdDiscapacidad = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.IdAreaTrabajo = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdTipoDocumento = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.Documento = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.FechaNacimiento = _reader.GetDateTime(18);
                if (!_reader.IsDBNull(19))
                    bean.Edad = _reader.GetString(19);
                if (!_reader.IsDBNull(20))
                    bean.IdSexo = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.CodigoUsuario = _reader.GetString(21);


                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }

        public PsEntidad obtenerDatos(PsEntidad bean)
        {

            IQueryable<PsEntidad> query = this.getCriteria();

            if (bean.ApellidoPaterno != null && bean.Nombres != null && bean.Documento == null)
            {
                query = query.Where(p => p.ApellidoPaterno == bean.ApellidoPaterno && p.Nombres == bean.Nombres);

            }

            if (bean.Documento != null)
            {
                query = query.Where(p => p.Documento == bean.Documento);
            }

            List<PsEntidad> lst = query.ToList();
            List<PsEntidad> lst2 = new List<PsEntidad>();


            foreach (var item in lst)
            {
                PsBeneficiarioDao beneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
                List<PsBeneficiario> b = beneficiarioDao.obtenerPorEntidad(item.IdEntidad);
                if (b.Count > 0)
                {
                    lst2.Add(item);
                }
            }

            PsEntidad objeto = new PsEntidad();

            if (lst2.Count > 1)
            {
                objeto = lst2[0];
            }
            else
            {
                return null;
            }

            return objeto;
        }

        public List<DtoComponenteFurh> exportarFurh(DtoComponenteFurh filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoComponenteFurh> lstResultado = new List<DtoComponenteFurh>();

            if (String.IsNullOrEmpty(filtro.Estado))
            {
                filtro.Estado = null;
            }
            if (String.IsNullOrEmpty(filtro.IdEspecialidadAcademica))
            {
                filtro.IdEspecialidadAcademica = null;
            }
            if (String.IsNullOrEmpty(filtro.IdNivelAcademico))
            {
                filtro.IdNivelAcademico = null;
            }
            if (String.IsNullOrEmpty(filtro.Nombrecompleto))
            {
                filtro.Nombrecompleto = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_nivel", ConstanteUtil.TipoDato.String, filtro.IdNivelAcademico));
            parametros.Add(new ParametroPersistenciaGenerico("p_especialidad", ConstanteUtil.TipoDato.String, filtro.IdEspecialidadAcademica));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.String, filtro.Nombrecompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));

            _reader = this.listarPorQuery("psentidad.filtroPaginacion2", parametros);

            while (_reader.Read())
            {
                DtoComponenteFurh bean = new DtoComponenteFurh();

                if (!_reader.IsDBNull(0))
                    bean.IdInstitucionNombre = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.ApellidoPaterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.ApellidoMaterno = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Nombres = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.IdTipoDocumento = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Documento = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.IdSexo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.FechaNacimiento = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.Correo1 = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdDiscapacidad = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.FechaIngreso = _reader.GetDateTime(10);
                if (!_reader.IsDBNull(11))
                    bean.IdAreaTrabajo = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdNivelAcademicoNombre = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.IdEspecialidadAcademicaNombre = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.CodigoUsuario = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.Estado = _reader.GetString(15);
                lstResultado.Add(bean);
            }


            this.dispose();

            return lstResultado;
        }
    }
}
