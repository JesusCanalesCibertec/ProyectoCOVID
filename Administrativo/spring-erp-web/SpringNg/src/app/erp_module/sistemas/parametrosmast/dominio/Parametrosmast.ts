export class ParametrosmastPk {
    aplicacioncodigo: string;
    parametroclave: string;
    companiacodigo: string;
}
export class Parametrosmast extends ParametrosmastPk {
    descripcionparametro: string;
    explicacion: string;
    tipodedatoflag: string;
    texto: string;
    numero: number;
    decha: Date;
    dinancecomunflag: string;
    estado: string;
    explicacionadicional: string;
    texto1: string;
    texto2: string;
}