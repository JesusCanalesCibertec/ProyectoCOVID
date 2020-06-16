using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{
public class HrEncuestapreguntavaloresDaoImpl : GenericoDaoImpl<HrEncuestapreguntavalores>, HrEncuestapreguntavaloresDao
    {        private IServiceProvider servicioProveedor;


        public HrEncuestapreguntavaloresDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuestapreguntavalores")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestapreguntavalores obtenerPorId(HrEncuestapreguntavaloresPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuestapreguntavalores obtenerPorId(Nullable<Int32> pPregunta, Nullable<Int32> pValor)
        {
            return obtenerPorId(new HrEncuestapreguntavaloresPk(pPregunta, pValor));
        }

        public HrEncuestapreguntavalores coreInsertar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEncuestapreguntavalores coreActualizar(UsuarioActual usuarioActual, HrEncuestapreguntavalores bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPregunta, Nullable<Int32> pValor)
        {
            coreEliminar(new HrEncuestapreguntavaloresPk(pPregunta, pValor));
        }

        public void coreEliminar(HrEncuestapreguntavaloresPk id)
        {
            HrEncuestapreguntavalores bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuestapreguntavalores coreAnular(UsuarioActual usuarioActual, HrEncuestapreguntavaloresPk id)
        {
            HrEncuestapreguntavalores bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrEncuestapreguntavalores coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPregunta, Nullable<Int32> pValor)
        {
            return coreAnular(usuarioActual, new HrEncuestapreguntavaloresPk(pPregunta, pValor));
        }

       public IList<DtoTabla> obtenerValores(HrEncuestapreguntaPk hrEncuestapreguntaPk)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lstRegistros = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("p_pregunta", ConstanteUtil.TipoDato.Integer, hrEncuestapreguntaPk.Pregunta));

            _reader = this.listarPorQuery("hrencuestapreguntavalores.obtenerPreguntas", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.value = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.label = _reader.GetString(1);

                lstRegistros.Add(bean);
            }
            this.dispose();

            return lstRegistros;
        }
        public List<HrEncuestapreguntavalores> listarValores(int? numero)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<HrEncuestapreguntavalores> lst = new List<HrEncuestapreguntavalores>();

           
            _parametros.Add(new ParametroPersistenciaGenerico("p_pregunta", ConstanteUtil.TipoDato.Integer, numero));
            

            _reader = this.listarPorQuery("hrencuestapreguntavalores.filtro", _parametros);

            while (_reader.Read())
            {
                HrEncuestapreguntavalores bean = new HrEncuestapreguntavalores();

                if (!_reader.IsDBNull(0))
                    bean.Pregunta = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Valor = _reader.GetInt32(1);

                if (!_reader.IsDBNull(2))
                    bean.Descripcion = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Ultimousuario = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Ultimafechamodif = _reader.GetDateTime(4);

                if (!_reader.IsDBNull(5))
                    bean.Peso = _reader.GetDecimal(5);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

        public void EliminarDetalle(int? pregunta)
        {
            var list = listarValores(pregunta);

            foreach (var item in list)
            {
                coreEliminar(item);
            }
            

        }
       


}
}
