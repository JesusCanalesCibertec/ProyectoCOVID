using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao.impl
{
    public class PersonamastDaoImpl : GenericoDaoImpl<Personamast>, PersonamastDao
    {
        private IServiceProvider servicioProveedor;


        public PersonamastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "personamast")
        {
            servicioProveedor = _servicioProveedor;
        }

        public DtoTabla obtenerNombrePorJefeUnidadOperativa(string unidadoperativa)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_unidad", framework.constante.ConstanteUtil.TipoDato.String, unidadoperativa));
            _reader = this.listarPorQuery("personamast.obtenerNombrePorJefeUnidadOperativa", parametros);
            DtoTabla nombre = new DtoTabla() { Id = 0, Descripcion = "" };
            if (_reader.Read())
            {
                nombre.Id = _reader.GetInt32(0);
                nombre.Descripcion = _reader.GetString(1);
            }

            this.dispose();

            return nombre;
        }

        public bool esJefePorUnidadOperativa(int? personaId)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_empleado", ConstanteUtil.TipoDato.Integer, personaId));

            Int32 contador = this.contar("personamast.esJefePorUnidadOperativa", parametros);

            this.dispose();

            return contador > 0 ? true : false;
        }

        public List<Personamast> listarPorCorreoElectronico(string codigousuario)
        {
            IQueryable<Personamast> query = this.getCriteria();
            query = query.Where(p => p.Correoelectronico == codigousuario);
            return query.ToList();
        }

        public string obtenerNombrePorPk(PersonamastPk pk)
        {
            Personamast personamast = obtenerPorId(pk);
            if (personamast == null)
            {
                return null;
            }
            return personamast.Nombrecompleto;
        }

        public List<Personamast> obtenerPorCorreoElectronico(string correoElectronicoEnviado)
        {
            IQueryable<Personamast> query = this.getCriteria();
            query = query.Where(p => p.Correoelectronico == correoElectronicoEnviado);
            List<Personamast> lst = query.ToList();
            return lst;
        }

        public Personamast obtenerPorId(PersonamastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
