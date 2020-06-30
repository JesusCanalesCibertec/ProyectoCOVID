using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using net.royal.spring.covid.dominio.filtro;
using net.royal.spring.framework.core;

namespace net.royal.spring.covid.dao.impl
{

    public class CiudadanoDaoImpl: GenericoDaoImpl<Ciudadano>, CiudadanoDao
    {

        private IServiceProvider servicioProveedor;

        public CiudadanoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "covidciudadano")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroCiudadano filtro)
        {

            if (!UString.estaVacio(filtro.documento))
            {
                if (filtro.documento.Length < 8)
                    filtro.documento = null;
            }
                
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCiudadano> lstResultado = new List<DtoCiudadano>();

            Int32 contador = 0;

            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.documento));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_pais", ConstanteUtil.TipoDato.String, filtro.pais));
            parametros.Add(new ParametroPersistenciaGenerico("p_departamento", ConstanteUtil.TipoDato.String, filtro.departamento));
            parametros.Add(new ParametroPersistenciaGenerico("p_provincia", ConstanteUtil.TipoDato.String, filtro.provincia));
            parametros.Add(new ParametroPersistenciaGenerico("p_distrito", ConstanteUtil.TipoDato.String, filtro.distrito));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.Integer, filtro.estado));

            contador = this.contar("covidciudadano.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "covidciudadano.filtroPaginacion");

            while (_reader.Read())
            {
                DtoCiudadano bean = new DtoCiudadano();

                if (!_reader.IsDBNull(0))
                    bean.codigo = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.documento = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nombrecompleto = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.pais = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nompais = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.direccion = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.departamento = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.nomdepartamento = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.provincia = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.nomprovincia = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.distrito = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.nomdistrito = _reader.GetString(11);

                if (!_reader.IsDBNull(12))
                    bean.estado = _reader.GetInt32(12);

                if (!_reader.IsDBNull(13))
                    bean.nomestado = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.edad = _reader.GetInt32(14);

                if (!_reader.IsDBNull(15))
                    bean.nombre = _reader.GetString(15);

                if (!_reader.IsDBNull(16))
                    bean.apellido = _reader.GetString(16);

                if (!_reader.IsDBNull(17))
                    bean.cantidad = _reader.GetInt32(17);

                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public Ciudadano registrar(UsuarioActual usuarioActual, Ciudadano bean)
        {
            bean.IdCiudadano = this.generarCodigo();
            bean.Estado = 6;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<Ciudadano> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdCiudadano).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public Ciudadano actualizar(UsuarioActual usuarioActual, Ciudadano bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public List<Ciudadano> listado(DtoTabla filtro)
        {
            //IQueryable<Ciudadano> query = this.getCriteria();

            //query = query.Where(p => p.Tipo == filtro.valor1);
            //if (filtro.valor1 == "M")
            //{
            //    query = query.Where(p => p.Area == null);
            //}
            //else
            //{
            //    query = query.Where(p => p.Area == filtro.valor2);
            //}
            //query = query.Where(p => p.Estado == "A");
            //query = query.OrderBy(p => p.Nombre);
            //List<Ciudadano> lst = query.ToList();

            //if (lst.Count > 0)
            //    return lst;
            return null;
        }

        public Ciudadano ObtenerPersonaxDNI(string nroDocumento)
        {
      
                IQueryable<Ciudadano> query = this.getCriteria();
                query = query.Where(res => res.NroDocumento == nroDocumento);

                List<Ciudadano> lst = query.ToList();

                if (lst.Count > 0)
                    return lst.FirstOrDefault();
                return null;
            
        }

        public List<DtoTabla> ListarPie()
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            _reader = base.listarPorQuery("covidciudadano.listarpie", parametros);

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
            if (lst.Count > 0)
                return lst;
            return null;
        }

        public List<DtoTabla> listarPiexDepartamento(string pDepa)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoTabla> lst = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("pDepartamento", ConstanteUtil.TipoDato.String, pDepa));

            _reader = base.listarPorQuery("covidciudadano.listarpiexDepartamento", parametros);

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
            if (lst.Count > 0)
                return lst;
            return null;
        }

        public List<DtoTabla> listarPiexProvincia(string pDepa, string pProv)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("pDepartamento", ConstanteUtil.TipoDato.String, pDepa));
            parametros.Add(new ParametroPersistenciaGenerico("pProvincia", ConstanteUtil.TipoDato.String, pProv));

            _reader = base.listarPorQuery("covidciudadano.listarpiexProvincia", parametros);

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
            if (lst.Count > 0)
                return lst;
            return null;
        }

        public List<DtoTabla> listarPiexDistrito(string pDepa, string pProv, string pDist)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();


            parametros.Add(new ParametroPersistenciaGenerico("pDepartamento", ConstanteUtil.TipoDato.String, pDepa));
            parametros.Add(new ParametroPersistenciaGenerico("pProvincia", ConstanteUtil.TipoDato.String, pProv));
            parametros.Add(new ParametroPersistenciaGenerico("pDistrito", ConstanteUtil.TipoDato.String, pDist));

            _reader = base.listarPorQuery("covidciudadano.listarpiexDistrito", parametros);

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
            if (lst.Count > 0)
                return lst;
            return null;
        }
    }
    
}
