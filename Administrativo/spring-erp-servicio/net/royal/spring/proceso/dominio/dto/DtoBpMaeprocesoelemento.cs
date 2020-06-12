using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.proceso.dominio.dto
{
    public class DtoBpMaeprocesoelemento
    {
        public String idProceso { get; set; }
        public String nomProceso { get; set; }
        public String idElemento { get; set; }
        public String idUnico { get; set; }
        public String nomElemento { get; set; }
        public String idTipoElemento { get; set; }
        public String nomTipoElemento { get; set; }
        public String idEstado { get; set; }
        public String nomEstado { get; set; }
        public String idRol { get; set; }
        public String nomRol { get; set; }
        public String idNivelSeg { get; set; }
        public String nomNivelSeg { get; set; }
        public String Icono { get; set; }
        public Int32? Avance { get; set; }
        public Int32? Dias { get; set; }
        public String IdColor { get; set; }
        public String NomColor { get; set; }
        public String RutaIcono { get; set; }
        public Int32? PosX { get; set; }
        public Int32? PosY { get; set; }
      

    }
}
