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
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsBeneficiarioDaoImpl : GenericoDaoImpl<PsBeneficiario>, PsBeneficiarioDao
    {
        private IServiceProvider servicioProveedor;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;


        public PsBeneficiarioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psbeneficiario")
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
        }

        public PsBeneficiario obtenerPorId(PsBeneficiarioPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsBeneficiario obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return obtenerPorId(new PsBeneficiarioPk(pIdInstitucion, pIdBeneficiario));
        }

        public PsBeneficiario coreInsertar(UsuarioActual usuarioActual, PsBeneficiario bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsBeneficiario coreActualizar(UsuarioActual usuarioActual, PsBeneficiario bean, String estado)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            coreEliminar(new PsBeneficiarioPk(pIdInstitucion, pIdBeneficiario));
        }

        public void coreEliminar(PsBeneficiarioPk id)
        {
            PsBeneficiario bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsBeneficiario coreAnular(UsuarioActual usuarioActual, PsBeneficiarioPk id)
        {
            PsBeneficiario bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsBeneficiario coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario)
        {
            return coreAnular(usuarioActual, new PsBeneficiarioPk(pIdInstitucion, pIdBeneficiario));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro)
        {
            Int32 contador = 0;

            List<DtoBeneficiario> lstRetorno = new List<DtoBeneficiario>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.sexo))
            {
                filtro.sexo = null;
            }
            if (UString.estaVacio(filtro.institucion))
            {
                filtro.institucion = null;
            }

            if (filtro.edad == null)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, null));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, null));
            }
            if (filtro.edad == 1 || filtro.edad == 11)
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.edad * 5) - 5));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, (filtro.edad * 5)));
            }
            if (filtro.edad == 4 || filtro.edad == 19)
            {
                if (filtro.edad == 19)
                {
                    parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.edad * 5) - 4));
                }
                else
                {
                    parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.edad * 5) - 4));
                }
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, 120));
            }
            else
            {
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadDesde", ConstanteUtil.TipoDato.Integer, (filtro.edad * 5) - 4));
                parametros.Add(new ParametroPersistenciaGenerico("p_EdadHasta", ConstanteUtil.TipoDato.Integer, filtro.edad * 5));
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_dni", ConstanteUtil.TipoDato.String, filtro.dni));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.sexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_desdeNac", ConstanteUtil.TipoDato.Date, filtro.desdeNac));
            parametros.Add(new ParametroPersistenciaGenerico("p_hastaNac", ConstanteUtil.TipoDato.Date, filtro.hastaNac));
            parametros.Add(new ParametroPersistenciaGenerico("p_desdeReg", ConstanteUtil.TipoDato.Date, filtro.desdeReg));
            parametros.Add(new ParametroPersistenciaGenerico("p_hastaReg", ConstanteUtil.TipoDato.Date, filtro.hastaReg));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.programa));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));


            contador = this.contar("psbeneficiario.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psbeneficiario.filtroPaginacion");

            while (_reader.Read())
            {
                DtoBeneficiario bean = new DtoBeneficiario();

                if (!_reader.IsDBNull(0))
                    bean.idInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.institucion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.idBeneficiario = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.beneficiario = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.dni = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.tipodocumento = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.sexo = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.edad = _reader.GetInt32(7);

                if (!_reader.IsDBNull(8))
                    bean.fechaReg = _reader.GetDateTime(8);

                if (!_reader.IsDBNull(9))
                    bean.tipoNuevo = _reader.GetInt32(9);

                if (!_reader.IsDBNull(10))
                    bean.IdPrograma = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.estado = _reader.GetString(11);

                if (!_reader.IsDBNull(12))
                    bean.IdDiscapacidad = _reader.GetString(12);

                if (!_reader.IsDBNull(13))
                    bean.IdComponenteSalud = _reader.GetInt32(13);
                if (!_reader.IsDBNull(14))
                    bean.IdComponenteCapacidad = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.IdComponenteSocioAmbiental = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.IdComponenteNutricion = _reader.GetInt32(16);

                if (!_reader.IsDBNull(17))
                    bean.IdMotivo = _reader.GetString(17);

                //if (!_reader.IsDBNull(18))
                //    bean.secuencia = _reader.GetInt32(18);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public List<PsBeneficiario> obtenerPorEntidad(int? idEntidad)
        {
            IQueryable<PsBeneficiario> query = getCriteria();
            query = query.Where(x => x.IdBeneficiario == idEntidad);
            query = query.Where(x => x.Estado == "EGR");
            return query.ToList();
        }

        public void eliminarDetalles(string auxInstitucion, int? idEntidad, int? idUltimoIngreso)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("institucion", ConstanteUtil.TipoDato.String, auxInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("entidad", ConstanteUtil.TipoDato.Integer, idEntidad));
            parametros.Add(new ParametroPersistenciaGenerico("ultimoingreso", ConstanteUtil.TipoDato.Integer, idUltimoIngreso));

            this.ejecutarPorQuery("psbeneficiario.eliminarDetalles", parametros);

            this.dispose();
        }
        public List<MaMiscelaneosdetalle> listarValoracionNutricional(int IdBeneficiario, String IdInstitucion)
        {
            PsBeneficiario bene = this.obtenerPorId(new PsBeneficiarioPk(IdInstitucion, IdBeneficiario));


            List<MaMiscelaneosdetalle> lst = new List<MaMiscelaneosdetalle>();

            if (bene != null)
            {
                if (bene.IdPrograma == "NNA")
                    lst = maMiscelaneosdetalleDao.listar(new FiltroMiscelaneosDetalle("PS", "NUTRVALNIN"));
                else
                    lst = maMiscelaneosdetalleDao.listar(new FiltroMiscelaneosDetalle("PS", "NUTRVALADU"));
            }

            return lst;
        }

        public List<string> listarCorreos(string auxInstitucion, string proceso)
        {
            List<String> lstRetorno = new List<String>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, auxInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_proceso", ConstanteUtil.TipoDato.String, proceso));

            _reader = this.listarPorQuery("psbeneficiario.listarCorreos", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    lstRetorno.Add(_reader.GetString(0));
            }
            this.dispose();

            return lstRetorno;
        }

        public ParametroPaginacionGenerico listarBeneficiarios(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro)
        {
            Int32 contador = 0;

            List<DtoPsBeneficiario> lstRetorno = new List<DtoPsBeneficiario>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.codigo))
                filtro.codigo = null;

            if (UString.estaVacio(filtro.dni))
                filtro.dni = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_beneficiario", ConstanteUtil.TipoDato.Integer, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.dni));
            parametros.Add(new ParametroPersistenciaGenerico("p_edad", ConstanteUtil.TipoDato.Integer, filtro.edad));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.area));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.sexo));


            contador = this.contar("psbeneficiario.filtroContar2", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psbeneficiario.filtroPaginacion2");

            while (_reader.Read())
            {
                DtoPsBeneficiario bean = new DtoPsBeneficiario();

                if (!_reader.IsDBNull(0))
                    bean.idInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.idBeneficiario = _reader.GetInt32(1);



                if (!_reader.IsDBNull(2))
                    bean.nombre = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.documento = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idPrograma = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.auxInstitucion = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.sexo = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.edad = _reader.GetInt32(7);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public int generarCodigo(string auxInstitucion)
        {
            IQueryable<PsBeneficiario> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == auxInstitucion);
            List<PsBeneficiario> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdBeneficiario));
            }

            return var + 1;
        }

        public DtoPsBeneficiario obtenerPrograma(PsEntidad bean)
        {

            List<DtoPsBeneficiario> lstRetorno = new List<DtoPsBeneficiario>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(bean.Documento))
                bean.Documento = null;

            if (bean.Documento != null)
            {
                bean.ApellidoPaterno = null;
                bean.Nombres = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, bean.auxPrograma));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, bean.Documento));
            parametros.Add(new ParametroPersistenciaGenerico("p_apepa", ConstanteUtil.TipoDato.String, bean.ApellidoPaterno));
            parametros.Add(new ParametroPersistenciaGenerico("p_nom", ConstanteUtil.TipoDato.String, bean.Nombres));


            _reader = this.listarPorQuery("psbeneficiario.filtroprograma", parametros);

            while (_reader.Read())
            {
                DtoPsBeneficiario objeto = new DtoPsBeneficiario();

                if (!_reader.IsDBNull(0))
                    objeto.idInstitucion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    objeto.idBeneficiario = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    objeto.nombre = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    objeto.documento = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    objeto.idPrograma = _reader.GetString(4);




                lstRetorno.Add(objeto);
            }

            this.dispose();
            if (lstRetorno.Count == 1)
            {
                return lstRetorno[0];
            }
            else
            {
                return null;
            }

        }

        public List<DtoPrevencionSalud> ListarBeneficiariosSinEvaluacion(FiltroGraficos filtro)
        {
            List<DtoPrevencionSalud> lstRetorno = new List<DtoPrevencionSalud>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdComponente", ConstanteUtil.TipoDato.String, filtro.IdComponente));

            _reader = this.listarPorQuery("psbeneficiario.sinevaluacion", parametros);


            while (_reader.Read())
            {
                DtoPrevencionSalud bean = new DtoPrevencionSalud();

                if (!_reader.IsDBNull(0))
                    bean.apePaterno = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.apeMaterno = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nombres = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomComponente = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.idInstitucion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.idBeneficiario = _reader.GetInt32(5);




                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public ParametroPaginacionGenerico listarPaginacion2(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro)
        {
            List<List<String>> lstRetorno = new List<List<String>>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.sexo))
            {
                filtro.sexo = null;
            }
            if (UString.estaVacio(filtro.institucion))
            {
                filtro.institucion = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_dni", ConstanteUtil.TipoDato.String, filtro.dni));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.institucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.sexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_desdeNac", ConstanteUtil.TipoDato.Date, filtro.desdeNac));
            parametros.Add(new ParametroPersistenciaGenerico("p_hastaNac", ConstanteUtil.TipoDato.Date, filtro.hastaNac));
            parametros.Add(new ParametroPersistenciaGenerico("p_desdeReg", ConstanteUtil.TipoDato.Date, filtro.desdeReg));
            parametros.Add(new ParametroPersistenciaGenerico("p_hastaReg", ConstanteUtil.TipoDato.Date, filtro.hastaReg));
            parametros.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, filtro.programa));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            // _reader = this.listarPorQuery("psbeneficiario.filtroPaginacion22", parametros);
            _reader = this.listarPorQuery("psbeneficiario.listarPaginacionBeneficiario", parametros);



            while (_reader.Read())
            {
                List<String> bean = new List<String>();

                var col = 1;

                for (; col < 173; col++)
                {
                    if (!_reader.IsDBNull(col - 1))
                        bean.Add(_reader.GetString(col - 1));
                }

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;

            return paginacion;
        }

        public ParametroPaginacionGenerico paginacionBeneficiariosSinEvaluacion(ParametroPaginacionGenerico paginacion, FiltroGraficos filtro)
        {
            Int32 contador = 0;

            List<DtoPrevencionSalud> lstRetorno = new List<DtoPrevencionSalud>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_IdBeneficiario", ConstanteUtil.TipoDato.Integer, filtro.idBeneficiario));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdPeriodo", ConstanteUtil.TipoDato.String, filtro.IdPeriodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_IdComponente", ConstanteUtil.TipoDato.String, filtro.IdComponente));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));




            _reader = this.listarPorQuery("psbeneficiario.sinevaluacion", parametros);

            while (_reader.Read())
            {
                DtoPrevencionSalud bean = new DtoPrevencionSalud();


                if (!_reader.IsDBNull(0))
                    bean.apePaterno = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.apeMaterno = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nombres = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomComponente = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.idInstitucion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.idBeneficiario = _reader.GetInt32(5);

                if (!_reader.IsDBNull(7))
                    contador = _reader.GetInt32(7);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }
    }
}
