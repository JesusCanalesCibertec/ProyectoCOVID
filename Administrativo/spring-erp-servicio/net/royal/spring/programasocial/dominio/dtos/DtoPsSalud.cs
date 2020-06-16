using net.royal.spring.framework.core.dominio.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoPsSalud {
        public String IdInstitucion { get; set; }
        public Nullable<Int32> IdBeneficiario { get; set; }
        public String NombreCompleto { get; set; }
        public Nullable<Int32> IdSalud { get; set; }
        public String IdPeriodo { get; set; }
        public String IdInstitucionNombre { get; set; }
        public String IdPrograma { get; set; }
        public String EsNuevo { get; set; }
        public Nullable<DateTime> FechaInforme { get; set; }
        public Nullable<Decimal> Hemoglobina { get; set; }

        public String HemoglobinaResultado { get; set; }
        public String IdTratamientoAnemia { get; set; }
        public String IdDescarteTbc { get; set; }
        public String IdDescarteSerologico { get; set; }
        public String IdAyudaMedica { get; set; }
        public String Comentarios { get; set; }
        public String Estado { get; set; }
        public String IdTbc { get; set; }
        public String IdHta { get; set; }
        public String IdDiabetes { get; set; }
        public String IdOsteoatrosis { get; set; }
        public String IdCognitivo { get; set; }
        public String IdAfectivo { get; set; }
        public String IdDiscapacidad { get; set; }

        public String IdGrupoSanguineo { get; set; }
        public String IdFactorRh { get; set; }

        public List<DtoTabla> listaEstado { get; set; }
        public List<DtoTabla> listaTerapias { get; set; }
        public List<DtoTabla> listaSubCondicion { get; set; }
        public List<DtoTabla> listaExamenes { get; set; }
        public List<DtoTabla> listaBioMecanica { get; set; }
        public List<DtoTabla> listaDiscapacidad { get; set; }
        public List<DtoTabla> listaDiagnostico { get; set; }
        public List<DtoTabla> listaTratramiento { get; set; }
        public List<DtoTabla> listaAyuda { get; set; }

        public String IdTipoAyuda { get; set; }
        public String TipoAyuda { get; set; }
        public String EstadoAyuda { get; set; }
        public String IdEstadoAyuda { get; set; }
        public String IdEstado { get; set; }
        public String EstadoNombre { get; set; }
        public String IdExamen { get; set; }
        public String IdResultado { get; set; }
        public String Examen { get; set; }
        public String Resultado { get; set; }
        public String IdCondicion { get; set; }
        public String IdSubCondicion { get; set; }
        public String Condicion { get; set; }
        public String SubCondicion { get; set; }
        public String IdTerapia { get; set; }
        public String Terapia { get; set; }
        public Boolean ValidarNinos { get; set; }
        public String IdOrigen { get; set; }
        public String OtrasEnfermedades { get; set; }
        public String DiagnosticosNombre { get; set; }
        public Nullable<Int32> CantDiagnosticos { get; set; }
        public Nullable<Int32> IdDiagnostico { get; set; }

        public String TratamientoNombre { get; set; }
        public Nullable<Int32> CantTratamientos { get; set; }
        public Nullable<Int32> IdDetalle { get; set; }
        public String TipoRas { get; set; }
        public Nullable<Int64> Secuencia { get; set; }
        public Nullable<Int32> EdadMenarquia { get; set; }
        public Nullable<DateTime> FechaUltimaMestruacion { get; set; }
        public String IdPruebaVIH { get; set; }

        public String sexo { get; set; }

        public String AyudaMedicaNombre { get; set; }
        public String AfectivoNombre { get; set; }
        public String CognitivoNombre { get; set; }
        public Boolean EvaluadoBoolean { get; set; }
        public String Evaluado { get; set; }
        public String NombreAyudaMedica { get; set; }
        public String NombreCognitivo { get; set; }
        public String NombreAfectivo { get; set; }

        public Nullable<Int32> Edad { get; set; }

        public String Id_Nombre { get; set; }
        public String Id_Nombre2 { get; set; }
        public String Id_Nombre3 { get; set; }
        public String Id_Nombre_ayuda_biomecanica { get; set; }
        public String Id_Nombre_ayuda_biomecanica2 { get; set; }
        public String Id_Nombre_ayuda_biomecanica3 { get; set; }
        public String Id_Nombre_estado_ayuda_biomecanica { get; set; }
        public String Id_Nombre_estado_ayuda_biomecanica2 { get; set; }
        public String Id_Nombre_estado_ayuda_biomecanica3 { get; set; }
        public String Id_Nombre_diagnostico { get; set; }
        public String Id_Nombre_diagnostico2 { get; set; }
        public String Id_Nombre_diagnostico3 { get; set; }
        public String Id_Nombre_discapacidad { get; set; }
        public String Id_Nombre_discapacidad2 { get; set; }
        public String Id_Nombre_discapacidad3 { get; set; }
        public String Id_Nombre_estado { get; set; }
        public String Id_Nombre_estado2 { get; set; }
        public String Id_Nombre_estado3 { get; set; }
        public String Id_Nombre_examen { get; set; }
        public String Id_Nombre_resultado { get; set; }
        public String Id_Nombre_examen2 { get; set; }
        public String Id_Nombre_resultado2 { get; set; }
        public String Id_Nombre_examen3 { get; set; }
        public String Id_Nombre_resultado3 { get; set; }
        public String Id_Nombre_condicion { get; set; }
        public String Id_Nombre_sub_condicion { get; set; }
        public String Id_Nombre_condicion2 { get; set; }
        public String Id_Nombre_sub_condicion2 { get; set; }
        public String Id_Nombre_condicion3 { get; set; }
        public String Id_Nombre_sub_condicion3 { get; set; }
        public String Id_Nombre_terapia { get; set; }
        public String Id_Nombre_terapia2 { get; set; }
        public String Id_Nombre_terapia3 { get; set; }
        public String Id_Nombre_tratamiento { get; set; }
        public String Id_Nombre_tratamiento2 { get; set; }
        public String Id_Nombre_tratamiento3 { get; set; }




    }
}
