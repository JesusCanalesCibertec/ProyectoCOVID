using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio.dto {
    public class DtoTabla {
        private String _Codigo;

        public String Codigo {
            get { return (UString.estaVacio(_Codigo)) ? null : _Codigo.Trim(); }
            set { _Codigo = value; }
        }



        public DtoTabla() {
            paginacion = new ParametroPaginacionGenerico();
        }

        public DtoTabla(String Codigo, String Nombre) {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
        }

        public DtoTabla(Nullable<Int32> CodigoNumerico) {
            this.CodigoNumerico = CodigoNumerico;
        }

        public Nullable<Int32> CodigoNumerico { get; set; }
        public String Nombre { get; set; }
        public String Estado { get; set; }
        public String Descripcion { get; set; }
        public Int32? Id { get; set; }
        public int? value { get; set; }
        public Nullable<Int32> Secuencia { get; set; }
        public string label { get; set; }
        public bool internalValue { get; set; }
        public String base64 { get; set; }

        public String Explicacion { get; set; }
        public Nullable<Decimal> Porcentaje { get; set; }
        public Nullable<Decimal> valorNumerico { get; set; }
        public String valorFlag { get; set; }

        public String IdTipoAyuda { get; set; }
        public String IdResultado { get; set; }
        public Double? IdResultadoNumerico { get; set; }

        public String IdSubCondicion { get; set; }

        public String valor1 { get; set; }
        public String valor2 { get; set; }
        public String valor3 { get; set; }

        public Int32? num1 { get; set; }
        public Int32? num2 { get; set; }
        public Int32? num3 { get; set; }

        public Int32? num4 { get; set; }
        public Int32? num5 { get; set; }
        public Int32? num6 { get; set; }

        public Int32? num7 { get; set; }
        public Int32? num8 { get; set; }
        public Int32? num9 { get; set; }

        public String dato1 { get; set; }
        public String dato2 { get; set; }

        public Nullable<DateTime> fechainicio1 { get; set; }
        public Nullable<DateTime> fechafin1 { get; set; }

        public Nullable<DateTime> fechainicio2 { get; set; }
        public Nullable<DateTime> fechafin2 { get; set; }


        public ParametroPaginacionGenerico paginacion { get; set; }
        public static List<DtoTabla> obtenerLista(String[] listaCodigos) {
            List<DtoTabla> lst = new List<DtoTabla>();
            if (listaCodigos == null)
                return lst;
            for (int i = 0; i < listaCodigos.Length; i++) {
                if (!UString.estaVacio(listaCodigos[i])) {
                    lst.Add(new DtoTabla(listaCodigos[i], null));
                }

            }
            return lst;
        }

    }
}
