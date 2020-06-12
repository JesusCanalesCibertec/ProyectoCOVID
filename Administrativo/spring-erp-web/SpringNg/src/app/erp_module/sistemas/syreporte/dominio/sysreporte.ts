
export class SyReportePk {
    aplicacioncodigo: string;
    reportecodigo: string;

}

export class SyReporte extends SyReportePk {
    private static MSG_DESCRIPCIONLOCAL_LONGITUD = 'El campo DESCRIPCIONLOCAL solo permite {max} caracteres';
    private static MSG_DESCRIPCIONINGLES_LONGITUD = 'El campo DESCRIPCIONINGLES solo permite {max} caracteres';
    private static MSG_TOPICO_LONGITUD = 'El campo TOPICO solo permite {max} caracteres';
    private static MSG_PERIODOIMPLEMENTACION_LONGITUD = 'El campo PERIODOIMPLEMENTACION solo permite {max} caracteres';
    private static MSG_VENTANAOBJETO_LONGITUD = 'El campo VENTANAOBJETO solo permite {max} caracteres';
    private static MSG_PARAMETROSFLAG_LONGITUD = 'El campo PARAMETROSFLAG solo permite {max} caracteres';
    private static MSG_FORMATODEFAULTFLAG_LONGITUD = 'El campo FORMATODEFAULTFLAG solo permite {max} caracteres';
    private static MSG_DESCRIPCIONDATA_LONGITUD = 'El campo DESCRIPCIONDATA solo permite {max} caracteres';
    private static MSG_COMENTARIOS_LONGITUD = 'El campo COMENTARIOS solo permite {max} caracteres';
    private static MSG_RESTRICCIONES_LONGITUD = 'El campo RESTRICCIONES solo permite {max} caracteres';
    private static MSG_ESTADO_LONGITUD = 'El campo ESTADO solo permite {max} caracteres';
    private static MSG_ULTIMOUSUARIO_LONGITUD = 'El campo ULTIMOUSUARIO solo permite {max} caracteres';
    private static MSG_REPORTESTANDARDFLAG_LONGITUD = 'El campo REPORTESTANDARDFLAG solo permite {max} caracteres';
    private static MSG_FRECUENCIA_LONGITUD = 'El campo FRECUENCIA solo permite {max} caracteres';
    private static MSG_INVENTARIOFLAG_LONGITUD = 'El campo INVENTARIOFLAG solo permite {max} caracteres';

    descripcionlocal: string;
    tiporeporte: string;
    carpetaTemporal: string;
    estado: string;
    ultimousuario: string;
    ultimafechamodif: Date;
}
