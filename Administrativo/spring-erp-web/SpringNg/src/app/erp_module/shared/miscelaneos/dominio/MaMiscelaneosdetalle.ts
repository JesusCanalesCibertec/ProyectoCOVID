export class MaMiscelaneosdetallePk {

    aplicacioncodigo: string;
    codigotabla: string;
    compania: string;
    codigoelemento: string;
}

export class MaMiscelaneosdetalle extends MaMiscelaneosdetallePk {
    public static readonly MISCELANEO_MONEDA = 'PRESTMONED';
    public static readonly MISCELANEO_FINANCIAMIENTO_CAPACITACION = 'FINANCAPAC';
    public static readonly MISCELANEO_MODALIDAD_CAPACITACION = 'MODACAPA';
    public static readonly MISCELANEO_TIPO_CAPACITACION = 'TIPOCURSO';




    descripcionlocal: string;
    estado: string;
    valorCodigo1: string;
    valorCodigo2: string;
    valorCodigo: string;
    valorNumerico: number;
    valorNumerico2: number;

    auxCampo: boolean = false;
}
