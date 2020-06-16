using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.dominio.dto
{
    public class DtoNivelDetalle: DtoTablaEntero
    {
        public List<SyAprobacionnivelesDet> ListaDetalle { get; set; }

        public DtoNivelDetalle()
        {
        }

        public DtoNivelDetalle(int codigo, String nombre)
        {
            this.codigo=codigo;
            this.nombre=nombre;
        }

    }
}
