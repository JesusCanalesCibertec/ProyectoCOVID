using net.royal.spring.programasocial.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoActividadesPreventivas
    {
        public String codResultado { get; set; }
        public String nombre { get; set; }
        public String tipo { get; set; }
        public Nullable<Int32> numActividades { get; set; }
        public Nullable<Int32> asistencia { get; set; }
        public Nullable<Decimal> numActividades_porc { get; set; }
        public Nullable<Decimal> asistencia_porc { get; set; }


        /**RES08 */
        public String diagnostico { get; set; }
        public Nullable<Int32> atencion { get; set; }
        public Nullable<Decimal> prevalencia { get; set; }
        /**RES08 */

        /**RES11 */
        public String nomInstitucion { get; set; }
        public String programa { get; set; }
        /**RES11 */

        /**RES10 */
        public Nullable<Int32> cantidad { get; set; }
        public Nullable<Decimal> porcentaje { get; set; }
        public String equipo { get; set; }
        /**RES10 */



    }
}
