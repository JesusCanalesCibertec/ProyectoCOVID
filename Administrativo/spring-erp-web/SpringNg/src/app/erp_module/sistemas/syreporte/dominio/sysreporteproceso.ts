
export class SyReporteprocesoPk {
    aplicacioncodigo: string;
    reportecodigo: string;

}

export class SyReporteproceso extends SyReporteprocesoPk {
   
    descripcionlocal: string;
    estado: string;
    ultimousuario: string;
    ultimafechamodif: Date;
}
