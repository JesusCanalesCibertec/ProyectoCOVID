using net.royal.spring.framework.constante;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio
{
    public class ParametroListaGenerico : ArrayList
    {
        public void parametroRemover(String parametro)
        {            
            if (this.Count == 0)
                return;

            for (int index = 0; index < this.Count; index++)
            {
                ParametroPersistenciaGenerico bean;
                bean = (ParametroPersistenciaGenerico)this[index];
                if (bean != null)
                {
                    if (bean.Campo.Equals(parametro))
                        this.Remove(index);
                }
            }
        }

        public void parametroAgregar(String pfield, ConstanteUtil.TipoDato pclazz, Object pvalue)
        {
            this.parametroRemover(pfield);
            ParametroPersistenciaGenerico e = null;
            e = new ParametroPersistenciaGenerico(pfield, pclazz, pvalue);
            this.Add(e);
        }

        public void parametroAgregarLista(List<ParametroPersistenciaGenerico> lista)
        {
            if (lista == null)
                return;
            foreach (ParametroPersistenciaGenerico bean in lista)
            {
                this.parametroAgregar(bean.Campo, bean.Clase, bean.Valor);
            }
        }

        public String parametroObtenerString(String parametro)
        {
            String retorno = null;

            if (this.Count == 0)
                return null;

            for (int index = 0; index < this.Count; index++)
            {
                ParametroPersistenciaGenerico bean;
                bean = (ParametroPersistenciaGenerico)this[index];
                if (bean != null)
                {
                    if (bean.Campo.Equals(parametro))
                        return (String)bean.Valor;
                }
            }
            return retorno;
        }
    }
}
