import { ParametroPaginacionGenerico } from '../ParametroPaginacionGenerico';
import { DomSanitizer } from '@angular/platform-browser';

export class DtoTabla {
    id: number;
    codigo: string;
    codigoNumerico: number;
    nombre: string;
    descripcion: string;
    estado: string;
    value: any;
    label: string;
    internalValue: any;
    valorFlag: string;
    valorNumerico: number;
    secuencia: number;
    idTipoAyuda: string;
    idResultado: string;
    idSubCondicion: string;
    base64: string;
    valor1: string;
    valor2: string;
    valor3: string;
    explicacion: string;
    porcentaje: number;
    paginacion: ParametroPaginacionGenerico;
    min: number;
    max: number;
    color: string;

    num1: number;
    num2: number;
    num3: number;
    num4: number;
    num5: number;
    num6: number;
    num7: number;
    num8: number;
    num9: number;

    fechainicio1: Date;
    fechafin1:Date;
    fechainicio2: Date;
    fechafin2:Date;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
}
export class DtoPublicacionVista {

    private _inner: any = "";
    tamanio: string = "";
    altura: number = 100;

    constructor(private sanitizer: DomSanitizer) {

    }
    get inner(): any {
        return this.sanitizer.bypassSecurityTrustHtml(this._inner);
    }
    set inner(theBar: any) {
        this._inner = theBar;
    }
}
