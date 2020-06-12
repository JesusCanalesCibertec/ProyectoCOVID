using System;
using System.Collections.Generic;
using System.Text;


namespace net.royal.spring.framework.core
{
    public class UDecimal
    {
        public static string obtenerMonto2Decimales(Nullable<decimal> monto)
        {
            if (!monto.HasValue)
            {
                return "";
            }
            return String.Format("{0:#,###,##0.00}", monto);
        }
        public static string obtenerMonto2DecimalesDef0(Nullable<decimal> monto)
        {
            if (!monto.HasValue)
            {
                monto = 0;
            }
            return obtenerMonto2Decimales(monto);
        }
    }
}
