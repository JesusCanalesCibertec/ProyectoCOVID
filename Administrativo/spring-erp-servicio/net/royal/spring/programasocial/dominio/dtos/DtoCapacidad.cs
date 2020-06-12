using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoCapacidad {
        public String IdInstitucion { get; set; }
        public String NombreCompleto { get; set; }
        public Nullable<Int32> IdBeneficiario { get; set; }
        public Nullable<Int32> IdCapacidad { get; set; }
        public Nullable<DateTime> FechaInforme { get; set; }
        public String IdTipoInstitucion { get; set; }
        public String IdPeriodo { get; set; }
        public String IdNivel { get; set; }
        public String IdGradoEducativo { get; set; }
        public String AnioRetraso { get; set; }
        public String ComentarioRetraso { get; set; }
        public String IdTipoComunicacion { get; set; }
        public String ComentarioRendimiento { get; set; }
        public String ComentarioTalleres { get; set; }
        public String IdRiesgoCaida { get; set; }
        public String IdRiesgoCaidaDetalle { get; set; }

        public String IdHabilidadOcupacional { get; set; }
        public String IdEvaluarOcupacional { get; set; }
        public String ComentarioCapacidad { get; set; }
        public Nullable<Int32> Katz1 { get; set; }
        public Nullable<Int32> Katz2 { get; set; }
        public Nullable<Int32> Katz3 { get; set; }
        public Nullable<Int32> Katz4 { get; set; }
        public Nullable<Int32> Katz5 { get; set; }
        public Nullable<Int32> Katz6 { get; set; }
        public Nullable<Int32> Barthel1 { get; set; }
        public Nullable<Int32> Barthel2 { get; set; }
        public Nullable<Int32> Barthel3 { get; set; }
        public Nullable<Int32> Barthel4 { get; set; }
        public Nullable<Int32> Barthel5 { get; set; }
        public Nullable<Int32> Barthel6 { get; set; }
        public Nullable<Int32> Barthel7 { get; set; }
        public Nullable<Int32> Barthel8 { get; set; }
        public Nullable<Int32> Barthel9 { get; set; }
        public Nullable<Int32> Barthel10 { get; set; }
        public String Estado { get; set; }
        public String UsurioCodigo { get; set; }
        public String GradoDependenciaBarthel { get; set; }
        public String GradoDependenciaKatz { get; set; }
        public String NombreHabilidadOcupacional { get; set; }


        //************* CURSOS ****************//
        public String NotaLogicoMatematico { get; set; }
        public String NotaComunicacion { get; set; }
        public String NotaComprensionLectora { get; set; }
        public String NotaPersonalSocial { get; set; }
        public String NotaCienciaAmbiente { get; set; }

        public String IdCursoMatematica { get; set; }
        public String IdCursoComunicacion { get; set; }
        public String IdCursoCompresion { get; set; }
        public String IdCursopersonal { get; set; }
        public String IdCursoAmbiente { get; set; }
        public String IdCurso { get; set; }
        public String IdNotaCurso { get; set; }

        //************* TALLERES ****************//

        public Nullable<Int32> IdTaller { get; set; }
        public String IdNotaTaller { get; set; }
        public Nullable<Int32> CantidadTaller { get; set; }
        public String IdNotaTallerArtistico { get; set; }
        public Nullable<Int32> CantidadTallerArtistico { get; set; }
        public String IdNotaTallerFormativo { get; set; }
        public Nullable<Int32> CantidadTallerFormativo { get; set; }
        public String IdNotaTallerDeportivo { get; set; }
        public Nullable<Int32> CantidadTallerDeportivo { get; set; }
        public String IdTallerArtistico { get; set; }
        public String IdTallerFormativo { get; set; }
        public String IdTallerDeportivo { get; set; }
        public String IdTallerCognitivo { get; set; }
        public String IdTallerFisico { get; set; }

        public String IdNotaTallerCognitivo { get; set; }
        public Nullable<Int32> CantidadTallerCognitivo { get; set; }
        public String IdNotaTallerFisico { get; set; }
        public Nullable<Int32> CantidadTallerFisico { get; set; }


        public Nullable<Decimal> PorcentajeGradoBarthel { get; set; }
        public Nullable<Decimal> PorcentajeGradoKatz { get; set; }
        public Boolean ValidarNinos { get; set; }
        public String IdOrigen { get; set; }
        public String IdSaanee { get; set; }
        public String IdIletrado { get; set; }
        public String OcupacionAnterior { get; set; }
        public String Evaluado { get; set; }
        public Boolean EvaluadoBoolean { get; set; }

        public String NombreInstitucion { get; set; }
        public String NombreNivel { get; set; }
        public String NombreGrado { get; set; }
        public Nullable<Int32> Edad { get; set; }

        public Int32? secuencia { get; set; }
        public String sexo { get; set; }






    }
}
