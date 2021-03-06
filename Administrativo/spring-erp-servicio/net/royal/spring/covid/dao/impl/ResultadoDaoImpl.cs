﻿using net.royal.spring.framework.constante;
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

    public class ResultadoDaoImpl: GenericoDaoImpl<Resultado>, ResultadoDao
    {

        private IServiceProvider servicioProveedor;

        public ResultadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "resultado")
        {
            servicioProveedor = _servicioProveedor;
        }

        //public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroResultado filtro)
        //{

        //    if (!UString.estaVacio(filtro.documento))
        //    {
        //        if (filtro.documento.Length < 8)
        //            filtro.documento = null;
        //    }
                
        //    List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
        //    List<DtoResultado> lstResultado = new List<DtoResultado>();

        //    Int32 contador = 0;

        //    parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.documento));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_pais", ConstanteUtil.TipoDato.String, filtro.pais));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_departamento", ConstanteUtil.TipoDato.String, filtro.departamento));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_provincia", ConstanteUtil.TipoDato.String, filtro.provincia));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_distrito", ConstanteUtil.TipoDato.String, filtro.distrito));
        //    parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.Integer, filtro.estado));

        //    contador = this.contar("Resultado.filtroContar", parametros);

        //    _reader = this.listarConPaginacion(paginacion,parametros, "Resultado.filtroPaginacion");

        //    while (_reader.Read())
        //    {
        //        DtoResultado bean = new DtoResultado();

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

        public Resultado registrar(UsuarioActual usuarioActual, Resultado bean)
        {
            bean.IdResultado = this.generarCodigo();
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<Resultado> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdResultado).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public Resultado actualizar(UsuarioActual usuarioActual, Resultado bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public List<Resultado> listado(DtoTabla filtro)
        {
            //IQueryable<Resultado> query = this.getCriteria();

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
            //List<Resultado> lst = query.ToList();

            //if (lst.Count > 0)
            //    return lst;
            return null;
        }
    }
    
}
