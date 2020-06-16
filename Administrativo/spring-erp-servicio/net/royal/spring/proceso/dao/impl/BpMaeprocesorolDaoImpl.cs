using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpMaeprocesorolDaoImpl : GenericoDaoImpl<BpMaeprocesorol>, BpMaeprocesorolDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesorolDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesorol")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpMaeprocesorol> listado(string codigo)
        {
           
            IQueryable<BpMaeprocesorol> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == codigo);

            List<BpMaeprocesorol> lst = query.ToList();
            if (lst.Count > 0)
                return lst;
            return null;
        }

       

        public string obtenercadena(string pIdProceso, string pNombre)
        {
            IQueryable<BpMaeprocesorol> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == pIdProceso && p.Nombre == pNombre);

            List<BpMaeprocesorol> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdProceso, string pIdRol)
        {
            IQueryable<BpMaeprocesorol> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdRol == pIdRol
                );

            List<BpMaeprocesorol> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdRol;
            return null;
        }


        /*
       public List<BpMaeprocesorol> listado()
       {
           List<BpMaeprocesorol> lst = new List<BpMaeprocesorol>();

           _reader = this.listarPorQuery("bpmaeprocesorol.listado");

           while (_reader.Read())
           {
               BpMaeprocesorol bean = new BpMaeprocesorol();

               if (!_reader.IsDBNull(0))
                   bean.IdRol = _reader.GetString(0);

               if (!_reader.IsDBNull(1))
                   bean.IdUnico = _reader.GetString(1);

               if (!_reader.IsDBNull(2))
                   bean.Nombre = _reader.GetString(2);

               if (!_reader.IsDBNull(3))
                   bean.Alto = _reader.GetInt32(3);

               if (!_reader.IsDBNull(4))
                   bean.IdTipoSeguridad = _reader.GetString(4);


               lst.Add(bean);
           }
           this.dispose();
           return lst;
       }*/

    }
}
