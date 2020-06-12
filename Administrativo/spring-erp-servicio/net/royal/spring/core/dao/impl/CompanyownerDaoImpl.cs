using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.dao.impl
{
    public class CompanyownerDaoImpl : GenericoDaoImpl<Companyowner>, CompanyownerDao
    {
        private IServiceProvider servicioProveedor;


        public CompanyownerDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "companyowner")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<DtoTabla> listarActivos()
        {
            List<DtoTabla> lstRetorno = new List<DtoTabla>();
            DtoTabla bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            _reader = this.listarPorQuery("companyowner.listarActivos", parametros);
            while (_reader.Read())
            {
                bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<DtoTabla> listarCompaniasPorSeguridad(string usuario)
        {
            List<DtoTabla> lstRetorno = new List<DtoTabla>();
            DtoTabla bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, usuario));

            _reader = this.listarPorQuery("companyowner.listarCompaniaPorSeguridad", parametros);
            while (_reader.Read())
            {
                bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

                lstRetorno.Add(bean);
            }
            this.dispose();

            return lstRetorno;
        }

        public List<Companyowner> listarTodos()
        {
            IQueryable<Companyowner> compa = getCriteria();
            return compa.ToList();
        }

        public string obtenerNombre(string companyowner)
        {
            CompaniamastPk pk = new CompaniamastPk() { Companiacodigo = companyowner };
            var c = obtenerPorId(pk.obtenerArreglo());
            return c == null ? null : c.Description;
        }

        public Companyowner obtenerPorId(CompanyownerPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
