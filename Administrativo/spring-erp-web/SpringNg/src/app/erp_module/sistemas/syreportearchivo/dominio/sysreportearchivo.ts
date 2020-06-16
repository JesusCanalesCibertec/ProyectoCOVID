export class SyReporteArchivoPk {
    aplicacioncodigo: string;
    reportecodigo: string;
    companiasocio: string;
    periodo: string;
    version: string;
}

export class SyReporteArchivo extends SyReporteArchivoPk {
    reporte: string;
    estado: string;
    ultimousuario;
    ultimafechamodif: Date;

    auxString: string;
}
