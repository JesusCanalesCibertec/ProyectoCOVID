using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmTareaIperCompletadaEjecutar : BpEjecutarServicio
    {
        public void ejecutar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            /*if (bpProcesoConexion.getSalidaIdElemento().equals("FIN"))
            {
                PmTarea tareaPrincipal = pmTareaDao.obtenerPorIdTransaccion(bpTransaccion.getPk().getIdTransaccion());

                PmPretarea pmPretarea = pmPretareaDao.obtenerPorIdTarea(tareaPrincipal.getPk());
                if (pmPretarea != null)
                {
                    if (UString.obtenerValorCadenaSinNulo(pmPretarea.getIdTipoExterno()).equals("HSIPERDETALLE")
                            && !UInteger.esCeroOrNulo(pmPretarea.getIdControl()))
                    {

                        logger.debug("pmPretarea.getIdExterno():" + pmPretarea.getIdExterno());

                        String[] output = pmPretarea.getIdExterno().split("-");

                        Integer idCliente = Integer.valueOf(output[0]);
                        Integer idIper = Integer.valueOf(output[1]);
                        Integer idDetalle = Integer.valueOf(output[2]);

                        HsIper HsIper = hsIperDao.obtenerPorId(new HsIperPk(idCliente, idIper));
                        HsIperDetalle HsIperDetalle = hsIperDetalleDao
                                .obtenerPorId(new HsIperDetallePk(idCliente, idIper, idDetalle));

                        List lst = hsIperDetalleControlDao.listarDetallaControl(idCliente, idIper, idDetalle, "P",
                                pmPretarea.getIdControl());

                        if (lst.size() == 0)
                        {
                            HsIperDetalleControl hsIperDetalleControl = new HsIperDetalleControl();
                            hsIperDetalleControl.getPk().setIdCliente(idCliente);
                            hsIperDetalleControl.getPk().setIdIper(idIper);
                            hsIperDetalleControl.getPk().setIdDetalle(idDetalle);
                            hsIperDetalleControl.getPk().setIdTipoIper("P");
                            hsIperDetalleControl.getPk().setIdControl(pmPretarea.getIdControl());
                            hsIperDetalleControl.getPk().setIdVersion(HsIper.getIdVersion());
                            hsIperDetalleControl.setEstado("A");
                            hsIperDetalleControl.setCreacionFecha(new Date());
                            hsIperDetalleControl.setCreacionTerminal(usuarioActual.getUsuarioIpAddress());
                            hsIperDetalleControl.setCreacionUsuario(usuarioActual.getUsuarioLogin());
                            hsIperDetalleControlDao.registrar(hsIperDetalleControl);
                        }
                    }
                }

            }*/
        }
    }
}
