using net.royal.spring.framework.core.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto
{
    public class DtoEncuesta
    {

        public Nullable<Int32> secuencia { get; set; }
        public Nullable<Int32> Sujeto { get; set; }
        public Nullable<Int32> Pregunta { get; set; }
        public String Descripcion { get; set; }
        public String DescripcionValor { get; set; }
        public String Valor { get; set; }
        public Nullable<Decimal> Peso { get; set; }


        public List<DtoGrupo> grupos { get; set; }
        
        public String periodo { get; set; }
        public String titulo { get; set; }
        public Nullable<Int32> muestras { get; set; }
        public String muestrasDescripcion { get; set; }
        public String estadoDescripcion { get; set; }
        public String tipoDescripcion { get; set; }
        public String tipoGuia { get; set; }
        public String afeNombre { get; set; }
        public String afe { get; set; }
        public int finalizada { get; set; }


        public bool registrar { get; set; }
        public DateTime? fecha { get; set; }
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }

        public string compania { get; set; }
        public string observaciones { get; set; }
        public string instrucciones { get; set; }

        public IList<DtoArea> areas { get; set; }

        public String sexo { get; set; }
        public String edad { get; set; }
        public Int32? numero { get; set; }

        public String titulo1 { get; set; }
        public String titulo2 { get; set; }
        public String titulo3 { get; set; }
        public String titulo4 { get; set; }

        public String miscelaneo1 { get; set; }
        public String miscelaneo2 { get; set; }
        public String miscelaneo3 { get; set; }
        public String miscelaneo4 { get; set; }

        public List<SelectItem> listaMiscelaneo1 { get; set; }
        public List<SelectItem> listaMiscelaneo2 { get; set; }
        public List<SelectItem> listaMiscelaneo3 { get; set; }
        public List<SelectItem> listaMiscelaneo4 { get; set; }

        public String institucion { get; set; }
        public String idInstitucionArea { get; set; }
        public String idPrograma { get; set; }
        public String idComponente { get; set; }

        public DtoEncuesta()
        {
            this.grupos = new List<DtoGrupo>();
            this.areas = new List<DtoArea>();
            listaMiscelaneo1 = new List<SelectItem>();
            listaMiscelaneo2 = new List<SelectItem>();
            listaMiscelaneo3 = new List<SelectItem>();
            listaMiscelaneo4 = new List<SelectItem>();
        }
    }
    public class DtoGrupo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public IList<DtoArea> areas { get; set; }
        public DtoGrupo()
        {
            areas = new List<DtoArea>();
        }
    }
    public class DtoArea
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public IList<DtoPreguntas> preguntas { get; set; }
        public string grupo { get; set; }
        public DtoArea()
        {
            preguntas = new List<DtoPreguntas>();
        }
    }
    public class SelectItem
    {
        public string label { get; set; }
        public string value { get; set; }
    }
    public class DtoPreguntas
    {
        public int? sujeto { get; set; }
        public Int32 pregunta { get; set; }
        public int? orden { get; set; }
        public bool conValor { get; set; }
        public bool conComentario { get; set; }
        public string valor { get; set; }
        public string comentario { get; set; }
        public string preguntaDescripcion { get; set; }
        public IList<DtoTabla> valores { get; set; }
        public string area { get; set; }
        public string tipo { get; set; }

        public string requiereValor { get; set; }
        public int? requierePregunta { get; set; }
        public string requiereFlag { get; set; }
        public String grupo { get; set; }

        public DtoPreguntas()
        {
            this.valores = new List<DtoTabla>();
        }
    }
}
