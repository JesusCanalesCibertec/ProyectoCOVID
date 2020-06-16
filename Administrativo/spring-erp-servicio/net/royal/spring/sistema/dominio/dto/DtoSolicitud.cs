using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoSolicitud
    {
        internal string accion;

        public String servicio { get; set; }

        public String FlgSeleccionado { get; set; }

        public Int32? Secuencia { get; set; }

        public String AplicacionCodigo { get; set; }
        public String AplicacionNombre { get; set; }

        public String ProcesoCodigo { get; set; }
        public String ProcesoNombre { get; set; }
        public Int32? ProcesoNro { get; set; }

        public Int32? NivelActual { get; set; }
        public Int32? NivelAprobar { get; set; }

        public String DocumentoReferencia { get; set; }
        public DateTime DocumentoFecha { get; set; }

        public String CompaniaCodigo { get; set; }
        public String CompaniaNombre { get; set; }

        public String UnidadReplicacionCodigo { get; set; }
        public String UnidadReplicacionNombre { get; set; }

        public Int32? SolicitanteId { get; set; }
        public String SolicitanteNombre { get; set; }

        public String EstadoCodigo { get; set; }
        public String EstadoNombre { get; set; }

        public Int32? ProcesoAprobar { get; set; }
        public Int32? ProcesoInternoAprobar { get; set; }
        public Int32? DiasHabilies { get; set; }
        public Int32? tipoDia { get; set; }
        public String Llave { get; set; }
        public String Observaciones { get; set; }
        public String Capacitaciones { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public String Uuid { get; set; }

        public String FlgSolicitarInformacionPrestamo { get; set; }
        public String PrestamoOtorgadoFlag { get; set; }
        public String PrestamoMonedaPago { get; set; }
        public String PrestamoTipoPago { get; set; }
        public String PrestamoNumeroRecibo { get; set; }
        public Int32? PrestamoNumeroCheque { get; set; }
        public String PrestamoCuentaBancaria { get; set; }

        /**
         * Indica si el usuario que esta consultando es SUPER-USUARIO
         */
        public String FlgEsSuperUsuario { get; set; }

        /**
         * Indica si el usuario que esta consultando es ADMINISTRADOR
         */
        public String FlgEsAdministrador { get; set; }

        /**
         * si el usuario es ADMINISTRADOR, se le pregunta si Aprobara todos los
         * niveles
         */
        public String FlgAdministradorApruebaTodo { get; set; }

        /**
         * indica si el proceso tiene Correos a enviar
         */
        public String FlgTieneCorreos { get; set; }

        /**
         * si el proceso no tiene correos se le pregunta al usuario si procesara la
         * informacion
         */
        public String FlgEnviarSinCorreos { get; set; }

        /**
         * es obligatorio cuando se esta rechazanda una Solicitud pedir Observaciones
         * informacion
         */
        public String FlgSolicitarObservaciones { get; set; }


        public String ObservacionAccion { get; set; }

        public Int32? idTransaccion { get; set; }
        public String Inicio { get; set; }
        public String Fin { get; set; }
        public String Concepto { get; set; }
        public String Desde { get; set; }
        public String Hasta { get; set; }
        public String Autorizado { get; set; }
        public String Aprobar { get; set; }
    }
}
