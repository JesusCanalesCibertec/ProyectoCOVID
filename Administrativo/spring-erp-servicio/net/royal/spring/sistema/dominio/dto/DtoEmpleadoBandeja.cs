using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoEmpleadoBandeja
    {
        public Int32? PersonaId { get; set; }
        public String PersonaNombre { get; set; }
        public String PersonaNroDocumento { get; set; }
        public String CentroCostosId { get; set; }
        public String CentroCostosNombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public String FechaNacimientoDescripcion { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public String FechaIngresoDescripcion { get; set; }
        public String CorreoInterno { get; set; }
        public String Anexo { get; set; }
        public byte[] ArchivoFotoByte { get; set; }
        public String ArchivoFotoCadena { get; set; }

        public Int32 Dia { get; set; }

        public String Mes { get; set; }
    }
}
