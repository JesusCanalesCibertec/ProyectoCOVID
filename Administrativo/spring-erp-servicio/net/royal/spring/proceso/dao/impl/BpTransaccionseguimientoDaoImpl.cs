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
using net.royal.spring.proceso.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpTransaccionseguimientoDaoImpl : GenericoDaoImpl<BpTransaccionseguimiento>, BpTransaccionseguimientoDao
    {
        private IServiceProvider servicioProveedor;

        public BpTransaccionseguimientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bptransaccionseguimiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpTransaccionseguimiento> listarTransaccionSeguimiento(int idTransaccion)
        {
            IQueryable<BpTransaccionseguimiento> query = this.getCriteria();
            query = query.Where(x => x.IdTransaccion == idTransaccion);
            query = query.OrderByDescending(x => x.IdSeguimiento);
            return query.ToList();
        }

        public BpTransaccionseguimiento registrar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, string comentarios, BpProcesoconexion bpProcesoConexion)
        {
            BpTransaccionseguimiento transaccionSeguimiento = new BpTransaccionseguimiento();

            Int32 idSeguimiento = this.generarIdSeguimiento(bpTransaccion.IdTransaccion.Value);

            transaccionSeguimiento.IdTransaccion = bpTransaccion.IdTransaccion;
            transaccionSeguimiento.IdSeguimiento = idSeguimiento;

            if (!UString.estaVacio(bpProcesoConexion.EntradaIdElemento))
            {
                transaccionSeguimiento.EntradaIdProceso = bpProcesoConexion.EntradaIdProceso;
                transaccionSeguimiento.EntradaIdElemento = bpProcesoConexion.EntradaIdElemento;
            }
            if (!UString.estaVacio(bpProcesoConexion.SalidaIdElemento))
            {
                transaccionSeguimiento.SalidaIdProceso = bpProcesoConexion.SalidaIdProceso;
                transaccionSeguimiento.SalidaIdElemento = bpProcesoConexion.SalidaIdElemento;
            }
            transaccionSeguimiento.FechaEvento = DateTime.Now;

            if (UString.estaVacio(comentarios))
            {
                comentarios = bpProcesoConexion.Accion;
                transaccionSeguimiento.Comentarios = comentarios;
            }
            else
            {
                transaccionSeguimiento.Comentarios = comentarios;
            }

            transaccionSeguimiento.IdPersona = usuarioActual.PersonaId;
            transaccionSeguimiento.NombreCompleto = usuarioActual.PersonaNombreCompleto;
            transaccionSeguimiento.Cargo = usuarioActual.PuestoEmpresaNombre;
            transaccionSeguimiento.Usuario = usuarioActual.UsuarioLogin;

            transaccionSeguimiento.EstadoIdProceso = bpTransaccion.IdProceso;
            transaccionSeguimiento.EstadoId = bpTransaccion.IdEstado;

            transaccionSeguimiento.CreacionFecha = DateTime.Now;
            transaccionSeguimiento.CreacionUsuario = usuarioActual.UsuarioLogin;
            registrar(transaccionSeguimiento);
            return transaccionSeguimiento;
        }

        public Int32 generarIdSeguimiento(Int32 idTransaccion)
        {
            IQueryable<BpTransaccionseguimiento> query = this.getCriteria();
            query = query.Where(p => p.IdTransaccion == idTransaccion);
            Int32? contador = query.Select(p => p.IdSeguimiento).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }
    }
}
