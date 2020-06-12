using Microsoft.Extensions.Configuration;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace net.royal.spring.framework.web.dao.impl
{
    public class GenericoBase
    {
        public static XmlNodeList obtenerSentenciaXml(string nombreAlias, string nombreQuery)
        {
            string nombreArchivo = nombreAlias + ".sql.hbm.xml";
            string ruta = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = null;
            settings.DtdProcessing = DtdProcessing.Parse;

            ruta = Directory.GetCurrentDirectory();
            ruta = ruta + @"\sql\" + nombreArchivo;

            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            return doc.GetElementsByTagName("sql-query");            
        }

        public static String obtenerSentencia(XmlNodeList elemList, string nombreQuery) {
            String sentenciaSql = null;
            foreach (XmlNode isbn in elemList)
            {
                if (isbn.Attributes[0].Value == nombreQuery) {
                    sentenciaSql = isbn.InnerText;
                }                
            }
            return sentenciaSql;
        }

        public static String getNamedQueryByPatametersSet(IConfigurationRoot _configuration,
            String _sentenciaSql, List<ParametroPersistenciaGenerico> _parametros)
        {
            String temporal = null;
            String formatoFecha = "yyyy-MM-dd";
            String formatoFechaHora = "yyyy-MM-dd HH:mm:ss";

            if (_parametros == null)
                return _sentenciaSql;

            temporal = _configuration.GetSection("Entorno").GetSection("formato_bd_fecha").Value;
            if (temporal != null)
                formatoFecha = temporal;
            temporal = _configuration.GetSection("Entorno").GetSection("formato_bd_fecha_hora").Value;
            if (temporal != null)
                formatoFechaHora = temporal;

            foreach (ParametroPersistenciaGenerico parametro in _parametros) {
                if (parametro.Clase == ConstanteUtil.TipoDato.Date)
                {

                    if (parametro.Valor != null)
                    {
                        DateTime _fecha = (DateTime)parametro.Valor;
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "'" + UFechaHora.convertirFechaCadena(_fecha, formatoFecha) + "'");
                    }
                    else {
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                    }
                        
                }
                if (parametro.Clase == ConstanteUtil.TipoDato.DateTime)
                {                    
                    if (parametro.Valor != null)
                    {
                        DateTime _fechaHora = (DateTime)parametro.Valor;
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo,"'" + UFechaHora.convertirFechaCadena(_fechaHora, formatoFechaHora) + "'");
                    }
                    else {
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                    }
                        
                }
                if (parametro.Clase == ConstanteUtil.TipoDato.String) {
                    if (parametro.Valor != null)
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "'"+(String)parametro.Valor+"'");
                    else
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                }
                if (parametro.Clase == ConstanteUtil.TipoDato.Integer)
                {
                    if (parametro.Valor != null) {
                        Int32 valint32 = (Int32)parametro.Valor;
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, valint32.ToString());
                    }                        
                    else{
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                    }
                        
                }
                if (parametro.Clase == ConstanteUtil.TipoDato.Decimal)
                {
                    if (parametro.Valor != null) {
                        Decimal valDecimal = (Decimal)parametro.Valor;
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, valDecimal.ToString());
                    }                        
                    else{
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                    }
                        
                }

                if (parametro.Clase == ConstanteUtil.TipoDato.ArrayInteger) {
                    if (parametro.Valor != null) {
                        //String[] arrayInteger = (String[])parametro.Valor;
                        var arrayInteger = parametro.Valor.ToString().Split(',');
                        String array = String.Join(",", arrayInteger);
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, array);
                    }
                    else {
                        _sentenciaSql = _sentenciaSql.Replace(":" + parametro.Campo, "null");
                    }

                }
            }

            return _sentenciaSql;
        }
    }
}
