using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.programasocial.dominio.dtos {
    public class DtoReportesGraficos {
        public Nullable<Int32> Anio { get; set; }
        public Nullable<Int32> Puricultorio { get; set; }
        public Nullable<Int32> Sanvicente { get; set; }
        public Nullable<Int32> Canevaro { get; set; }
        public Nullable<Int32> Desamparados { get; set; }
        public Nullable<Int32> Misioneras { get; set; }
        public Nullable<Int32> Inmaculada { get; set; }
        public Nullable<Int32> Sanfrancisco { get; set; }
        public Nullable<Int32> Neurologico { get; set; }
        public Nullable<Int32> Total { get; set; }


        public String Tipo { get; set; }
        public Nullable<Decimal> PuricultorioD { get; set; }
        public Nullable<Decimal> SanvicenteD { get; set; }
        public Nullable<Decimal> CanevaroD { get; set; }
        public Nullable<Decimal> MisionerasD { get; set; }
        public Nullable<Decimal> InmaculadaD { get; set; }
        public Nullable<Decimal> SanfranciscoD { get; set; }
        public Nullable<Decimal> AsiloHermanitas { get; set; }

        public Nullable<Int32> Anio2013 { get; set; }
        public Nullable<Int32> Anio2014 { get; set; }
        public Nullable<Int32> Anio2015 { get; set; }
        public Nullable<Int32> Anio2016 { get; set; }
        public Nullable<Int32> Anio2017 { get; set; }

    }


}
