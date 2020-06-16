using net.royal.spring.programasocial.dominio.dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.rrhh.dominio.dto {
    public class DtoIndicadoresGraficos {
        public Nullable<Decimal> Nnapor { get; set; }
        public Nullable<Decimal> Ncdpor { get; set; }
        public Nullable<Decimal> Aampor { get; set; }
        public String Descripcion { get; set; }
        public String Cabecera { get; set; }
        public Nullable<Decimal> NnaCantidad { get; set; }
        public Nullable<Decimal> NcdCantidad { get; set; }
        public Nullable<Decimal> AamCantidad { get; set; }


        public Nullable<Decimal> ReqInstitucionPorc { get; set; }
        public Nullable<Decimal> AporFundacionPorc { get; set; }
        public Nullable<Decimal> OtrosAportesPorc { get; set; }
        public Nullable<Decimal> ReqInstitucionCant { get; set; }
        public Nullable<Decimal> AporFundacionCant { get; set; }
        public Nullable<Decimal> OtrosAportesCant { get; set; }
        public String GruposNutricionales { get; set; }

        public Nullable<Int32> Puntaje { get; set; }
        public Nullable<Int32> PuntajePueric { get; set; }
        public Nullable<Int32> PuntajeCanev { get; set; }

        public String Institucion { get; set; }
        public String Resultado { get; set; }

        public String Nombre { get; set; }
        public Nullable<Decimal> Cantidad { get; set; }

        public Nullable<Decimal> NNABuena { get; set; }
        public Nullable<Decimal> NNARegular { get; set; }
        public Nullable<Decimal> NNAMala { get; set; }
        public Nullable<Decimal> NCDBuena { get; set; }
        public Nullable<Decimal> NCDRegular { get; set; }
        public Nullable<Decimal> NCDMala { get; set; }
        public Nullable<Decimal> AAMBuena { get; set; }
        public Nullable<Decimal> AAMRegular { get; set; }
        public Nullable<Decimal> AAMMala { get; set; }
        public Nullable<Decimal> NNABuenaCant { get; set; }
        public Nullable<Decimal> NNARegularCant { get; set; }
        public Nullable<Decimal> NNAMalaCant { get; set; }
        public Nullable<Decimal> NCDBuenaCant { get; set; }
        public Nullable<Decimal> NCDRegularCant { get; set; }
        public Nullable<Decimal> NCDMalaCant { get; set; }
        public Nullable<Decimal> AAMBuenaCant { get; set; }
        public Nullable<Decimal> AAMRegularCant { get; set; }
        public Nullable<Decimal> AAMMalaCant { get; set; }


        public Nullable<Decimal> NNAPorcDesarollanHSS { get; set; }
        public Nullable<Decimal> NNAPorcProcesoDHSS { get; set; }
        public Nullable<Decimal> NNAPorcNoDesarrollanHSS { get; set; }
        public Nullable<Decimal> NCDPorcDesarollanHSS { get; set; }
        public Nullable<Decimal> NCDPorcProcesoDHSS { get; set; }
        public Nullable<Decimal> NCDPorcNoDesarrollanHSS { get; set; }
        public Nullable<Decimal> AAMPorcDesarollanHSS { get; set; }
        public Nullable<Decimal> AAMPorcProcesoDHSS { get; set; }
        public Nullable<Decimal> AAMPorcNoDesarrollanHSS { get; set; }

        public Nullable<Int32> NroTallerFor { get; set; }
        public Nullable<Int32> NroTallerDep { get; set; }
        public Nullable<Int32> NroTallerArt { get; set; }
        public Nullable<Decimal> TalleForRegular { get; set; }
        public Nullable<Decimal> TalleForBueno { get; set; }
        public Nullable<Decimal> TalleForExcelente { get; set; }
        public Nullable<Decimal> TalleDepRegular { get; set; }
        public Nullable<Decimal> TalleDepBueno { get; set; }
        public Nullable<Decimal> TalleDepExcelente { get; set; }
        public Nullable<Decimal> TalleArtRegular { get; set; }
        public Nullable<Decimal> TalleArtBueno { get; set; }
        public Nullable<Decimal> TalleArtExcelente { get; set; }


        public Nullable<Decimal> NutriRegular { get; set; }
        public Nullable<Decimal> NutriBueno { get; set; }
        public Nullable<Decimal> NutriExcelente { get; set; }

        public Nullable<Int32> NutriRegularCan { get; set; }
        public Nullable<Int32> NutriBuenoCan { get; set; }
        public Nullable<Int32> NutriExcelenteCan { get; set; }

        public Nullable<Decimal> SaludRegular { get; set; }
        public Nullable<Decimal> SaludBueno { get; set; }
        public Nullable<Decimal> SaludExcelente { get; set; }

        public Nullable<Int32> SaludRegularCan { get; set; }
        public Nullable<Int32> SaludBuenoCan { get; set; }
        public Nullable<Int32> SaludExcelenteCan { get; set; }

        public Nullable<Decimal> CapaciRegular { get; set; }
        public Nullable<Decimal> CapaciBueno { get; set; }
        public Nullable<Decimal> CapaciExcelente { get; set; }

        public Nullable<Int32> CapaciRegularCan { get; set; }
        public Nullable<Int32> CapaciBuenoCan { get; set; }
        public Nullable<Int32> CapaciExcelenteCan { get; set; }

        public Nullable<Decimal> DesarrRegular { get; set; }
        public Nullable<Decimal> DesarrBueno { get; set; }
        public Nullable<Decimal> DesarrExcelente { get; set; }

        public Nullable<Int32> DesarrRegularCan { get; set; }
        public Nullable<Int32> DesarrBuenoCan { get; set; }
        public Nullable<Int32> DesarrExcelenteCan { get; set; }


        public Nullable<Int32> NutriCantidad { get; set; }
        public Nullable<Int32> SaludCantidad { get; set; }
        public Nullable<Int32> CapaciCantidad { get; set; }
        public Nullable<Int32> DesarrCantidad { get; set; }



        public Nullable<Int32> TotalBeneficiarios { get; set; }
        public Nullable<Int32> NroEvaluados { get; set; }
        public Nullable<Int32> BarIndeCantidad { get; set; }
        public Nullable<Int32> BarDepenLeveCantidad { get; set; }
        public Nullable<Decimal> BarIndePorcen { get; set; }
        public Nullable<Decimal> BarDepenLevePorcent { get; set; }


        public Nullable<Int32> PoblacionTotal { get; set; }
        public Nullable<Int32> DesarrollanHO { get; set; }
        public Nullable<Int32> NoDesarrollanHO { get; set; }
        public Nullable<Int32> EnProcesoDeDesarrollarHO { get; set; }
        public Nullable<Int32> NoEvaluados { get; set; }


        public Nullable<Decimal> HabIntegracionPorc { get; set; }
        public Nullable<Int32> HabIntegracionCant { get; set; }
        public Nullable<Decimal> HabAsertividadPorc { get; set; }
        public Nullable<Int32> HabAsertividadCant { get; set; }

        public Nullable<Decimal> HabComuPorc { get; set; }
        public Nullable<Int32> HabComuCant { get; set; }
        public Nullable<Decimal> HabPartiPorc { get; set; }
        public Nullable<Int32> HabPartiCant { get; set; }
        public Nullable<Decimal> HabCoopPorc { get; set; }
        public Nullable<Int32> HabCoopCant { get; set; }
        public Nullable<Decimal> HabEmpaPorc { get; set; }
        public Nullable<Int32> HabEmpaCant { get; set; }

        public Nullable<Decimal> HabEmoPorc { get; set; }
        public Nullable<Int32> HabEmoCant { get; set; }

        public String EdadNombre { get; set; }
        public Nullable<Decimal> HabSociConflictosPorc { get; set; }
        public Nullable<Int32> HabSociConflictosCant { get; set; }
        public Nullable<Decimal> HabSociComunicaPorc { get; set; }
        public Nullable<Int32> HabSociComunicaCant { get; set; }






















    }
}
