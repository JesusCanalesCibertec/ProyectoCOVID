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

    public class TriajeDaoImpl: GenericoDaoImpl<Triaje>, TriajeDao
    {

        private IServiceProvider servicioProveedor;

        public TriajeDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "covidtriaje")
        {
            servicioProveedor = _servicioProveedor;
        }

        //public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroTriaje filtro)
        //{

        //    if (!UString.estaVacio(filtro.documento))
        //    {
        //        if (filtro.documento.Length < 8)
        //            filtro.documento = null;
        //    }
                
        //    List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
        //    List<DtoTriaje> lstResultado = new List<DtoTriaje>();

        //    Int32 contador = 0;

        //    parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.documento));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_pais", ConstanteUtil.TipoDato.String, filtro.pais));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_departamento", ConstanteUtil.TipoDato.String, filtro.departamento));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_provincia", ConstanteUtil.TipoDato.String, filtro.provincia));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_distrito", ConstanteUtil.TipoDato.String, filtro.distrito));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.Integer, filtro.estado));

        //    contador = this.contar("Triaje.filtroContar", parametros);

        //    _reader = this.listarConPaginacion(paginacion,parametros, "Triaje.filtroPaginacion");

        //    while (_reader.Read())
        //    {
        //        DtoTriaje bean = new DtoTriaje();

        //        if (!_reader.IsDBNull(0))
        //            bean.codigo = _reader.GetInt32(0);

        //        if (!_reader.IsDBNull(1))
        //            bean.documento = _reader.GetString(1);

        //        if (!_reader.IsDBNull(2))
        //            bean.nombrecompleto = _reader.GetString(2);

        //        if (!_reader.IsDBNull(3))
        //            bean.pais = _reader.GetString(3);

        //        if (!_reader.IsDBNull(4))
        //            bean.nompais = _reader.GetString(4);

        //        if (!_reader.IsDBNull(5))
        //            bean.direccion = _reader.GetString(5);

        //        if (!_reader.IsDBNull(6))
        //            bean.departamento = _reader.GetString(6);

        //        if (!_reader.IsDBNull(7))
        //            bean.nomdepartamento = _reader.GetString(7);

        //        if (!_reader.IsDBNull(8))
        //            bean.provincia = _reader.GetString(8);

        //        if (!_reader.IsDBNull(9))
        //            bean.nomprovincia = _reader.GetString(9);

        //        if (!_reader.IsDBNull(10))
        //            bean.distrito = _reader.GetString(10);

        //        if (!_reader.IsDBNull(11))
        //            bean.nomdistrito = _reader.GetString(11);

        //        if (!_reader.IsDBNull(12))
        //            bean.estado = _reader.GetInt32(12);

        //        if (!_reader.IsDBNull(13))
        //            bean.nomestado = _reader.GetString(13);

        //        lstResultado.Add(bean);
        //    }

        //    this.dispose();

        //    paginacion.ListaResultado = lstResultado;
        //    paginacion.RegistroEncontrados = contador;

        //    return paginacion;
        //}

        public Triaje registrar(UsuarioActual usuarioActual, Triaje bean)
        {
            bean.IdTriaje = this.generarCodigo();
            bean.Fecharegistro = DateTime.Now;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<Triaje> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdTriaje).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public Triaje actualizar(UsuarioActual usuarioActual, Triaje bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public List<Triaje> listado(DtoTabla filtro)
        {
            //IQueryable<Triaje> query = this.getCriteria();

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
            //List<Triaje> lst = query.ToList();

            //if (lst.Count > 0)
            //    return lst;
            return null;
        }

        public List<DtoTabla> listado(int pIdCiudadano)
        {
            List<DtoTabla> lst = new List<DtoTabla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("pIdCiudadano", ConstanteUtil.TipoDato.Integer, pIdCiudadano));


            _reader = this.listarPorQuery("covidtriaje.listarporciudadano", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Secuencia = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.num1 = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.num2 = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.num3 = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.CodigoNumerico = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.fechainicio1 = _reader.GetDateTime(5);
                if (!_reader.IsDBNull(6))
                    bean.valor1 = _reader.GetString(6);

                lst.Add(bean);
            }
            this.dispose();

            if(lst.Count > 0)
                return lst;

            return null;
        }
    }
    
}
