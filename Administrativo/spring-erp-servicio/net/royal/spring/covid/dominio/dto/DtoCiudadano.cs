
using System;


namespace net.royal.spring.covid.dominio.filtro
{
    public class DtoCiudadano
    {
        public int codigo { get; set; }
        public String documento { get; set; }
        public String nombrecompleto { get; set; }
        public String pais { get; set; }
        public String nompais { get; set; }
        public String direccion { get; set; }
        public String departamento { get; set; }
        public String nomdepartamento { get; set; }
        public String provincia { get; set; }
        public String nomprovincia { get; set; }
        public String distrito { get; set; }
        public String nomdistrito { get; set; }
        public Nullable<Int32> estado { get; set; }
        public String nomestado { get; set; }
    }
}
