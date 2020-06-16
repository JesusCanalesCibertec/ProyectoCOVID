using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proyecto.dominio.dto
{
    public class DtoTarea
    {
        public Int32 idProyecto { get; set; }
        public Int32 idTarea { get; set; }
        public Nullable<DateTime> fechaRegistro { get; set; }
        public Nullable<DateTime> fechaUltimaActualizacion { get; set; }
        public Nullable<DateTime> realFechaInicio { get; set; }
        public Nullable<DateTime> realFechaFin { get; set; }

        public Nullable<DateTime> programacionFechaInicio { get; set; }
        public Nullable<DateTime> programacionFechaFin { get; set; }

        public Int32 realDuracion { get; set; }
        public Int32 realTrabajo { get; set; }
        public Int32 realTrabajoHorasExtra { get; set; }
        public Int32 realCosto { get; set; }
        public Int32 realCostoHorasExtra { get; set; }
        public Int32 realCrtr { get; set; }
        public Int32 realCompletado { get; set; }
        public String idTipoResponsable { get; set; }
        public Int32 idResponsable { get; set; }
        public Int32 idSolicitante { get; set; }
        public String flgHito { get; set; }
        public String flgSobreasignado { get; set; }
        public String flgRepetitiva { get; set; }
        public Int32 esquemaNivel { get; set; }
        public Int32 esquemaNumero { get; set; }
        public Int32 idProceso { get; set; }
        public String flgTareaExterna { get; set; }
        public String idTipoTareaExterno { get; set; }
        public String idTareaExterno { get; set; }
        public String idTipo1 { get; set; }
        public String idTipo2 { get; set; }
        public String idTipo3 { get; set; }
        public String idTipo4 { get; set; }
        public Int32 idTareaPadre { get; set; }
        public String estado { get; set; }
        public String estado2 { get; set; }
        public String creacionUsuario { get; set; }
        public Nullable<DateTime> creacionFecha { get; set; }
        public String creacionTerminal { get; set; }
        public String modificacionUsuario { get; set; }
        public Nullable<DateTime> modificacionFecha { get; set; }
        public String modificacionTerminal { get; set; }

        //
        public String nombreProyecto { get; set; }
        public String nombreTarea { get; set; }
        public String descripcionTarea { get; set; }
        public String responsableNombre { get; set; }
        public String nombreProceso { get; set; }
        public String estadodescripcion { get; set; }
        public String estado2descripcion { get; set; }
        public String idTipo1descripcion { get; set; }
        public String idTipo2descripcion { get; set; }
        public String idTipo3descripcion { get; set; }
        public String estadoTransaccion { get; set; }
        public String estadoTransaccionDes { get; set; }
        public String elementoNombre { get; set; }
        public Int32 idTransaccion { get; set; }
        public Int32 idRegistro { get; set; }


        public String flagGeneraCompromiso { get; set; }
        public String estadopermiso { get; set; }
        public String color { get; set; }
        public Int32? permiso { get; set; }
    }
}
